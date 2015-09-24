using System.Drawing;
using System.Drawing.Imaging;
using CommonLibrary.Properties;
using DevExpress.XtraEditors.Drawing;

namespace CommonLibrary.ColumnsPopupFormFolder
{
    public class QuickHideEditPainter : ButtonEditPainter
    {
        protected override void DrawContent(ControlGraphicsInfoArgs info)
        {
            info.Graphics.FillRectangle(info.Cache.GetSolidBrush(Color.White), info.ViewInfo.ClientRect);
            var icon = ((QuickHideEditViewInfo)info.ViewInfo).GetIcon;
            if (icon.Image == null)
            {
                icon.Image = Resources.Customization;
            }
            var attr = new ImageAttributes();
            attr.SetColorKey(icon.TransperentColor, icon.TransperentColor);
            info.Graphics.DrawImageUnscaled(icon.Image, info.ViewInfo.ClientRect);
        }
    }
}