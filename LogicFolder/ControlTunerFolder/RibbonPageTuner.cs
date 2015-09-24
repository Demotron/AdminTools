using System.Data;
using System.Drawing;
using System.Xml;
using System.Xml.Linq;
using DevExpress.XtraBars.Ribbon;
using ServerInformation;

namespace CommonLibrary.ControlTunerFolder
{
    public class RibbonPageTuner : ControlTuner
    {
        public RibbonPageTuner(object c)
            : base(c)
        {
            var control = (RibbonPage) c;
            if (control.PageInfo == null)
            {
                return;
            }
            var bound = control.PageInfo.Bounds;
            Size = new Size(bound.Width, bound.Height);
            PointToScreenLocation = control.Ribbon.PointToScreen(bound.Location);
        }

        public RibbonPage Page
        {
            get { return (RibbonPage) Control; }
        }

        public override sealed Size Size { get; set; }
        public override sealed Point PointToScreenLocation { get; set; }

        public override bool Visible
        {
            get { return ((RibbonPage) Control).Visible; }
        }

        public override void TuneControl(object objectControl, XmlDocument xml)
        {
            base.TuneControl(objectControl, xml);
            var page = objectControl as RibbonPage;
            if (page == null)
            {
                return;
            }
            foreach (var group in page.Groups)
            {
                @group.GetControlTuner()
                    .TuneControl(group, xml);
            }
        }

        public override XElement GetPropertiesXml(object objectControl)
        {
            var xml = new XElement(Page.Name, Page.PropertiesToXml());
            foreach (var group in Page.Groups)
            {
                xml.Add(@group.GetControlTuner()
                    .GetPropertiesXml(group));
            }
            return xml;
        }

        public override void GetControlAdminInfo(object objectControl, DataTable dataTable)
        {
            var parentName = !string.IsNullOrWhiteSpace(Page.Category.Name)
                ? Page.Category.Name
                : Page.Ribbon.Name;
            dataTable.Rows.Add(Page.Name, MainSettings.GetRussianName(Page.GetType()
                .Name), Page.Text, Page, parentName);
            if (Page.Groups.Count == 0)
            {
                return;
            }
            foreach (var group in Page.Groups)
            {
                @group.GetControlTuner()
                    .GetControlAdminInfo(group, dataTable);
            }
        }
    }
}