using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout.Utils;

namespace CommonLibrary.UserFolder
{
    public partial class ChangePasswordForm : XtraForm
    {
        private bool isAdmin;
        private string oldPass;

        public ChangePasswordForm()
        {
            InitializeComponent();
        }

        private void ChangePasswordFormShown(object sender, EventArgs e)
        {
            if (lciOldPassword.Visibility == LayoutVisibility.Never)
            {
                txtNewPass.Focus();
            }
            if (!isAdmin)
            {
                return;
            }
            btnOk.Text = "Изменить";
            lciOldPassword.Visibility = LayoutVisibility.Never;
        }

        public string NewPassword()
        {
            lciCancel.Visibility = LayoutVisibility.Never;
            lciOldPassword.Visibility = LayoutVisibility.Never;
            return ShowDialog() == DialogResult.OK ? Security.CreateHash(txtNewPass.Text) : null;
        }

        public string ChangePassword(string pass, bool isadmin = false)
        {
            isAdmin = isadmin;
            oldPass = pass;
            return ShowDialog() == DialogResult.OK ? Security.CreateHash(txtNewPass.Text) : null;
        }

        private void OkClick(object sender, EventArgs e)
        {
            errorProvider.ClearErrors();
            if (lciOldPassword.Visibility != LayoutVisibility.Never && !isAdmin && !Security.MatchHash(oldPass, txtOldPass.Text))
            {
                errorProvider.SetError(txtOldPass, "Вы неправильно ввели старый пароль");
                DialogResult = DialogResult.None;
            }
            if (txtNewPass.Text.Length < 6)
            {
                errorProvider.SetError(txtNewPass, "Пароль должен быть длиннее 5 символов.");
                DialogResult = DialogResult.None;
            }
            else if (!txtNewPass.Text.Equals(txtNewPassAgain.Text))
            {
                errorProvider.SetError(txtNewPassAgain, "Новые введенные пароли не совпадают.");
                DialogResult = DialogResult.None;
            }
            else
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void CancelClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void OldPasswordHidden(object sender, EventArgs e)
        {
            Size = new Size(Size.Width, Size.Height - 20);
        }
    }
}