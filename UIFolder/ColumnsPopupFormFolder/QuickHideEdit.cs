using System.Drawing;
using CommonLibrary.GridControlFolder;
using CommonLibrary.TreeListFolder;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Container;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Popup;

namespace CommonLibrary.ColumnsPopupFormFolder
{
    public sealed class QuickHideEdit : PopupContainerEdit
    {
        private const int EditorWidth = 10;
        private PopupContainerControl integratedPopupControl;
        private readonly EditorContainer editor;

        public QuickHideEdit(EditorContainer _editor)
        {
            editor = _editor;
            if (TreeList != null)
            {
                Properties.LookAndFeel.Assign(TreeList.ElementsLookAndFeel);
            }
            else if (GridControl != null)
            {
                Properties.LookAndFeel.Assign(GridControl.MainView.GetLookAndFeel());
            }
            MakeUnEnable();
            Size = new Size(EditorWidth, EditorHeight);
            BorderStyle = BorderStyles.Simple;
            CreatePopupControl();
        }

        static QuickHideEdit()
        {
            RepositoryItemQuickHideEdit.RegisterQuickHideEdit();
        }

        public int EditorHeight { get; private set; }

        public override string EditorTypeName
        {
            get { return RepositoryItemQuickHideEdit.QuickHideEditName; }
        }

        public new RepositoryItemQuickHideEdit Properties
        {
            get { return base.Properties as RepositoryItemQuickHideEdit; }
        }

        public CommonTreeList TreeList
        {
            get { return editor as CommonTreeList; }
        }

        public CommonGridControl GridControl
        {
            get { return editor as CommonGridControl; }
        }

        private void CreatePopupControl()
        {
            integratedPopupControl = new PopupContainerControl();
            Controls.Add(integratedPopupControl);
            Properties.PopupControl = integratedPopupControl;
        }

        public void MakeEnable()
        {
            Visible = true;
            Enabled = true;
        }

        private void MakeUnEnable()
        {
            Visible = false;
            Enabled = false;
        }

        protected override PopupBaseForm CreatePopupForm()
        {
            var form = new QuickHidePopupForm(this);
            return form;
        }

        protected override void DoShowPopup()
        {
            base.DoShowPopup();
            ((QuickHidePopupForm)PopupForm).PopulateListBox();
        }

        protected override void DoClosePopup(PopupCloseMode closeMode)
        {
            base.DoClosePopup(closeMode);
            MakeUnEnable();
        }

        public override void ClosePopup()
        {
            base.ClosePopup();
            Properties.Columns = ((QuickHidePopupForm)PopupForm).GetCollumns(Properties.Columns);
            if (TreeList != null)
            {
                TreeList.AcceptQuickHide();
            }
            else if (GridControl != null)
            {
                GridControl.MainView.AcceptQuickHide();
            }
        }
    }
}