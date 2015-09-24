using System.Drawing;
using CommonLibrary.ColumnsPopupFormFolder;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.ViewInfo;

namespace CommonLibrary.TreeListFolder
{
    public class CommonTreeListViewInfo : TreeListViewInfo
    {
        public QuickCustomisationIconStatusEnum QuickCustomisationIconStatus;
        private readonly TreeList treeList;

        public CommonTreeListViewInfo(TreeList _treeList)
            : base(_treeList)
        {
            treeList = _treeList;
            QuickCustomisationIconStatus = QuickCustomisationIconStatusEnum.Hot;
        }

        private new CommonTreeList TreeList
        {
            get { return base.TreeList as CommonTreeList; }
        }

        public Rectangle QuickCustomisationBounds
        {
            get
            {
                return treeList.OptionsView.ShowAutoFilterRow
                    ? new Rectangle
                    {
                        Location = new Point(ViewRects.IndicatorBounds.Left + 2, ViewRects.IndicatorBounds.Top - ViewRects.ColumnPanel.Height - 15),
                        Size = new Size(ViewRects.IndicatorBounds.Width, ViewRects.ColumnPanel.Height)
                    }
                    : new Rectangle
                    {
                        Location = new Point(ViewRects.IndicatorBounds.Left + 2, ViewRects.IndicatorBounds.Top - ViewRects.ColumnPanel.Height + 7),
                        Size = new Size(ViewRects.IndicatorBounds.Width, ViewRects.ColumnPanel.Height)
                    };
            }
        }

        public bool AllowQuickCustomisation
        {
            get { return TreeList.OptionsBehavior.AllowQuickCustomisation; }
        }

        public QuickCustomizationIcon QuickCustomisationIcon
        {
            get { return TreeList.OptionsBehavior.QuickCustomizationIcons; }
        }

        public bool IsQuickCustomisationButton(Point p)
        {
            return QuickCustomisationBounds.Contains(p);
        }
    }
}