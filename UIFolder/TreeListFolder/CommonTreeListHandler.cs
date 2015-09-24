using System.Drawing;
using System.Windows.Forms;
using CommonLibrary.ColumnsPopupFormFolder;
using DevExpress.Utils;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Handler;

namespace CommonLibrary.TreeListFolder
{
    public class CommonTreeListHandler : TreeListHandler
    {
        new CommonTreeList TreeList
        {
            get { return base.TreeList as CommonTreeList; }
        }

        public CommonTreeListHandler(CommonTreeList treeList)
            : base(treeList)
        { }

        public override ToolTipControlInfo GetObjectTipInfo(Point point)
        {
            var ht = GetHitTest(point);
            return ht.Column != null ? base.GetObjectTipInfo(point) : null;
        }

        public override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Clicks != 1 || e.Button != MouseButtons.Left)
            {
                return;
            }
            var hit = TreeList.CalcHitInfo(e.Location);
            if (hit == null || hit.HitInfoType != HitInfoType.ColumnButton || !TreeList.OptionsBehavior.AllowQuickCustomisation)
            {
                return;
            }
            if (TreeList.ViewInfo.IsQuickCustomisationButton(hit.MousePoint))
            {
                TreeList.ShowColumnCustomizationMenu();
            }
        }

        public override void OnMouseMove(MouseEventArgs ev)
        {
            var e = DXMouseEventArgs.GetMouseArgs(ev);
            var p = new Point(e.X, e.Y);
            UpdateQuickCustumisationIconState(p);
            base.OnMouseMove(ev);
        }


        //        public override void DoClickAction(TreeListHitInfo hitInfo)
        //        {
        //            base.DoClickAction(hitInfo);
        //            var hit = hitInfo as GridHitInfo;
        //            if (hit == null || (hit.HitTest != GridHitTest.ColumnButton || !((CustomGridView)View).OptionsCustomization.AllowQuickCustomisation))
        //            {
        //                return;
        //            }
        //            if (((CommonGridViewInfo)ViewInfo).IsQuickCustomisationButton(hitInfo.HitPoint))
        //            {
        //                ((CustomGridView)View).ShowColumnCustomizationMenu();
        //            }
        //        }
        //

        private void UpdateQuickCustumisationIconState(Point p)
        {
            var vi = TreeList.ViewInfo;
            if (vi != null && !vi.AllowQuickCustomisation)
            {
                return;
            }
            var hi = TreeList.CalcHitInfo(p);
            if (hi.HitInfoType == HitInfoType.ColumnButton)
            {
                if (vi != null && vi.IsQuickCustomisationButton(p))
                {
                    if (vi.QuickCustomisationIconStatus == QuickCustomisationIconStatusEnum.Hot)
                    {
                        return;
                    }
                    vi.QuickCustomisationIconStatus = QuickCustomisationIconStatusEnum.Hot;
                    TreeList.Invalidate();
                    return;
                }
                if (vi == null || vi.QuickCustomisationIconStatus == QuickCustomisationIconStatusEnum.Enabled)
                {
                    return;
                }
                vi.QuickCustomisationIconStatus = QuickCustomisationIconStatusEnum.Enabled;
                TreeList.Invalidate();
            }
            else if (vi != null && vi.QuickCustomisationIconStatus != QuickCustomisationIconStatusEnum.Hidden)
            {
                vi.QuickCustomisationIconStatus = QuickCustomisationIconStatusEnum.Hidden;
                TreeList.Invalidate();
            }
        }
    }
}