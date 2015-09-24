using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using ServerInformation;

namespace CommonLibrary.ControlTunerFolder
{
    /// <summary>
    ///     Абстрактный класс который будет описывать модификатор контрола
    /// </summary>
    public abstract class ControlTuner
    {
        protected ControlTuner(object c)
        {
            Control = c;
        }

        /// <summary>
        ///     Указатель на пользовательский контрол
        /// </summary>
        public object Control { get; private set; }

        /// <summary>
        ///     Получить размеры данного контрола
        /// </summary>
        public abstract Size Size { get; set; }

        /// <summary>
        ///     Получить положение контрола на форме относительно экрана
        /// </summary>
        public abstract Point PointToScreenLocation { get; set; }

        /// <summary>
        ///     Виден ли контрол на форме в данный момент
        /// </summary>
        public virtual bool Visible
        {
            get
            {
                var control = Control as Control;
                if (control == null)
                {
                    return true;
                }
                var visible = control.Visible;
                control = control.Parent;
                while (visible && control != null)
                {
                    visible = control.Visible;
                    control = control.Parent;
                }
                return visible;
            }
        }

        /// <summary>
        ///     Перевести фокус ввода на предок текущего контрола
        /// </summary>
        public virtual void ActivateParent()
        {}

        /// <summary>
        ///     Модифицировать форму используя правила в таблице
        /// </summary>
        public virtual void TuneControl(object objectControl, XmlDocument xml)
        {
            objectControl.PropertyFromXml(xml);
        }

        /// <summary>
        ///     Получает состояние контрола в виде XML для сохранения в базу данных
        /// </summary>
        public virtual XElement GetPropertiesXml(object objectControl)
        {
            return objectControl.PropertiesToXml();
        }

        /// <summary>
        ///     Получает состояние контрола в виде таблицы для удобства отображения
        /// </summary>
        public virtual void GetControlAdminInfo(object objectControl, DataTable dataTable)
        {
            var control = objectControl as Control;
            if (control == null || control.Name.Equals(string.Empty))
            {
                return;
            }
            dataTable.Rows.Add(control.Name, MainSettings.GetRussianName(control.GetType()
                .Name),
                control.Text, control, control.Parent.GetParentName());
        }
    }
}