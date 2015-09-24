using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using CommonLibrary.AdminFolder;
using CommonLibrary.ControlTunerFolder;
using CommonLibrary.GridControlFolder;
using CommonLibrary.TreeListFolder;
using DevExpress.XtraEditors.Container;
using ServerInformation;
using ServerInformation.ExceptionFolder;

namespace CommonLibrary
{
    /// <summary>
    ///     Класс для работы с контролами формы
    /// </summary>
    public static class FormControls
    {
        /// <summary>
        ///     Объект главной формы приложения
        /// </summary>
        public static CommonParentForm MainForm { get; set; }

        private static DataTable DataTableSchema()
        {
            var result = new DataTable();
            result.Columns.Add(new DataColumn("Name")
            {
                DataType = typeof(string)
            });
            result.Columns.Add(new DataColumn("Type")
            {
                DataType = typeof(string)
            });
            result.Columns.Add(new DataColumn("Caption")
            {
                DataType = typeof(string)
            });
            result.Columns.Add(new DataColumn("Control")
            {
                DataType = typeof(object)
            });
            result.Columns.Add(new DataColumn("Parent")
            {
                DataType = typeof(string)
            });
            return result;
        }

        /// <summary>
        /// Получить название контрола вместе с именем формы
        /// </summary>
        /// <param name="control">Контрол</param>
        /// <returns>Название в формате "Имя_формы"."Имя_контрола"</returns>
        public static string UniqueName(this Control control)
        {
            var form = control.FindForm();
            if (form != null)
                return form.Name + "." + control.Name;
            throw new ArgumentException("Форма не задана");
        }

        /// <summary>
        ///     Получить имя для данного контрола, которое не должно быть пустым, если только это не форма
        /// </summary>
        public static string GetParentName(this Control control)
        {
            while (true)
            {
                if (control == null || control is Form)
                {
                    return String.Empty;
                }
                if (!control.Name.Equals(String.Empty))
                {
                    return control.Name;
                }
                control = control.Parent;
            }
        }

        /// <summary>
        ///     Получить таблицу со списком контролов, которые находятся на форме
        /// </summary>
        /// <param name="activeForm">форма, из которой необходимо загрузить контролы</param>
        public static DataTable GetDataTableControl(this Form activeForm)
        {
            var table = DataTableSchema();
            foreach (var control in activeForm.Controls.Cast<object>()
                .Where(control => !(control is MdiClient)))
            {
                control.GetControlTuner().GetControlAdminInfo(control, table);
            }
            return table;
        }

        /// <summary>
        ///     Сохранить свойства в виде xml
        /// </summary>
        /// <param name="instance">Объект элемента</param>
        /// <param name="name">Название элемента</param>
        public static XElement PropertiesToXml(this object instance, string name = null)
        {
            if (string.IsNullOrEmpty(name))
            {
                name = instance.GetType()
                    .GetProperty("Name")
                    .GetValue(instance, null)
                    .ToString();
            }
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }
            var element = new XElement(name);
            var properties = instance.GetType().GetProperties();
            foreach (var property in properties)
            {
                var visiblePropertie = MainSettings.GetPropertie(property.Name);
                if (visiblePropertie == null)
                {
                    continue;
                }
                element.Add(visiblePropertie.IsComplex
                    ? new XElement(property.GetValue(instance, null).PropertiesToXml(visiblePropertie.Name))
                    : new XElement(property.Name, property.GetValue(instance, null)));
            }
            return element;
        }

        /// <summary>
        ///     Загрузить свойства объекта из xml
        /// </summary>
        /// <param name="instance">Элемент управления</param>
        /// <param name="xml">Xml c настройками</param>
        /// <param name="name">Название компонента</param>
        public static void PropertyFromXml(this object instance, XmlDocument xml, string name = null)
        {
            if (xml == null)
            {
                return;
            }
            if (name == null)
            {
                name = instance.GetType()
                    .GetProperty("Name")
                    .GetValue(instance, null)
                    .ToString();
            }
            foreach (var xmlNode in xml.GetElementsByTagName(name)
                .Cast<XmlNode>()
                .Where(node => node != null))
            {
                var properties = instance.GetType()
                    .GetProperties();
                foreach (var property in properties)
                {
                    try
                    {
                        var visiblePropertie = MainSettings.GetPropertie(property.Name);
                        if (visiblePropertie == null || xmlNode[property.Name] == null)
                        {
                            continue;
                        }
                        if (visiblePropertie.IsComplex)
                        {
                            var xmldoc = new XmlDocument();
                            xmldoc.LoadXml(xmlNode.OuterXml);
                            property.GetValue(instance, null).PropertyFromXml(xmldoc, property.Name);
                        }
                        else
                        {
                            var typeConverter = TypeDescriptor.GetConverter(property.PropertyType);
                            var value = typeConverter.ConvertFromString(xmlNode[property.Name].InnerText);
                            property.SetValue(instance, value, null);
                        }
                    }
                    catch (Exception ex)
                    {
                        DBException.WriteLog(ex);
                    }
                }
            }
        }

