using System.Drawing;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.ViewInfo;

namespace CommonLibrary.ColumnsPopupFormFolder
{
    /// <summary>
    ///     Кастомный класс прорисовщика
    /// </summary>
    public class CustomPainterCheckedListBox : PainterCheckedListBox
    {
        private const int LineWidth = 1;

        /// <summary>
        ///     Перегружаемый метод прорисовки
        /// </summary>
        protected override void DrawItemCore(ControlGraphicsInfoArgs info, BaseListBoxViewInfo.ItemInfo itemInfo, ListBoxDrawItemEventArgs e)
        {
            base.DrawItemCore(info, itemInfo, e);
            var customInfo = itemInfo as CustomCheckedListBoxViewInfo.CustomCheckedItemInfo;
            if (customInfo == null)
            {
                return;
            }
            var rec = new Rectangle(itemInfo.Bounds.Location, new Size(itemInfo.Bounds.Width, LineWidth));
            var lineColor = ((CustomCheckedListBoxViewInfo) info.ViewInfo).DragDropLineColor;
            if (itemInfo.Index == 0)
            {
                var font = new Font(itemInfo.PaintAppearance.Font.FontFamily, itemInfo.PaintAppearance.Font.Size, FontStyle.Bold);
                info.Graphics.FillRectangle(Brushes.Lavender, e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);
                e.Graphics.DrawString(itemInfo.Text, font, Brushes.Black, e.Bounds.X, e.Bounds.Y + 2);
            }
            if (customInfo.IsOverLine)
            {
                if (customInfo.Index == 0)
                {
                    rec.Height++;
                }
                info.Graphics.FillRectangle(info.Cache.GetSolidBrush(lineColor), rec);
            }
            if (!customInfo.IsUnderLine)
            {
                return;
            }
            rec.Offset(0, itemInfo.Bounds.Height - LineWidth);
            if (customInfo.Index == ((CustomCheckedListBoxViewInfo) info.ViewInfo).ItemCountAccessMethod() - 1)
            {
                rec.Height++;
            }
            info.Graphics.FillRectangle(info.Cache.GetSolidBrush(lineColor), rec);
        }
    }
}