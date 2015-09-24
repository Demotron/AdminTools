using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CommonLibrary.AdminFolder;
using CommonLibrary.GridControlFolder;
using CommonLibrary.LogicFolder;
using CommonLibrary.Properties;
using CommonLibrary.TreeListFolder;
using CommonLibrary.UserFolder;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Container;
using DevExpress.XtraGrid;
using DevExpress.XtraLayout;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraTreeList;
using ServerInformation;
using ServerInformation.ExceptionFolder;

namespace CommonLibrary
{
    /// <summary>
    ///     Предок для всех дочерних форм в приложении
    /// </summary>
    public partial class CommonChildForm : XtraForm, IActionButton
    {

        private ApplicationEntitie dbContext;
        private bool saveState = true;
        private bool askToSaveData = true;
        private readonly SplashScreenManager ssManager;

        public delegate void EventSimpleSignature(int? id = null, bool changedDB = false);
        public event EventSimpleSignature DoDataUpdate;
        public event Action RibbonChangedStyle;

        protected CommonChildForm()
        {
            InitializeComponent();
            ssManager = new SplashScreenManager(this, typeof(XtraWaitForm), true, true);
        }

        [Browsable(false)]
        protected ApplicationEntitie DBAppContext
        {
            get { return dbContext ?? (dbContext = new ApplicationEntitie(0)); }
            set { dbContext = value; }
        }

        /// <summary>
        ///     Обозначает нужно ли сохранять состояние формы при загрузке
        /// </summary>
        [Description("Форма сохраняет свое состояние при закрытии и загружает его при открытии"), DefaultValue(true)]
        public bool SaveState
        {
            get { return saveState; }
            set { saveState = value; }
        }

        /// <summary>
        ///     Обозначает нужно ли спрашивать перед закрытием формы о сохранении изменений
        /// </summary>
        [Browsable(true), Description("Перед закрытием формы проверяем были ли сделаны изменения"), DefaultValue(true)]
        public bool AskToSaveData
        {
            get { return askToSaveData; }
            set { askToSaveData = value; }
        }

        /// <summary>
        ///     Активное дерево в приложении
        /// </summary>
        private TreeList ActiveTreeList
        {
            get
            {
                var control = ActiveControl as SplitContainer;
                if (control != null)
                {
                    var containerControl = control;
                    return containerControl.ActiveControl as TreeList;
                }
                var ctrl = ActiveControl;
                while (ctrl != null)
                {
                    var list = ctrl as TreeList;
                    if (list != null)
                    {
                        return list;
                    }
                    ctrl = ctrl.Parent;
                }
                return null;
            }
        }

        /// <summary>
        ///     Активный таблица в приложении
        /// </summary>
        protected GridControl ActiveGrid
        {
            get
            {
                var layout = ActiveControl as LayoutControl;
                if (layout != null)
                {
                    return layout.ActiveControl as CommonGridControl;
                }
                var control = ActiveControl as SplitContainer;
                if (control != null)
                {
                    var containerControl = control;
                    return containerControl.ActiveControl as GridControl;
                }
                var ctrl = ActiveControl;
                while (ctrl != null)
                {
                    var grid = ctrl as GridControl;
                    if (grid != null)
                    {
                        return grid;
                    }
                    ctrl = ctrl.Parent;
                }
                return null;
            }
        }

        /// <summary>
        ///     Активный пиво в приложении
        /// </summary>
        protected PivotGridControl ActivePivot
        {
            get
            {
                var layout = ActiveControl as LayoutControl;
                if (layout != null)
                {
                    return layout.ActiveControl as PivotGridControl;
                }
                var control = ActiveControl as SplitContainer;
                if (control != null)
                {
                    var containerControl = control;
                    return containerControl.ActiveControl as PivotGridControl;
                }
                var ctrl = ActiveControl;
                while (ctrl != null)
                {
                    var pivot = ctrl as PivotGridControl;
                    if (pivot != null)
                    {
                        return pivot;
                    }
                    ctrl = ctrl.Parent;
                }
                return null;
            }
        }

        /// <summary>
        ///     Активное дерево в приложении
        /// </summary>
        protected virtual CommonTreeList ActiveTree
        {
            get
            {
                var trees = FormControls
                    .GetControls<EditorContainer>(Controls)
                    .OfType<CommonTreeList>()
                    .ToArray();
                if (trees.Length == 1)
                {
                    return trees[0];
                }
                var control = ActiveControl as SplitContainer;
                if (control == null)
                {
                    return ActiveControl as CommonTreeList;
                }
                var containerControl = control;
                return containerControl.ActiveControl as CommonTreeList;
            }
        }

