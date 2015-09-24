using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace CommonLibrary.ControlTunerFolder
{
    public class OtherControlTuner : ControlTuner
    {
        public OtherControlTuner(object c)
            : base(c)
        {
            var control = (Control) c;
            Size = control.Size;
            PointToScreenLocation = control.Parent != null
                ? control.Parent.PointToScreen(control.Location)
                : control.PointToScreen(control.Location);
        }

        private Control Element
        {
            get { return Control as Control; }
        }

        public override sealed Size Size { get; set; }
        public override sealed Point PointToScreenLocation { get; set; }

        public override void TuneControl(object objectControl, XmlDocument xml)
        {
            base.TuneControl(objectControl, xml);
            if (Element == null || Element.Controls.Count == 0)
            {
                return;
            }
            foreach (Control c in Element.Controls)
            {
                c.GetControlTuner()
                    .TuneControl(c, xml);
            }
        }

        public override XElement GetPropertiesXml(object objectControl)
        {
            if (Element == null || string.IsNullOrEmpty(Element.Name))
            {
                return null;
            }
            var xml = new XElement(Element.Name, base.GetPropertiesXml(Element));
            foreach (Control c in Element.Controls)
            {
                xml.Add(c.GetControlTuner()
                    .GetPropertiesXml(c));
            }
            return xml;
        }

        public override void GetControlAdminInfo(object objectControl, DataTable dataTable)
        {
            base.GetControlAdminInfo(objectControl, dataTable);
            if (Element == null)
            {
                return;
            }
            foreach (Control c in Element.Controls)
            {
                c.GetControlTuner()
                    .GetControlAdminInfo(c, dataTable);
            }
        }
    }
}