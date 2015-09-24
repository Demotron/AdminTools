using System.Collections;

namespace CommonLibrary.ColumnsPopupFormFolder
{
    public sealed class ColumnPropertiesCollection : ArrayList
    {
        public new CustomColumnProperties this[int index]
        {
            get
            {
                if (index < 0 || index > Count - 1)
                {
                    return null;
                }
                return base[index] as CustomColumnProperties;
            }
            set
            {
                if (index < 0 || index > Count - 1)
                {
                    return;
                }
                base[index] = value;
            }
        }

        public CustomColumnProperties this[string caption]
        {
            get { return this[IndexFromeCaption(caption)]; }
        }

        private int IndexFromeCaption(string caption)
        {
            for (var i = 0; i < Count; i++)
            {
                if (this[i].Caption == caption)
                {
                    return i;
                }
            }
            return -1;
        }

        public void Add(string caption, bool visible, int visibleIndex, bool allowQuickHide, bool allowMove)
        {
            Add(new CustomColumnProperties(caption, visible, visibleIndex, allowQuickHide, allowMove));
        }

        public void Add(string caption, bool visible, int visibleIndex, bool allowQuickHide)
        {
            Add(new CustomColumnProperties(caption, visible, visibleIndex, allowQuickHide, true));
        }
    }
}