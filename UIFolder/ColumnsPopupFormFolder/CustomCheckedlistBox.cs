using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.ViewInfo;
using ItemCheckEventArgs = DevExpress.XtraEditors.Controls.ItemCheckEventArgs;

namespace CommonLibrary.ColumnsPopupFormFolder
{
    /// <summary>
    ///     CheckedListBox в котором элемены можно сортировать, перетаскивать.
    /// </summary>
    /// <remarks>
    ///     Используется в CommonGridColumn для управления отображением и порядком колонок
    ///     Большинство методов в классе реализовывает механизм перетаскивания
    /// </remarks>
    public sealed class CustomCheckedlistBox : CheckedListBoxControl
    {
        private bool[] allowMove;
        private Rectangle dragBeginRect;

        private int dragSourceIndex, dragTargetIndex;

        private bool isDraging;

        public CustomCheckedlistBox()
        {
            SelectionMode = SelectionMode.None;
            TabStop = false;
            CheckOnClick = true;
            isDraging = false;
            MultiColumn = true;
            ItemCheck += CheckedListBoxItemCheck;
        }

        private bool IsDraging
        {
            get { return isDraging; }
            set
            {
                // ReSharper disable RedundantCheckBeforeAssignment
                if (isDraging != value)
                {
                    isDraging = value;
                }

                // ReSharper restore RedundantCheckBeforeAssignment
            }
        }

        private new CustomCheckedListBoxViewInfo ViewInfo
        {
            get { return base.ViewInfo as CustomCheckedListBoxViewInfo; }
        }

        private void CheckedListBoxItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.Index != 0)
            {
                return;
            }
            var checkState = Items[e.Index].CheckState;
            for (var i = 1; i < Items.Count; i++)
            {
                Items[i].CheckState = checkState;
            }
        }

        internal void SetAllowMoving(int index, bool value)
        {
            allowMove[index] = value;
        }

        public void CreateAllowMovingArray()
        {
            allowMove = new bool[Items.Count];
        }

        protected override BaseStyleControlViewInfo CreateViewInfo()
        {
            return new CustomCheckedListBoxViewInfo(this);
        }

        protected override BaseControlPainter CreatePainter()
        {
            return new CustomPainterCheckedListBox();
        }

        /// <summary>
        /// Добавить элемент в список
        /// </summary>
        /// <param name="item">Новый элемент</param>
        /// <returns>Позиция добавленного элемента в списке</returns>
        public int Add(object item)
        {
            if (Items.Count == 0)
                Items.Add("Скрыть/Показать всё");
            CreateAllowMovingArray();
            return Items.Add(item, true);
        }

        /// <summary>
        /// Добавить строку в список
        /// </summary>
        /// <param name="item">Новый элемент</param>
        /// <returns>Позиция добавленного элемента в списке</returns>
        public int AddUnique(object item)
        {
            if (Items.Count == 0)
                Items.Add("Скрыть/Показать всё");
            CreateAllowMovingArray();
            if (!Items.Cast<object>().Any(i => i.ToString().Equals(item.ToString())))
                return Items.Add(item, true);
            return -1;
        }

        public void AddRange(params object[] items)
        {
            if (Items.Count == 0)
                Items.Add("Скрыть/Показать всё");
            CreateAllowMovingArray();
            Items.AddRange(items);
        }

        #region Реализация перетаскивания элементов

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button != MouseButtons.Left)
            {
                return;
            }
            var index = IndexFromPoint(e.Location);
            if (index < 0 || index >= Items.Count || allowMove.Length < index || !allowMove[index])
            {
                return;
            }
            IsDraging = false;
            dragSourceIndex = index;
            dragTargetIndex = dragSourceIndex;
            if (dragSourceIndex != -1)
            {
                var dragSize = SystemInformation.DragSize;
                dragBeginRect = new Rectangle(new Point(e.X - (dragSize.Width / 2), e.Y - (dragSize.Height / 2)), dragSize);
            }
            else
            {
                dragBeginRect = Rectangle.Empty;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (!IsDraging || dragBeginRect == Rectangle.Empty)
            {
                base.OnMouseUp(e);
            }
            if (dragBeginRect == Rectangle.Empty)
            {
                return;
            }
            if (dragSourceIndex != -1 && dragTargetIndex != dragSourceIndex)
            {
                ChangeItemsPositionCore(dragSourceIndex, dragTargetIndex);
            }
            dragBeginRect = Rectangle.Empty;
            Refresh();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if ((e.Button & MouseButtons.Left) != MouseButtons.Left)
            {
                return;
            }
            if (dragBeginRect == Rectangle.Empty || dragBeginRect.Contains(e.X, e.Y) || allowMove == null)
            {
                return;
            }
            IsDraging = true;
            dragTargetIndex = IndexFromPoint(e.Location);
            if (dragTargetIndex == -1)
            {
                if (e.Y < ViewInfo.GetItemRectangle(0).Bottom)
                {
                    dragTargetIndex = 0;
                }
                else
                {
                    dragTargetIndex = Items.Count;
                }
            }
            var info = ViewInfo.GetItemByIndex(dragSourceIndex);
            if (info != null)
            {
                info.PaintAppearance.ForeColor = Color.Red;
            }
            ViewInfo.MarkItem(dragTargetIndex, dragSourceIndex);
        }

        private void ChangeItemsPositionCore(int source, int target)
        {
            if (target == -1 || target == 0)
            {
                return;
            }
            CorrectAllowMove(source, target);
            Items.Insert(target, Items[source]);
            if (source > target)
            {
                source++;
            }
            Items.RemoveAt(source);
        }

        private void CorrectAllowMove(int source, int target)
        {
            var b = allowMove[source];
            if (target == ItemCount)
            {
                target = Items.Count - 1;
            }
            if (target > source)
            {
                for (var i = source; i < target; i++)
                {
                    allowMove[i] = allowMove[i + 1];
                }
            }
            else
            {
                for (var i = source; i > target; i--)
                {
                    allowMove[i] = allowMove[i - 1];
                }
            }
            allowMove[target] = b;
        }

        #endregion

        public int[] GetCheckedCarNumbers()
        {
            var result = new List<int>();
            for (var i = 1; i < Items.Count; i++)
            {
                if (Items[i].CheckState == CheckState.Checked)
                    result.Add(Convert.ToInt32(Items[i].Value));
            }
            return result.ToArray();
        }
    }
}