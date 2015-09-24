using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Registrator;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Base.Handler;
using DevExpress.XtraGrid.Views.Base.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;

namespace CommonLibrary.GridControlFolder
{
    public class CommonGridInfoRegistrator : GridInfoRegistrator
    {
        public override string ViewName
        {
            get { return "CommonGridView"; }
        }

        public override BaseViewHandler CreateHandler(BaseView view)
        {
            return new CommonGridHandler(view as GridView);
        }

        public override BaseViewPainter CreatePainter(BaseView view)
        {
            return new CommonGridPainter(view as GridView);
        }

        public override BaseViewInfo CreateViewInfo(BaseView view)
        {
            return new CommonGridViewInfo(view as GridView);
        }

        public override BaseView CreateView(GridControl grid)
        {
            return new CommonGridView(grid);
        }
    }
}