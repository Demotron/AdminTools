using System.ComponentModel;
using DevExpress.Utils.Serializing;
using DevExpress.XtraGrid.Columns;

namespace CommonLibrary.GridControlFolder
{
    public class CommonGridColumn : GridColumn
    {
        [Description("Provides access to the column's options."), Category("Options"),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
         XtraSerializableProperty(XtraSerializationVisibility.Content, XtraSerializationFlags.DefaultValue)]
        public new CommonOptionsColumn OptionsColumn
        {
            get { return (CommonOptionsColumn) base.OptionsColumn; }
        }

        protected override OptionsColumn CreateOptionsColumn()
        {
            return new CommonOptionsColumn();
        }
    }
}