using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CommonLibrary.UserFolder;
using ServerInformation;
using ServerInformation.ExceptionFolder;

namespace CommonLibrary.AdminFolder
{
    public static class DBAdmin
    {
        /// <summary>
        ///     Получить настройки для ролей данного пользователя для заданной формы
        /// </summary>
        /// <param name="form">Заданная формы</param>
        /// <returns>Список xml с настройками</returns>
        public static List<string> RolesRule_Load_FormName(this Form form)
        {
            using (var dbAdmin = new ApplicationEntitie(0))
            {
                var rolesId = DBUser.Working.Roles.Select(r => r.Id);
                return (from rr in dbAdmin.RolesRules
                        where rolesId.Contains(rr.RoleId) && rr.FormName == form.Name
                        select rr.ControlsXML).ToList();
            }
        }

        /// <summary>
        ///     Загрузить настройки внешнего вида для формы
        /// </summary>
        /// <param name="form">форма</param>
        /// <param name="xml">Xml с настройками внешнего вида формы</param>
        public static void DefaultFormState_InsertUpdate(this Form form, string xml)
        {
            using (var dbAdmin = new ApplicationEntitie(0))
            {
                try
                {
                    if (!dbAdmin.DefaultFormStates.Any(f => f.FormName == form.Name))
                    {
                        var defauleRule = new DefaultFormState
                        {
                            FormName = form.Name,
                            DefaultXml = xml
                        };
                        dbAdmin.DefaultFormStates.Add(defauleRule);
                    }
                    else
                    {
                        dbAdmin.DefaultFormStates.First(f => f.FormName == form.Name)
                            .DefaultXml = xml;
                    }
                    dbAdmin.SaveChanges();
                }
                catch (Exception ex)
                {
                    DBException.WriteLog(ex);
                }
            }
        }

        /// <summary>
        ///     Загрузить состояние контролов по умолчанию для формы
        /// </summary>
        /// <param name="form">Форма для загрузки настроек</param>
        public static string DefaultFormState_Load_FormName(this Form form)
        {
            using (var dbAdmin = new ApplicationEntitie(0))
            {
                return dbAdmin.DefaultFormStates.Any(f => f.FormName == form.Name)
                    ? dbAdmin.DefaultFormStates.First(f => f.FormName == form.Name).DefaultXml
                    : null;
            }
        }

        /// <summary>
        /// Выбрать все роли в системе
        /// </summary>
        public static List<Role> Roles_SelectAll(this ApplicationEntitie db)
        {
            return db.Roles.ToList();
        }

        /// <summary>
        ///     Сохранить текущий внешний вид для объекта класса
        ///     <see cref="T:DevExpress.XtraGrid.GridControl" />
        ///     в базу данных
        /// </summary>
        /// <param name="formName">Имя формы</param>
        /// <param name="controlName">Имя контрола</param>
        /// <param name="layout">Массив настроек контрола</param>
        /// <param name="layoutType">Тип сохранения контрола</param>
        public static UserLayout UserLayout_InsertUpdate(string formName, string controlName, byte[] layout, int layoutType = 1)
        {
            UserLayout userLayout;
            var name = formName + "." + controlName;
            using (var db = new ApplicationEntitie(0))
            {
                userLayout = db.UserLayouts.FirstOrDefault(ul => ul.UserId == DBUser.Working.Id &&
                    ul.TableName == name && ul.LayoutType == layoutType);
                if (userLayout != null)
                {
                    userLayout.Layout = layout;
                }
                else
                {
                    userLayout = new UserLayout
                    {
                        Layout = layout,
                        TableName = name,
                        LayoutType = layoutType,
                        UserId = DBUser.Working.Id
                    };
                    db.UserLayouts.Add(userLayout);
                }
                db.SaveChanges();
            }
            return userLayout;
        }

        /// <summary>
        ///     Сохранить текущий внешний вид для объекта класса
        ///     <see cref="T:DevExpress.XtraGrid.GridControl" />
        ///     в базу данных
        /// </summary>
        /// <param name="formName">Имя формы</param>
        /// <param name="controlName">Имя контрола</param>
        /// <param name="layoutName">Название сохранённой настройки</param>
        /// <param name="layout">Массив настроек контрола</param>
        /// <param name="layoutType">Тип сохранения контрола</param>
        public static UserLayout UserLayout_Insert(string formName, string controlName, string layoutName, byte[] layout, int layoutType = 1)
        {
            UserLayout userLayout;
            var name = formName + "." + controlName;
            using (var db = new ApplicationEntitie(0))
            {
                userLayout = new UserLayout
                {
                    Layout = layout,
                    TableName = name,
                    LayoutType = layoutType,
                    LayoutName = layoutName,
                    UserId = DBUser.Working.Id
                };
                db.UserLayouts.Add(userLayout);
                db.SaveChanges();
            }
            return userLayout;
        }
    }
}