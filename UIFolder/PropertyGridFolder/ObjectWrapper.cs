using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CommonLibrary.PropertyGridFolder
{
    internal class ObjectWrapper : ICustomTypeDescriptor
    {
        private PropertyDescriptorCollection globalizedProps;

        /// <summary>
        ///     Contain a reference to the collection of properties to show in the parent PropertyGrid.
        /// </summary>
        /// <remarks>By default, propertyDescriptors contain all the properties of the object. </remarks>
        private List<GlobalPropertyDescriptor> propertyDescriptors = new List<GlobalPropertyDescriptor>();

        /// <summary>Contain a reference to the selected objet that will linked to the parent PropertyGrid.</summary>
        private object selectedObject;

        /// <summary>Simple constructor.</summary>
        /// <param name="obj">A reference to the selected object that will linked to the parent PropertyGrid.</param>
        internal ObjectWrapper(object obj)
        {
            selectedObject = obj;
        }

        /// <summary>
        ///     Get or set a reference to the selected objet that will linked to the parent PropertyGrid.
        /// </summary>
        public object SelectedObject
        {
            get { return selectedObject; }
            set
            {
                if (selectedObject != null && selectedObject != value)
                {
                    selectedObject = value;
                }
            }
        }

        /// <summary>
        ///     Get or set a reference to the collection of properties to show in the parent PropertyGrid.
        /// </summary>
        public List<GlobalPropertyDescriptor> PropertyDescriptors
        {
            get { return propertyDescriptors; }
            set
            {
                propertyDescriptors = value;
                propertyDescriptors.Remove(null);
            }
        }

        #region ICustomTypeDescriptor Members

        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            // Get the collection of properties
            var baseProps = GetProperties();
            globalizedProps = new PropertyDescriptorCollection(null);

            // For each property use a property descriptor of our own that is able to be globalized
            for (var i = 0; i < baseProps.Count; i++)
            {
                globalizedProps.Add(new GlobalPropertyDescriptor(baseProps[i]));
            }
            return globalizedProps;
        }

        public PropertyDescriptorCollection GetProperties()
        {
            // Get the collection of properties
            var baseProps = new PropertyDescriptorCollection(propertyDescriptors.ToArray(), true);
            globalizedProps = new PropertyDescriptorCollection(null);

            // For each property use a property descriptor of our own that is able to be globalized
            foreach (PropertyDescriptor oProp in baseProps)
            {
                globalizedProps.Add(new GlobalPropertyDescriptor(oProp));
            }
            return globalizedProps;
        }

        public AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes(selectedObject, true);
        }

        public String GetClassName()
        {
            return TypeDescriptor.GetClassName(selectedObject, true);
        }

        public String GetComponentName()
        {
            return TypeDescriptor.GetComponentName(selectedObject, true);
        }

        public TypeConverter GetConverter()
        {
            return TypeDescriptor.GetConverter(selectedObject, true);
        }

        public EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(selectedObject, true);
        }

        public PropertyDescriptor GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(selectedObject, true);
        }

        public object GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }

        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(selectedObject, attributes, true);
        }

        public EventDescriptorCollection GetEvents()
        {
            return TypeDescriptor.GetEvents(selectedObject, true);
        }

        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return selectedObject;
        }

        #endregion
    }
}