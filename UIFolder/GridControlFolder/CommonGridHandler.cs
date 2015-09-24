using System.Drawing;
using System.Windows.Forms;
using CommonLibrary.ColumnsPopupFormFolder;
using DevExpress.Utils;
using DevExpress.XtraGrid.Dragging;
using DevExpress.XtraGrid.Views.Base.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.Handler;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace CommonLibrary.GridControlFolder
{
    public class CommonGridHandler : GridHandler
    {
        public CommonGridHandler(GridView gridView)
            : base(gridView)
        { }

        protected override GridDragManager CreateDragManager()
        {
            return new CommonGridDragManager(View);
        }

        public override void DoClickAction(BaseHitInfo hitInfo)
        {
            base.DoClickAction(hitInfo);
            var hit = hitInfo as GridHitInfo;
            if (hit == null || (hit.HitTest != GridHitTest.ColumnButton || !((CommonGridView)View).OptionsCustomization.AllowQuickCustomisation))
            {
                return;
            }
            if (((CommonGridViewInfo)ViewInfo).IsQuickCustomisationButton(hitInfo.HitPoint))
            {
                ((CommonGridView)View).ShowColumnCustomizationMenu();
            }
        }

        protected override bool OnMouseMove(MouseEventArgs ev)
        {
            var e = DXMouseEventArgs.GetMouseArgs(ev);
            var p = new Point(e.X, e.Y);
            UpdateQuickCustumisationIconState(p);
            return base.OnMouseMove(ev);
        }

        private void UpdateQuickCustumisationIconState(Point p)
        {
            var vi = ViewInfo as CommonGridViewInfo;
            if (vi != null && !vi.AllowQuickCustomisation)
            {
                return;
            }
            var hi = ViewInfo.CalcHitInfo(p);
            if (hi.HitTest == GridHitTest.ColumnButton)
            {
                if (vi != null && vi.IsQuickCustomisationButton(p))
                {
                    if (vi.QuickCustomisationIconStatus == QuickCustomisationIconStatusEnum.Hot)
                    {
                        return;
                    }
                    vi.QuickCustomisationIconStatus = QuickCustomisationIconStatusEnum.Hot;
                    ViewInfo.View.Invalidate();
                    return;
                }
                if (vi == null || vi.QuickCustomisationIconStatus == QuickCustomisationIconStatusEnum.Enabled)
                {
                    return;
                }
                vi.QuickCustomisationIconStatus = QuickCustomisationIconStatusEnum.Enabled;
                ViewInfo.View.Invalidate();
            }
            else if (vi != null && vi.QuickCustomisationIconStatus != QuickCustomisationIconStatusEnum.Hidden)
            {
                vi.QuickCustomisationIconStatus = QuickCustomisationIconStatusEnum.Hidden;
                ViewInfo.View.Invalidate();
            }
        }
    }
}