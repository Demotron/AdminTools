using System.ComponentModel;
using DevExpress.Utils.Serializing;
using DevExpress.XtraTreeList.Columns;

namespace CommonLibrary.TreeListFolder
{
    public class CommonTreeListOptionsColumn : TreeListOptionsColumn
    {
        public CommonTreeListOptionsColumn()
        {
            AllowQuickHide = true;
            ShowInQuickHide = true;
        }

        [Description("Может ли быть колонка кастомизирована в QuickHide."), DefaultValue(true), XtraSerializableProperty]
        public bool AllowQuickHide { get; set; }

        [Description("Видна ли колонка в QuickHide."), DefaultValue(true), XtraSerializableProperty]
        public bool ShowInQuickHide { get; set; }
    }
}