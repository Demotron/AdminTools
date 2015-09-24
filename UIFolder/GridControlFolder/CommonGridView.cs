using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using CommonLibrary.ColumnsPopupFormFolder;
using CommonLibrary.LogicFolder;
using CommonLibrary.Properties;
using CommonLibrary.UIFolder.GridControlFolder;
using DevExpress.LookAndFeel.Helpers;
using DevExpress.Utils.Menu;
using DevExpress.Utils.Serializing;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Menu;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace CommonLibrary.GridControlFolder
{
    public class CommonGridView : GridView
    {
        private const string AddCaption = "Добавить данные";
        private const string RemoveCaption = "Удалить выбранные данные";
        private const string MsgDelete = "Уверены, что хотите удалить строку и данные?";

        private QuickHideEdit hideEdit;
        private string idFieldName;
        private int savedFocusedRowHandle;
        private ArrayList saveExpList;
        private ArrayList saveMasterRowsList;
        private ArrayList saveSelList;
        private int visibleRowIndex = -1;
        internal Bitmap IndicatorImage;

        #region Delegates

        [UsedImplicitly]
        public delegate bool EventBeforeAdd();
        public delegate void EventAfterDelete(params object[] objects);
        public delegate bool EventBeforeDelete(object[] obj);

        #endregion

        #region Events

        public event EventHandler<EventRowHandlerArgs> DoBeforeAddRow;
        public event EventHandler<EventRowHandlerArgs> DoCheckAddRow;
        public event EventHandler<EventRowHandlerArgs> CheckRowBeforeChangeValue;


        [UsedImplicitly]
        public event Func<object> DoInsteadAddRow;
        public event Action<object> DoAfterAddRow;
        public event EventBeforeDelete DoBeforeDeleteRow;
        public event EventHandler<EventRowHandlerArgs> DoCheckDeleteRow;
        public event EventAfterDelete DoAfterDeleteRow;

        /// <summary>
        /// Событие перед вызовом прорисовки индикатора у грида
        /// </summary>
        [Browsable(true)]
        public event EventHandler<EventRowHandlerArgs> DoCheckBeforeDrawIndicator;

        #endregion


        private ArrayList SaveExpList
        {
            get { return saveExpList ?? (saveExpList = new ArrayList()); }
        }

        private ArrayList SaveSelList
        {
            get { return saveSelList ?? (saveSelList = new ArrayList()); }
        }

        private ArrayList SaveMasterRowsList
        {
            get { return saveMasterRowsList ?? (saveMasterRowsList = new ArrayList()); }
        }

        protected override string ViewName
        {
            get { return "CommonGridView"; }
        }

        [Browsable(false)]
        public new CommonGridColumn FocusedColumn
        {
            get { return base.FocusedColumn as CommonGridColumn; }
        }

        [Description("Доступ к опциям CommonGridView"), Category("Options"),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
         XtraSerializableProperty(XtraSerializationVisibility.Content, XtraSerializationFlags.DefaultValue)]
        public new CommonGridOptionsCustomization OptionsCustomization
        {
            get { return base.OptionsCustomization as CommonGridOptionsCustomization; }
        }

        public override bool IsFocusedView
        {
            get
            {
                if (hideEdit == null)
                {
                    return base.IsFocusedView;
                }
                return hideEdit.Enabled || base.IsFocusedView;
            }
        }

        public PopupMenu CustomPopupMenu { get; set; }

        public new CommonGridControl GridControl
        {
            // ReSharper disable once MemberCanBeProtected.Global
            get { return base.GridControl as CommonGridControl; }
            set { base.GridControl = value; }
        }

        internal EmbeddedLookAndFeel GetLookAndFeel()
        {
            return ElementsLookAndFeel;
        }

        // ReSharper disable once MemberCanBePrivate.Global

        public CommonGridView()
        {
            KeyDown += GridViewKeyDown;
            MouseUp += GridViewMouseUp;
            MouseMove += GridViewMouseMove;
            MouseDown += GridViewMouseDown;
            MouseWheel += CommonGridViewMouseWheel;
            PopupMenuShowing += CustomMenuPopupMenuShowing;
            CustomDrawRowIndicator += ImageCustomDrawRowIndicator;
        }

        public CommonGridView(GridControl grid)
            : base(grid)
        {
            SetGridControlAccessMetod(grid);
        }

        private void SetGridControlAccessMetod(GridControl newControl)
        {
            SetGridControl(newControl);
        }

        #region Popup menu methods

        BarButtonItem bbiAddRow;
        BarButtonItem bbiRemove;

        private BarButtonItem ButtonAddRow
        {
            get
            {
                if (bbiAddRow != null)
                {
                    return bbiAddRow;
                }
                bbiAddRow = new BarButtonItem
                {
                    Caption = AddCaption,
                    Glyph = Resources.Add,
                    Id = 9
                };
                bbiAddRow.ItemClick += AddRow;
                return bbiAddRow;
            }
        }

        private BarButtonItem ButtonRemove
        {
            get
            {
                if (bbiRemove != null)
                {
                    return bbiRemove;
                }
                bbiRemove = new BarButtonItem
                {
                    Caption = RemoveCaption,
                    Glyph = Resources.Remove
                };
                bbiRemove.ItemClick += RemoveRows;
                return bbiRemove;
            }
        }

        protected virtual void CustomMenuPopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            //При вызове обычного меню на клике по строке
            switch (e.MenuType)
            {
                case GridMenuType.User:
                case GridMenuType.Row:
                    RepositoryItem repository = null;
                    //Показать меню для выбора значения выделенных строк
                    if (OptionsBehavior.Editable && OptionsCustomization.ShowSelectedRowMenu && FocusedColumn != null &&
                        FocusedColumn.OptionsColumn.ShowEditorInPopupMenu && FocusedColumn.OptionsColumn.AllowEdit &&
                        !FocusedColumn.OptionsColumn.ReadOnly && SelectedRowsCount > 1)
                    {
                        repository = DXCustomMenuCreator.DXCreateBarItemByFocusedColumn(FocusedColumn);
                        var buttonRepository = repository as RepositoryItemButtonEdit;
                        if (buttonRepository != null && buttonRepository.Buttons.Count > 1)
                        {
                            buttonRepository.ButtonClick += ClearSelectedRowsFromMenu;
                        }
                    }
                    if (CustomPopupMenu != null)
                    {
                        AddButtonInPopupMenu();
                        DeleteButtonInPopupMenu();
                        var link = CustomPopupMenu.ItemLinks
                            .OfType<BarEditItemLink>()
                            .FirstOrDefault(bei => bei.Item.Id == -1);
                        if (link != null)
                        {
                            CustomPopupMenu.RemoveLink(link);
                        }
                        if (repository != null)
                        {
                            var bei = new BarEditItem
                            {
                                Edit = repository,
                                Id = -1,
                                Tag = FocusedColumn
                            };
                            bei.EditValueChanged += SelectedRowEditValueChanged;
                            CustomPopupMenu.AddItem(bei);
                        }
                    }
                    else
                    {
                        var dxmAdd = new DXMenuItem(AddCaption, AddRow, Resources.Add);
                        var dxmRemove = new DXMenuItem(RemoveCaption, RemoveRows, Resources.Remove);
                        if (e.MenuType == GridMenuType.User && e.Menu == null)
                            e.Menu = new GridViewMenu(this);
                        if (OptionsCustomization.AllowAdd)
                            e.Menu.Items.Add(dxmAdd);
                        if (OptionsCustomization.AllowDelete)
                        {
                            e.Menu.Items.Add(dxmRemove);
                            dxmRemove.Enabled = SelectedRowsCount != 0;
                        }
                        if (repository != null)
                        {
                            var bei = new DXEditMenuItem
                            {
                                Edit = repository,
                                Tag = FocusedColumn
                            };
                            bei.EditValueChanged += SelectedRowEditValueChanged;
                            e.Menu.Items.Add(bei);
                        }
                    }
                    break;
                case GridMenuType.Column:
                    DXPopupMenu menu = e.Menu as GridViewColumnMenu;
                    if (menu == null)
                    {
                        return;
                    }
                    DXCustomMenuCreator.HideCustomizationColumns(menu);
                    DXCustomMenuCreator.DXCreateSelectSelectionMenu(menu, CustomItemClick, RowItemClick, CheckboxColumnItemClick);
                    DXCustomMenuCreator.DXCreateLayoutMenuItems(GridControl, ref menu);
                    break;
            }
        }

        private void ClearSelectedRowsFromMenu(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete && e.Button.Tag is bool && !(bool)e.Button.Tag)
            {
                ChangeSelectedRowValue(FocusedColumn, Convert.DBNull);
            }
        }

        private void SelectedRowEditValueChanged(object sender, EventArgs e)
        {
            if (sender is DXEditMenuItem)
                ChangeSelectedRowValue(FocusedColumn, ((DXEditMenuItem)(sender)).EditValue);
            else
                ChangeSelectedRowValue(FocusedColumn, ((BarEditItem)(sender)).EditValue);

        }

        /// <summary>
        /// Изменить значение у выделенных строк данной колонки
        /// </summary>
        /// <param name="column">Колонка, для которой необходимо изменить значение</param>
        /// <param name="value">Значение, на которое мы меняем</param>
        private void ChangeSelectedRowValue(GridColumn column, object value)
        {
            BeginDataUpdate();
            try
            {
                foreach (var rowHandle in GetSelectedRows())
                {
                    if (CheckRowBeforeChangeValue != null)
                    {
                        var erg = new EventRowHandlerArgs(rowHandle);
                        CheckRowBeforeChangeValue.Raise(this, erg);
                        if (erg.Cancel)
                        {
                            continue;
                        }
                    }
                    SetRowCellValue(rowHandle, column, value);
                }
            }
            finally
            {
                EndDataUpdate();
            }
        }

        /// <summary>
        /// Добавить кнопку удаления в сплывающее меню
        /// </summary>
        private void DeleteButtonInPopupMenu()
        {
            if (!OptionsCustomization.AllowDelete)
            {
                //                var item = CustomPopupMenu.ItemLinks
                //                    .OfType<BarButtonItemLink>()
                //                    .FirstOrDefault(l => l.Item == ButtonRemove);
                //                if (item != null)
                //                    CustomPopupMenu.ItemLinks.Remove(item);
                ButtonRemove.Enabled = false;
                return;
            }
            var erg = new EventRowHandlerArgs(InvalidRowHandle);
            DoCheckDeleteRow.Raise(this, erg);
            if (erg.Cancel)
            {
                return;
            }
            if (ButtonRemove.Links.Count == 0)
                CustomPopupMenu.ItemLinks.Add(ButtonRemove);
            ButtonRemove.Enabled = SelectedRowsCount != 0;
        }

        /// <summary>
        /// Добавить кнопку добавления в сплывающее меню
        /// </summary>
        private void AddButtonInPopupMenu()
        {
            if (!OptionsCustomization.AllowAdd)
            {
                ButtonAddRow.Enabled = false;
                //                var item = CustomPopupMenu.ItemLinks
                //                    .OfType<BarButtonItemLink>()
                //                    .FirstOrDefault(l => l.Item == ButtonRemove);
                //                if(item != null)
                //                    CustomPopupMenu.ItemLinks.Remove(item);
                return;
            }
            var erg = new EventRowHandlerArgs(InvalidRowHandle);
            DoCheckAddRow.Raise(this, erg);
            if (erg.Cancel)
            {
                return;
            }
            if (OptionsCustomization.AllowAdd && ButtonAddRow.Links.Count == 0)
                CustomPopupMenu.ItemLinks.Add(ButtonAddRow);
        }

        private void AddRow(object sender, EventArgs eventArgs)
        {
            AddRowButtonClick();
        }

        private void RemoveRows(object sender, EventArgs eventArgs)
        {
            RemoveRowsButtonClick();
        }

        private void CheckboxColumnItemClick(object sender, EventArgs e)
        {
            OptionsCustomization.AllowSelectionColumn = true;
        }

        private void RowItemClick(object sender, EventArgs e)
        {
            OptionsCustomization.AllowSelectionColumn = false;
            OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
        }

        private void CustomItemClick(object sender, EventArgs e)
        {
            OptionsCustomization.AllowSelectionColumn = false;
            OptionsSelection.MultiSelect = true;
            OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
        }

        #endregion

        #region Overrides

        protected override bool AllowFixedCheckboxSelectorColumn
        {
            get
            {
                return true;
            }
        }

        protected override void CreateCheckboxSelectorColumn()
        {
            base.CreateCheckboxSelectorColumn();
            FixedLineWidth = 1;
            CheckboxSelectorColumn.Fixed = FixedStyle.Left;
        }


        protected override GridColumnCollection CreateColumnCollection()
        {
            return new CommonGridColumnCollection(this);
        }

        protected override GridOptionsCustomization CreateOptionsCustomization()
        {
            var cgoc = new CommonGridOptionsCustomization();
            cgoc.AcceptSelectionChangeEvent(SelectionHasChanged);
            return cgoc;
        }

        #endregion

        #region Сохранение и восстановление внешнего вида грида

        /// <summary>
        ///     Сохранить информацию о внешнем виде view типа <see cref="T:CommonGridView" />
        /// </summary>
        public void SaveViewInfo(string _idFieldName = "Id")
        {
            idFieldName = _idFieldName;
            SaveExpandedMasterRows(SaveMasterRowsList);
            SaveExpansionViewInfo(SaveExpList);
            SaveSelectionViewInfo(SaveSelList);
            SaveVisibleIndex();
        }

        /// <summary>
        ///     Загрузить сохранённую информацию об view типа <see cref="T:CommonGridView" />
        /// </summary>
        public void LoadViewInfo()
        {
            if (String.IsNullOrEmpty(idFieldName))
            {
                return;
            }
            LoadExpandedMasterRows(saveMasterRowsList);
            LoadExpansionViewInfo(saveExpList);
            LoadSelectionViewInfo(saveSelList);
            LoadVisibleIndex();
        }

        private void SaveSelectionViewInfo(IList list)
        {
            list.Clear();
            savedFocusedRowHandle = FocusedRowHandle;
            for (var i = 0; i < RowCount; i++)
            {
                RowInfo rowInfo;
                rowInfo.IsSelected = IsRowSelected(i);
                rowInfo.Value = GetRowCellValue(i, idFieldName);
                rowInfo.Level = GetRowLevel(i);
                list.Add(rowInfo);
            }
        }

        private void SaveExpansionViewInfo(IList list)
        {
            if (GroupedColumns.Count == 0)
            {
                return;
            }
            list.Clear();
            var column = Columns[idFieldName];
            for (var i = -1; i > Int32.MinValue; i--)
            {
                if (!IsValidRowHandle(i))
                {
                    break;
                }
                if (!GetRowExpanded(i))
                {
                    continue;
                }
                var rowInfo = new RowInfo();
                var dataRowHandle = GetDataRowHandleByGroupRowHandle(i);
                rowInfo.Value = GetRowCellValue(dataRowHandle, column);
                rowInfo.Level = GetRowLevel(i);
                list.Add(rowInfo);
            }
        }

        private void SaveExpandedMasterRows(IList list)
        {
            if (GridControl.Views.Count == 1)
            {
                return;
            }
            list.Clear();
            var column = Columns[idFieldName];
            for (var i = 0; i < DataRowCount; i++)
            {
                if (GetMasterRowExpanded(i))
                {
                    list.Add(GetRowCellValue(i, column));
                }
            }
        }

        private void SaveVisibleIndex()
        {
            visibleRowIndex = GetVisibleIndex(FocusedRowHandle) - TopRowIndex;
        }

        private void LoadVisibleIndex()
        {
            MakeRowVisible(FocusedRowHandle, true);
            TopRowIndex = GetVisibleIndex(FocusedRowHandle) - visibleRowIndex;
        }

        private void LoadSelectionViewInfo(IEnumerable list)
        {
            BeginSelection();
            try
            {
                ClearSelection();
                foreach (var rowInfo in list)
                {
                    LoadRowByRowInfo((RowInfo)rowInfo);
                }
            }
            finally
            {
                EndSelection();
                FocusedRowHandle = savedFocusedRowHandle;
            }
        }

        private void LoadRowByRowInfo(RowInfo rowInfo)
        {
            if (rowInfo.IsSelected)
            {
                SelectRow(GetRowHandleToSelect(rowInfo));
            }
        }

        private void LoadExpansionViewInfo(IEnumerable list)
        {
            if (GroupedColumns.Count == 0)
            {
                return;
            }
            BeginUpdate();
            try
            {
                CollapseAllGroups();
                foreach (RowInfo info in list)
                {
                    ExpandRowByRowInfo(info);
                }
            }
            finally
            {
                EndUpdate();
            }
        }

        private void LoadExpandedMasterRows(IEnumerable list)
        {
            BeginUpdate();
            try
            {
                CollapseAllDetails();
                var column = Columns[idFieldName];
                foreach (var rowHandle in from object t in list
                                          select LocateByValue(0, column, t))
                {
                    SetMasterRowExpanded(rowHandle, true);
                }
            }
            finally
            {
                EndUpdate();
            }
        }

        private void ExpandRowByRowInfo(RowInfo rowInfo)
        {
            var dataRowHandle = LocateByValue(0, Columns[idFieldName], rowInfo.Value);
            if (dataRowHandle == DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            {
                return;
            }
            var parentRowHandle = FindParentRowHandle(rowInfo, dataRowHandle);
            SetRowExpanded(parentRowHandle, true, false);
        }

        private int FindParentRowHandle(RowInfo rowInfo, int rowHandle)
        {
            var result = GetParentRowHandle(rowHandle);
            while (GetRowLevel(result) != rowInfo.Level)
            {
                result = GetParentRowHandle(result);
            }
            return result;
        }

        private int GetRowHandleToSelect(RowInfo rowInfo)
        {
            var dataRowHandle = LocateByValue(idFieldName, rowInfo.Value);
            if (dataRowHandle == DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            {
                return dataRowHandle;
            }
            return GetRowLevel(dataRowHandle) != rowInfo.Level
                ? FindParentRowHandle(rowInfo, dataRowHandle)
                : dataRowHandle;
        }

        #endregion

        #region Показать/Скрыть форму для настройки колонок

        internal void ShowColumnCustomizationMenu()
        {
            if (hideEdit == null)
            {
                CreateHideEdit();
            }
            LocateHideEdit();
            PopulateHideEdit();
            if (hideEdit != null)
            {
                hideEdit.ShowPopup();
            }
        }

        private void LocateHideEdit()
        {
            hideEdit.MakeEnable();
            hideEdit.Location = ((CommonGridViewInfo)ViewInfo).QuickCustomisationBounds.Location;
        }

        private void CreateHideEdit()
        {
            hideEdit = new QuickHideEdit(GridControl);
            LocateHideEdit();
            GridControl.Controls.Add(hideEdit);
            Focus();
        }

        internal void AcceptQuickHide()
        {
            foreach (GridColumn col in Columns)
            {
                var cp = hideEdit.Properties.Columns[col.ToString()];
                if (cp == null)
                {
                    continue;
                }
                col.VisibleIndex = cp.VisibleIndex;
                col.Visible = cp.Visible;
            }
        }

        private void PopulateHideEdit()
        {
            hideEdit.Properties.Columns.Clear();
            if (Columns.Count == 0)
            {
                return;
            }
            var usedColumns = Columns.Cast<CommonGridColumn>()
                .Where(col => col.OptionsColumn.ShowInQuickHide &&
                              (col.Visible || col.OptionsColumn.ShowInCustomizationForm) &&
                              !CommonUtilities.IsEnglishOnly(col.Caption))
                .OrderBy(col => col.VisibleIndex)
                .ToArray();
            var checkAllValue = true;
            var i = 1;
            while (i < usedColumns.Count() && checkAllValue)
            {
                if (!usedColumns[i].Visible)
                {
                    checkAllValue = false;
                }
                i++;
            }
            hideEdit.Properties.Columns.Add("Скрыть/Показать всё", checkAllValue, 0, true, false);
            foreach (var col in usedColumns)
            {
                hideEdit.Properties.Columns.Add(col.ToString(), col.Visible, col.VisibleIndex, GetColumnHideState(col), GetColumnMoveState(col));
            }
        }

        private bool GetColumnHideState(CommonGridColumn column)
        {
            return column.OptionsColumn.AllowQuickHide && OptionsCustomization.AllowQuickHideColumns;
        }

        private bool GetColumnMoveState(CommonGridColumn column)
        {
            return column.OptionsColumn.AllowMove && OptionsCustomization.AllowColumnMoving;
        }

        #endregion

        #region Собития для View таблицы

        /// <summary>
        ///     Скопировать в буфер обмена только значение из выделенной ячейки
        /// </summary>
        private void GridViewKeyDown(object sender, KeyEventArgs e)
        {
            if (IsDesignMode)
            {
                return;
            }
            // ReSharper disable once InvertIf
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.Insert) && !IsEditing)
            {
                var selectedCellsText = this.GetSelectedValues();
                Clipboard.SetDataObject(selectedCellsText);
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.A && !IsEditing)
            {
                if (SelectedRowsCount == RowCount)
                {
                    ClearSelection();
                    ClearSelection();
                }
                else
                {
                    SelectAllRows();
                }
            }
            //            else if (e.KeyData == Keys.Delete && OptionsBehavior.AllowDeleteRows != DefaultBoolean.False && RowCount != 0)
            //            {
            //                RemoveSelectedRowAndData();
            //            }
        }

        private void GridViewMouseDown(object sender, MouseEventArgs e)
        {
            if (IsDesignMode)
            {
                return;
            }
            switch (e.Button)
            {
                case MouseButtons.Left:
                    startRowHandle = this.GetRowAt(e.X, e.Y);
                    break;
                case MouseButtons.Right:
                    GridControl.Focus();
                    var hitInfo = CalcHitInfo(e.Location);
                    if (!hitInfo.InRow)
                    {
                        return;
                    }
                    base.FocusedColumn = hitInfo.Column;
                    FocusedRowHandle = hitInfo.RowHandle;
                    break;
            }
        }

        private void GridViewMouseMove(object sender, MouseEventArgs e)
        {
            if (IsEditing || e.Button != MouseButtons.Left || GridControl.DownHitInfo != null ||
                FocusedColumn == null || FocusedColumn.FieldName == CheckBoxSelectorColumnName)
            {
                return;
            }
            var newRowHandle = this.GetRowAt(e.X, e.Y);
            if (currentRowHandle == newRowHandle)
            {
                return;
            }
            currentRowHandle = newRowHandle;
            if (startRowHandle <= -1 || currentRowHandle <= -1)
            {
                return;
            }
            BeginSelection();
            ClearSelection();
            SelectRange(startRowHandle, currentRowHandle);
            EndSelection();
        }

        private void CommonGridViewMouseWheel(object sender, MouseEventArgs e)
        {
            if (GridControl == null || IsDesignMode)
            {
                return;
            }
            var findForm = GridControl.FindForm();
            if (findForm != null && findForm.ContainsFocus)
            {
                GridControl.Focus();
            }
        }

        private void GridViewMouseUp(object sender, MouseEventArgs e)
        {
            EscapeStartedSelectionByMove();
        }

        #endregion

        /// <summary>
        ///     Кастомное изображение на столбце индикатора
        /// </summary>
        public void ImageCustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            var view = (CommonGridView)sender;
            if (e.RowHandle < 0 || view.IndicatorImage == null)
            {
                return;
            }
            var erg = new EventRowHandlerArgs(e.RowHandle);
            view.DoCheckBeforeDrawIndicator.Raise(this, erg);
            if (erg.Cancel)
            {
                return;
            }
            e.Info.ImageIndex = -1;
            e.Painter.DrawObject(e.Info);
            var r = e.Bounds;
            r.Inflate(-1, -1);
            var x = r.X + (r.Width - view.IndicatorImage.Size.Width) / 2;
            var y = r.Y + (r.Height - view.IndicatorImage.Size.Height) / 2;
            e.Graphics.DrawImageUnscaled(view.IndicatorImage, x, y);
            e.Handled = true;
        }

        protected void SelectionHasChanged(bool value)
        {
            if (value)
            {
                OptionsSelection.MultiSelect = true;
                OptionsSelection.CheckBoxSelectorColumnWidth = 25;
                OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
            }
            else
            {
                OptionsSelection.MultiSelect = false;
                OptionsSelection.CheckBoxSelectorColumnWidth = 0;
                OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
            }
            Invalidate();
        }

        /// <summary>
        ///     Получить индексы всех строк, которые не выделены в режиме MultiSelect
        /// </summary>
        /// <returns>Индексы невыделенных строк</returns>
        public IEnumerable<int> GetNotSelectedRows()
        {
            if (!OptionsCustomization.AllowSelectionColumn)
            {
                return new List<int>();
            }
            var result = new List<int>();
            for (var rh = 0; rh < RowCount; rh++)
            {
                if (!IsRowSelected(rh))
                {
                    result.Add(rh);
                }
            }
            return result;
        }

        private int startRowHandle = -1;
        private int currentRowHandle = -1;

        /// <summary>
        /// Отменить начатое выделение колонок в гриде
        /// </summary>
        public void EscapeStartedSelectionByMove()
        {
            startRowHandle = -1;
            currentRowHandle = -1;
        }

        /// <summary>
        /// Проверка таблицы на возможность добавления данных и добавление новой строки
        /// </summary>
        public void AddRowButtonClick()
        {
            if (!OptionsCustomization.AllowAdd)
            {
                return;
            }
            try
            {
                //BeginDataUpdate();
                var erg = new EventRowHandlerArgs(InvalidRowHandle);
                DoCheckAddRow.Raise(this, erg);
                if (erg.Cancel)
                {
                    return;
                }
                erg = new EventRowHandlerArgs(InvalidRowHandle);
                DoBeforeAddRow.Raise(this, erg);
                if (erg.Cancel)
                {
                    return;
                }
                object item = null;
                if (DoInsteadAddRow != null)
                {
                    item = DoInsteadAddRow();
                }
                else
                {
                    var bs = DataSource as BindingSource;
                    if (bs != null)
                    {
                        item = bs.AddNew();
                    }
                    else
                        AddNewRow();
                }
                if (DoAfterAddRow == null)
                {
                    if (item == null || !item.GetType().FullName.Contains("ServerInformation"))
                    {
                        return;
                    }
                    var form = GridControl.FindForm() as CommonChildForm;
                    if (form != null)
                        form.AddToDBContext(item);
                    return;
                }
                if (item != null)
                {
                    DoAfterAddRow(item);
                    return;
                }
                var rowHandle = GetRowHandle(DataRowCount);
                if (IsNewItemRow(rowHandle))
                {
                    DoAfterAddRow(GetRow(rowHandle));
                }
            }
            finally
            {
                //EndDataUpdate();
            }
        }

        /// <summary>
        /// Проверка таблицы на возможность удаления данных и удаление выделенных строк
        /// </summary>
        public void RemoveRowsButtonClick()
        {
            if (!OptionsCustomization.AllowDelete || State != GridState.Normal)
            {
                return;
            }
            var erg = new EventRowHandlerArgs(InvalidRowHandle);
            DoCheckDeleteRow.Raise(this, erg);
            if (erg.Cancel)
            {
                return;
            }
            if (OptionsCustomization.AllowSelectionColumn && SelectedRowsCount == 0)
            {
                MessageWindow.GetInstance("Выберите записи, которые хотите удалить", MessageType.Info);
                return;
            }
            if (XtraMessageBox.Show(MsgDelete, "Удаление данных",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }
            var data = GetSelectedObjectRows();
            if (DoBeforeDeleteRow != null && !DoBeforeDeleteRow(data))
            {
                return;
            }
            if (OptionsCustomization.AllowSelectionColumn)
            {
                DeleteSelectedRows();
            }
            else
            {
                DeleteRow(FocusedRowHandle);
            }
            if (DoAfterDeleteRow != null)
            {
                DoAfterDeleteRow(data);
            }
            else
            {
                var form = GridControl.FindForm() as CommonChildForm;
                if (form != null && form.CheckDBContext(data))
                    form.DeleteFromDBContext(data);
            }
        }

        public object[] GetSelectedObjectRows()
        {
            return GetSelectedRows()
                .Select(GetRow)
                .ToArray();
        }

        /// <summary>
        ///     Закоммитить изменения в DataSource у грида на форме
        /// </summary>
        public void CommitGridViewChanges()
        {
            PostEditor();
            CloseEditor();
            UpdateCurrentRow();
        }

        public GridViewInfo GetCommonViewInfo()
        {
            var pi = GetType().GetProperty("ViewInfo", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
            return pi.GetValue(this, null) as GridViewInfo;
        }

        [Serializable]
        private struct RowInfo
        {
            public bool IsSelected;
            public int Level;
            public object Value;
        };
    }
}