        /// <summary>
        ///     Активный GridView у XtraGrid в данный момент
        /// </summary>
        protected virtual CommonGridView ActiveView
        {
            get
            {
                var grids = FormControls
                    .GetControls<EditorContainer>(Controls)
                    .OfType<CommonGridControl>()
                    .ToArray();
                if (grids.Length == 1)
                {
                    return grids[0].MainView;
                }
                var splitCont = ActiveControl as SplitContainer;
                if (splitCont != null)
                {
                    var containerControl = splitCont;
                    var control = containerControl.ActiveControl as CommonGridControl;
                    if (control != null)
                    {
                        return control.MainView;
                    }
                }
                else
                {
                    var control = ActiveControl;
                    while (control != null && !(control is CommonGridControl))
                    {
                        control = control.Parent;
                    }
                    if (control != null)
                    {
                        return ((CommonGridControl)control).MainView;
                    }
                }
                return null;
            }
        }

        public List<string> XmlSettings { get; set; }

        //        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        //        {
        //            var view = ActiveView;
        //            if (view is CommonGridView && (view as CommonGridView).OptionsCustomization.CustomCopyPast)
        //            {
        //                if (keyData == (Keys.Control | Keys.C))
        //                {
        //                    CopyToClipboard();
        //                    return true;
        //                }
        //                if (keyData == (Keys.Control | Keys.V))
        //                {
        //                    PasteFromClipboard();
        //                    return true;
        //                }
        //            }
        //            return base.ProcessCmdKey(ref msg, keyData);
        //        }

        private void MdiChildFormLoad(object sender, EventArgs e)
        {
            ChangeRibbonStyleSize();
            if (DesignMode || !saveState)
            {
                return;
            }
            this.LoadFormsControlsBeforeShow();

            //            StatebleControlTuner.LoadControlsStatesBeforeShow(Controls);
        }

        public void ChangeRibbonStyleSize()
        {
            var ribbon = FormControls.GetControls<RibbonControl>(Controls).FirstOrDefault();
            if (ribbon == null)
            {
                return;
            }
            switch (Settings.Default.ImageStyle)
            {
                case 0:
                    ribbon.RibbonStyle = RibbonControlStyle.Office2013;
                    foreach (BarItem item in ribbon.Manager.Items)
                    {
                        item.RibbonStyle = RibbonItemStyles.Default;
                    }
                    break;
                case 1:
                    ribbon.RibbonStyle = RibbonControlStyle.Office2013;
                    foreach (BarItem item in ribbon.Manager.Items)
                    {
                        item.RibbonStyle = RibbonItemStyles.SmallWithText | RibbonItemStyles.SmallWithoutText;
                    }
                    break;
                case 2:
                    ribbon.RibbonStyle = RibbonControlStyle.TabletOffice;
                    break;
            }
            ribbon.AutoSizeItems = true;
            if (RibbonChangedStyle != null)
                RibbonChangedStyle();
        }

        private void MdiChildFormShown(object sender, EventArgs e)
        {
            Application.DoEvents();
            if (DesignMode)
            {
                return;
            }
            ShowWaitForm();
        }

        private void MdiChildFormActivated(object sender, EventArgs e)
        {
            var mainForm = FormControls.MainForm as IAdminTool;
            if (mainForm == null)
            {
                return;
            }
            mainForm.ActivatedFormChanged();
        }

        private void MdiChildFormFormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = CheckToSaveData() == DialogResult.Cancel;
            if (!saveState)
            {
                return;
            }
            StatebleControlTuner.SaveControlsStatesBeforeClose(Controls);
        }

        private void MdiChildFormFormClosed(object sender, FormClosedEventArgs e)
        {
            CloseWaitForm();
            var mainForm = FormControls.MainForm as IAdminTool;
            if (DoDataUpdate != null)
            {
                DoDataUpdate();
            }
            if (mainForm != null)
            {
                mainForm.CloseActivateForm(this);
            }
        }

        public DialogResult ShowDialog(Form owner = null)
        {
#if !DEBUG
            if (SaveState && DBUser.DesignMode)
            {
                MdiParent = FormControls.MainForm;
                Show();
                Activate();
                return DialogResult.Abort;
            }
#endif
            return base.ShowDialog(owner);
        }

        /// <summary>
        /// Показать форму ожидания загрузки
        /// </summary>
        public void ShowWaitForm(string caption = null)
        {
            if (ssManager.IsSplashFormVisible)
            {
                return;
            }
            if (!string.IsNullOrEmpty(caption))
                ssManager.SetWaitFormCaption(caption);
            ssManager.ShowWaitForm();
        }

