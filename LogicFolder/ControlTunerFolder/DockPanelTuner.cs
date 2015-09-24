using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using DevExpress.XtraBars.Docking;

namespace CommonLibrary.ControlTunerFolder
{
    public class DockPanelTuner : ControlTuner
    {
        public DockPanelTuner(object c)
            : base(c)
        {
            var control = (Control) c;
            Size = control.Size;
            PointToScreenLocation = control.Parent != null
                ? control.Parent.PointToScreen(control.Location)
                : control.PointToScreen(control.Location);
        }

        public DockPanel Panel
        {
            get { return (DockPanel) Control; }
        }

        public override sealed Size Size { get; set; }
        public override sealed Point PointToScreenLocation { get; set; }

        public override void TuneControl(object objectControl, XmlDocument xml)
        {
            //foreach (Control c in Panel.Controls)
            //    c.GetControlTuner().TuneControl(c, xml);
        }

        public override XElement GetPropertiesXml(object objectControl)
        {
            /*var xml = new XElement(Panel.Name);
            foreach (Control c in Panel.Controls)
                xml.Add(c.GetControlTuner().GetPropertiesXml(c));
            return xml;*/
            return null;
        }

        public override void GetControlAdminInfo(object objectControl, DataTable dataTable)
        {
            /*base.GetControlAdminInfo(objectControl, dataTable);
            var control = objectControl as Control;
            if (control == null)
                return;
            foreach (Control c in control.Controls)
                c.GetControlTuner().GetControlAdminInfo(c, dataTable);*/
        }
    }
}