        /// <summary>
        ///     Сохранить настройки формы в базу данных
        /// </summary>
        /// <param name="form">форма для сохранения</param>
        public static string SaveXmlFormRule(this Form form)
        {
            var xml = new StringBuilder();
            foreach (Control c in form.Controls)
            {
                xml.Append(c.GetControlTuner()
                    .GetPropertiesXml(c));
            }
            return "<Controls>" + xml + "</Controls>";
        }

        /// <summary>
        ///     Загрузить настройки для данной формы
        /// </summary>
        /// <param name="form">настраиваемая форма</param>
        /// <param name="xmlString">xml с настройками для формы</param>
        public static void LoadAllControlsState(this Form form, string xmlString)
        {
            form.LoadAllControlsState(new List<string>
            {
                xmlString
            });
        }

        /// <summary>
        ///     Загрузить настройки внешнего вида для формы
        /// </summary>
        /// <param name="form">форма</param>
        public static void LoadFormsControlsBeforeShow(this Form form)
        {
            try
            {
                form.DefaultFormState_InsertUpdate(form.SaveXmlFormRule());
                form.LoadLastFormSettings();
            }
            catch (Exception ex)
            {
                DBException.WriteLog(ex);
            }
        }

        /// <summary>
        /// Загрузить последние настройки интерфейса для формы
        /// </summary>
        /// <param name="form">форма</param>
        public static void LoadLastFormSettings(this Form form)
        {
            try
            {
                var xmlSettings = form.RolesRule_Load_FormName();
                form.SetXmlSettings(xmlSettings);
                form.LoadAllControlsState(xmlSettings);
            }
            catch (Exception ex)
            {
                DBException.WriteLog(ex);
            }
        }

        /// <summary>
        ///     Загрузить состояние контролов по умолчанию для формы
        /// </summary>
        /// <param name="form">Форма для загрузки настроек</param>
        public static void LoadDefaultStates(this Form form)
        {
            var defaultXml = form.DefaultFormState_Load_FormName();
            form.LoadAllControlsState(defaultXml);
        }

        /// <summary>
        ///     Настройки для формы в виде XML
        /// </summary>
        public static List<string> GetXmlSettings(this Form form)
        {
            var childForm = form as CommonChildForm;
            if (childForm != null)
            {
                return childForm.XmlSettings;
            }
            var parentForm = form as CommonParentForm;
            return parentForm != null ? parentForm.XmlSettings : new List<string>();
        }

        /// <summary>
        ///     Настройки для формы в виде XML
        /// </summary>
        private static void SetXmlSettings(this Form form, List<string> xml)
        {
            var childForm = form as CommonChildForm;
            if (childForm != null)
            {
                childForm.XmlSettings = xml;
            }
            var parentForm = form as CommonParentForm;
            if (parentForm != null)
            {
                parentForm.XmlSettings = xml;
            }
        }

        /// <summary>
        ///     Закоммитить изменения в DataSource у всех гридов и деревьев на форме
        /// </summary>
        public static void CommitFormsChanges(this Form form)
        {
            foreach (var editor in GetControls<EditorContainer>(form.Controls))
            {
                CommitEditorChanges(editor);
            }
        }

        /// <summary>
        ///     Загрузить настройки для данной формы
        /// </summary>
        /// <param name="form">настраиваемая форма</param>
        /// <param name="xmlStrings">xml-и с настройками для формы</param>
        private static void LoadAllControlsState(this Control form, ICollection<string> xmlStrings)
        {
            if (xmlStrings.Count == 0)
            {
                return;
            }
            try
            {
                var xmlResult = XmlDocumentFromStrinList(xmlStrings);
                foreach (Control c in form.Controls)
                {
                    c.GetControlTuner()
                        .TuneControl(c, xmlResult);
                }
            }
            catch (Exception e)
            {
                DBException.WriteLog(e);
            }
        }

