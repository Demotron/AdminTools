using System.ComponentModel;
using DevExpress.XtraEditors;

namespace CommonLibrary
{
    partial class MainSplashScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainSplashScreen));
            this.lblCopyright = new DevExpress.XtraEditors.LabelControl();
            this.peImageCompany = new DevExpress.XtraEditors.PictureEdit();
            this.peLogo = new DevExpress.XtraEditors.PictureEdit();
            this.mpbcDocument = new DevExpress.XtraEditors.MarqueeProgressBarControl();
            ((System.ComponentModel.ISupportInitialize)(this.peImageCompany.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.peLogo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mpbcDocument.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCopyright
            // 
            this.lblCopyright.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCopyright.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.lblCopyright.Location = new System.Drawing.Point(12, 391);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(115, 13);
            this.lblCopyright.TabIndex = 6;
            this.lblCopyright.Text = "Copyright © 2007-2014";
            // 
            // peImageCompany
            // 
            this.peImageCompany.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.peImageCompany.Location = new System.Drawing.Point(12, 12);
            this.peImageCompany.Name = "peImageCompany";
            this.peImageCompany.Properties.AllowFocused = false;
            this.peImageCompany.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.peImageCompany.Properties.Appearance.Options.UseBackColor = true;
            this.peImageCompany.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.peImageCompany.Properties.ShowMenu = false;
            this.peImageCompany.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.peImageCompany.Size = new System.Drawing.Size(467, 311);
            this.peImageCompany.TabIndex = 9;
            // 
            // peLogo
            // 
            this.peLogo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.peLogo.Location = new System.Drawing.Point(319, 371);
            this.peLogo.Name = "peLogo";
            this.peLogo.Properties.AllowFocused = false;
            this.peLogo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.peLogo.Properties.Appearance.Options.UseBackColor = true;
            this.peLogo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.peLogo.Properties.ShowMenu = false;
            this.peLogo.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.peLogo.Size = new System.Drawing.Size(160, 48);
            this.peLogo.TabIndex = 8;
            // 
            // mpbcDocument
            // 
            this.mpbcDocument.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mpbcDocument.EditValue = 0;
            this.mpbcDocument.Location = new System.Drawing.Point(12, 346);
            this.mpbcDocument.Name = "mpbcDocument";
            this.mpbcDocument.Properties.LookAndFeel.SkinName = "Seven";
            this.mpbcDocument.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.mpbcDocument.Properties.MarqueeAnimationSpeed = 60;
            this.mpbcDocument.Size = new System.Drawing.Size(467, 19);
            this.mpbcDocument.TabIndex = 10;
            // 
            // MainSplashScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 425);
            this.Controls.Add(this.mpbcDocument);
            this.Controls.Add(this.peImageCompany);
            this.Controls.Add(this.peLogo);
            this.Controls.Add(this.lblCopyright);
            this.Name = "MainSplashScreen";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainSplashScreen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.peImageCompany.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.peLogo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mpbcDocument.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LabelControl lblCopyright;
        private PictureEdit peLogo;
        private MarqueeProgressBarControl mpbcDocument;
        protected internal PictureEdit peImageCompany;
    }
}
