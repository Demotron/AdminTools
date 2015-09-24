using System;
using System.Data;
using System.Drawing;
using System.Xml;
using System.Xml.Linq;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using ServerInformation;

namespace CommonLibrary.ControlTunerFolder
{
    public class BarItemTuner : ControlTuner
    {
        public BarItemTuner(object c)
            : base(c)
        {
            UpdateSize();
        }

        public BarItem Item
        {
            get { return (BarItem) Control; }
        }

        public BarButtonItem ButtonItem
        {
            get { return Control as BarButtonItem; }
        }

        public override sealed Size Size { get; set; }
        public override sealed Point PointToScreenLocation { get; set; }

        public override bool Visible
        {
            get
            {
                if (Item.Visibility != BarItemVisibility.Always)
                {
                    return false;
                }
                var collection = Item.Links[0].Links as RibbonPageGroupItemLinkCollection;
                return collection == null || collection.PageGroup.Visible;
            }
        }

        /// <summary>
        ///     Обновить размер контрола
        /// </summary>
        private void UpdateSize()
        {
            if (Item.Links.Count == 0)
            {
                return;
            }
            var barItemLink = Item.Links[Item.Links.Count - 1];
            if (barItemLink == null)
            {
                return;
            }
            var bound = barItemLink.Bounds;
            Size = new Size(bound.Width, bound.Height);
            PointToScreenLocation = barItemLink.LinkPointToScreen(bound.Location);
        }

        public override void ActivateParent()
        {
            BarItem ownerItem = null;
            BarItemLink barItemLink = null;
            foreach (BarItemLink bil in Item.Links)
            {
                barItemLink = bil;
                while (!(barItemLink.Links is RibbonPageGroupItemLinkCollection))
                {
                    ownerItem = barItemLink.OwnerItem;
                    if (ownerItem == null || ownerItem.Links.Count == 0)
                    {
                        break;
                    }
                    barItemLink = ownerItem.Links[0];
                }
            }
            if (barItemLink.Links is RibbonPageGroupItemLinkCollection)
            {
                var itemsColl = (RibbonPageGroupItemLinkCollection) barItemLink.Links;
                itemsColl.PageGroup.Ribbon.SelectedPage = itemsColl.PageGroup.Page;
                if (ownerItem == null)
                {
                    return;
                }
                var barSubItemLink = ownerItem.Links[0] as BarSubItemLink;
                if (barSubItemLink != null)
                {
                    //Убрать подсветку при закрытии меню
                    HighlightControl.TopMost = true;
                    ((BarSubItem) ownerItem).CloseUp -= ClosedMenu;
                    ((BarSubItem) ownerItem).CloseUp += ClosedMenu;
                    barSubItemLink.OpenMenu();
                }
                UpdateSize();
            }
        }

        private static void ClosedMenu(object sender, EventArgs e)
        {
            HighlightControl.CloseForm();
        }

        public override void TuneControl(object objectControl, XmlDocument xml)
        {
            base.TuneControl(objectControl, xml);
            if (ButtonItem == null)
            {
                return;
            }
            var items = ButtonItem.DropDownControl as PopupMenu;
            if (items == null || items.ItemLinks.Count <= 0)
            {
                return;
            }
            foreach (BarBaseButtonItemLink c in items.ItemLinks)
            {
                c.GetControlTuner()
                    .TuneControl(c, xml);
            }
        }

        public override XElement GetPropertiesXml(object objectControl)
        {
            if (string.IsNullOrEmpty(Item.Name))
            {
                return null;
            }
            var xml = new XElement(Item.Name, objectControl.PropertiesToXml());
            if (ButtonItem == null)
            {
                return xml;
            }
            var items = ButtonItem.DropDownControl as PopupMenu;
            if (items == null || items.ItemLinks.Count <= 0)
            {
                return xml;
            }
            foreach (BarButtonItemLink c in items.ItemLinks)
            {
                xml.Add(c.GetControlTuner()
                    .GetPropertiesXml(c));
            }
            return xml;
        }

        public override void GetControlAdminInfo(object objectControl, DataTable dataTable)
        {
            if (Item.Name.Equals(string.Empty) || Item.Links.Count == 0)
            {
                return;
            }
            string parentName;
            if (Item.Links[Item.Links.Count - 1].OwnerItem != null)
            {
                parentName = Item.Links[Item.Links.Count - 1].OwnerItem.Name;
            }
            else
            {
                var links = Item.Links[Item.Links.Count - 1].Links as RibbonPageGroupItemLinkCollection;
                if (links == null)
                {
                    return;
                }
                parentName = ((RibbonPageGroupItemLinkCollection) (Item.Links[Item.Links.Count - 1].Links)).PageGroup.Name;
            }
            if (string.IsNullOrEmpty(parentName))
            {
                return;
            }
            dataTable.Rows.Add(Item.Name, MainSettings.GetRussianName(Item.GetType()
                .Name), Item.Caption, Item, parentName);
            if (ButtonItem == null)
            {
                return;
            }
            var items = ButtonItem.DropDownControl as PopupMenu;
            if (items == null || items.ItemLinks.Count <= 0)
            {
                return;
            }
            foreach (BarButtonItemLink c in items.ItemLinks)
            {
                c.GetControlTuner()
                    .GetControlAdminInfo(c, dataTable);
            }
        }
    }
}