        /// <summary>
        ///     Загрузить xml из списка строк
        /// </summary>
        /// <param name="xmlStrings">Список строк</param>
        public static XmlDocument XmlDocumentFromStrinList(ICollection<string> xmlStrings)
        {
            if (xmlStrings.Count == 0)
            {
                return null;
            }
            var xmls = new List<XmlDocument>();
            foreach (var xmlString in xmlStrings)
            {
                var xml = new XmlDocument();
                xml.LoadXml(xmlString);
                xmls.Add(xml);
            }
            var xmlResult = xmls.First();
            if (xmls.Count < 2)
            {
                return xmlResult;
            }
            for (var i = 1; i < xmls.Count; i++)
            {
                XmlNodeProceed(xmlResult.SelectSingleNode("Controls"), xmls[i].SelectSingleNode("Controls"));
            }
            return xmlResult;
        }

        /// <summary>
        ///     Пробегаемся по всем звеньям xml и сливаем их в одно общее правило
        /// </summary>
        /// <param name="xmlResult">Результирующая xml</param>
        /// <param name="xmlCurrent">Текущая xml</param>
        private static void XmlNodeProceed(XmlNode xmlResult, XmlNode xmlCurrent)
        {
            foreach (XmlNode xmlNode in xmlCurrent.ChildNodes)
            {
                if (!xmlNode.HasChildNodes)
                {
                    if (xmlNode.ParentNode == null)
                    {
                        continue;
                    }
                    var propertie = MainSettings.GetPropertie(xmlNode.ParentNode.Name);
                    if (propertie != null && MainSettings.CanMergeProperties(propertie.Name))
                    {
                        xmlResult.InnerText = MainSettings.MergeValues(xmlResult.InnerText, xmlNode.InnerText);
                    }
                }
                else
                {
                    var resultNode = xmlResult.SelectSingleNode(xmlNode.Name);
                    if (resultNode == null)
                    {
                        if (xmlResult.OwnerDocument == null)
                        {
                            continue;
                        }
                        var xmlNew = xmlResult.OwnerDocument.CreateElement(xmlNode.Name);
                        xmlNew.InnerXml = xmlNode.InnerXml;
                        xmlResult.AppendChild(xmlNew);
                        continue;
                    }
                    XmlNodeProceed(resultNode, xmlNode);
                }
            }
        }

        /// <summary>
        ///     Закоммитить изменения в DataSource у грида на форме
        /// </summary>
        private static void CommitEditorChanges(EditorContainer container)
        {
            var grid = container as CommonGridControl;
            if (grid != null)
            {
                grid.MainView.CommitGridViewChanges();
            }
            else
            {
                var tree = container as CommonTreeList;
                if (tree != null)
                {
                    tree.CommitTreeListChanges();
                }
            }
        }

        /// <summary>
        /// Вернуть список контролов, которые расположены на форме данного типа
        /// </summary>
        /// <typeparam name="T">Тип контролов</typeparam>
        /// <param name="controlCollection">Список всех контролов</param>
        public static IEnumerable<T> GetControls<T>(IEnumerable controlCollection) where T : class
        {
            var res = new List<T>();
            foreach (Control control in controlCollection)
            {
                var container = control as T;
                if (container != null)
                {
                    res.Add(container);
                }
                res.AddRange(GetControls<T>(control.Controls));
            }
            return res.ToArray();
        }

        /// <summary>
        ///     Создать новую зависимую форму
        /// </summary>
        /// <param name="args">Параметры, передаваемые на форму</param>
        public static T CreateChildForm<T>(params object[] args) where T : class
        {
            return MainForm.CreateChildForm<T>(args);
        }

        /// <summary>
        ///     Создать новую зависимую форму
        /// </summary>
        /// <param name="type">Тип создаваемой формы</param>
        /// <param name="args">Параметры, передаваемые на форму</param>
        public static CommonChildForm CreateChildForm(Type type, params object[] args)
        {
            return MainForm.CreateChildForm(type, args);
        }
    }
}