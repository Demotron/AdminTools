using System.Data;
using System.Drawing;
using System.Xml.Linq;
using DevExpress.XtraBars;
using ServerInformation;

namespace CommonLibrary.ControlTunerFolder
{
    public class BarButtonItemLinkTuner : ControlTuner
    {
        public BarButtonItemLinkTuner(object c)
            : base(c)
        {
            var barButton = ((BarButtonItemLink) c);
            if (barButton == null)
            {
                return;
            }
            var bound = barButton.Bounds;
            Size = new Size(bound.Width, bound.Height);
            PointToScreenLocation = barButton.LinkPointToScreen(bound.Location);
        }

        public BarButtonItemLink Link
        {
            get { return (BarButtonItemLink) Control; }
        }

        /// <summary>
        ///     Получить размеры данного контрола
        /// </summary>
        public override sealed Size Size { get; set; }

        /// <summary>
        ///     Получить положение контрола на форме относительно экрана
        /// </summary>
        public override sealed Point PointToScreenLocation { get; set; }

        public override bool Visible
        {
            get { return ((BarButtonItem) Control).Visibility == BarItemVisibility.Always; }
        }

        public override XElement GetPropertiesXml(object objectControl)
        {
            if (Link.Item.Name.Equals(string.Empty))
            {
                return null;
            }
            return Link.Item.PropertiesToXml();
        }

        public override void GetControlAdminInfo(object objectControl, DataTable dataTable)
        {
            dataTable.Rows.Add(Link.Item.Name, MainSettings.GetRussianName(Link.Item.GetType()
                .Name),
                Link.Item.Caption, Link.Item, Link.OwnerItem.Name);
        }
    }
}