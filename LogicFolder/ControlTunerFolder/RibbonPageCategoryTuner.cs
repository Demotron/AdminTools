using System.Data;
using System.Drawing;
using System.Xml;
using System.Xml.Linq;
using DevExpress.XtraBars.Ribbon;
using ServerInformation;

namespace CommonLibrary.ControlTunerFolder
{
    public class RibbonPageCategoryTuner : ControlTuner
    {
        public RibbonPageCategoryTuner(object c)
            : base(c)
        {
            if (Category.CategoryInfo == null)
            {
                return;
            }
            var bound = Category.CategoryInfo.Bounds;
            Size = Category.CategoryInfo.Bounds.Size;
            PointToScreenLocation = Category.Ribbon.PointToScreen(bound.Location);
        }

        public RibbonPageCategory Category
        {
            get { return (RibbonPageCategory) Control; }
        }

        public override sealed Size Size { get; set; }
        public override sealed Point PointToScreenLocation { get; set; }

        public override bool Visible
        {
            get { return ((RibbonPageCategory) Control).Visible; }
        }

        public override void TuneControl(object objectControl, XmlDocument xml)
        {
            base.TuneControl(objectControl, xml);
            foreach (var page in Category.Pages)
            {
                page.GetControlTuner()
                    .TuneControl(page, xml);
            }
        }

        public override XElement GetPropertiesXml(object objectControl)
        {
            var xml = new XElement(Category.Name, objectControl.PropertiesToXml());
            foreach (var page in Category.Pages)
            {
                xml.Add(page.GetControlTuner()
                    .GetPropertiesXml(page));
            }
            return xml;
        }

        public override void GetControlAdminInfo(object objectControl, DataTable dataTable)
        {
            dataTable.Rows.Add(Category.Name, MainSettings.GetRussianName(Category.GetType()
                .Name), Category.Text, Category, Category.Ribbon.Name);
            foreach (var page in Category.Pages)
            {
                page.GetControlTuner()
                    .GetControlAdminInfo(page, dataTable);
            }
        }
    }
}