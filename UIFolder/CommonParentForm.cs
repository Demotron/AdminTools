using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using CommonLibrary.AdminFolder;
using CommonLibrary.Properties;
using CommonLibrary.UserFolder;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraSplashScreen;
using ServerInformation;
using ServerInformation.ExceptionFolder;

namespace CommonLibrary
{
    /// <summary>
    ///     Предок для главной формы в приложении
    /// </summary>
    public partial class CommonParentForm : RibbonForm, IAdminTool
    {
        public List<string> XmlSettings { get; set; }

        protected CommonParentForm(User user = null)
        {
            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                LoadUserInformation(user);
            }
            // ReSharper disable once ObjectCreationAsStatement
            new SplashScreenManager(this, typeof(MainSplashScreen), true, true);
            InitializeComponent();
        }

        private void CommonParentFormLoad(object sender, EventArgs e)
        {
            if (DesignMode)
            {
                return;
            }
            documentMng.View = Settings.Default.WindowStyle
                ? documentMng.ViewCollection[0]
                : documentMng.ViewCollection[1];
            ChangeRibbonStyleSize();
            this.LoadFormsControlsBeforeShow();
        }

        protected void ChangeRibbonStyleSize()
        {
            switch (Settings.Default.ImageStyle)
            {
                case 0:
                    rbnMain.RibbonStyle = RibbonControlStyle.Office2013;
                    foreach (BarItem item in rbnMain.Manager.Items)
                    {
                        item.RibbonStyle = RibbonItemStyles.Default;
                    }
                    break;
                case 1:
                    rbnMain.RibbonStyle = RibbonControlStyle.Office2013;
                    foreach (BarItem item in rbnMain.Manager.Items)
                    {
                        item.RibbonStyle = RibbonItemStyles.SmallWithText | RibbonItemStyles.SmallWithoutText;
                    }
                    break;
                case 2:
                    rbnMain.RibbonStyle = RibbonControlStyle.TabletOffice;
                    break;
            }
            Application.OpenForms.OfType<CommonChildForm>().ToList().ForEach(f => f.ChangeRibbonStyleSize());
        }

        private void CommonParentFormActivated(object sender, EventArgs e)
        {
            HighlightControl.CloseForm();
        }

        private void CommonParentFormShown(object sender, EventArgs e)
        {
            if (DesignMode)
            {
                return;
            }
            defaultLookAndFeel.LookAndFeel.SetSkinStyle(Settings.Default.SkinStyle);
            Text = string.Format("Добро пожаловать, {0}", DBUser.Working.SurnameInitials);
            LoadAdminSettings();
        }

        /// <summary>
        /// Загрузить настройки для админской роли
        /// </summary>
        private void LoadAdminSettings()
        {
            rpAdmin.Visible = DBUser.IsAdmin;
        }

        /// <summary>
        ///     Загрузить информацию о пользователе
        /// </summary>
        private static void LoadUserInformation(User user = null)
        {
            try
            {
                if (user != null)
                {
                    DBUser.Working = user;
                    return;
                }
                var loginform = new AuthenticationForm();
                DBUser.Working = loginform.Login();
                if (DBUser.Working == null)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                Environment.Exit(0);
            }
        }

        public void ApplicationLostConnection()
        { }

        public void ApplicationResetConnection()
        { }

        private void RibbonShowCustomizationMenu(object sender, RibbonCustomizationMenuEventArgs e)
        {
            e.ShowCustomizationMenu = false;
        }

        /// <summary>
        ///     Создать новую зависимую форму
        /// </summary>
        /// <param name="args">Параметры, передаваемые на форму</param>
        public T CreateChildForm<T>(params object[] args) where T : class
        {
            Application.DoEvents();
            var type = typeof(T);
            try
            {
                var f = MdiChildren.FirstOrDefault(a => a.Name == type.Name);
                if (f == null)
                {
                    f = (Form)Activator.CreateInstance(type, args);
                    f.MdiParent = this;
                }
                f.Show();
                f.Activate();
                return f as T;
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Неожиданное поведение при загрузке формы, данные о ней отправлены программистам.",
                    @"Сообщение отправлено программистам", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DBException.WriteLog(ex);
            }
            return null;
        }

        /// <summary>
        ///     Создать новую зависимую форму
        /// </summary>
        /// <param name="type">Тип создаваемой формы</param>
        /// <param name="args">Параметры, передаваемые на форму</param>
        public CommonChildForm CreateChildForm(Type type, params object[] args)
        {
            Application.DoEvents();
            try
            {
                var f = MdiChildren.FirstOrDefault(a => a.Name == type.Name);
                if (f == null)
                {
                    f = (Form)Activator.CreateInstance(type, args);
                    f.MdiParent = this;
                }
                f.Show();
                f.Activate();
                return (CommonChildForm)f;
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Неожиданное поведение при загрузке формы, данные о ней отправлены программистам.",
                    @"Сообщение отправлено программистам", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DBException.WriteLog(ex);
            }
            return null;
        }

        private void MainMerge(object sender, RibbonMergeEventArgs e)
        {
            if (rbnMain.MergedPages.Count != 0)
            {
                rbnMain.SelectedPage = rbnMain.MergedPages[0];
            }
        }

        /// <summary>
        ///     Настройка ролей для пользователей
        /// </summary>
        private void RoleEditItemClick(object sender, ItemClickEventArgs e)
        {
            CreateChildForm<RolesChangeRibbonForm>();
        }

        /// <summary>
        ///     Настройка интерфейсов для форм
        /// </summary>
        private void AdminItemClick(object sender, ItemClickEventArgs e)
        {
            AdminTool.GetInstace(ActiveMdiChild ?? this);
        }

        #region Сменить текущего пользователя

        protected void LoadUserInterfaceSettings()
        {
            rbnMain.MdiMergeStyle = DBUser.IsAdmin
                ? RibbonMdiMergeStyle.Never
                : RibbonMdiMergeStyle.Always;
        }

        #endregion

        #region IAdminTool

        public void ActivatedFormChanged()
        {
            if (AdminTool.Instance != null && ActiveMdiChild != null)
            {
                AdminTool.Instance.ChangeActiveForm(ActiveMdiChild);
            }
        }

        public void CloseActivateForm(Form form)
        {
            if (AdminTool.Instance != null && ((CommonChildForm)form).SaveState)
            {
                AdminTool.Instance.CloseActiveForm(form);
            }
        }

        #endregion

        #region IActionButton

        private void AddItemClick(object sender, ItemClickEventArgs e)
        {
            var actionButton = ActiveMdiChild as IActionButton;
            if (actionButton != null)
            {
                actionButton.AddNewRow();
            }
        }

        private void RemoveItemClick(object sender, ItemClickEventArgs e)
        {
            var actionButton = ActiveMdiChild as IActionButton;
            if (actionButton != null)
            {
                actionButton.DeleteRow();
            }
        }

        private void RefreshItemClick(object sender, ItemClickEventArgs e)
        {
            var actionButton = ActiveMdiChild as IActionButton;
            if (actionButton != null)
            {
                actionButton.RefreshData();
            }
        }

        private void CommitItemClick(object sender, ItemClickEventArgs e)
        {
            var actionButton = ActiveMdiChild as IActionButton;
            if (actionButton != null)
            {
                actionButton.SaveChanges();
            }
        }

        private void ExelItemClick(object sender, ItemClickEventArgs e)
        {
            var actionButton = ActiveMdiChild as IActionButton;
            if (actionButton != null)
            {
                actionButton.ExportToExcel();
            }
        }

        #endregion
    }
}