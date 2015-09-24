using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CommonLibrary.ColumnsPopupFormFolder;
using CommonLibrary.LogicFolder;
using CommonLibrary.Properties;
using DevExpress.LookAndFeel;
using DevExpress.Utils.Menu;
using DevExpress.Utils.Serializing;
using DevExpress.XtraBars;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Handler;
using DevExpress.XtraTreeList.Localization;
using DevExpress.XtraTreeList.Menu;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.Painter;
using DevExpress.XtraTreeList.ViewInfo;

namespace CommonLibrary.TreeListFolder
{
    public class CommonTreeList : TreeList, IShowLayoutWorkMenu
    {
        private QuickHideEdit hideEdit;
        private ArrayList saveSelList;
        private TreeListNode focusedNode;
        private readonly TreeListScrollHelper helper;
        readonly BarButtonItem bbiExpandAll = new BarButtonItem();
        readonly BarButtonItem bbiCollapseAll = new BarButtonItem();

        private TreeListColumn IdColumn
        {
            get
            {
                if (Columns.ColumnByFieldName("Id") != null)
                {
                    return Columns["Id"];
                }
                if (Columns.ColumnByFieldName("ID") != null)
                {
                    return Columns["ID"];
                }
                return null;
            }
        }


        /// <summary>
        /// Нужно ли показывать в дереве меню для сохранения состояния
        /// </summary>
        [DefaultValue(true)]
        public bool ShowLayoutWorkMenu { get; set; }

        private ArrayList SaveSelList
        {
            get { return saveSelList ?? (saveSelList = new ArrayList()); }
        }

        public CommonTreeList()
        {
            helper = new TreeListScrollHelper(this);
        }

        public PopupMenu CustomPopupMenu { get; set; }

        public new CommonTreeListViewInfo ViewInfo
        {
            get { return base.ViewInfo as CommonTreeListViewInfo; }
        }

        [Description("Доступ к опциям CommonTreeList"), Category("Options"),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
         XtraSerializableProperty(XtraSerializationVisibility.Content, XtraSerializationFlags.DefaultValue)]
        public new CommonTreeListOptionsBehavior OptionsBehavior
        {
            get { return base.OptionsBehavior as CommonTreeListOptionsBehavior; }
        }

        protected internal new UserLookAndFeel ElementsLookAndFeel
        {
            get { return base.ElementsLookAndFeel; }
        }

        private void InitializePopupMenuButtons()
        {
            //bbiExpandAll
            bbiExpandAll.Caption = @"Развернуть всё";
            bbiExpandAll.Glyph = Resources.expand;
            bbiExpandAll.Id = 9;
            bbiExpandAll.ItemClick += ExpandAllItemClick;
            // bbiCollapseAll
            bbiCollapseAll.Caption = @"Свернуть все ветки";
            bbiCollapseAll.Glyph = Resources.collapse;
            bbiCollapseAll.ItemClick += CollapseAllItemClick;
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            ShowLayoutWorkMenu = true;
            helper.EnableScrollingOnColumnDragging();
            InitializePopupMenuButtons();
            //События
            FilterNode += OnFilterNode;
            Disposed += TreeListDisposed;
            MouseDown += TreeListMouseDown;
            MouseUp += TreeListMouseUp;
            DragEnter += TreeListDragEnter;
            MouseWheel += CustomTreeListMouseWheel;
            PopupMenuShowing += TreePopupMenuShowing;
            PopupMenuShowing += CustomMenuPopupMenuShowing;
        }

        protected override TreeListViewInfo CreateViewInfo()
        {
            return new CommonTreeListViewInfo(this);
        }

        protected override TreeListPainter CreatePainter()
        {
            return new CommonTreeListPainter(this);
        }

        protected override TreeListHandler CreateHandler()
        {
            return new CommonTreeListHandler(this);
        }

        protected override TreeListColumnCollection CreateColumns()
        {
            return new CommonTreeListColumnCollection(this);
        }

        protected override TreeListOptionsBehavior CreateOptionsBehavior()
        {
            var ctlob = new CommonTreeListOptionsBehavior(this);
            ctlob.AcceptChangeEventAction(AllowMoveHasChanged, CustomisationChanged);
            return ctlob;
        }

        public override void PopulateColumns()
        {
            base.PopulateColumns();
            OptionsBehavior.AutoPopulateColumns = false;
            foreach (var column in Columns.Cast<CommonTreeListColumn>()
                .Where(col => CommonUtilities.IsEnglishOnly(col.Caption)))
            {
                column.Visible = false;
            }
        }

        #region Обработка событий в дереве

