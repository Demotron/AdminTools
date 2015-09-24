using System.ComponentModel;
using System.Windows.Forms;
using CommonLibrary.GridControlFolder;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;

namespace CommonLibrary.AdminFolder
{
    partial class RolesChangeRibbonForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RolesChangeRibbonForm));
            this.sccRoles = new DevExpress.XtraEditors.SplitContainerControl();
            this.cgcRole = new CommonLibrary.GridControlFolder.CommonGridControl();
            this.bsRole = new System.Windows.Forms.BindingSource();
            this.cgvRole = new CommonLibrary.GridControlFolder.CommonGridView();
            this.colRoleName = new CommonLibrary.GridControlFolder.CommonGridColumn();
            this.cgcUsers = new CommonLibrary.GridControlFolder.CommonGridControl();
            this.bsRoleUsers = new System.Windows.Forms.BindingSource();
            this.cgvUsers = new CommonLibrary.GridControlFolder.CommonGridView();
            this.colSurnameInitials = new CommonLibrary.GridControlFolder.CommonGridColumn();
            this.riccbeUsers = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
            this.toolTip = new DevExpress.Utils.ToolTipController();
            this.sccUsers = new DevExpress.XtraEditors.SplitContainerControl();
            this.cgcAllUsers = new CommonLibrary.GridControlFolder.CommonGridControl();
            this.bsUser = new System.Windows.Forms.BindingSource();
            this.cgvAllUsers = new CommonLibrary.GridControlFolder.CommonGridView();
            this.colUserId = new CommonLibrary.GridControlFolder.CommonGridColumn();
            this.colLastName = new CommonLibrary.GridControlFolder.CommonGridColumn();
            this.colFirstName = new CommonLibrary.GridControlFolder.CommonGridColumn();
            this.colMiddleName = new CommonLibrary.GridControlFolder.CommonGridColumn();
            this.colIsLocked = new CommonLibrary.GridControlFolder.CommonGridColumn();
            this.pmUsers = new DevExpress.XtraBars.PopupMenu();
            this.bbiChangePassword = new DevExpress.XtraBars.BarButtonItem();
            this.bbiRemovePassword = new DevExpress.XtraBars.BarButtonItem();
            this.bbiLockUser = new DevExpress.XtraBars.BarButtonItem();
            this.bbiUnlock = new DevExpress.XtraBars.BarButtonItem();
            this.bm = new DevExpress.XtraBars.BarManager();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.sccRoles)).BeginInit();
            this.sccRoles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cgcRole)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsRole)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cgvRole)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cgcUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsRoleUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cgvUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riccbeUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sccUsers)).BeginInit();
            this.sccUsers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cgcAllUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cgvAllUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pmUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bm)).BeginInit();
            this.SuspendLayout();
            // 
            // sccRoles
            // 
            this.sccRoles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sccRoles.Horizontal = false;
            this.sccRoles.Location = new System.Drawing.Point(0, 0);
            this.sccRoles.Name = "sccRoles";
            this.sccRoles.Panel1.Controls.Add(this.cgcRole);
            this.sccRoles.Panel1.Text = "Panel1";
            this.sccRoles.Panel2.Controls.Add(this.cgcUsers);
            this.sccRoles.Panel2.Text = "Panel2";
            this.sccRoles.Size = new System.Drawing.Size(404, 681);
            this.sccRoles.SplitterPosition = 276;
            this.sccRoles.TabIndex = 2;
            this.sccRoles.Text = "sccRoles";
            // 
            // cgcRole
            // 
            this.cgcRole.Cursor = System.Windows.Forms.Cursors.Default;
            this.cgcRole.DataSource = this.bsRole;
            this.cgcRole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cgcRole.Location = new System.Drawing.Point(0, 0);
            this.cgcRole.MainView = this.cgvRole;
            this.cgcRole.Name = "cgcRole";
            this.cgcRole.Size = new System.Drawing.Size(404, 276);
            this.cgcRole.TabIndex = 3;
            this.cgcRole.UseEmbeddedNavigator = false;
            this.cgcRole.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.cgvRole});
            // 
            // bsRole
            // 
            this.bsRole.DataSource = typeof(ServerInformation.Role);
            this.bsRole.CurrentChanged += new System.EventHandler(this.RoleCurrentChanged);
            // 
            // cgvRole
            // 
            this.cgvRole.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colRoleName});
            this.cgvRole.CustomPopupMenu = null;
            this.cgvRole.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.cgvRole.GridControl = this.cgcRole;
            this.cgvRole.Name = "cgvRole";
            this.cgvRole.NewItemRowText = "Кликните здесь для добавления новой роли";
            this.cgvRole.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused;
            this.cgvRole.OptionsCustomization.AllowQuickCustomisation = false;
            this.cgvRole.OptionsView.ShowDetailButtons = false;
            this.cgvRole.OptionsView.ShowGroupPanel = false;
            this.cgvRole.OptionsView.ShowViewCaption = true;
            this.cgvRole.ViewCaption = "Список ролей в системе";
            this.cgvRole.DoBeforeDeleteRow += new CommonLibrary.GridControlFolder.CommonGridView.EventBeforeDelete(this.RoleBeforeDelete);
            this.cgvRole.DoCheckBeforeDrawIndicator += new System.EventHandler<CommonLibrary.UIFolder.GridControlFolder.EventRowHandlerArgs>(this.RoleDoCheckBeforeDrawIndicator);
            // 
            // colRoleName
            // 
            this.colRoleName.Caption = "Название роли";
            this.colRoleName.FieldName = "Name";
            this.colRoleName.Name = "colRoleName";
            this.colRoleName.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.colRoleName.Visible = true;
            this.colRoleName.VisibleIndex = 0;
            this.colRoleName.Width = 256;
            // 
            // cgcUsers
            // 
            this.cgcUsers.AllowDrop = true;
            this.cgcUsers.Cursor = System.Windows.Forms.Cursors.Default;
            this.cgcUsers.DataSource = this.bsRoleUsers;
            this.cgcUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cgcUsers.Location = new System.Drawing.Point(0, 0);
            this.cgcUsers.MainView = this.cgvUsers;
            this.cgcUsers.Name = "cgcUsers";
            this.cgcUsers.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riccbeUsers});
            this.cgcUsers.Size = new System.Drawing.Size(404, 400);
            this.cgcUsers.TabIndex = 2;
            this.cgcUsers.UseEmbeddedNavigator = false;
            this.cgcUsers.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.cgvUsers});
            // 
            // bsRoleUsers
            // 
            this.bsRoleUsers.AllowNew = false;
            this.bsRoleUsers.DataSource = typeof(ServerInformation.User);
            // 
            // cgvUsers
            // 
            this.cgvUsers.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSurnameInitials});
            this.cgvUsers.CustomPopupMenu = null;
            this.cgvUsers.FixedLineWidth = 1;
            this.cgvUsers.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.cgvUsers.GridControl = this.cgcUsers;
            this.cgvUsers.Name = "cgvUsers";
            this.cgvUsers.NewItemRowText = "Кликните здесь для добавления пользователей к роли";
            this.cgvUsers.OptionsCustomization.AllowAdd = false;
            this.cgvUsers.OptionsCustomization.AllowQuickCustomisation = false;
            this.cgvUsers.OptionsCustomization.AllowSelectionColumn = true;
            this.cgvUsers.OptionsSelection.CheckBoxSelectorColumnWidth = 25;
            this.cgvUsers.OptionsSelection.MultiSelect = true;
            this.cgvUsers.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.cgvUsers.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.cgvUsers.OptionsView.ShowGroupPanel = false;
            this.cgvUsers.OptionsView.ShowViewCaption = true;
            this.cgvUsers.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colSurnameInitials, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.cgvUsers.ViewCaption = "Сотрудники, относящиеся к выбранной роли";
            this.cgvUsers.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.UsersCustomRowCellEdit);
            this.cgvUsers.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.UsersShowingEditor);
            // 
            // colSurnameInitials
            // 
            this.colSurnameInitials.Caption = "Сотрудник";
            this.colSurnameInitials.FieldName = "SurnameInitials";
            this.colSurnameInitials.Name = "colSurnameInitials";
            this.colSurnameInitials.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.colSurnameInitials.Visible = true;
            this.colSurnameInitials.VisibleIndex = 1;
            this.colSurnameInitials.Width = 193;
            // 
            // riccbeUsers
            // 
            this.riccbeUsers.AutoHeight = false;
            this.riccbeUsers.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riccbeUsers.DisplayMember = "SurnameInitials";
            this.riccbeUsers.Name = "riccbeUsers";
            this.riccbeUsers.SelectAllItemCaption = "Выбрать всё";
            this.riccbeUsers.ValueMember = "Id";
            this.riccbeUsers.EditValueChanged += new System.EventHandler(this.UsersEditValueChanged);
            // 
            // toolTip
            // 
            this.toolTip.Rounded = true;
            this.toolTip.ShowBeak = true;
            // 
            // sccUsers
            // 
            this.sccUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sccUsers.Location = new System.Drawing.Point(0, 0);
            this.sccUsers.Name = "sccUsers";
            this.sccUsers.Panel1.Controls.Add(this.sccRoles);
            this.sccUsers.Panel1.Text = "Panel1";
            this.sccUsers.Panel2.Controls.Add(this.cgcAllUsers);
            this.sccUsers.Panel2.Text = "Panel2";
            this.sccUsers.Size = new System.Drawing.Size(1086, 681);
            this.sccUsers.SplitterPosition = 404;
            this.sccUsers.TabIndex = 3;
            this.sccUsers.Text = "sccUsers";
            // 
            // cgcAllUsers
            // 
            this.cgcAllUsers.AllowDrop = true;
            this.cgcAllUsers.Cursor = System.Windows.Forms.Cursors.Default;
            this.cgcAllUsers.DataSource = this.bsUser;
            this.cgcAllUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cgcAllUsers.Location = new System.Drawing.Point(0, 0);
            this.cgcAllUsers.MainView = this.cgvAllUsers;
            this.cgcAllUsers.Name = "cgcAllUsers";
            this.cgcAllUsers.Size = new System.Drawing.Size(677, 681);
            this.cgcAllUsers.TabIndex = 5;
            this.cgcAllUsers.UseEmbeddedNavigator = false;
            this.cgcAllUsers.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.cgvAllUsers});
            // 
            // bsUser
            // 
            this.bsUser.DataSource = typeof(ServerInformation.User);
            // 
            // cgvAllUsers
            // 
            this.cgvAllUsers.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colUserId,
            this.colLastName,
            this.colFirstName,
            this.colMiddleName,
            this.colIsLocked});
            this.cgvAllUsers.CustomPopupMenu = null;
            this.cgvAllUsers.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.cgvAllUsers.GridControl = this.cgcAllUsers;
            this.cgvAllUsers.Name = "cgvAllUsers";
            this.cgvAllUsers.NewItemRowText = "Кликните здесь для добавления нового сотрудника";
            this.cgvAllUsers.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused;
            this.cgvAllUsers.OptionsCustomization.AllowDelete = false;
            this.cgvAllUsers.OptionsCustomization.AllowQuickCustomisation = false;
            this.cgvAllUsers.OptionsDetail.EnableMasterViewMode = false;
            this.cgvAllUsers.OptionsView.ShowGroupPanel = false;
            this.cgvAllUsers.OptionsView.ShowViewCaption = true;
            this.cgvAllUsers.ViewCaption = "Список всех пользователей в системе";
            this.cgvAllUsers.DoCheckBeforeDrawIndicator += new System.EventHandler<CommonLibrary.UIFolder.GridControlFolder.EventRowHandlerArgs>(this.CheckBeforeDrawImage);
            this.cgvAllUsers.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.UserPopupMenuShowing);
            this.cgvAllUsers.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.AllUsersCellValueChanging);
            this.cgvAllUsers.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.AllUsersValidateRow);
            // 
            // colUserId
            // 
            this.colUserId.FieldName = "Id";
            this.colUserId.Name = "colUserId";
            this.colUserId.OptionsColumn.ReadOnly = true;
            // 
            // colLastName
            // 
            this.colLastName.Caption = "Фамилия";
            this.colLastName.FieldName = "LastName";
            this.colLastName.Name = "colLastName";
            this.colLastName.Visible = true;
            this.colLastName.VisibleIndex = 0;
            // 
            // colFirstName
            // 
            this.colFirstName.Caption = "Имя";
            this.colFirstName.FieldName = "FirstName";
            this.colFirstName.Name = "colFirstName";
            this.colFirstName.Visible = true;
            this.colFirstName.VisibleIndex = 1;
            // 
            // colMiddleName
            // 
            this.colMiddleName.Caption = "Отчество";
            this.colMiddleName.FieldName = "MiddleName";
            this.colMiddleName.Name = "colMiddleName";
            this.colMiddleName.Visible = true;
            this.colMiddleName.VisibleIndex = 2;
            // 
            // colIsLocked
            // 
            this.colIsLocked.Caption = "Заблокирован";
            this.colIsLocked.FieldName = "IsLocked";
            this.colIsLocked.Name = "colIsLocked";
            this.colIsLocked.Visible = true;
            this.colIsLocked.VisibleIndex = 3;
            // 
            // pmUsers
            // 
            this.pmUsers.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiChangePassword),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiRemovePassword),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiLockUser),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiUnlock)});
            this.pmUsers.Manager = this.bm;
            this.pmUsers.Name = "pmUsers";
            this.pmUsers.BeforePopup += new System.ComponentModel.CancelEventHandler(this.UsersBeforePopup);
            // 
            // bbiChangePassword
            // 
            this.bbiChangePassword.Caption = "Изменить пароль";
            this.bbiChangePassword.Glyph = ((System.Drawing.Image)(resources.GetObject("bbiChangePassword.Glyph")));
            this.bbiChangePassword.Id = 4;
            this.bbiChangePassword.Name = "bbiChangePassword";
            this.bbiChangePassword.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.NewPasswordUserClick);
            // 
            // bbiRemovePassword
            // 
            this.bbiRemovePassword.Caption = "Удалить пароль";
            this.bbiRemovePassword.Glyph = ((System.Drawing.Image)(resources.GetObject("bbiRemovePassword.Glyph")));
            this.bbiRemovePassword.Id = 3;
            this.bbiRemovePassword.Name = "bbiRemovePassword";
            this.bbiRemovePassword.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.RemovePasswordUserClick);
            // 
            // bbiLockUser
            // 
            this.bbiLockUser.Caption = "Заблокировать сотрудника";
            this.bbiLockUser.Glyph = ((System.Drawing.Image)(resources.GetObject("bbiLockUser.Glyph")));
            this.bbiLockUser.Id = 5;
            this.bbiLockUser.Name = "bbiLockUser";
            this.bbiLockUser.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.LockUserClick);
            // 
            // bbiUnlock
            // 
            this.bbiUnlock.Caption = "Разблокировать сотрудника";
            this.bbiUnlock.Glyph = ((System.Drawing.Image)(resources.GetObject("bbiUnlock.Glyph")));
            this.bbiUnlock.Id = 6;
            this.bbiUnlock.Name = "bbiUnlock";
            this.bbiUnlock.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.UnlockUserClick);
            // 
            // bm
            // 
            this.bm.DockControls.Add(this.barDockControlTop);
            this.bm.DockControls.Add(this.barDockControlBottom);
            this.bm.DockControls.Add(this.barDockControlLeft);
            this.bm.DockControls.Add(this.barDockControlRight);
            this.bm.Form = this;
            this.bm.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bbiRemovePassword,
            this.bbiChangePassword,
            this.bbiLockUser,
            this.bbiUnlock});
            this.bm.MaxItemId = 9;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1086, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 681);
            this.barDockControlBottom.Size = new System.Drawing.Size(1086, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 681);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1086, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 681);
            // 
            // RolesChangeRibbonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1086, 681);
            this.Controls.Add(this.sccUsers);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "RolesChangeRibbonForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройка ролей и сотрудников";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.RolesChangeRibbonFormLoad);
            this.Shown += new System.EventHandler(this.RolesChangeRibbonFormShown);
            ((System.ComponentModel.ISupportInitialize)(this.sccRoles)).EndInit();
            this.sccRoles.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cgcRole)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsRole)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cgvRole)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cgcUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsRoleUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cgvUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riccbeUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sccUsers)).EndInit();
            this.sccUsers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cgcAllUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cgvAllUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pmUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bm)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private SplitContainerControl sccRoles;
        private CommonGridControl cgcUsers;
        private CommonGridView cgvUsers;
        private CommonGridControl cgcRole;
        private CommonGridView cgvRole;
        private CommonGridColumn colRoleName;
        private CommonGridColumn colSurnameInitials;
        private ToolTipController toolTip;
        private SplitContainerControl sccUsers;
        private CommonGridControl cgcAllUsers;
        private CommonGridView cgvAllUsers;
        private CommonGridColumn colUserId;
        private CommonGridColumn colLastName;
        private CommonGridColumn colFirstName;
        private CommonGridColumn colMiddleName;
        private RepositoryItemCheckedComboBoxEdit riccbeUsers;
        private BindingSource bsUser;
        private BindingSource bsRoleUsers;
        private BindingSource bsRole;
        private PopupMenu pmUsers;
        private BarManager bm;
        private BarDockControl barDockControlTop;
        private BarDockControl barDockControlBottom;
        private BarDockControl barDockControlLeft;
        private BarDockControl barDockControlRight;
        private BarButtonItem bbiChangePassword;
        private BarButtonItem bbiRemovePassword;
        private BarButtonItem bbiLockUser;
        private BarButtonItem bbiUnlock;
        private CommonGridColumn colIsLocked;
    }
}

