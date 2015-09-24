using System;
using CommonLibrary.Properties;
using DevExpress.XtraSplashScreen;

namespace CommonLibrary
{
    public partial class MainSplashScreen : SplashScreen
    {
        public MainSplashScreen()
        {
            InitializeComponent();
        }

        private void MainSplashScreen_Load(object sender, EventArgs e)
        {
            peImageCompany.EditValue = Resources.Company;
            peLogo.EditValue = Resources.Logo;
            lblCopyright.Text = Settings.Default.Copyright;
        }
    }
}