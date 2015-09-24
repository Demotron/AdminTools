using System.Drawing;
using CommonLibrary.ColumnsPopupFormFolder;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace CommonLibrary.GridControlFolder
{
    public class CommonGridViewInfo : GridViewInfo
    {
        public QuickCustomisationIconStatusEnum QuickCustomisationIconStatus;

        public CommonGridViewInfo(GridView gridView)
            : base(gridView)
        {
            QuickCustomisationIconStatus = QuickCustomisationIconStatusEnum.Hot;
        }

        public Rectangle QuickCustomisationBounds
        {
            get
            {
                var rec = new Rectangle
                {
                    Location = new Point(ColumnsInfo[0].Bounds.Left, ColumnsInfo[0].Bounds.Top),
                    Size = new Size(ColumnsInfo[0].Bounds.Width, ColumnsInfo[0].Bounds.Height)
                };
                return rec;
            }
        }

        public bool AllowQuickCustomisation
        {
            get { return ((CommonGridView) View).OptionsCustomization.AllowQuickCustomisation; }
        }

        public QuickCustomizationIcon QuickCustomisationIcon
        {
            get { return ((CommonGridView) View).OptionsCustomization.QuickCustomizationIcons; }
        }

        public bool IsQuickCustomisationButton(Point p)
        {
            return QuickCustomisationBounds.Contains(p);
        }
    }
}