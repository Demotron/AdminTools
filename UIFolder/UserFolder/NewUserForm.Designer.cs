using System.ComponentModel;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;

namespace CommonLibrary.UserFolder
{
    partial class NewUserForm
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
            this.lblLastName = new DevExpress.XtraEditors.LabelControl();
            this.txtLastName = new DevExpress.XtraEditors.TextEdit();
            this.txtMiddleName = new DevExpress.XtraEditors.TextEdit();
            this.lblMiddleName = new DevExpress.XtraEditors.LabelControl();
            this.txtFirstName = new DevExpress.XtraEditors.TextEdit();
            this.lblFirstName = new DevExpress.XtraEditors.LabelControl();
            this.btnCreate = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.lblRole = new DevExpress.XtraEditors.LabelControl();
            this.errorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.ccbRoles = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLastName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMiddleName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFirstName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccbRoles.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblLastName
            // 
            this.lblLastName.Location = new System.Drawing.Point(56, 15);
            this.lblLastName.Name = "lblLastName";
            this.lblLastName.Size = new System.Drawing.Size(48, 13);
            this.lblLastName.TabIndex = 0;
            this.lblLastName.Text = "Фамилия:";
            // 
            // txtLastName
            // 
            this.txtLastName.Location = new System.Drawing.Point(110, 12);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Properties.MaxLength = 50;
            this.txtLastName.Properties.ValidateOnEnterKey = true;
            this.txtLastName.Size = new System.Drawing.Size(211, 20);
            this.txtLastName.TabIndex = 1;
            this.txtLastName.Validated += new System.EventHandler(this.TextEditValidated);
            // 
            // txtMiddleName
            // 
            this.txtMiddleName.Location = new System.Drawing.Point(110, 92);
            this.txtMiddleName.Name = "txtMiddleName";
            this.txtMiddleName.Properties.MaxLength = 50;
            this.txtMiddleName.Properties.ValidateOnEnterKey = true;
            this.txtMiddleName.Size = new System.Drawing.Size(211, 20);
            this.txtMiddleName.TabIndex = 3;
            this.txtMiddleName.Validated += new System.EventHandler(this.TextEditValidated);
            // 
            // lblMiddleName
            // 
            this.lblMiddleName.Location = new System.Drawing.Point(51, 95);
            this.lblMiddleName.Name = "lblMiddleName";
            this.lblMiddleName.Size = new System.Drawing.Size(53, 13);
            this.lblMiddleName.TabIndex = 2;
            this.lblMiddleName.Text = "Отчество:";
            // 
            // txtFirstName
            // 
            this.txtFirstName.Location = new System.Drawing.Point(110, 52);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Properties.MaxLength = 50;
            this.txtFirstName.Properties.ValidateOnEnterKey = true;
            this.txtFirstName.Size = new System.Drawing.Size(211, 20);
            this.txtFirstName.TabIndex = 2;
            this.txtFirstName.Validated += new System.EventHandler(this.TextEditValidated);
            // 
            // lblFirstName
            // 
            this.lblFirstName.Location = new System.Drawing.Point(81, 55);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.Size = new System.Drawing.Size(23, 13);
            this.lblFirstName.TabIndex = 4;
            this.lblFirstName.Text = "Имя:";
            // 
            // btnCreate
            // 
            this.btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCreate.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnCreate.Location = new System.Drawing.Point(12, 172);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(125, 35);
            this.btnCreate.TabIndex = 5;
            this.btnCreate.Text = "Создать";
            this.btnCreate.Click += new System.EventHandler(this.CreateClick);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.CausesValidation = false;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(194, 172);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(125, 35);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Отмена";
            // 
            // lblRole
            // 
            this.lblRole.Location = new System.Drawing.Point(11, 135);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(93, 13);
            this.lblRole.TabIndex = 11;
            this.lblRole.Text = "Роли в программе:";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // ccbRoles
            // 
            this.ccbRoles.Location = new System.Drawing.Point(110, 132);
            this.ccbRoles.Name = "ccbRoles";
            this.ccbRoles.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ccbRoles.Properties.DisplayMember = "Name";
            this.ccbRoles.Properties.NullValuePrompt = "Выберите необходимые роли";
            this.ccbRoles.Properties.ValueMember = "Id";
            this.ccbRoles.Size = new System.Drawing.Size(211, 20);
            this.ccbRoles.TabIndex = 4;
            this.ccbRoles.Validated += new System.EventHandler(this.RolesValidated);
            // 
            // NewUserForm
            // 
            this.AcceptButton = this.btnCreate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(331, 219);
            this.ControlBox = false;
            this.Controls.Add(this.ccbRoles);
            this.Controls.Add(this.lblRole);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.txtFirstName);
            this.Controls.Add(this.lblFirstName);
            this.Controls.Add(this.txtMiddleName);
            this.Controls.Add(this.lblMiddleName);
            this.Controls.Add(this.txtLastName);
            this.Controls.Add(this.lblLastName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "NewUserForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Создание нового пользователя";
            this.Shown += new System.EventHandler(this.NewUserFormShown);
            ((System.ComponentModel.ISupportInitialize)(this.txtLastName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMiddleName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFirstName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccbRoles.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LabelControl lblLastName;
        private TextEdit txtLastName;
        private TextEdit txtMiddleName;
        private LabelControl lblMiddleName;
        private TextEdit txtFirstName;
        private LabelControl lblFirstName;
        private SimpleButton btnCreate;
        private SimpleButton btnCancel;
        private LabelControl lblRole;
        private DXErrorProvider errorProvider;
        private CheckedComboBoxEdit ccbRoles;
    }
}