        private static void TreeListDragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void CustomMenuPopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            switch (e.Menu.MenuType)
            {
                case TreeListMenuType.Column:
                    HideCustomizationColumns(e.Menu);
                    CreateSelectLayoutSubmenu(e.Menu);
                    break;
                case TreeListMenuType.Node:
                    if (CustomPopupMenu == null)
                    {
                        e.Menu.Items.Add(new DXMenuItem("Развернуть всё", ExpandAllItemClick, Resources.expand));
                        e.Menu.Items.Add(new DXMenuItem("Свернуть все ветки", CollapseAllItemClick, Resources.collapse));
                    }
                    else
                    {
                        if (bbiExpandAll.Links.Count == 0)
                            CustomPopupMenu.ItemLinks.Add(bbiExpandAll);
                        if (bbiCollapseAll.Links.Count == 0)
                            CustomPopupMenu.ItemLinks.Add(bbiCollapseAll);
                        ShowPopupMenu(e);
                    }
                    break;
            }
        }

        /// <summary>
        /// Показать контекстное меню в точке клика правого клика мыши
        /// </summary>
        private void ShowPopupMenu(PopupMenuShowingEventArgs e)
        {
            var hitInfo = CalcHitInfo(e.Point);
            if (hitInfo.Node == null)
            {
                return;
            }
            CustomPopupMenu.ShowPopup(PointToScreen(e.Point));
        }

        private void CreateSelectLayoutSubmenu(DXPopupMenu menu)
        {
            DXCustomMenuCreator.DXCreateLayoutMenuItems(this, ref menu);
        }

        private void CollapseAllItemClick(object sender, EventArgs e)
        {
            CollapseAll();
        }

        private void ExpandAllItemClick(object sender, EventArgs eventArgs)
        {
            FocusedNode.ExpandAll();
        }

        /// <summary>
        /// Скрыть стандартные кнопки из меню Грида
        /// </summary>
        private static void HideCustomizationColumns(DXSubMenuItem menu)
        {
            var showColumns = menu.Items
                .Cast<DXMenuItem>()
                .FirstOrDefault(i => i.Tag is TreeListStringId && (TreeListStringId)i.Tag == TreeListStringId.MenuColumnColumnCustomization);
            if (showColumns != null)
                showColumns.Visible = false;
        }

