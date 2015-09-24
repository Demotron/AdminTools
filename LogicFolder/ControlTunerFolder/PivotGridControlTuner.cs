using System.Data;
using System.Drawing;
using System.Xml;
using System.Xml.Linq;
using CommonLibrary.LogicFolder;
using DevExpress.XtraPivotGrid;
using ServerInformation;

namespace CommonLibrary.ControlTunerFolder
{
    /// <summary>
    ///     Класс для загрузки пивота
    /// </summary>
    public class PivotGridControlTuner : ControlTuner,
        ILayoutTuner
    {
        public PivotGridControlTuner(object c)
            : base(c)
        {
            Size = Pivot.Size;
            PointToScreenLocation = Pivot.PointToScreen(Pivot.Location);
        }

        public PivotGridControl Pivot
        {
            get { return (PivotGridControl) Control; }
        }

        public override sealed Size Size { get; set; }
        public override sealed Point PointToScreenLocation { get; set; }

        public override bool Visible
        {
            get { return ((PivotGridControl) Control).Visible; }
        }

        public void LoadPropertiesFromXml(object control, XmlDocument xml)
        {
            //Настройка видимости и доступности данного грида
            Pivot.PropertyFromXml(xml);
            foreach (PivotGridField field in Pivot.Fields)
            {
                field.GetControlTuner()
                    .TuneControl(field, xml);
            }
        }

        public override void TuneControl(object objectControl, XmlDocument xml)
        {
            if (xml == null)
            {
                return;
            }

            //Настройка видимости и доступности данного грида
            Pivot.PropertyFromXml(xml);
            foreach (PivotGridField field in Pivot.Fields)
            {
                field.GetControlTuner()
                    .TuneControl(field, xml);
            }
        }

        public override XElement GetPropertiesXml(object objectControl)
        {
            Pivot.FindForm()
                .SaveDefaultLayout(Pivot.Name, Pivot);
            var xml = new XElement(Pivot.Name, Pivot.PropertiesToXml());
            foreach (PivotGridField field in Pivot.Fields)
            {
                xml.Add(field.GetControlTuner()
                    .GetPropertiesXml(field));
            }
            return xml;
        }

        public override void GetControlAdminInfo(object objectControl, DataTable dataTable)
        {
            if (!string.IsNullOrEmpty(Pivot.Name))
            {
                dataTable.Rows.Add(Pivot.Name, MainSettings.GetRussianName(Pivot.GetType()
                    .Name),
                    Pivot.Text, Pivot, Pivot.Parent.GetParentName());
            }
            foreach (PivotGridField field in Pivot.Fields)
            {
                field.GetControlTuner()
                    .GetControlAdminInfo(field, dataTable);
            }
        }
    }
}