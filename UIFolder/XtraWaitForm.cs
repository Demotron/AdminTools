using System;
using DevExpress.XtraWaitForm;

namespace CommonLibrary
{
    public partial class XtraWaitForm : WaitForm
    {
        public XtraWaitForm()
        {
            InitializeComponent();
            ppWait.AutoHeight = true;
        }

        #region Overrides

        public override void SetCaption(string caption)
        {
            base.SetCaption(caption);
            ppWait.Caption = caption;
        }

        public override void SetDescription(string description)
        {
            base.SetDescription(description);
            ppWait.Description = description;
        }

        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
        }

        #endregion
    }
}