using System.Data;
using System.Drawing;
using System.Xml.Linq;
using DevExpress.XtraPivotGrid;
using ServerInformation;

namespace CommonLibrary.ControlTunerFolder
{
    /// <summary>
    ///     Класс описывает модификатор для колонок в объектах класса
    ///     <see cref="T:DevExpress.XtraPivotGrid.PivotGridControl" />
    /// </summary>
    public class PivotGridFieldTuner : ControlTuner
    {
        public PivotGridFieldTuner(object c)
            : base(c)
        {}

        public PivotGridField PivotField
        {
            get { return (PivotGridField) Control; }
        }

        public override sealed Size Size { get; set; }
        public override sealed Point PointToScreenLocation { get; set; }

        public override bool Visible
        {
            get { return ((PivotGridField) Control).Visible; }
        }

        public override XElement GetPropertiesXml(object objectControl)
        {
            return PivotField.PropertiesToXml();
        }

        public override void GetControlAdminInfo(object objectControl, DataTable dataTable)
        {
            dataTable.Rows.Add(PivotField.Name, MainSettings.GetRussianName(PivotField.GetType()
                .
                Name), PivotField.Caption, PivotField, PivotField.PivotGrid.Name);
        }
    }
}