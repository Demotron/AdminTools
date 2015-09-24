using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;

namespace CommonLibrary.GridControlFolder
{
    public class CommonGridColumnCollection : GridColumnCollection
    {
        public CommonGridColumnCollection(ColumnView view)
            : base(view)
        {}

        protected override GridColumn CreateColumn()
        {
            return new CommonGridColumn();
        }
    }
}