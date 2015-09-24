using System.Drawing;
using System.Windows.Forms;
using CommonLibrary.ColumnsPopupFormFolder;
using CommonLibrary.Properties;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Painter;

namespace CommonLibrary.TreeListFolder
{
    public sealed class CommonTreeListPaintHelper : TreeListPaintHelper
    {
        private readonly CommonTreeList treeList;

        public CommonTreeListPaintHelper(CommonTreeList _treeList)
        {
            treeList = _treeList;
        }

        public override void DrawColumn(CustomDrawColumnHeaderEventArgs e)
        {
            base.DrawColumn(e);
            DrawQuickCustomisationIcon(e);
        }

        private void DrawQuickCustomisationIcon(CustomDrawColumnHeaderEventArgs e)
        {
            if (e.ColumnType != HitInfoType.ColumnButton)
            {
                return;
            }
            DrawQuickCustomisationIconCore(e, treeList.ViewInfo.QuickCustomisationIcon,
                treeList.ViewInfo.QuickCustomisationBounds);
        }

        private void DrawQuickCustomisationIconCore(CustomDrawEventArgs e,
            QuickCustomizationIcon icon, Rectangle bounds)
        {
            if (icon == null)
            {
                return;
            }
            ControlPaint.DrawBorder3D(e.Graphics, e.Bounds, Border3DStyle.RaisedInner);
            if (icon.Image == null)
            {
                icon.Image = Resources.Customization;
            }
            e.Graphics.PageUnit = GraphicsUnit.Pixel;
            e.Graphics.DrawImageUnscaled(icon.Image, treeList.ViewInfo.QuickCustomisationBounds);
        }
    }
}