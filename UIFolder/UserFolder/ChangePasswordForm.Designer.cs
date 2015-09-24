using System.ComponentModel;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraLayout;

namespace CommonLibrary.UserFolder
{
    partial class ChangePasswordForm
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
            this.txtNewPassAgain = new DevExpress.XtraEditors.TextEdit();
            this.lcForm = new DevExpress.XtraLayout.LayoutControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.txtOldPass = new DevExpress.XtraEditors.TextEdit();
            this.txtNewPass = new DevExpress.XtraEditors.TextEdit();
            this.lcgPassword = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lciNewPasswordAgain = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciNewPassword = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciOldPassword = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciCancel = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.errorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.txtNewPassAgain.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcForm)).BeginInit();
            this.lcForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtOldPass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewPass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNewPasswordAgain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNewPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciOldPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // txtNewPassAgain
            // 
            this.txtNewPassAgain.Location = new System.Drawing.Point(102, 61);
            this.txtNewPassAgain.Name = "txtNewPassAgain";
            this.txtNewPassAgain.Properties.MaxLength = 15;
            this.txtNewPassAgain.Properties.PasswordChar = '*';
            this.txtNewPassAgain.Size = new System.Drawing.Size(170, 20);
            this.txtNewPassAgain.StyleController = this.lcForm;
            this.txtNewPassAgain.TabIndex = 2;
            // 
            // lcForm
            // 
            this.lcForm.Controls.Add(this.btnCancel);
            this.lcForm.Controls.Add(this.btnOk);
            this.lcForm.Controls.Add(this.txtOldPass);
            this.lcForm.Controls.Add(this.txtNewPassAgain);
            this.lcForm.Controls.Add(this.txtNewPass);
            this.lcForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lcForm.Location = new System.Drawing.Point(0, 0);
            this.lcForm.Name = "lcForm";
            this.lcForm.Root = this.lcgPassword;
            this.lcForm.Size = new System.Drawing.Size(284, 132);
            this.lcForm.TabIndex = 420;
            this.lcForm.Text = "layoutControl1";
            // 
            // btnCancel
            // 
            this.btnCancel.AutoWidthInLayoutControl = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(172, 95);
            this.btnCancel.MinimumSize = new System.Drawing.Size(100, 30);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.StyleController = this.lcForm;
            this.btnCancel.TabIndex = 421;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.Click += new System.EventHandler(this.CancelClick);
            // 
            // btnOk
            // 
            this.btnOk.AutoWidthInLayoutControl = true;
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(12, 95);
            this.btnOk.MinimumSize = new System.Drawing.Size(100, 30);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(100, 30);
            this.btnOk.StyleController = this.lcForm;
            this.btnOk.TabIndex = 420;
            this.btnOk.Text = "Вход";
            this.btnOk.Click += new System.EventHandler(this.OkClick);
            // 
            // txtOldPass
            // 
            this.txtOldPass.Location = new System.Drawing.Point(102, 9);
            this.txtOldPass.Name = "txtOldPass";
            this.txtOldPass.Properties.MaxLength = 15;
            this.txtOldPass.Properties.PasswordChar = '*';
            this.txtOldPass.Size = new System.Drawing.Size(170, 20);
            this.txtOldPass.StyleController = this.lcForm;
            this.txtOldPass.TabIndex = 0;
            // 
            // txtNewPass
            // 
            this.txtNewPass.Location = new System.Drawing.Point(102, 35);
            this.txtNewPass.Name = "txtNewPass";
            this.txtNewPass.Properties.MaxLength = 15;
            this.txtNewPass.Properties.PasswordChar = '*';
            this.txtNewPass.Size = new System.Drawing.Size(170, 20);
            this.txtNewPass.StyleController = this.lcForm;
            this.txtNewPass.TabIndex = 1;
            // 
            // lcgPassword
            // 
            this.lcgPassword.CustomizationFormText = "lcgPassword";
            this.lcgPassword.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcgPassword.GroupBordersVisible = false;
            this.lcgPassword.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciNewPasswordAgain,
            this.lciNewPassword,
            this.lciOldPassword,
            this.layoutControlItem1,
            this.lciCancel,
            this.emptySpaceItem2});
            this.lcgPassword.Location = new System.Drawing.Point(0, 0);
            this.lcgPassword.Name = "lcgPassword";
            this.lcgPassword.Padding = new DevExpress.XtraLayout.Utils.Padding(10, 10, 5, 5);
            this.lcgPassword.Size = new System.Drawing.Size(284, 132);
            this.lcgPassword.Text = "lcgPassword";
            this.lcgPassword.TextVisible = false;
            // 
            // lciNewPasswordAgain
            // 
            this.lciNewPasswordAgain.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lciNewPasswordAgain.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lciNewPasswordAgain.Control = this.txtNewPassAgain;
            this.lciNewPasswordAgain.CustomizationFormText = "Повтор пароля";
            this.lciNewPasswordAgain.Location = new System.Drawing.Point(0, 52);
            this.lciNewPasswordAgain.Name = "lciNewPasswordAgain";
            this.lciNewPasswordAgain.Size = new System.Drawing.Size(264, 26);
            this.lciNewPasswordAgain.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 2, 0);
            this.lciNewPasswordAgain.Text = "Повтор пароля";
            this.lciNewPasswordAgain.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lciNewPasswordAgain.TextSize = new System.Drawing.Size(80, 13);
            this.lciNewPasswordAgain.TextToControlDistance = 10;
            // 
            // lciNewPassword
            // 
            this.lciNewPassword.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lciNewPassword.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lciNewPassword.Control = this.txtNewPass;
            this.lciNewPassword.CustomizationFormText = "Новый пароль";
            this.lciNewPassword.Location = new System.Drawing.Point(0, 26);
            this.lciNewPassword.Name = "lciNewPassword";
            this.lciNewPassword.Size = new System.Drawing.Size(264, 26);
            this.lciNewPassword.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 2, 0);
            this.lciNewPassword.Text = "Новый пароль";
            this.lciNewPassword.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lciNewPassword.TextSize = new System.Drawing.Size(80, 13);
            this.lciNewPassword.TextToControlDistance = 10;
            // 
            // lciOldPassword
            // 
            this.lciOldPassword.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lciOldPassword.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lciOldPassword.Control = this.txtOldPass;
            this.lciOldPassword.CustomizationFormText = "Старый пароль";
            this.lciOldPassword.Location = new System.Drawing.Point(0, 0);
            this.lciOldPassword.Name = "lciOldPassword";
            this.lciOldPassword.Size = new System.Drawing.Size(264, 26);
            this.lciOldPassword.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 2, 0);
            this.lciOldPassword.Text = "Старый пароль";
            this.lciOldPassword.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lciOldPassword.TextSize = new System.Drawing.Size(80, 13);
            this.lciOldPassword.TextToControlDistance = 10;
            this.lciOldPassword.Hidden += new System.EventHandler(this.OldPasswordHidden);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btnOk;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 78);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(104, 44);
            this.layoutControlItem1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 10, 0);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // lciCancel
            // 
            this.lciCancel.Control = this.btnCancel;
            this.lciCancel.CustomizationFormText = "lciCancel";
            this.lciCancel.Location = new System.Drawing.Point(160, 78);
            this.lciCancel.Name = "lciCancel";
            this.lciCancel.Size = new System.Drawing.Size(104, 44);
            this.lciCancel.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 10, 0);
            this.lciCancel.Text = "lciCancel";
            this.lciCancel.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lciCancel.TextSize = new System.Drawing.Size(0, 0);
            this.lciCancel.TextToControlDistance = 0;
            this.lciCancel.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(104, 78);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(56, 44);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // ChangePasswordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 132);
            this.ControlBox = false;
            this.Controls.Add(this.lcForm);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ChangePasswordForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Изменение пароля";
            this.Shown += new System.EventHandler(this.ChangePasswordFormShown);
            ((System.ComponentModel.ISupportInitialize)(this.txtNewPassAgain.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcForm)).EndInit();
            this.lcForm.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtOldPass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewPass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNewPasswordAgain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNewPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciOldPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TextEdit txtNewPassAgain;
        private TextEdit txtNewPass;
        private TextEdit txtOldPass;
        private DXErrorProvider errorProvider;
        private LayoutControl lcForm;
        private LayoutControlGroup lcgPassword;
        private LayoutControlItem lciNewPasswordAgain;
        private LayoutControlItem lciNewPassword;
        private LayoutControlItem lciOldPassword;
        private SimpleButton btnCancel;
        private SimpleButton btnOk;
        private LayoutControlItem layoutControlItem1;
        private LayoutControlItem lciCancel;
        private EmptySpaceItem emptySpaceItem2;
    }
}