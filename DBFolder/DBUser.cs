using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ServerInformation;
using ServerInformation.ExceptionFolder;

namespace CommonLibrary.UserFolder
{
    /// <summary>
    ///     Класс, для получения информации о пользователе
    /// </summary>
    public static class DBUser
    {
        /// <summary>
        ///     Поле для обозначения того, что работа ведётся под админом и
        ///     запущена форма AdminTool для настройки свойств контролов
        /// </summary>
        public static bool DesignMode;

        private static User workingUser;

        /// <summary>
        ///     Текущий пользователь, под которым работает программа
        /// </summary>
        public static User Working
        {
            get { return workingUser; }
            set
            {
                if (value == null)
                {
                    return;
                }
                workingUser = value;
                DBException.WorkingId = workingUser.Id;
            }
        }

        /// <summary>
        /// Выход текущего пользователя из приложения
        /// </summary>
        public static void ExitWorkingUser()
        {
            workingUser = null;
        }

        /// <summary>
        ///     Является ли пользователь админом
        /// </summary>
        public static bool IsAdmin
        {
            get { return Working.Roles.Any(r => r.IsAdmin); }
        }

        public static User Current { get; set; }

        /// <summary>
        ///     Получить список ролей для текущего пользователя
        /// </summary>
        /// <returns>Строка с перечисленными ролями пользователя</returns>
        public static string GetRolesIdStrin(this User user)
        {
            var result = new StringBuilder();
            using (var db = new ApplicationEntitie(0))
            {
                var roles = db.Users.First(u => u.Id == user.Id).Roles;
                foreach (var role in roles)
                {
                    result.Append(role.Id + " ");
                }
            }
            return result.ToString();
        }

        /// <summary>
        ///     Получить список всех ролей из базы данных
        /// </summary>
        /// <param name="db">DBContext</param>
        /// <param name="formName">Название формы</param>
        /// <param name="roleId">Id роли</param>
        /// <returns>Настройки для формы</returns>
        public static string RolesRule_GetFormXML(this ApplicationEntitie db, string formName, int roleId)
        {
            string result = null;
            var roleRule = db.Where<RolesRule>(rr => rr.FormName.Equals(formName) && rr.RoleId == roleId).FirstOrDefault();
            if (roleRule == null)
            {
                if (db.Where<DefaultFormState>(f => f.FormName == formName).Any())
                {
                    result = db
                        .Where<DefaultFormState>(f => f.FormName == formName)
                        .First()
                        .DefaultXml;
                }
            }
            else
            {
                result = roleRule.ControlsXML;
            }
            return result;
        }

        /// <summary>
        ///     Получить список всех ролей из базы данных
        /// </summary>
        /// <param name="db">DBContext</param>
        /// <returns>Массив ролей в системе</returns>
        public static List<Role> Roles_SelectAll(this ApplicationEntitie db)
        {
            return db.Roles.ToList();
        }

        /// <summary>
        ///     Получить список всех ролей из базы данных вместе с пользователями
        /// </summary>
        /// <param name="db">DBContext</param>
        /// <returns>Массив ролей в системе</returns>
        public static List<Role> Roles_SelectAll_IncludeUsers(this ApplicationEntitie db)
        {
            return db.Roles
                .Include(r => r.Users)
                .ToList();
        }

        /// <summary>
        ///     Получить всех пользователей в виде массива
        /// </summary>
        /// <param name="db">DBContext</param>
        /// <returns>Массив объектов пользователей</returns>
        public static List<User> Users_SelectAll(this ApplicationEntitie db)
        {
            return db.Users.ToList();
        }

        /// <summary>
        ///     Получить всех пользователей в виде массива
        /// </summary>
        /// <returns>Массив объектов пользователей</returns>
        public static List<User> Users_SelectNotLocked()
        {
            using (var db = new ApplicationEntitie(0))
            {
                return db.Users
                    .Where(u => !u.IsLocked)
                    .ToList();
            }
        }

        /// <summary>
        ///     Получить пользователя по Id
        /// </summary>
        /// <param name="id">Id пользователя</param>
        /// <returns>Объект пользователя</returns>
        public static User Users_Select_Id(int id)
        {
            using (var db = new ApplicationEntitie(0))
            {
                return db.Users.Include(u => u.Roles)
                    .First(u => u.Id == id);
            }
        }

        /// <summary>
        ///     Получить пользователя по Id
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <param name="password">Новый пароль</param>
        /// <returns>Объект пользователя</returns>
        public static bool UpdatePassword(this User user, string password)
        {
            user.UserPassword = password;
            using (var db = new ApplicationEntitie(0))
            {
                db.Users.Find(user.Id)
                    .UserPassword = password;
                db.SaveChanges();
                return true;
            }
        }

        /// <summary>
        ///     Удалить пароль пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        public static bool Users_RemovePassword_Id(this User user)
        {
            if (XtraMessageBox.Show("Вы уверены, что хотите удалить пароль сотрудника?", "Удаление пароля",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return false;
            }
            user.UserPassword = null;
            using (var db = new ApplicationEntitie(0))
            {
                db.Users.Find(user.Id)
                    .UserPassword = null;
                db.SaveChanges();
                return true;
            }
        }
    }
}