        /// <summary>
        /// Показать форму ожидания загрузки
        /// </summary>
        protected void ShowWaitForm(Delegate method, out object result, params object[] args)
        {
            ShowWaitForm();
            result = method.DynamicInvoke(args);
            CloseWaitForm();
        }

        /// <summary>
        /// Закрыть форму ожидания загрузки
        /// </summary>
        public void CloseWaitForm()
        {
            if (ssManager.IsSplashFormVisible)
            {
                ssManager.CloseWaitForm();
            }
        }

        public bool CheckDBContext(IEnumerable<object> data)
        {
            return DBAppContext.CheckExist(data.First());
        }

        public void DeleteFromDBContext(params object[] objects)
        {
            foreach (var t in objects)
            {
                DBAppContext.Remove(t);
            }
        }

        public void AddToDBContext(object obj)
        {
            DBAppContext.Add(obj);
        }

        #region Implementation of IActionButton

        public virtual void AddNewRow()
        {
            if (ActiveView != null)
            {
                ActiveView.AddRowButtonClick();
            }
        }

        public virtual void DeleteRow()
        {
            if (ActiveView != null)
            {
                ActiveView.RemoveRowsButtonClick();
            }
        }

        public virtual void RefreshData()
        {
            if (DesignMode)
            {
                return;
            }
            if (CheckToSaveData() == DialogResult.Cancel)
            {
                return;
            }
            if (DBAppContext != null)
            {
                DBAppContext = new ApplicationEntitie(0);
            }
        }

        public virtual bool SaveChanges(bool showMsg = true)
        {
            try
            {
                this.CommitFormsChanges();
                DBAppContext.SaveChanges();
                if (showMsg)
                    MessageWindow.GetInstance("Изменения успешно сохранены.", MessageType.Info);
            }
            catch (SqlException e)
            {
                var msg = e.Message.GetUserMessageByErrorMessage();
                if (msg.IsEmptyAfterTrim())
                {
                    DBException.WriteLog(e);
                }
                else
                {
                    XtraMessageBox.Show(this, msg, @"Операция отменена", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return false;
            }
            catch (DbUpdateException e)
            {
                var inner = e.InnerException;
                while (inner.InnerException != null)
                {
                    inner = inner.InnerException;
                }
                var msg = inner.Message.GetUserMessageByErrorMessage();
                if (msg.IsEmptyAfterTrim())
                {
                    DBException.WriteLog(e);
                }
                else
                {
                    XtraMessageBox.Show(this, msg, @"Операция отменена", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return false;
            }
            catch (Exception e)
            {
                MessageWindow.GetInstance("Произошла ошибка при обновлении информации в базе данных. Операция отменена.");
                DBException.WriteLog(e);
                return false;
            }
            return true;
        }

        public virtual void ExportToExcel()
        {
            if (ActiveGrid == null && ActiveTreeList == null && ActivePivot == null)
            {
                return;
            }
            var fileName = ActiveGrid != null
                ? ActiveGrid.MainView.ViewCaption
                : string.Empty;
            var od = new SaveFileDialog
            {
                Filter = @"Excel Files (.xls)|*.xls",
                FileName = fileName
            };
            if (od.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            if (ActiveGrid != null)
            {
                if (!File.Exists(od.FileName))
                {
                    if (!File.Exists(od.FileName))
                    {
                        var fs = File.Create(od.FileName);
                        fs.Close();
                    }
                    ActiveGrid.ExportToXls(od.FileName);
                }
                ActiveGrid.ExportToXls(od.FileName);
            }
            else if (ActiveTreeList != null)
            {
                ActiveTreeList.ExportToXls(od.FileName);
            }
            else if (ActivePivot != null)
            {
                ActivePivot.ExportToXls(od.FileName);
            }

        }

        protected virtual bool HasUnsavedChanges()
        {
            return DBAppContext.HasUnsavedChanges();
        }

        protected virtual DialogResult CheckToSaveData()
        {
            if (!askToSaveData || DBAppContext == null)
            {
                return DialogResult.None;
            }
            this.CommitFormsChanges();
            Validate();
            if (!HasUnsavedChanges())
            {
                return DialogResult.None;
            }
            switch (
                XtraMessageBox.Show("Не все изменения были сохранены, хотите сохранить изменения в базу данных?", "Сохранение изменений",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
            {
                case DialogResult.Yes:
                    return SaveChanges()
                        ? DialogResult.Yes
                        : DialogResult.Cancel;
                case DialogResult.No:
                    return DialogResult.No;
                case DialogResult.Cancel:
                    return DialogResult.Cancel;
            }
            return DialogResult.None;
        }

        #endregion
    }
}