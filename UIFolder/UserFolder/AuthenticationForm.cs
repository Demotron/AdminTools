using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommonLibrary.Properties;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ServerInformation;

namespace CommonLibrary.UserFolder
{
    /// <summary>
    ///     Форма для входа пользователя в систему, т.е. ввода логина и пароля
    /// </summary>
    public partial class AuthenticationForm : XtraForm
    {
        private User user;
        private static bool changeUser;

        public static void BeginChangeUser()
        {
            changeUser = true;
        }

        public static void EndChangeUser()
        {
            changeUser = false;
        }

        public  AuthenticationForm()
        {
            InitializeComponent();

            //            lueUser.Properties.Buttons[1].Visible = selfCreation;
        }

        private void AuthenticationFormLoad(object sender, EventArgs e)
        {
            bsUser.DataSource = DBUser.Users_SelectNotLocked();
        }

        private void AuthenticationFormShown(object sender, EventArgs e)
        {
            Application.DoEvents();
            if (Settings.Default.LastLogonUser.Equals(string.Empty) || changeUser)
            {
                return;
            }
            lueUser.EditValue = Int32.Parse(Settings.Default.LastLogonUser);

#if DEBUG
            txtPassword.Text = Security.AdminPassword;
            btnOk.PerformClick();
#else
            txtPassword.Focus();
#endif
            Activate();
            BringToFront();
        }

        /// <summary>
        /// Получить выбранного пользователя
        /// </summary>
        public User GetUser()
        {
            return user;
        }

        private void BtnOkClick(object sender, EventArgs e)
        {
            UserLogin((int?)lueUser.EditValue, txtPassword.Text);
        }

        private void CancelClick(object sender, EventArgs e)
        {
            user = null;
        }

        /// <summary>
        ///     Войти в систему с введёнными данными
        /// </summary>
        /// <param name="id">id пользователя для входа</param>
        /// <param name="password">Пароль для данного логина</param>
        private void UserLogin(int? id, string password)
        {
            try
            {
                if (id == null)
                {
                    throw new ArgumentException("Необходимо выбрать пользователя из списка");
                }
                user = Security.TryLogin((int)id, password);
                if (user.UserPassword == null)
                {
                    throw new InvalidOperationException("Для вашей учетной записи пароль не указан. Необходимо это исправить.", new Exception());
                }
                DBUser.Current = user;
                Settings.Default.LastLogonUser = user.Id.ToString();
                Settings.Default.Save();
            }
            catch (TaskCanceledException)
            {
                DialogResult = DialogResult.None;
            }
            catch (InvalidOperationException ex)
            {
                XtraMessageBox.Show(ex.Message, "Ошибка при входе", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (user.UpdatePassword(new ChangePasswordForm().NewPassword()))
                {
                    MessageWindow.GetInstance("Пароль успешно сохранён.", MessageType.Info);
                }
            }
            catch (ArgumentException ex)
            {
                XtraMessageBox.Show(ex.Message, "Ошибка при входе", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
            }
        }

        /// <summary>
        ///     Показать форму ввода логина и пароля
        /// </summary>
        /// <returns>Возвращается объект класса <see cref="User" /></returns>
        public User Login()
        {
            ShowDialog();
            return user;
        }

        private void LoginButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if ((bool)e.Button.Tag)
            {
                return;
            }

            //Создать нового пользователя
            var form = new NewUserForm();
            if (form.ShowDialog() != DialogResult.OK || form.NewUser == null)
            {
                return;
            }
            user = form.NewUser;
            Settings.Default.LastLogonUser = lueUser.EditValue.ToString();
            Settings.Default.Save();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void PasswordTextChanged(object sender, EventArgs e)
        {
            dp.SetError(txtPassword, string.Empty);
        }

        public static bool GetChangeUser()
        {
            return changeUser;
        }
    }
}