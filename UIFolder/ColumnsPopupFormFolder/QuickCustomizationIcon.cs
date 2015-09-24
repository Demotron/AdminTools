using System.ComponentModel;
using System.Drawing;
using CommonLibrary.Properties;
using DevExpress.Utils.Controls;
using DevExpress.Utils.Serializing;

namespace CommonLibrary.ColumnsPopupFormFolder
{
    public class QuickCustomizationIcon : BaseOptions
    {
        public QuickCustomizationIcon()
        {
            TransperentColor = Color.Empty;
            Image = Resources.Customization;
        }
        
        [Description("Allow to chose image to show on QuickCustomisationButton"), XtraSerializableProperty]
        public Image Image { get; set; }

        [Description("Allow to chose transperent color for QuickCustumisationImage"), XtraSerializableProperty]
        public Color TransperentColor { get; set; }
    }
}