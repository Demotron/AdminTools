using System.Drawing;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;

namespace CommonLibrary.ColumnsPopupFormFolder
{
    public sealed class QuickHideEditViewInfo : PopupContainerEditViewInfo
    {
        public QuickHideEditViewInfo(RepositoryItem item)
            : base(item)
        { }

        internal QuickCustomizationIcon GetIcon
        {
            get
            {
                if (((QuickHideEdit)OwnerEdit).TreeList != null)
                {
                    return ((QuickHideEdit)OwnerEdit).TreeList.OptionsBehavior.QuickCustomizationIcons;
                }
                return ((QuickHideEdit)OwnerEdit).GridControl != null
                    ? ((QuickHideEdit)OwnerEdit).GridControl.MainView.OptionsCustomization.QuickCustomizationIcons
                    : new QuickCustomizationIcon();
            }
        }

        protected override int CalcMinHeightCore(Graphics g)
        {
            return ((QuickHideEdit)OwnerEdit).EditorHeight;
        }
    }
}