using System.ComponentModel;
using System.Windows.Forms;
using CommonLibrary.PropertyGridFolder;
using CommonLibrary.TreeListFolder;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;

namespace CommonLibrary.AdminFolder
{
    partial class AdminTool
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminTool));
            this.tlControls = new CommonTreeList();
            this.tlcType = new CommonTreeListColumn();
            this.tlcText = new CommonTreeListColumn();
            this.lueForms = new DevExpress.XtraEditors.LookUpEdit();
            this.bsForms = new System.Windows.Forms.BindingSource(this.components);
            this.lcAdmin = new DevExpress.XtraLayout.LayoutControl();
            this.pgFilter = new FilterPropertyGrid();
            this.lueRoles = new DevExpress.XtraEditors.LookUpEdit();
            this.lcgAdmin = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lciForm = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciTree = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciRole = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciProperty = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            this.rcAdmin = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.bbiDefault = new DevExpress.XtraBars.BarButtonItem();
            this.bbiRestore = new DevExpress.XtraBars.BarButtonItem();
            this.bbiSave = new DevExpress.XtraBars.BarButtonItem();
            this.rpMain = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.rpgSettings = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.toolTip = new DevExpress.Utils.ToolTipController(this.components);
            this.pmControls = new DevExpress.XtraBars.PopupMenu(this.components);
            this.bbiExpand = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.tlControls)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueForms.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsForms)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcAdmin)).BeginInit();
            this.lcAdmin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueRoles.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgAdmin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciForm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciTree)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciRole)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciProperty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcAdmin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pmControls)).BeginInit();
            this.SuspendLayout();
            // 
            // tlControls
            // 
            this.tlControls.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.tlcType,
            this.tlcText});
            this.tlControls.KeyFieldName = "Name";
            this.tlControls.Location = new System.Drawing.Point(7, 85);
            this.tlControls.Name = "tlControls";
            this.tlControls.OptionsBehavior.Editable = false;
            this.tlControls.OptionsView.ShowAutoFilterRow = true;
            this.tlControls.ParentFieldName = "Parent";
            this.tlControls.Size = new System.Drawing.Size(377, 397);
            this.tlControls.TabIndex = 2;
            this.tlControls.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.ControlsFocusedNodeChanged);
            this.tlControls.PopupMenuShowing += new DevExpress.XtraTreeList.PopupMenuShowingEventHandler(this.ControlsPopupMenuShowing);
            // 
            // tlcType
            // 
            this.tlcType.Caption = "Тип элемента";
            this.tlcType.FieldName = "Type";
            this.tlcType.Name = "tlcType";
            this.tlcType.OptionsColumn.AllowEdit = false;
            this.tlcType.OptionsFilter.AutoFilterCondition = DevExpress.XtraTreeList.Columns.AutoFilterCondition.Contains;
            this.tlcType.Visible = true;
            this.tlcType.VisibleIndex = 0;
            this.tlcType.Width = 190;
            // 
            // tlcText
            // 
            this.tlcText.Caption = "Текст элемента";
            this.tlcText.FieldName = "Caption";
            this.tlcText.Name = "tlcText";
            this.tlcText.OptionsColumn.AllowEdit = false;
            this.tlcText.OptionsFilter.AutoFilterCondition = DevExpress.XtraTreeList.Columns.AutoFilterCondition.Contains;
            this.tlcText.Visible = true;
            this.tlcText.VisibleIndex = 1;
            this.tlcText.Width = 113;
            // 
            // lueForms
            // 
            this.lueForms.Location = new System.Drawing.Point(94, 17);
            this.lueForms.MaximumSize = new System.Drawing.Size(0, 20);
            this.lueForms.Name = "lueForms";
            this.lueForms.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueForms.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Value", "Заголовок формы", 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.lueForms.Properties.DataSource = this.bsForms;
            this.lueForms.Properties.DisplayMember = "Value";
            this.lueForms.Properties.DropDownRows = 10;
            this.lueForms.Properties.NullText = "";
            this.lueForms.Properties.ValueMember = "Key";
            this.lueForms.Size = new System.Drawing.Size(290, 20);
            this.lueForms.StyleController = this.lcAdmin;
            this.lueForms.TabIndex = 4;
            this.lueForms.EditValueChanged += new System.EventHandler(this.FormsEditValueChanged);
            // 
            // bsForms
            // 
            this.bsForms.DataSource = typeof(System.Collections.Generic.KeyValuePair<string, string>);
            // 
            // lcAdmin
            // 
            this.lcAdmin.Controls.Add(this.pgFilter);
            this.lcAdmin.Controls.Add(this.tlControls);
            this.lcAdmin.Controls.Add(this.lueForms);
            this.lcAdmin.Controls.Add(this.lueRoles);
            this.lcAdmin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lcAdmin.Location = new System.Drawing.Point(0, 96);
            this.lcAdmin.Name = "lcAdmin";
            this.lcAdmin.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(655, 271, 250, 350);
            this.lcAdmin.Root = this.lcgAdmin;
            this.lcAdmin.Size = new System.Drawing.Size(391, 692);
            this.lcAdmin.TabIndex = 1;
            this.lcAdmin.Text = "lcAdmin";
            // 
            // pgFilter
            // 
            this.pgFilter.Location = new System.Drawing.Point(7, 491);
            this.pgFilter.Name = "pgFilter";
            this.pgFilter.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.pgFilter.Size = new System.Drawing.Size(377, 194);
            this.pgFilter.TabIndex = 0;
            this.pgFilter.ToolbarVisible = false;
            this.pgFilter.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.FilterPropertyValueChanged);
            // 
            // lueRoles
            // 
            this.lueRoles.Location = new System.Drawing.Point(94, 51);
            this.lueRoles.MaximumSize = new System.Drawing.Size(0, 20);
            this.lueRoles.Name = "lueRoles";
            this.lueRoles.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueRoles.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Name", 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.lueRoles.Properties.DisplayMember = "Name";
            this.lueRoles.Properties.DropDownRows = 10;
            this.lueRoles.Properties.ImmediatePopup = true;
            this.lueRoles.Properties.NullText = "";
            this.lueRoles.Properties.NullValuePrompt = "Выберите роль для настройки";
            this.lueRoles.Properties.PopupSizeable = false;
            this.lueRoles.Properties.ShowHeader = false;
            this.lueRoles.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lueRoles.Properties.ValueMember = "Id";
            this.lueRoles.Size = new System.Drawing.Size(290, 20);
            this.lueRoles.StyleController = this.lcAdmin;
            this.lueRoles.TabIndex = 0;
            this.lueRoles.EditValueChanged += new System.EventHandler(this.RolesEditValueChanged);
            // 
            // lcgAdmin
            // 
            this.lcgAdmin.CustomizationFormText = "Root";
            this.lcgAdmin.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcgAdmin.GroupBordersVisible = false;
            this.lcgAdmin.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciForm,
            this.lciTree,
            this.lciRole,
            this.lciProperty,
            this.splitterItem1});
            this.lcgAdmin.Location = new System.Drawing.Point(0, 0);
            this.lcgAdmin.Name = "Root";
            this.lcgAdmin.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lcgAdmin.Size = new System.Drawing.Size(391, 692);
            this.lcgAdmin.Text = "Root";
            this.lcgAdmin.TextVisible = false;
            // 
            // lciForm
            // 
            this.lciForm.Control = this.lueForms;
            this.lciForm.CustomizationFormText = "Активная форма";
            this.lciForm.Location = new System.Drawing.Point(0, 0);
            this.lciForm.Name = "lciForm";
            this.lciForm.Size = new System.Drawing.Size(381, 34);
            this.lciForm.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 10, 0);
            this.lciForm.Text = "Активная форма";
            this.lciForm.TextSize = new System.Drawing.Size(84, 13);
            // 
            // lciTree
            // 
            this.lciTree.Control = this.tlControls;
            this.lciTree.CustomizationFormText = "lciTree";
            this.lciTree.Location = new System.Drawing.Point(0, 68);
            this.lciTree.Name = "lciTree";
            this.lciTree.Size = new System.Drawing.Size(381, 411);
            this.lciTree.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 10, 0);
            this.lciTree.Text = "lciTree";
            this.lciTree.TextSize = new System.Drawing.Size(0, 0);
            this.lciTree.TextToControlDistance = 0;
            this.lciTree.TextVisible = false;
            // 
            // lciRole
            // 
            this.lciRole.Control = this.lueRoles;
            this.lciRole.CustomizationFormText = "Активная роль";
            this.lciRole.Location = new System.Drawing.Point(0, 34);
            this.lciRole.Name = "lciRole";
            this.lciRole.Size = new System.Drawing.Size(381, 34);
            this.lciRole.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 10, 0);
            this.lciRole.Text = "Активная роль";
            this.lciRole.TextSize = new System.Drawing.Size(84, 13);
            // 
            // lciProperty
            // 
            this.lciProperty.Control = this.pgFilter;
            this.lciProperty.CustomizationFormText = "lciProperty";
            this.lciProperty.Location = new System.Drawing.Point(0, 484);
            this.lciProperty.Name = "lciProperty";
            this.lciProperty.Size = new System.Drawing.Size(381, 198);
            this.lciProperty.Text = "lciProperty";
            this.lciProperty.TextSize = new System.Drawing.Size(0, 0);
            this.lciProperty.TextToControlDistance = 0;
            this.lciProperty.TextVisible = false;
            // 
            // splitterItem1
            // 
            this.splitterItem1.AllowHotTrack = true;
            this.splitterItem1.CustomizationFormText = "splitterItem1";
            this.splitterItem1.Location = new System.Drawing.Point(0, 479);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new System.Drawing.Size(381, 5);
            // 
            // rcAdmin
            // 
            this.rcAdmin.ExpandCollapseItem.Id = 0;
            this.rcAdmin.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.rcAdmin.ExpandCollapseItem,
            this.bbiDefault,
            this.bbiRestore,
            this.bbiSave,
            this.bbiExpand});
            this.rcAdmin.Location = new System.Drawing.Point(0, 0);
            this.rcAdmin.MaxItemId = 7;
            this.rcAdmin.Name = "rcAdmin";
            this.rcAdmin.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.rpMain});
            this.rcAdmin.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            this.rcAdmin.ShowCategoryInCaption = false;
            this.rcAdmin.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            this.rcAdmin.ShowFullScreenButton = DevExpress.Utils.DefaultBoolean.False;
            this.rcAdmin.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide;
            this.rcAdmin.ShowToolbarCustomizeItem = false;
            this.rcAdmin.Size = new System.Drawing.Size(391, 96);
            this.rcAdmin.Toolbar.ShowCustomizeItem = false;
            this.rcAdmin.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
            // 
            // bbiDefault
            // 
            this.bbiDefault.Caption = "По умолчанию";
            this.bbiDefault.CategoryGuid = new System.Guid("6ffddb2b-9015-4d97-a4c1-91613e0ef537");
            this.bbiDefault.Enabled = false;
            this.bbiDefault.Hint = "Вернуть настройки элементов интерфеса для выбранной формы к изначальным";
            this.bbiDefault.Id = 3;
            this.bbiDefault.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("bbiDefault.LargeGlyph")));
            this.bbiDefault.Name = "bbiDefault";
            this.bbiDefault.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.DefaultItemClick);
            // 
            // bbiRestore
            // 
            this.bbiRestore.Caption = "Последние сохранённые";
            this.bbiRestore.CategoryGuid = new System.Guid("6ffddb2b-9015-4d97-a4c1-91613e0ef537");
            this.bbiRestore.Enabled = false;
            this.bbiRestore.Hint = "Вернуть настройки элементов интерфеса для выбранной формы к последним сохранённым" +
    "";
            this.bbiRestore.Id = 4;
            this.bbiRestore.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("bbiRestore.LargeGlyph")));
            this.bbiRestore.Name = "bbiRestore";
            this.bbiRestore.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.RestoreItemClick);
            // 
            // bbiSave
            // 
            this.bbiSave.Caption = "Сохранить";
            this.bbiSave.CategoryGuid = new System.Guid("6ffddb2b-9015-4d97-a4c1-91613e0ef537");
            this.bbiSave.Enabled = false;
            this.bbiSave.Glyph = ((System.Drawing.Image)(resources.GetObject("bbiSave.Glyph")));
            this.bbiSave.Hint = "Сохранить текущие настройки для выбранной формы и роли ";
            this.bbiSave.Id = 5;
            this.bbiSave.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("bbiSave.LargeGlyph")));
            this.bbiSave.Name = "bbiSave";
            this.bbiSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.SaveItemClick);
            // 
            // rpMain
            // 
            this.rpMain.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.rpgSettings});
            this.rpMain.Name = "rpMain";
            this.rpMain.Text = "Администрирование";
            // 
            // rpgSettings
            // 
            this.rpgSettings.Glyph = ((System.Drawing.Image)(resources.GetObject("rpgSettings.Glyph")));
            this.rpgSettings.ItemLinks.Add(this.bbiDefault);
            this.rpgSettings.ItemLinks.Add(this.bbiRestore);
            this.rpgSettings.ItemLinks.Add(this.bbiSave);
            this.rpgSettings.Name = "rpgSettings";
            this.rpgSettings.Text = "Настройки";
            // 
            // toolTip
            // 
            this.toolTip.Rounded = true;
            this.toolTip.ShowBeak = true;
            this.toolTip.ToolTipLocation = DevExpress.Utils.ToolTipLocation.LeftCenter;
            // 
            // pmControls
            // 
            this.pmControls.ItemLinks.Add(this.bbiExpand);
            this.pmControls.Name = "pmControls";
            this.pmControls.Ribbon = this.rcAdmin;
            // 
            // bbiExpand
            // 
            this.bbiExpand.Caption = "Развернуть всё";
            this.bbiExpand.Glyph = ((System.Drawing.Image)(resources.GetObject("bbiExpand.Glyph")));
            this.bbiExpand.Id = 6;
            this.bbiExpand.Name = "bbiExpand";
            this.bbiExpand.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ExpandButtonClick);
            // 
            // AdminTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 788);
            this.Controls.Add(this.lcAdmin);
            this.Controls.Add(this.rcAdmin);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AdminTool";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Настройка интерфейса";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AdminToolFormClosed);
            this.Load += new System.EventHandler(this.AdminToolLoad);
            this.Shown += new System.EventHandler(this.AdminToolShown);
            ((System.ComponentModel.ISupportInitialize)(this.tlControls)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueForms.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsForms)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcAdmin)).EndInit();
            this.lcAdmin.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lueRoles.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgAdmin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciForm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciTree)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciRole)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciProperty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcAdmin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pmControls)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FilterPropertyGrid pgFilter;
        private LookUpEdit lueForms;
        private LookUpEdit lueRoles;
        private CommonTreeList tlControls;
        private CommonTreeListColumn tlcType;
        private CommonTreeListColumn tlcText;
        private ToolTipController toolTip;
        private RibbonControl rcAdmin;
        private BarButtonItem bbiDefault;
        private BarButtonItem bbiRestore;
        private BarButtonItem bbiSave;
        private RibbonPage rpMain;
        private RibbonPageGroup rpgSettings;
        private LayoutControl lcAdmin;
        private LayoutControlGroup lcgAdmin;
        private LayoutControlItem lciForm;
        private LayoutControlItem lciTree;
        private LayoutControlItem lciRole;
        private LayoutControlItem lciProperty;
        private BindingSource bsForms;
        private SplitterItem splitterItem1;
        private BarButtonItem bbiExpand;
        private PopupMenu pmControls;
    }
}