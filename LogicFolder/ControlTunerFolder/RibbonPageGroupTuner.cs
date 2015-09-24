using System.Data;
using System.Drawing;
using System.Reflection;
using System.Xml.Linq;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.Ribbon.ViewInfo;
using ServerInformation;

namespace CommonLibrary.ControlTunerFolder
{
    public sealed class RibbonPageGroupTuner : ControlTuner
    {
        public RibbonPageGroupTuner(object c)
            : base(c)
        {}

        public RibbonPageGroup Group
        {
            get { return (RibbonPageGroup) Control; }
        }

        public override Size Size { get; set; }
        public override Point PointToScreenLocation { get; set; }

        public override bool Visible
        {
            get { return ((RibbonPageGroup) Control).Visible; }
        }

        private void UpdateSize()
        {
            var barItemLink = Group.ItemLinks[0];
            var pr = Group.GetType()
                .GetProperty("GroupInfo", BindingFlags.Instance | BindingFlags.NonPublic);
            var info = pr.GetValue(Group, null) as RibbonPageGroupViewInfo;
            if (barItemLink == null || info == null)
            {
                return;
            }
            Size = new Size(info.Bounds.Width, info.Bounds.Height);
            PointToScreenLocation = barItemLink.LinkPointToScreen(info.Bounds.Location);
        }

        public override void ActivateParent()
        {
            var pageGroup = Control as RibbonPageGroup;
            if (pageGroup == null)
            {
                return;
            }
            pageGroup.Ribbon.SelectedPage = pageGroup.Page;
            UpdateSize();
        }

        public override XElement GetPropertiesXml(object objectControl)
        {
            return new XElement(Group.Name, Group.PropertiesToXml());
        }

        public override void GetControlAdminInfo(object objectControl, DataTable dataTable)
        {
            dataTable.Rows.Add(Group.Name, MainSettings.GetRussianName(Group.GetType()
                .Name),
                Group.Text, Group, Group.Page.Name);
        }
    }
}