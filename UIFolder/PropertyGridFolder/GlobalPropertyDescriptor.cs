using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using ServerInformation;

namespace CommonLibrary.PropertyGridFolder
{
    /// <summary>
    ///     GlobalizedPropertyDescriptor enhances the base class obtaining the display name for a property
    ///     from the resource.
    /// </summary>
    public class GlobalPropertyDescriptor : PropertyDescriptor
    {
        private string localizedName = "";
        private readonly PropertyDescriptor basePropertyDescriptor;

        public GlobalPropertyDescriptor(PropertyDescriptor _basePropertyDescriptor)
            : base(_basePropertyDescriptor)
        {
            // Set the property-descriptor where we work on.
            basePropertyDescriptor = _basePropertyDescriptor;
            var browsable = basePropertyDescriptor.Attributes[typeof (BrowsableAttribute)];
            var isBrowsable = browsable.GetType()
                .
                GetField("Browsable", BindingFlags.IgnoreCase | BindingFlags.NonPublic | BindingFlags.Instance);
            if (isBrowsable != null)
            {
                isBrowsable.SetValue(browsable, true);
            }
        }

        #region Abstract Overrides

        /// <summary>
        ///     Gets the displayname for a property
        /// </summary>
        public override string DisplayName
        {
            get
            {
                var displayNameKey = string.Empty;
                foreach (var oAttrib in basePropertyDescriptor.Attributes.Cast<Attribute>()
                    .
                    Where(oAttrib => oAttrib.GetType() == typeof (GlobalPropertyAttribute)))
                {
                    displayNameKey = ((GlobalPropertyAttribute) oAttrib).NameKey;
                    break;
                }
                if (string.IsNullOrEmpty(displayNameKey))
                {
                    displayNameKey = basePropertyDescriptor.DisplayName;
                }
                localizedName = MainSettings.GetRussianName(displayNameKey);
                if (string.IsNullOrEmpty(localizedName))
                {
                    localizedName = basePropertyDescriptor.DisplayName;
                }
                return localizedName;
            }
        }

        #endregion

        #region Abstract Dummy Methods

        public override Type ComponentType
        {
            get { return basePropertyDescriptor.ComponentType; }
        }

        public override bool IsReadOnly
        {
            get { return basePropertyDescriptor.IsReadOnly; }
        }

        public override bool IsBrowsable
        {
            get { return true; }
        }

        public override string Name
        {
            get { return basePropertyDescriptor.Name; }
        }

        public override Type PropertyType
        {
            get { return basePropertyDescriptor.PropertyType; }
        }

        public override string Description
        {
            get { return basePropertyDescriptor.Description; }
        }

        public override bool CanResetValue(object component)
        {
            return basePropertyDescriptor.CanResetValue(component);
        }

        public override object GetValue(object component)
        {
            return basePropertyDescriptor.GetValue(component);
        }

        public override void ResetValue(object component)
        {
            basePropertyDescriptor.ResetValue(component);
        }

        public override bool ShouldSerializeValue(object component)
        {
            return basePropertyDescriptor.ShouldSerializeValue(component);
        }

        public override void SetValue(object component, object value)
        {
            basePropertyDescriptor.SetValue(component, value);
        }

        #endregion
    }
}