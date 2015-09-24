using System.Xml;

namespace CommonLibrary.ControlTunerFolder
{
    public interface ILayoutTuner
    {
        void LoadPropertiesFromXml(object gc, XmlDocument xml);
    }
}