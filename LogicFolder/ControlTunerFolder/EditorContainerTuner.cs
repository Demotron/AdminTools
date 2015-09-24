using System.Data;
using System.Drawing;
using System.Xml;
using System.Xml.Linq;
using CommonLibrary.LogicFolder;
using DevExpress.XtraEditors.Container;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using ServerInformation;

namespace CommonLibrary.ControlTunerFolder
{
    public class EditorContainerTuner : ControlTuner,
        ILayoutTuner
    {
        public EditorContainerTuner(object c)
            : base(c)
        {
            Size = Editor.Size;
            PointToScreenLocation = Editor.PointToScreen(Editor.Location);
        }

        private EditorContainer Editor
        {
            get { return (EditorContainer)Control; }
        }

        private TreeList Tree
        {
            get { return Editor as TreeList; }
        }

        private GridControl Grid
        {
            get { return Editor as GridControl; }
        }

        private GridView View
        {
            get { return (GridView)Grid.MainView; }
        }

        public override sealed Size Size { get; set; }
        public override sealed Point PointToScreenLocation { get; set; }

        public void LoadPropertiesFromXml(object control, XmlDocument xml)
        {
            if (Grid == null)
            {
                return;
            }
            Grid.PropertyFromXml(xml);
            foreach (GridColumn column in View.Columns)
            {
                column.GetControlTuner().TuneControl(column, xml);
            }
        }

        public override void TuneControl(object objectControl, XmlDocument xml)
        {
            if (xml == null)
            {
                return;
            }
            objectControl.PropertyFromXml(xml);
            if (Grid != null)
            {
                foreach (GridColumn column in View.Columns)
                {
                    column.GetControlTuner().TuneControl(column, xml);
                }
            }
            else if (Tree != null)
            {
                foreach (TreeListColumn column in Tree.Columns)
                {
                    column.GetControlTuner().TuneControl(column, xml);
                }
            }
        }

        public override XElement GetPropertiesXml(object objectControl)
        {
            var xml = new XElement(Editor.Name, Editor.PropertiesToXml());
            if (Grid != null)
            {
                Grid.FindForm().SaveDefaultLayout(Grid.Name, Grid);
                foreach (GridColumn column in View.Columns)
                {
                    xml.Add(column.GetControlTuner().GetPropertiesXml(column));
                }
            }
            else if (Tree != null)
            {
                foreach (TreeListColumn column in Tree.Columns)
                {
                    xml.Add(column.GetControlTuner().GetPropertiesXml(column));
                }
            }
            return xml;
        }

        public override void GetControlAdminInfo(object objectControl, DataTable dataTable)
        {
            if (Grid != null)
            {
                dataTable.Rows.Add(Grid.Name, MainSettings.GetRussianName(Grid.GetType()
                    .Name),
                    Grid.Text, Grid, Grid.Parent.GetParentName());
                foreach (GridColumn column in View.Columns)
                {
                    column.GetControlTuner().GetControlAdminInfo(column, dataTable);
                }
            }
            else if (Tree != null)
            {
                dataTable.Rows.Add(Tree.Name, MainSettings.GetRussianName(Tree.GetType().Name),
                    Tree.Text, Tree, Tree.Parent.GetParentName());
                foreach (TreeListColumn column in Tree.Columns)
                {
                    column.GetControlTuner().GetControlAdminInfo(column, dataTable);
                }
            }
        }
    }
}