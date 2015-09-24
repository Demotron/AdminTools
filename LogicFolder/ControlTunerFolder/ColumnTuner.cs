using System.Data;
using System.Drawing;
using System.Xml;
using System.Xml.Linq;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraTreeList.Columns;
using ServerInformation;

namespace CommonLibrary.ControlTunerFolder
{
    public class ColumnTuner : ControlTuner
    {
        public ColumnTuner(object c)
            : base(c)
        {
            if (GColumn != null)
            {
                var info = (GridViewInfo) GColumn.View.GetViewInfo();
                var colinfo = info.ColumnsInfo[GColumn];
                if (colinfo == null)
                {
                    return;
                }
                Size = new Size(GColumn.VisibleWidth, GColumn.View.GridControl.Height);
                PointToScreenLocation = GColumn.View.GridControl.PointToScreen(new Point(colinfo.Bounds.X, 0));
            }
            else
            {
                if (TreeColumn == null)
                {
                    return;
                }
                var info = TreeColumn.TreeList.ViewInfo.ColumnsInfo[TreeColumn];
                if (info == null)
                {
                    return;
                }
                Size = new Size(TreeColumn.VisibleWidth, TreeColumn.TreeList.Height);
                PointToScreenLocation = TreeColumn.TreeList.PointToScreen(new Point(info.Bounds.X, 0));
            }
        }

        private GridColumn GColumn
        {
            get { return Control as GridColumn; }
        }

        private TreeListColumn TreeColumn
        {
            get { return Control as TreeListColumn; }
        }

        public override sealed Size Size { get; set; }
        public override sealed Point PointToScreenLocation { get; set; }

        public override bool Visible
        {
            get
            {
                if (GColumn != null)
                {
                    return GColumn.Visible &&
                           GColumn.View.IsVisible &&
                           GColumn.View.GridControl.Visible &&
                           GColumn.View.GridControl.Parent.GetControlTuner()
                               .Visible;
                }
                return TreeColumn != null &&
                       TreeColumn.Visible &&
                       TreeColumn.TreeList.Visible &&
                       TreeColumn.TreeList.Parent.GetControlTuner()
                           .Visible;
            }
        }

        public override void TuneControl(object objectControl, XmlDocument xml)
        {
            base.TuneControl(objectControl, xml);

//            var customView = GColumn as CustomGridColumn;
//            if (customView == null || !customView.Visible)
//                return;
//            ((CustomGridColumn)objectControl).OptionsColumn.ShowInCustomizationForm = false;
//            ((CustomGridColumn)objectControl).OptionsFilter.AllowFilter = false;
//            ((CustomGridColumn)objectControl).OptionsColumn.ShowInQuickHide = false;
        }

        public override XElement GetPropertiesXml(object objectControl)
        {
            return objectControl.PropertiesToXml();
        }

        public override void GetControlAdminInfo(object objectControl, DataTable dataTable)
        {
            if (GColumn != null)
            {
                dataTable.Rows.Add(GColumn.Name, MainSettings.GetRussianName(GColumn.GetType()
                    .Name),
                    string.IsNullOrEmpty(GColumn.Caption)
                        ? GColumn.FieldName
                        : GColumn.Caption, GColumn, GColumn.View.GridControl.Name);
            }
            else if (TreeColumn != null)
            {
                dataTable.Rows.Add(TreeColumn.Name, MainSettings.GetRussianName(TreeColumn.GetType()
                    .Name),
                    string.IsNullOrEmpty(TreeColumn.Caption)
                        ? TreeColumn.FieldName
                        : TreeColumn.Caption, TreeColumn, TreeColumn.TreeList.Name);
            }
        }
    }
}