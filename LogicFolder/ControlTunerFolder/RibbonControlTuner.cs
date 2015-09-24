using System.Data;
using System.Drawing;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using DevExpress.XtraBars.Ribbon;
using ServerInformation;

namespace CommonLibrary.ControlTunerFolder
{
    public class RibbonControlTuner : ControlTuner
    {
        public RibbonControlTuner(object c)
            : base(c)
        {
            var control = (RibbonControl) c;
            Size = control.Size;
            PointToScreenLocation = control.PointToScreen(control.Location);
        }

        public RibbonControl Ribbon
        {
            get { return (RibbonControl) Control; }
        }

        public override sealed Size Size { get; set; }
        public override sealed Point PointToScreenLocation { get; set; }

        public override bool Visible
        {
            get { return ((RibbonControl) Control).Visible; }
        }

        public override void TuneControl(object objectControl, XmlDocument xml)
        {
            Ribbon.PropertyFromXml(xml);
            if (Ribbon.PageCategories.Count > 0)
            {
                foreach (var rbnCategory in Ribbon.PageCategories.OfType<RibbonPageCategory>())
                {
                    rbnCategory.GetControlTuner()
                        .TuneControl(rbnCategory, xml);
                }
            }
            if (Ribbon.Pages.Count > 0)
            {
                foreach (var rbnCtrl in Ribbon.Pages.OfType<RibbonPage>())
                {
                    rbnCtrl.GetControlTuner()
                        .TuneControl(rbnCtrl, xml);
                }
            }
            foreach (var rbnCtrl in Ribbon.Manager.Items)
            {
                rbnCtrl.GetControlTuner()
                    .TuneControl(rbnCtrl, xml);
            }
        }

        public override XElement GetPropertiesXml(object objectControl)
        {
            var xml = new XElement(Ribbon.Name, Ribbon.PropertiesToXml());
            if (Ribbon.PageCategories.Count > 0)
            {
                foreach (var rbnCategory in Ribbon.PageCategories.OfType<RibbonPageCategory>())
                {
                    xml.Add(rbnCategory.GetControlTuner()
                        .GetPropertiesXml(rbnCategory));
                }
            }
            if (Ribbon.Pages.Count > 0)
            {
                foreach (var rbnCtrl in Ribbon.Pages.OfType<RibbonPage>())
                {
                    xml.Add(rbnCtrl.GetControlTuner()
                        .GetPropertiesXml(rbnCtrl));
                }
            }
            if (Ribbon.Manager.Items.Count <= 0)
            {
                return xml;
            }
            foreach (var rbnCtrl in Ribbon.Manager.Items)
            {
                xml.Add(rbnCtrl.GetControlTuner()
                    .GetPropertiesXml(rbnCtrl));
            }
            return xml;
        }

        public override void GetControlAdminInfo(object objectControl, DataTable dataTable)
        {
            if (!Ribbon.Name.Equals(string.Empty))
            {
                var form = Ribbon.FindForm();
                if (form != null)
                {
                    dataTable.Rows.Add(Ribbon.Name, MainSettings.GetRussianName(Ribbon.GetType()
                        .Name),
                        Ribbon.Text, Ribbon, form.Name);
                }
            }
            foreach (var rbnCategory in Ribbon.PageCategories)
            {
                rbnCategory.GetControlTuner()
                    .GetControlAdminInfo(rbnCategory, dataTable);
            }
            foreach (var rbnPage in Ribbon.Pages)
            {
                rbnPage.GetControlTuner()
                    .GetControlAdminInfo(rbnPage, dataTable);
            }
            foreach (var rbnCtrl in Ribbon.Manager.Items)
            {
                rbnCtrl.GetControlTuner()
                    .GetControlAdminInfo(rbnCtrl, dataTable);
            }
        }
    }
}