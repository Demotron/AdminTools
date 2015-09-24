using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils.Drawing;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;

namespace CommonLibrary.TreeListFolder
{
    public class TreeListScrollHelper
    {
        private const int ScrollIndent = 20;
        private Timer scrollTimer;
        private readonly int scrollInterval;
        private readonly CommonTreeList tree;

        public TreeListScrollHelper(CommonTreeList _tree)
        {
            tree = _tree;
            scrollInterval = 1000;
        }

        public void EnableScrollingOnColumnDragging()
        {
            CreateScrollTimer();
            tree.DragObjectOver += OnDragObjectOver;
            tree.MouseUp += OnMouseUp;
        }

        private void CreateScrollTimer()
        {
            scrollTimer = new Timer
            {
                Interval = scrollInterval
            };
            scrollTimer.Tick += OnScrollTimerTick;
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            if (tree.State == TreeListState.ColumnDragging)
            {
                scrollTimer.Stop();
            }
        }

        private void OnDragObjectOver(object sender, DragObjectOverEventArgs e)
        {
            if (tree.State == TreeListState.ColumnDragging)
            {
                scrollTimer.Start();
            }
        }

        private void OnScrollTimerTick(object sender, EventArgs e)
        {
            var point = tree.PointToClient(Control.MousePosition);
            var hitInfo = tree.CalcHitInfo(point);
            var leftCoord = -1;
            if (!CanScroll(hitInfo))
            {
                return;
            }
            if (IsRightScroll(hitInfo))
            {
                var nextColumn = GetNextColumn();
                leftCoord = tree.ViewInfo.GetColumnLeftCoord(nextColumn);
            }
            else if (IsLeftScroll(hitInfo))
            {
                var prevColumn = GetPrevColumn();
                leftCoord = tree.ViewInfo.GetColumnLeftCoord(prevColumn);
            }
            if (leftCoord == -1)
            {
                return;
            }
            leftCoord += tree.ViewInfo.ViewRects.IndicatorWidth;
            if (tree.ViewInfo.HasFixedLeft)
            {
                leftCoord -= tree.ViewInfo.ViewRects.FixedLeft.Width;
                leftCoord += tree.FixedLineWidth;
            }
            tree.LeftCoord = leftCoord;
        }

        private TreeListColumn GetPrevColumn()
        {
            for (var i = 0; i < tree.VisibleColumns.Count; i++)
            {
                var prevColumn = tree.VisibleColumns[i];
                var columnInfo = tree.ViewInfo.ColumnsInfo[prevColumn];
                if (!IsValidColumn(prevColumn, columnInfo))
                {
                    continue;
                }
                if (prevColumn.VisibleIndex <= 0)
                {
                    return prevColumn;
                }
                var col = tree.VisibleColumns[prevColumn.VisibleIndex - 1];
                if (col.Fixed == FixedStyle.None)
                {
                    prevColumn = col;
                }
                return prevColumn;
            }
            return null;
        }

        private TreeListColumn GetNextColumn()
        {
            for (var i = 0; i < tree.VisibleColumns.Count; i++)
            {
                var nextColumn = tree.VisibleColumns[i];
                var columnInfo = tree.ViewInfo.ColumnsInfo[nextColumn];
                if (!IsValidColumn(nextColumn, columnInfo))
                {
                    continue;
                }
                if (nextColumn.VisibleIndex >= tree.VisibleColumns.Count - 1)
                {
                    return nextColumn;
                }
                var col = tree.VisibleColumns[nextColumn.VisibleIndex + 1];
                if (col.Fixed == FixedStyle.None)
                {
                    nextColumn = col;
                }
                return nextColumn;
            }
            return null;
        }

        private bool IsValidColumn(TreeListColumn column, GraphicsInfoArgs columnInfo)
        {
            return columnInfo != null && column.Fixed == FixedStyle.None &&
                   ((!tree.ViewInfo.HasFixedLeft && columnInfo.Bounds.X > 0)
                    || (tree.ViewInfo.HasFixedLeft && columnInfo.Bounds.X >= tree.ViewInfo.ViewRects.FixedLeft.Right));
        }

        private bool IsLeftScroll(TreeListHitInfo hitInfo)
        {
            if (tree.ViewInfo.HasFixedLeft)
            {
                return hitInfo.MousePoint.X < tree.ViewInfo.ViewRects.FixedLeft.Right + ScrollIndent;
            }
            return hitInfo.MousePoint.X < tree.ViewInfo.ViewRects.ClipRectangle.X + ScrollIndent;
        }

        private bool IsRightScroll(TreeListHitInfo hitInfo)
        {
            if (tree.ViewInfo.HasFixedRight)
            {
                return hitInfo.MousePoint.X > tree.ViewInfo.ViewRects.FixedRight.Left - ScrollIndent;
            }
            return hitInfo.MousePoint.X > tree.ViewInfo.ViewRects.ClipRectangle.Right - ScrollIndent;
        }

        private bool CanScroll(TreeListHitInfo hitInfo)
        {
            return tree.State == TreeListState.ColumnDragging &&
                   hitInfo.HitInfoType == HitInfoType.Column;
        }

        public void DisableScrollingOnColumnDragging()
        {
            if (scrollTimer == null)
            {
                return;
            }
            scrollTimer.Tick -= OnScrollTimerTick;
            scrollTimer.Dispose();
            scrollTimer = null;
            tree.DragObjectOver -= OnDragObjectOver;
            tree.MouseUp -= OnMouseUp;
        }

        public static void OnFilterNode(FilterNodeEventArgs e)
        {
            var filteredColumns = e.Node.TreeList.Columns.
                Cast<TreeListColumn>()
                .
                Where(c => c.FilterInfo.AutoFilterRowValue != null)
                .
                ToList();
            if (filteredColumns.Count == 0)
            {
                return;
            }
            e.Handled = true;
            var filtered = filteredColumns.Any(c => IsNodeMatchFilter(e.Node, c));
            e.Node.Visible = filtered;
            e.Node.Expanded = filtered;
        }

        private static bool IsNodeMatchFilter(TreeListNode node, TreeListColumn column)
        {
            var filterValue = column.FilterInfo.AutoFilterRowValue.ToString();
            if (node.GetDisplayText(column)
                .ToLower()
                .Contains(filterValue.ToLower()))
            {
                return true;
            }
            foreach (TreeListNode n in node.Nodes)
            {
                if (IsNodeMatchFilter(n, column))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        ///     Закоммитить изменения в DataSource у грида на форме
        /// </summary>
        public static void CommitTreeListChanges(TreeList tree)
        {
            tree.PostEditor();
            tree.CloseEditor();
        }

        /// <summary>
        ///     Получить Id всех предков для данной ветки
        /// </summary>
        /// <param name="node">Текущая ветка</param>
        /// <param name="idFieldName">Имя столбца с id</param>
        /// <returns>Cписок всех Id</returns>
        public static IEnumerable<int> ParentsNodesGetIds(TreeListNode node, string idFieldName)
        {
            var result = new List<int>();
            while (node != null)
            {
                result.Add(Convert.ToInt32(node.GetValue(idFieldName)));
                node = node.ParentNode;
            }
            return result;
        }
    }
}