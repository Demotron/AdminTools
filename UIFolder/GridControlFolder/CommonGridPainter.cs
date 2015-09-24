using System.Drawing;
using CommonLibrary.ColumnsPopupFormFolder;
using CommonLibrary.Properties;
using DevExpress.Utils.Drawing;
using DevExpress.XtraGrid.Drawing;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.Drawing;

namespace CommonLibrary.GridControlFolder
{
    public class CommonGridPainter : GridPainter
    {
        public CommonGridPainter(GridView v)
            : base(v)
        { }

        protected override void DrawIndicatorCore(GridViewDrawArgs e, IndicatorObjectInfoArgs info, int rowHandle, IndicatorKind kind)
        {
            base.DrawIndicatorCore(e, info, rowHandle, kind);
            DrawQuickCustomisationIcon(e, kind);
        }

        private void DrawQuickCustomisationIcon(GridViewDrawArgs e, IndicatorKind kind)
        {
            if (kind != IndicatorKind.Header)
            {
                return;
            }
            var viewInfo = (CommonGridViewInfo)e.ViewInfo;
            DrawQuickCustomisationIconCore(e, viewInfo.QuickCustomisationIcon, viewInfo.QuickCustomisationBounds,
                viewInfo.QuickCustomisationIconStatus);
        }

        private void DrawQuickCustomisationIconCore(GridViewDrawArgs e, QuickCustomizationIcon icon, Rectangle bounds,
            QuickCustomisationIconStatusEnum status)
        {
            if (icon == null)
            {
                return;
            }
            var patchedRec = new Rectangle(bounds.X + 1, bounds.Y, bounds.Width - 1, bounds.Height);
            var args = new GridColumnInfoArgs(e.Cache, e.ViewInfo.ColumnsInfo[0].Column)
            {
                Cache = e.Cache,
                Bounds = patchedRec,
                HeaderPosition = HeaderPositionKind.Center
            };
            if (status == QuickCustomisationIconStatusEnum.Hot)
            {
                args.State = ObjectState.Hot;
            }
            ElementsPainter.Column.DrawObject(args);
            if (icon.Image == null)
            {
                icon.Image = Resources.Customization;
            }
            var rec = new Rectangle
            {
                Location = new Point(bounds.Left + (bounds.Width - icon.Image.Width) / 2, bounds.Top + (bounds.Height - icon.Image.Height) / 2),
                Size = new Size(icon.Image.Width, icon.Image.Height)
            };
            e.Graphics.PageUnit = GraphicsUnit.Pixel;
            e.Graphics.DrawImageUnscaled(icon.Image, rec);
        }
    }
}