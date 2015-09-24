using System.ComponentModel;
using DevExpress.Utils.Serializing;
using DevExpress.XtraTreeList.Columns;

namespace CommonLibrary.TreeListFolder
{
    public class CommonTreeListColumn : TreeListColumn
    {
        [Description("Provides access to the column's options."), Category("Options"),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
         XtraSerializableProperty(XtraSerializationVisibility.Content, XtraSerializationFlags.DefaultValue)]
        public new CommonTreeListOptionsColumn OptionsColumn
        {
            get { return (CommonTreeListOptionsColumn)base.OptionsColumn; }
        }

        protected override TreeListOptionsColumn CreateOptionsColumn()
        {
            return new CommonTreeListOptionsColumn();
        }
    }
}