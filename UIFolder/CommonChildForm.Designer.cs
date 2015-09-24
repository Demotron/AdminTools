using System.ComponentModel;

namespace CommonLibrary
{
    /// <summary>
    ///     Предок для всех дочерних форм в приложении
    /// </summary>
    public partial class CommonChildForm
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
            this.SuspendLayout();
            // 
            // MdiChildForm
            // 
            this.ClientSize = new System.Drawing.Size(343, 285);
            this.Name = "MdiChildForm";
            this.Activated += new System.EventHandler(this.MdiChildFormActivated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MdiChildFormFormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MdiChildFormFormClosed);
            this.Load += new System.EventHandler(this.MdiChildFormLoad);
            this.Shown += new System.EventHandler(this.MdiChildFormShown);
            this.ResumeLayout(false);

        }

        #endregion

       }
}