using DevExpress.XtraTreeList.Painter;

namespace CommonLibrary.TreeListFolder
{
    public class CommonTreeListPainter : TreeListPainter
    {
        private readonly CommonTreeList treeList;

        public CommonTreeListPainter(CommonTreeList _treeList)
        {
            treeList = _treeList;
        }

        public override ITreeListPaintHelper CreatePaintHelper()
        {
            return new CommonTreeListPaintHelper(treeList);
        }
    }
}