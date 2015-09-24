using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Popup;

namespace CommonLibrary.ColumnsPopupFormFolder
{
    public sealed class QuickHidePopupForm : PopupContainerForm
    {
        private CustomCheckedlistBox listBox;

        public QuickHidePopupForm(PopupContainerEdit ownerEdit)
            : base(ownerEdit)
        {
            CreateChekedListBox();
        }

        private new RepositoryItemQuickHideEdit Properties
        {
            get { return (RepositoryItemQuickHideEdit)base.Properties; }
        }

        private void CreateChekedListBox()
        {
            listBox = new CustomCheckedlistBox();
            OwnerEdit.Properties.PopupControl.Controls.Add(listBox);
        }

        protected override void SetupButtons()
        {
            base.SetupButtons();
            fShowOkButton = true;
            fCloseButtonStyle = BlobCloseButtonStyle.Caption;
        }

        public void PopulateListBox()
        {
            listBox.Items.Clear();
            foreach (CustomColumnProperties column in Properties.Columns)
            {
                listBox.Items.Add(column.Caption, column.CheckState, column.AllowQuickHide);
            }
            listBox.CreateAllowMovingArray();
            for (var i = 0; i < listBox.ItemCount; i++)
            {
                listBox.SetAllowMoving(i, Properties.Columns[i].AllowMove);
            }
        }

        public ColumnPropertiesCollection GetCollumns(ColumnPropertiesCollection oldCollection)
        {
            var newCollection = new ColumnPropertiesCollection();
            foreach (CheckedListBoxItem item in listBox.Items)
            {
                newCollection.Add(item.Value.ToString(), item.CheckState == CheckState.Checked,
                    oldCollection[listBox.Items.IndexOf(item)].VisibleIndex, item.Enabled);
            }
            return newCollection;
        }

        protected override void UpdateControlPositionsCore()
        {
            base.UpdateControlPositionsCore();
            listBox.Bounds = ViewInfo.ContentRect;
            listBox.Width = listBox.Width - listBox.Left * 2;
            var btn = new SimpleButton
            {
                Size = new Size(OkButton.Size.Width * 2, OkButton.Size.Height),
                Location = new Point(10, OkButton.Location.Y),
                Anchor = AnchorStyles.Left | AnchorStyles.Bottom,
                Text = @"По алфавиту"
            };
            btn.Click += AbcSortButtonClick;
            Controls.Add(btn);
        }

        #region Сортировка по алфавиту

        private void AbcSortButtonClick(object sender, EventArgs e)
        {
            for (var i = 1; i < listBox.Items.Count - 1; i++)
            {
                for (var j = i + 1; j < listBox.Items.Count; j++)
                {
                    if (String.Compare(listBox.Items[i].ToString(), listBox.Items[j].ToString(), StringComparison.Ordinal) > 0)
                    {
                        ItemMove(j, i);
                    }
                }
            }
        }

        private void ItemMove(int source, int target)
        {
            if (target == -1)
            {
                listBox.Items.Add(listBox.Items[source]);
            }
            else
            {
                listBox.Items.Insert(target, listBox.Items[source]);
                if (source > target)
                {
                    source++;
                }
            }
            listBox.Items.RemoveAt(source);
        }

        #endregion
    }
}