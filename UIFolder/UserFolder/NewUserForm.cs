using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ServerInformation;

namespace CommonLibrary.UserFolder
{
    /// <summary>
    ///     Форма создания нового пользователя
    /// </summary>
    public partial class NewUserForm : XtraForm
    {
        public NewUserForm()
        {
            InitializeComponent();
        }

        public User NewUser { get; private set; }

        private void NewUserFormShown(object sender, EventArgs e)
        {
            using (var db = new ApplicationEntitie(0))
            {
                ccbRoles.Properties.DataSource = db.Roles.ToList();
            }
        }

        private void CreateClick(object sender, EventArgs e)
        {
            errorProvider.ClearErrors();
            if (!ValidateChildren() || errorProvider.HasErrors)
            {
                DialogResult = DialogResult.None;
                return;
            }
            var user = new User
            {
                LastName = ParseToDefaultForm(txtLastName.Text),
                FirstName = ParseToDefaultForm(txtFirstName.Text),
                MiddleName = ParseToDefaultForm(txtMiddleName.Text)
            };
            var newPass = new ChangePasswordForm().NewPassword();
            if (newPass == null)
            {
                DialogResult = DialogResult.None;
                return;
            }
            user.UserPassword = Security.CreateHash(newPass);
            using (var dbUser = new ApplicationEntitie(0))
            {
                var roles = ccbRoles.Properties.Items.Cast<CheckedListBoxItem>()
                    .
                    Where(item => item.CheckState == CheckState.Checked)
                    .
                    Select(r => (int) r.Value);
                foreach (var role in dbUser.Roles.Where(rr => roles.Contains(rr.Id)))
                {
                    dbUser.Roles.Attach(role);
                    user.Roles.Add(role);
                }
                dbUser.Users.Add(user);
                dbUser.SaveChanges();
                NewUser = user;
            }
        }

        /// <summary>
        ///     Преобразовать строку в формат, где первая буква большая остальные маленькие
        /// </summary>
        /// <param name="item">строка для преобразования</param>
        /// <returns>строка на выходе</returns>
        private static string ParseToDefaultForm(string item)
        {
            return item.Substring(0, 1)
                .
                ToUpper() + item.Substring(1)
                    .
                    ToLower();
        }

        private void TextEditValidated(object sender, EventArgs e)
        {
            var textEdit = (TextEdit) sender;
            errorProvider.SetError(textEdit, !Regex.IsMatch(textEdit.Text, @"^[a-zA-Zа-яёА-ЯЁ]+$")
                ? "Имя, Фамилия и Отчество могут содержать только буквы"
                : string.Empty);
        }

        private void RolesValidated(object sender, EventArgs e)
        {
            errorProvider.SetError(ccbRoles, string.IsNullOrEmpty(ccbRoles.Properties.GetCheckedItems()
                .
                ToString())
                ? "Необходимо указать роли, к которым относится пользователь"
                : string.Empty);
        }
    }
}