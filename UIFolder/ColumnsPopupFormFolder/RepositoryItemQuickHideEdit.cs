using System.Drawing;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;

namespace CommonLibrary.ColumnsPopupFormFolder
{
    public sealed class RepositoryItemQuickHideEdit : RepositoryItemPopupContainerEdit
    {
        private const int MinFormWidth = 400,
            MinFormHeight = 300;

        public const string QuickHideEditName = "QuickHideEdit";

        public RepositoryItemQuickHideEdit()
        {
            PopupFormMinSize = new Size(MinFormWidth, MinFormHeight);
            Columns = new ColumnPropertiesCollection();
        }

        static RepositoryItemQuickHideEdit()
        {
            RegisterQuickHideEdit();
        }

        public ColumnPropertiesCollection Columns { get; set; }

        public override string EditorTypeName
        {
            get { return QuickHideEditName; }
        }

        public static void RegisterQuickHideEdit()
        {
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(QuickHideEditName, typeof (QuickHideEdit),
                typeof (RepositoryItemQuickHideEdit), typeof (PopupContainerEditViewInfo), new QuickHideEditPainter(), true));
        }

        public override BaseEditViewInfo CreateViewInfo()
        {
            return new QuickHideEditViewInfo(this);
        }
    }
}