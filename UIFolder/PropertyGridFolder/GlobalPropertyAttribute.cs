using System;

namespace CommonLibrary.PropertyGridFolder
{
    [AttributeUsage(AttributeTargets.Property)]
    public class GlobalPropertyAttribute : Attribute
    {
        private string resourceKey = "";

        public GlobalPropertyAttribute()
        {
            resourceKey = string.Empty;
        }

        public GlobalPropertyAttribute(string nameKey)
        {
            resourceKey = nameKey;
        }

        public String NameKey
        {
            get { return resourceKey; }
            set { resourceKey = value; }
        }
    }
}