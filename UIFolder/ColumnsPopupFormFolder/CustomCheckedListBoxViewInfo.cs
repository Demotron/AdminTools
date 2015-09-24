using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.ViewInfo;

namespace CommonLibrary.ColumnsPopupFormFolder
{
    public sealed class CustomCheckedListBoxViewInfo : CheckedListBoxViewInfo
    {
        public CustomCheckedListBoxViewInfo(CheckedListBoxControl listBox) : base(listBox)
        {}

        public Color DragDropLineColor { get; set; }

        protected override ItemInfo CreateItemInfo(Rectangle bounds, object item, string text, int index)
        {
            var info = base.CreateItemInfo(bounds, item, text, index) as CheckedItemInfo;
            var patchedInfo = new CustomCheckedItemInfo(info);
            return patchedInfo;
        }

        private void UnderlineItem(int index)
        {
            var info = GetItemByIndex(index) as CustomCheckedItemInfo;
            if (info != null)
            {
                info.IsUnderLine = true;
            }
        }

        private void OverlineItem(int index)
        {
            var info = GetItemByIndex(index) as CustomCheckedItemInfo;
            if (info != null)
            {
                info.IsOverLine = true;
            }
        }

        internal int ItemCountAccessMethod()
        {
            return ItemCount;
        }

        private void DropLine()
        {
            foreach (CustomCheckedItemInfo info in VisibleItems)
            {
                info.DropLine();
            }
        }

        internal void MarkItem(int targetIndex, int sourceIndex)
        {
            DropLine();
            DragDropLineColor = Color.Red;
            if (targetIndex == sourceIndex || targetIndex == sourceIndex + 1 ||
                sourceIndex == ItemCount - 1 && targetIndex == -1 || targetIndex == 0)
            {
                DragDropLineColor = Color.LightGray;
            }
            if (targetIndex == -1)
            {
                UnderlineItem(ItemCount - 1);
            }
            else
            {
                OverlineItem(targetIndex);
            }
            if (targetIndex > 0)
            {
                UnderlineItem(targetIndex - 1);
            }
            OwnerControl.Invalidate();
        }

        public sealed class CustomCheckedItemInfo : CheckedItemInfo
        {
            private CustomCheckedItemInfo(Rectangle rect, object item, string text, int index, CheckState checkState, bool enabled)
                : base(rect, item, text, index, checkState, enabled)
            {
                DropLine();
            }

            public CustomCheckedItemInfo(CheckedItemInfo info)
                : this(info.Bounds, info.Item, info.Text, info.Index, info.CheckArgs.CheckState, info.Enabled)
            {
                CheckArgs.Assign(info.CheckArgs);
                TextRect = info.TextRect;
            }

            internal bool IsUnderLine { get; set; }
            internal bool IsOverLine { get; set; }

            internal void DropLine()
            {
                IsUnderLine = false;
                IsOverLine = false;
            }
        }
    }
}