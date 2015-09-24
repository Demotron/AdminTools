using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ServerInformation;

namespace CommonLibrary.PropertyGridFolder
{
    /// <summary>
    ///     This class overrides the standard PropertyGrid provided by Microsoft.
    ///     It also allows to hide (or filter) the properties of the SelectedObject displayed by the PropertyGrid.
    /// </summary>
    public partial class FilterPropertyGrid : PropertyGrid
    {
        /// <summary>Contain a reference to the wrapper that contains the object to be displayed into the PropertyGrid.</summary>
        private ObjectWrapper wrapper;

        /// <summary>Contain a reference to the collection of properties to show in the parent PropertyGrid.</summary>
        /// <remarks>By default, propertyDescriptors contain all the properties of the object. </remarks>
        private readonly List<GlobalPropertyDescriptor> propertyDescriptors = new List<GlobalPropertyDescriptor>();

        /// <summary>Public constructor.</summary>
        public FilterPropertyGrid()
        {
            InitializeComponent();
            base.SelectedObject = wrapper;
        }

        /// <summary>Overwrite the PropertyGrid.SelectedObject property.</summary>
        /// <remarks>The object passed to the base PropertyGrid is the wrapper.</remarks>
        public new object SelectedObject
        {
            get { return wrapper != null ? ((ObjectWrapper) base.SelectedObject).SelectedObject : null; }
            set
            {
                // Set the new object to the wrapper and create one if necessary.
                if (wrapper == null)
                {
                    wrapper = new ObjectWrapper(value);
                    RefreshProperties();
                }

                //var needrefresh = value.GetType() != wrapper.SelectedObject.GetType();
                wrapper.SelectedObject = value;

                //if (needrefresh)
                RefreshProperties();

                // Set the list of properties to the wrapper.
                wrapper.PropertyDescriptors = propertyDescriptors;

                // Link the wrapper to the parent PropertyGrid.
                base.SelectedObject = wrapper;
            }
        }

        /// <summary>
        ///     Build the list of the properties to be displayed in the PropertyGrid, following the filters defined the
        ///     Browsable and Hidden properties.
        /// </summary>
        private void RefreshProperties()
        {
            if (wrapper == null)
            {
                return;
            }
            propertyDescriptors.Clear();

            // Get all the properties of the SelectedObject
            var allproperties = TypeDescriptor.GetProperties(wrapper.SelectedObject);
            if (MainSettings.VisibleProperties == null)
            {
                return;
            }
            foreach (var propertyname in MainSettings.VisibleProperties)
            {
                try
                {
                    if (allproperties[propertyname.Name] == null)
                    {
                        continue;
                    }
                    ShowProperty(new GlobalPropertyDescriptor(allproperties[propertyname.Name]));
                }
                catch (Exception)
                {
                    throw new ArgumentException("Property not found", propertyname.Name);
                }
            }

            // Set the list of properties to the wrapper.
            wrapper.PropertyDescriptors = propertyDescriptors;

            // Link the wrapper to the parent PropertyGrid.
            base.SelectedObject = wrapper;
        }

        /// <summary>Add a property to the list of properties to be displayed in the PropertyGrid.</summary>
        /// <param name="property">The property to be added.</param>
        private void ShowProperty(GlobalPropertyDescriptor property)
        {
            if (!propertyDescriptors.Contains(property))
            {
                propertyDescriptors.Add(property);
            }
        }
    }
}