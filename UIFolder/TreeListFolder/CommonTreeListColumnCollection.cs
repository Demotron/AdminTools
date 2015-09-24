using DevExpress.XtraTreeList.Columns;

namespace CommonLibrary.TreeListFolder
{
    public class CommonTreeListColumnCollection : TreeListColumnCollection
    {
        public CommonTreeListColumnCollection(CommonTreeList _treeList)
            : base(_treeList)
        {}

        protected override TreeListColumn CreateColumn()
        {
            return new CommonTreeListColumn();
        }
    }
}