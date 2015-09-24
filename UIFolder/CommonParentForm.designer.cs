using System.ComponentModel;
using DevExpress.LookAndFeel;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraBars.Docking2010.Views.NativeMdi;
using DevExpress.XtraBars.Docking2010.Views.Tabbed;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraEditors.Repository;

namespace CommonLibrary
{
    partial class CommonParentForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CommonParentForm));
            this.rbnMain = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.appMenu = new DevExpress.XtraBars.Ribbon.ApplicationMenu(this.components);
            this.bbiSettings = new DevExpress.XtraBars.BarButtonItem();
            this.bbiExitUser = new DevExpress.XtraBars.BarButtonItem();
            this.siVersion = new DevExpress.XtraBars.BarStaticItem();
            this.alignButtonGroup = new DevExpress.XtraBars.BarButtonGroup();
            this.bbiAdd = new DevExpress.XtraBars.BarButtonItem();
            this.bbiRemove = new DevExpress.XtraBars.BarButtonItem();
            this.bbiRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.bbiExel = new DevExpress.XtraBars.BarButtonItem();
            this.bbiAdmin = new DevExpress.XtraBars.BarButtonItem();
            this.bbiSave = new DevExpress.XtraBars.BarButtonItem();
            this.bbiRoleEdit = new DevExpress.XtraBars.BarButtonItem();
            this.brDockMenuItem = new DevExpress.XtraBars.BarDockingMenuItem();
            this.brEdItControlsStates = new DevExpress.XtraBars.BarEditItem();
            this.ripceControlsStates = new DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit();
            this.rpMain = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.rpgMain = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.rpDictionaries = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.rpgCommonDictionaries = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.rpReports = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.rpSettings = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.rpAdmin = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.rpgAdmin = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.riceStates = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.rsbMain = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.dxErrorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.documentMng = new DevExpress.XtraBars.Docking2010.DocumentManager(this.components);
            this.MDIViewNative = new DevExpress.XtraBars.Docking2010.Views.NativeMdi.NativeMdiView(this.components);
            this.MDIViewTabbed = new DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView(this.components);
            this.bbiChangeUser = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.rbnMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.appMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ripceControlsStates)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riceStates)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.documentMng)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MDIViewNative)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MDIViewTabbed)).BeginInit();
            this.SuspendLayout();
            // 
            // rbnMain
            // 
            this.rbnMain.ApplicationButtonDropDownControl = this.appMenu;
            this.rbnMain.ApplicationButtonText = "Меню";
            this.rbnMain.AutoSizeItems = true;
            this.rbnMain.Categories.AddRange(new DevExpress.XtraBars.BarManagerCategory[] {
            new DevExpress.XtraBars.BarManagerCategory("Dictionaries", new System.Guid("bfb17616-73d9-42e6-8028-a10ce19e5160"))});
            this.rbnMain.ExpandCollapseItem.Id = 0;
            this.rbnMain.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.rbnMain.ExpandCollapseItem,
            this.siVersion,
            this.alignButtonGroup,
            this.bbiAdd,
            this.bbiRemove,
            this.bbiRefresh,
            this.bbiExel,
            this.bbiAdmin,
            this.bbiSave,
            this.bbiRoleEdit,
            this.brDockMenuItem,
            this.bbiSettings,
            this.brEdItControlsStates,
            this.bbiExitUser,
            this.bbiChangeUser});
            this.rbnMain.Location = new System.Drawing.Point(0, 0);
            this.rbnMain.MaxItemId = 162;
            this.rbnMain.Name = "rbnMain";
            this.rbnMain.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.rpMain,
            this.rpDictionaries,
            this.rpReports,
            this.rpSettings,
            this.rpAdmin});
            this.rbnMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.ripceControlsStates,
            this.riceStates});
            this.rbnMain.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2013;
            this.rbnMain.ShowToolbarCustomizeItem = false;
            this.rbnMain.Size = new System.Drawing.Size(1210, 173);
            this.rbnMain.StatusBar = this.rsbMain;
            this.rbnMain.Toolbar.ItemLinks.Add(this.bbiAdd);
            this.rbnMain.Toolbar.ItemLinks.Add(this.bbiRemove);
            this.rbnMain.Toolbar.ItemLinks.Add(this.bbiRefresh);
            this.rbnMain.Toolbar.ItemLinks.Add(this.bbiSave);
            this.rbnMain.Toolbar.ItemLinks.Add(this.bbiExel);
            this.rbnMain.Toolbar.ItemLinks.Add(this.brDockMenuItem);
            this.rbnMain.Toolbar.ShowCustomizeItem = false;
            this.rbnMain.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Below;
            this.rbnMain.ShowCustomizationMenu += new DevExpress.XtraBars.Ribbon.RibbonCustomizationMenuEventHandler(this.RibbonShowCustomizationMenu);
            this.rbnMain.Merge += new DevExpress.XtraBars.Ribbon.RibbonMergeEventHandler(this.MainMerge);
            // 
            // appMenu
            // 
            this.appMenu.ItemLinks.Add(this.bbiSettings, true);
            this.appMenu.ItemLinks.Add(this.bbiChangeUser);
            this.appMenu.ItemLinks.Add(this.bbiExitUser);
            this.appMenu.Name = "appMenu";
            this.appMenu.Ribbon = this.rbnMain;
            // 
            // bbiSettings
            // 
            this.bbiSettings.Caption = "Настройки";
            this.bbiSettings.Description = "Настройки программы";
            this.bbiSettings.Hint = "Настройки программы";
            this.bbiSettings.Id = 76;
            this.bbiSettings.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("bbiSettings.LargeGlyph")));
            this.bbiSettings.Name = "bbiSettings";
            // 
            // bbiExitUser
            // 
            this.bbiExitUser.Caption = "Выход";
            this.bbiExitUser.CategoryGuid = new System.Guid("6ffddb2b-9015-4d97-a4c1-91613e0ef537");
            this.bbiExitUser.Id = 160;
            this.bbiExitUser.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("bbiExitUser.LargeGlyph")));
            this.bbiExitUser.Name = "bbiExitUser";
            // 
            // siVersion
            // 
            this.siVersion.Caption = "Версия:";
            this.siVersion.Id = 31;
            this.siVersion.Name = "siVersion";
            this.siVersion.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // alignButtonGroup
            // 
            this.alignButtonGroup.Caption = "Align Commands";
            this.alignButtonGroup.Id = 52;
            this.alignButtonGroup.Name = "alignButtonGroup";
            // 
            // bbiAdd
            // 
            this.bbiAdd.Caption = "Добавить данные";
            this.bbiAdd.Glyph = ((System.Drawing.Image)(resources.GetObject("bbiAdd.Glyph")));
            this.bbiAdd.Hint = "Добавить новую запись в текущий элемент интерфейса";
            this.bbiAdd.Id = 62;
            this.bbiAdd.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N));
            this.bbiAdd.Name = "bbiAdd";
            this.bbiAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.AddItemClick);
            // 
            // bbiRemove
            // 
            this.bbiRemove.Caption = "Удалить данные";
            this.bbiRemove.Glyph = ((System.Drawing.Image)(resources.GetObject("bbiRemove.Glyph")));
            this.bbiRemove.Hint = "Удалить текущие данные";
            this.bbiRemove.Id = 63;
            this.bbiRemove.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete));
            this.bbiRemove.Name = "bbiRemove";
            this.bbiRemove.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.RemoveItemClick);
            // 
            // bbiRefresh
            // 
            this.bbiRefresh.Caption = "Обновить данные";
            this.bbiRefresh.Glyph = ((System.Drawing.Image)(resources.GetObject("bbiRefresh.Glyph")));
            this.bbiRefresh.Hint = "Обновить записи из базы данных";
            this.bbiRefresh.Id = 65;
            this.bbiRefresh.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5);
            this.bbiRefresh.Name = "bbiRefresh";
            this.bbiRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.RefreshItemClick);
            // 
            // bbiExel
            // 
            this.bbiExel.Caption = "Сохранить в Exel";
            this.bbiExel.Glyph = ((System.Drawing.Image)(resources.GetObject("bbiExel.Glyph")));
            this.bbiExel.Hint = "Сохранить данные в виде Excel";
            this.bbiExel.Id = 66;
            this.bbiExel.Name = "bbiExel";
            this.bbiExel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ExelItemClick);
            // 
            // bbiAdmin
            // 
            this.bbiAdmin.Caption = "Настройка интерфейсов";
            this.bbiAdmin.Glyph = ((System.Drawing.Image)(resources.GetObject("bbiAdmin.Glyph")));
            this.bbiAdmin.Hint = "Администрирование внешнего вида форм";
            this.bbiAdmin.Id = 69;
            this.bbiAdmin.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("bbiAdmin.LargeGlyph")));
            this.bbiAdmin.Name = "bbiAdmin";
            this.bbiAdmin.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.AdminItemClick);
            // 
            // bbiSave
            // 
            this.bbiSave.Caption = "Сохранить в базу";
            this.bbiSave.Glyph = ((System.Drawing.Image)(resources.GetObject("bbiSave.Glyph")));
            this.bbiSave.Hint = "Сохранить изменения текущего элемента на сервер";
            this.bbiSave.Id = 70;
            this.bbiSave.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S));
            this.bbiSave.Name = "bbiSave";
            this.bbiSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.CommitItemClick);
            // 
            // bbiRoleEdit
            // 
            this.bbiRoleEdit.Caption = "Настройка ролей и сотрудников";
            this.bbiRoleEdit.Glyph = ((System.Drawing.Image)(resources.GetObject("bbiRoleEdit.Glyph")));
            this.bbiRoleEdit.Id = 71;
            this.bbiRoleEdit.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("bbiRoleEdit.LargeGlyph")));
            this.bbiRoleEdit.Name = "bbiRoleEdit";
            this.bbiRoleEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.RoleEditItemClick);
            // 
            // brDockMenuItem
            // 
            this.brDockMenuItem.Caption = "Расположение форм";
            this.brDockMenuItem.Glyph = ((System.Drawing.Image)(resources.GetObject("brDockMenuItem.Glyph")));
            this.brDockMenuItem.Hint = "Настройка расположения форм";
            this.brDockMenuItem.Id = 78;
            this.brDockMenuItem.Name = "brDockMenuItem";
            // 
            // brEdItControlsStates
            // 
            this.brEdItControlsStates.Edit = this.ripceControlsStates;
            this.brEdItControlsStates.Id = 89;
            this.brEdItControlsStates.Name = "brEdItControlsStates";
            // 
            // ripceControlsStates
            // 
            this.ripceControlsStates.AutoHeight = false;
            this.ripceControlsStates.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ripceControlsStates.Name = "ripceControlsStates";
            // 
            // rpMain
            // 
            this.rpMain.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.rpgMain});
            this.rpMain.Name = "rpMain";
            this.rpMain.Text = "Главная";
            // 
            // rpgMain
            // 
            this.rpgMain.Name = "rpgMain";
            this.rpgMain.Text = "Главное";
            // 
            // rpDictionaries
            // 
            this.rpDictionaries.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.rpgCommonDictionaries});
            this.rpDictionaries.Name = "rpDictionaries";
            this.rpDictionaries.Text = "Справочники";
            // 
            // rpgCommonDictionaries
            // 
            this.rpgCommonDictionaries.Name = "rpgCommonDictionaries";
            this.rpgCommonDictionaries.Text = "Общие";
            // 
            // rpReports
            // 
            this.rpReports.Name = "rpReports";
            this.rpReports.Text = "Отчёты";
            // 
            // rpSettings
            // 
            this.rpSettings.Name = "rpSettings";
            this.rpSettings.Text = "Настройки системы";
            // 
            // rpAdmin
            // 
            this.rpAdmin.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.rpgAdmin});
            this.rpAdmin.Name = "rpAdmin";
            this.rpAdmin.Text = "Администрирование";
            this.rpAdmin.Visible = false;
            // 
            // rpgAdmin
            // 
            this.rpgAdmin.ItemLinks.Add(this.bbiAdmin);
            this.rpgAdmin.ItemLinks.Add(this.bbiRoleEdit);
            this.rpgAdmin.Name = "rpgAdmin";
            this.rpgAdmin.Text = "Администратор";
            // 
            // riceStates
            // 
            this.riceStates.Name = "riceStates";
            this.riceStates.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            // 
            // rsbMain
            // 
            this.rsbMain.ItemLinks.Add(this.siVersion);
            this.rsbMain.Location = new System.Drawing.Point(0, 669);
            this.rsbMain.Name = "rsbMain";
            this.rsbMain.Ribbon = this.rbnMain;
            this.rsbMain.Size = new System.Drawing.Size(1210, 31);
            // 
            // dxErrorProvider
            // 
            this.dxErrorProvider.ContainerControl = this;
            // 
            // documentMng
            // 
            this.documentMng.MdiParent = this;
            this.documentMng.MenuManager = this.rbnMain;
            this.documentMng.ShowThumbnailsInTaskBar = DevExpress.Utils.DefaultBoolean.False;
            this.documentMng.View = this.MDIViewNative;
            this.documentMng.ViewCollection.AddRange(new DevExpress.XtraBars.Docking2010.Views.BaseView[] {
            this.MDIViewNative,
            this.MDIViewTabbed});
            // 
            // bbiChangeUser
            // 
            this.bbiChangeUser.Caption = "Сменить пользователя";
            this.bbiChangeUser.Id = 161;
            this.bbiChangeUser.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("bbiChangeUser.LargeGlyph")));
            this.bbiChangeUser.Name = "bbiChangeUser";
            // 
            // CommonParentForm
            // 
            this.AllowFormGlass = DevExpress.Utils.DefaultBoolean.False;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1210, 700);
            this.Controls.Add(this.rsbMain);
            this.Controls.Add(this.rbnMain);
            this.IsMdiContainer = true;
            this.Name = "CommonParentForm";
            this.Ribbon = this.rbnMain;
            this.StatusBar = this.rsbMain;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.CommonParentFormActivated);
            this.Load += new System.EventHandler(this.CommonParentFormLoad);
            this.Shown += new System.EventHandler(this.CommonParentFormShown);
            ((System.ComponentModel.ISupportInitialize)(this.rbnMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.appMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ripceControlsStates)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riceStates)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.documentMng)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MDIViewNative)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MDIViewTabbed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RepositoryItemCheckEdit riceStates;
        protected RibbonControl rbnMain;
        protected BarStaticItem siVersion;
        private BarButtonGroup alignButtonGroup;
        protected RibbonPage rpMain;
        protected RibbonPageGroup rpgMain;
        private ApplicationMenu appMenu;
        private RibbonStatusBar rsbMain;
        private BarButtonItem bbiAdd;
        private BarButtonItem bbiRemove;
        private BarButtonItem bbiRefresh;
        private BarButtonItem bbiExel;
        private BarButtonItem bbiAdmin;
        private BarButtonItem bbiSave;
        private BarButtonItem bbiRoleEdit;
        private RibbonPage rpAdmin;
        protected RibbonPageGroup rpgAdmin;
        protected BarButtonItem bbiSettings;
        protected RibbonPage rpDictionaries;
        protected RibbonPageGroup rpgCommonDictionaries;
        protected DefaultLookAndFeel defaultLookAndFeel;
        private BarEditItem brEdItControlsStates;
        private RepositoryItemPopupContainerEdit ripceControlsStates;
        private DXErrorProvider dxErrorProvider;
        private BarDockingMenuItem brDockMenuItem;
        protected RibbonPage rpReports;
        protected RibbonPage rpSettings;
        protected DocumentManager documentMng;
        private NativeMdiView MDIViewNative;
        private TabbedView MDIViewTabbed;
        protected BarButtonItem bbiExitUser;
        protected BarButtonItem bbiChangeUser;
    }
}
