using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Dragging;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace CommonLibrary.GridControlFolder
{
    public class CommonGridDragManager : GridDragManager
    {
        public CommonGridDragManager(GridView view)
            : base(view)
        {}

        protected override PositionInfo CalcColumnDrag(GridHitInfo hit, GridColumn column)
        {
            var patchedPI = base.CalcColumnDrag(hit, column);
            if (patchedPI.Index != HideElementPosition || !patchedPI.Valid)
            {
                return patchedPI;
            }
            var col = column as CommonGridColumn;
            if (col == null)
            {
                return patchedPI;
            }
            if (!col.OptionsColumn.AllowQuickHide)
            {
                patchedPI = new PositionInfo
                {
                    Valid = false
                };
            }
            return patchedPI;
        }
    }
}