        static void TreePopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.Menu.MenuType != TreeListMenuType.Column)
            {
                return;
            }
            foreach (var item in e.Menu.Items.Cast<DXMenuItem>().Where(item => item.Caption == "Column Chooser"))
            {
                item.Enabled = false;
                break;
            }
        }

        private void TreeListDisposed(object sender, EventArgs e)
        {
            helper.DisableScrollingOnColumnDragging();
        }

        private void TreeListMouseDown(object sender, MouseEventArgs e)
        {
            var ht = CalcHitInfo(new Point(e.X, e.Y));
            if (ht != null && ht.Node != null)
            {
                FocusedNode = ht.Node;
            }
        }
        private void TreeListMouseUp(object sender, MouseEventArgs e)
        {
            var ht = CalcHitInfo(new Point(e.X, e.Y));
            if (e.Button == MouseButtons.Right && ht != null && ht.HitInfoType == HitInfoType.Empty && CustomPopupMenu != null)
            {
                CustomPopupMenu.ShowPopup(MousePosition);
            }
        }


        private void CustomTreeListMouseWheel(object sender, MouseEventArgs e)
        {
            var findForm = FindForm();
            if (findForm != null && findForm.ContainsFocus)
            {
                Focus();
            }
        }

        protected void AllowMoveHasChanged(bool value)
        {
            foreach (CommonTreeListColumn column in Columns)
            {
                column.OptionsColumn.AllowMove = value;
            }
            Invalidate();
        }

        protected void CustomisationChanged()
        {
            Invalidate();
        }

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
            hideEdit.Location = ViewInfo.QuickCustomisationBounds.Location;
        }

        private void CreateHideEdit()
        {
            hideEdit = new QuickHideEdit(this);
            LocateHideEdit();
            Controls.Add(hideEdit);
            Focus();
        }

        private void PopulateHideEdit()
        {
            hideEdit.Properties.Columns.Clear();
            if (Columns.Count == 0)
            {
                return;
            }
            var usedColumns = Columns
                .Cast<CommonTreeListColumn>()
                .Where(col => !String.IsNullOrEmpty(col.ToString()) && col.OptionsColumn.ShowInQuickHide &&
                    (col.Visible || col.OptionsColumn.ShowInCustomizationForm) && !CommonUtilities.IsEnglishOnly(col.Caption))
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
            hideEdit.Properties.Columns.Add("Скрыть/Показать все", checkAllValue, 0, true, false);
            foreach (var col in usedColumns)
            {
                hideEdit.Properties.Columns.Add(col.Caption, col.Visible,
                    col.VisibleIndex, GetColumnHideState(col), GetColumnMoveState(col));
            }
        }

        private bool GetColumnHideState(CommonTreeListColumn column)
        {
            return column.OptionsColumn.AllowQuickHide && OptionsCustomization.AllowQuickHideColumns;
        }

        private bool GetColumnMoveState(CommonTreeListColumn column)
        {
            return column.OptionsColumn.AllowMove && OptionsBehavior.AllowMove;
        }

        #endregion

        internal void AcceptQuickHide()
        {
            foreach (CommonTreeListColumn col in Columns)
            {
                var cp = hideEdit.Properties.Columns[col.Caption];
                if (cp == null)
                {
                    continue;
                }
                col.VisibleIndex = cp.VisibleIndex;
                col.Visible = cp.Visible;
            }
        }

        /// <summary>
        /// Доступность колонки для редактирования
        /// </summary>
        /// <param name="name">Название колонки</param>
        /// <param name="value">Значение</param>
        public void AllowEdit(string name, bool value)
        {
            Columns[name].OptionsColumn.AllowEdit = value;
        }

        /// <summary>
        ///     Сохранить информацию о внешнем виде объекта view класса <see cref="T:GridView" />
        /// </summary>
        public void SaveTreeInfo()
        {
            SaveSelectionTreeInfo(SaveSelList);
        }

        public void LoadTreeInfo()
        {
            LoadSelectionViewInfo(SaveSelList);
            LoadFocuseNode();
        }

        private void LoadFocuseNode()
        {
            if (focusedNode != null)
            {
                if (IdColumn != null)
                    FocuseNodeById(Convert.ToInt32(focusedNode.GetValue(IdColumn)));
            }
            else if (Nodes.Count != 0)
            {
                FocusedNode = Nodes[0];
            }
        }

        private void SaveSelectionTreeInfo(IList list)
        {
            list.Clear();
            focusedNode = FocusedNode;
            foreach (var node in GetAllCheckedNodes())
            {
                NodeInfo nodeInfo;
                nodeInfo.IsChecked = node.Checked;
                nodeInfo.Value = node.GetValue(IdColumn);
                nodeInfo.Level = node.Level;
                list.Add(nodeInfo);
            }
        }

        private void LoadSelectionViewInfo(IEnumerable list)
        {
            try
            {
                BeginUpdate();
                UncheckAll();
                foreach (var rowInfo in list)
                {
                    LoadRowByRowInfo((NodeInfo)rowInfo);
                }
            }
            finally
            {
                EndUpdate();
            }
        }

        private void LoadRowByRowInfo(NodeInfo nodeInfo)
        {
            if (!nodeInfo.IsChecked)
            {
                return;
            }
            var node = FindNodeByFieldValue(IdColumn.FieldName, nodeInfo.Value);
            if (node != null && nodeInfo.Level == node.Level)
            {
                node.Checked = true;
            }
        }

        public void FocuseNodeById(int wayId)
        {
            var node = FindNodeByFieldValue("Id", wayId);
            if (node.ParentNode != null)
            {
                node.ParentNode.Expanded = true;
            }
            FocusedNode = node;
        }

        private static void OnFilterNode(object sender, FilterNodeEventArgs e)
        {
            var filteredColumns = e.Node.TreeList.Columns
                .Where(c => c.FilterInfo.AutoFilterRowValue != null)
                .ToList();
            if (filteredColumns.Count == 0)
            {
                return;
            }
            e.Handled = true;
            var filtered = filteredColumns.Any(c => IsNodeMatchFilter(e.Node, c));
            e.Node.Visible = filtered;
            e.Node.Expanded = filtered;
        }

        private static bool IsNodeMatchFilter(TreeListNode node, TreeListColumn column)
        {
            var filterValue = column.FilterInfo.AutoFilterRowValue.ToString();
            if (node.GetDisplayText(column)
                .ToLower()
                .Contains(filterValue.ToLower()))
            {
                return true;
            }
            foreach (TreeListNode n in node.Nodes)
            {
                if (IsNodeMatchFilter(n, column))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        ///     Закоммитить изменения в DataSource у грида на форме
        /// </summary>
        public void CommitTreeListChanges()
        {
            PostEditor();
            CloseEditor();
            EndCurrentEdit();
        }

        [Serializable]
        private struct NodeInfo
        {
            public bool IsChecked;
            public int Level;
            public object Value;
        };

        public bool GetShowLayoutWorkMenu()
        {
            return ShowLayoutWorkMenu;
        }
    }
}
