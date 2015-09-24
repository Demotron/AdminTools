using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CommonLibrary.LogicFolder;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Registrator;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace CommonLibrary.GridControlFolder
{
    public class CommonGridControl : GridControl, IShowLayoutWorkMenu
    {
        /// <summary>
        ///     Информация о перетаскиваемых вагонах
        /// </summary>
        public GridHitInfo DownHitInfo;

        public new CommonGridView MainView
        {
            get { return (CommonGridView)base.MainView; }
            set { base.MainView = value; }
        }

        [DefaultValue(true)]
        public new bool UseEmbeddedNavigator
        {
            get { return base.UseEmbeddedNavigator; }
            set { base.UseEmbeddedNavigator = value; }
        }

        /// <summary>
        /// Нужно ли показывать в таблице меню для сохранения состояния
        /// </summary>
        [DefaultValue(true)]
        public bool ShowLayoutWorkMenu { get; set; }

        public CommonGridControl()
        {
            UseEmbeddedNavigator = true;
            ShowLayoutWorkMenu = true;
            MouseMove += GridMouseMove;
            DragEnter += GridDragEnter;
            // ReSharper disable once DoNotCallOverridableMethodsInConstructor
            EmbeddedNavigator.ButtonClick += EmbeddedNavigatorButtonClick;
        }

        protected override void RegisterAvailableViewsCore(InfoCollection collection)
        {
            base.RegisterAvailableViewsCore(collection);
            collection.Add(new CommonGridInfoRegistrator());
        }

        private void EmbeddedNavigatorButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            switch (e.Button.ButtonType)
            {
                case NavigatorButtonType.Remove:
                    MainView.RemoveRowsButtonClick();
                    e.Handled = true;  // disable the default processing
                    break;
                case NavigatorButtonType.Append:
                    MainView.AddRowButtonClick();
                    e.Handled = true;  // disable the default processing
                    break;
            }
        }

        protected override BaseView CreateDefaultView()
        {
            return CreateView("CustomGridView");
        }

        /// <summary>
        ///     Картинка курсора при перемещении выделенных вагонов по дереву
        /// </summary>
        private void GridMouseMove(object sender, MouseEventArgs e)
        {
            if (!AllowDrop || e.Button != MouseButtons.Left || DownHitInfo == null)
            {
                return;
            }
            var dragSize = SystemInformation.DragSize;
            var dragRect = new Rectangle(new Point(DownHitInfo.HitPoint.X - dragSize.Width / 2,
                DownHitInfo.HitPoint.Y - dragSize.Height / 2), dragSize);
            if (dragRect.Contains(new Point(e.X, e.Y)))
            {
                return;
            }
            MainView.EscapeStartedSelectionByMove();
            DoDragDrop(true, DragDropEffects.Move);
            DXMouseEventArgs.GetMouseArgs(e).Handled = true;
        }

        private void GridDragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        public int AdjustGridMinHeight()
        {
            var viewInfo = MainView.GetCommonViewInfo();
            return viewInfo.ColumnRowHeight + viewInfo.VisibleRowsHeight + 4;
        }

        public bool GetShowLayoutWorkMenu()
        {
            return ShowLayoutWorkMenu;
        }
    }
}