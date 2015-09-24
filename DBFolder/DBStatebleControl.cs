using System;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Windows.Forms;
using CommonLibrary.ControlTunerFolder;
using CommonLibrary.UserFolder;
using DevExpress.Utils.Serializing;
using DevExpress.XtraGrid;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraTreeList;
using ServerInformation;

namespace CommonLibrary.LogicFolder
{
    /// <summary>
    ///     Класс для сохранения и загрузки настроек внешнего вида контролов, а также дополнительных функций, которые
    ///     поддерживают интерфейс
    ///     <see cref="T:DevExpress.Utils.Serializing.ISupportXtraSerializer" />
    /// </summary>
    public static class DBStatebleControl
    {
        /// <summary>
        ///     Вернуть внешний вид для объекта в значение по умолчанию
        /// </summary>
        /// <param name="form">форма</param>
        /// <param name="name">имя объекта</param>
        /// <param name="pc">Объект, для которого необходимо вернуть состояние</param>
        /// <returns>Возвращается массив байтов</returns>
        public static void SaveDefaultLayout(this Form form, string name, Control pc)
        {
            using (var db = new ApplicationEntitie(0))
            {
                if (form == null)
                {
                    throw new ArgumentException("Форма не задана");
                }
                var tableName = form.Name + "." + name;
                var layout = pc.GetLayoutData();
                var defLayout = db.DefaultLayouts.FirstOrDefault(dl => dl.TableName == tableName);
                if (defLayout == null)
                {
                    defLayout = new DefaultLayout
                    {
                        TableName = tableName,
                        Layout = layout
                    };
                    db.DefaultLayouts.Add(defLayout);
                }
                else if (!db.DefaultLayouts.Any(dl => dl.cs_Layout == SqlFunctions.Checksum(layout)))
                {
                    defLayout.Layout = layout;
                }
                db.SaveChanges();
            }
        }

        /// <summary>
        ///     Вернуть последний сохранённый Layout для объекта
        /// </summary>
        /// <param name="form">форма</param>
        /// <param name="name">имя объекта</param>
        /// <returns>Возвращается массив байтов</returns>
        public static byte[] GetLastLayout(this Form form, string name)
        {
            using (var db = new ApplicationEntitie(0))
            {
                if (form == null)
                {
                    throw new ArgumentException("Форма не задана");
                }
                var tableName = form.Name + "." + name;
                var userlayout = db.UserLayouts.FirstOrDefault(ul => ul.UserId == DBUser.Working.Id && ul.TableName == tableName && ul.LayoutType == 2);
                if (userlayout != null)
                {
                    return userlayout.Layout;
                }
            }
            return null;
        }

        /// <summary>
        ///     Сохранить текущий внешний вид для объекта класса
        ///     <see cref="T:DevExpress.XtraGrid.GridControl" />
        ///     в базу данных
        /// </summary>
        /// <param name="control">Контрол для сохранения настроек</param>
        public static void SaveLastLayout(this Control control)
        {
            var tableName = control.UniqueName();
            using (var db = new ApplicationEntitie(0))
            {
                var layout = control.GetLayoutData();
                var userLayout = db.UserLayouts.FirstOrDefault(ul => ul.UserId == DBUser.Working.Id && ul.TableName == tableName && ul.LayoutType == 2);
                if (userLayout != null)
                {
                    userLayout.Layout = layout;
                }
                else
                {
                    userLayout = new UserLayout
                    {
                        Layout = layout,
                        TableName = tableName,
                        LayoutType = 2,
                        UserId = DBUser.Working.Id
                    };
                    db.UserLayouts.Add(userLayout);
                }
                db.SaveChanges();
            }
        }

        /// <summary>
        ///     Применить настройку внешнего вида для объекта из таблицы настроек по умолчанию
        /// </summary>
        /// <param name="form">Форма на которой находится объект</param>
        /// <param name="name">Название объекта</param>
        /// <param name="control">Объект</param>
        public static void AcceptDefaultLayoutForControl(this Form form, string name, Control control)
        {
            ISupportXtraSerializer sxs = null;
            var gc = control as GridControl;
            if (gc != null)
            {
                sxs = gc.MainView;
            }
            else
            {
                var pv = control as PivotGridControl;
                if (pv != null)
                {
                    sxs = pv;
                }
                else
                {
                    var tl = control as TreeList;
                    if (tl != null)
                    {
                        sxs = tl;
                    }
                }
            }
            if (sxs == null)
            {
                return;
            }
            using (var db = new ApplicationEntitie(0))
            {
                var tableName = form.Name + "." + name;
                var defLayout = db.DefaultLayouts.FirstOrDefault(dl => dl.TableName == tableName);
                if (defLayout == null)
                {
                    MessageWindow.GetInstance("Для данной формы не найдено начальное состояние.");
                    return;
                }
                sxs.RestoreLayoutSerializingFromStream(defLayout.Layout);
            }
            var tuner = control.GetControlTuner() as ILayoutTuner;
            if (tuner != null)
            {
                tuner.LoadPropertiesFromXml(control, FormControls.XmlDocumentFromStrinList(form.GetXmlSettings()));
            }
        }
    }
}