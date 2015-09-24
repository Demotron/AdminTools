using System;
using System.Windows.Forms;

namespace CommonLibrary.ColumnsPopupFormFolder
{
    public class CustomColumnProperties : IComparable
    {
        public CustomColumnProperties(string caption, bool visible, int visibleIndex, bool allowQuickHide, bool allowMove)
        {
            Visible = visible;
            VisibleIndex = visibleIndex;
            AllowQuickHide = allowQuickHide;
            AllowMove = allowMove;
            Caption = caption;
        }

        public bool Visible { get; set; }
        public int VisibleIndex { get; set; }
        public bool AllowQuickHide { get; set; }
        public bool AllowMove { get; set; }
        public string Caption { get; set; }

        public CheckState CheckState
        {
            get
            {
                return Visible
                    ? CheckState.Checked
                    : CheckState.Unchecked;
            }
            set { Visible = value == CheckState.Checked; }
        }

        #region IComparable Members

        public int CompareTo(object obj)
        {
            var customTreeListColumn = obj as CustomColumnProperties;
            if (customTreeListColumn == null)
            {
                return 0;
            }
            if (VisibleIndex < 0 && customTreeListColumn.VisibleIndex >= 0)
            {
                return 1;
            }
            if (VisibleIndex >= 0 && customTreeListColumn.VisibleIndex < 0)
            {
                return -1;
            }
            if (VisibleIndex < 0 && customTreeListColumn.VisibleIndex < 0)
            {
                return String.Compare(Caption, customTreeListColumn.Caption, StringComparison.Ordinal);
            }
            if (VisibleIndex > customTreeListColumn.VisibleIndex)
            {
                return 1;
            }
            if (VisibleIndex < customTreeListColumn.VisibleIndex)
            {
                return -1;
            }
            return 0;
        }

        #endregion
    }
}