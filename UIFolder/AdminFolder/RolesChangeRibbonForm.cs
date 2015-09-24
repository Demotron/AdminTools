using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using CommonLibrary.Properties;
using CommonLibrary.UIFolder.GridControlFolder;
using CommonLibrary.UserFolder;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ServerInformation;
using ServerInformation.ExceptionFolder;

namespace CommonLibrary.AdminFolder
{
    /// <summary>
    ///     Форма настройки ролей и пользователей в программе
    /// </summary>
    /// <remarks>Реализована с использованием паттерна проектирования Одиночка</remarks>
    /// <a href="https://ru.wikipedia.org/wiki/Одиночка_(шаблон_проектирования)">Одиночка(шаблон проектирования)</a>
    public partial class RolesChangeRibbonForm : CommonChildForm
    {
        /// <summary>
        /// Роль, на которой фокус в таблице ролей
        /// </summary>
        private Role CurrentRole
        {
            get { return (Role)bsRole.Current; }
        }

        /// <summary>
        /// Пользователь, на котором фокус в таблице пользователей
        /// </summary>
        private User CurrentUser
        {
            get { return (User)bsUser.Current; }
        }

        public RolesChangeRibbonForm()
        {
            InitializeComponent();
            cgvAllUsers.SetIndicatorImage(Resources.Lock);
            cgvAllUsers.CustomDrawRowIndicator += cgvAllUsers.ImageCustomDrawRowIndicator;
            cgvRole.SetIndicatorImage(Resources.Admin);
            cgvRole.CustomDrawRowIndicator += cgvRole.ImageCustomDrawRowIndicator;
        }

        private void RolesChangeRibbonFormLoad(object sender, EventArgs e)
        {
            //События для пользователей
            cgvAllUsers.CustomPopupMenu = pmUsers;
            cgvUsers.DoAfterDeleteRow += DeleteUserFromRole;
        }

        private void RolesChangeRibbonFormShown(object sender, EventArgs e)
        {
            RefreshData();

            //Не показывать пользователей, которые заблокированы
            colIsLocked.FilterInfo = new ColumnFilterInfo(ColumnFilterType.Value, 0, null);
        }

        private bool RoleBeforeDelete(object[] objects)
        {
            if (!objects.Cast<Role>().Any(r => r.IsAdmin))
            {
                return true;
            }
            MessageWindow.GetInstance("Вы не можете удалить роль с правами администратора.", MessageType.Attention);
            return false;
        }

        #region IActionButton

        public override void RefreshData()
        {
            base.RefreshData();
            ShowWaitForm();
            try
            {
                bsUser.DataSource = DBAppContext.Users_SelectAll();
                bsRole.DataSource = DBAppContext.Roles_SelectAll_IncludeUsers();
            }
            catch (Exception ex)
            {
                DBException.WriteLog(ex);
            }
            finally
            {
                CloseWaitForm();
            }
        }

        #endregion

        #region Работа с таблицей добавленных к роли сотрудников

        /// <summary>
        ///     Удалить выделенного пользователя из роли
        /// </summary>
        private void DeleteUserFromRole(params object[] objects)
        {
            try
            {
                foreach (var user in objects.Cast<User>())
                {
                    CurrentRole.Users.Remove(user);
                }
            }
            catch (Exception ex)
            {
                DBException.WriteLog(ex);
            }
        }

        /// <summary>
        ///     Событие при попадании фокуса в ячейку таблицы gcUsers, если ячейка доступна для редактирования
        /// </summary>
        /// <remarks>
        ///     При клике пользователя на строку добавления сотрудников к роли,
        ///     показывается CheckedComboBox с пользователями, ещё не относящимися к данной роли. В данном
        ///     методе находят все сотрудники не из текущей роли и формируется список, если все сотрудники
        ///     добавлены, то показывается сообщение в виде ToolTip
        /// </remarks>
        private void UsersShowingEditor(object sender, CancelEventArgs e)
        {
            if (cgvUsers.FocusedRowHandle == GridControl.NewItemRowHandle)
            {
                if (cgvUsers.FocusedColumn == colSurnameInitials)
                {
                    var userIds = CurrentRole.Users
                        .Select(u => u.Id)
                        .ToList();
                    var queue = DBAppContext
                        .Where<User>(u => !userIds.Contains(u.Id) && !u.IsLocked)
                        .OrderBy(u => u.SurnameInitials);
                    if (queue.Any())
                    {
                        riccbeUsers.DataSource = queue.ToList();
                    }
                    else
                    {
                        MessageWindow.GetInstance("К данной роли добавлены все пользователи", MessageType.Attention);
                        e.Cancel = true;
                    }
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else
            {
                if (cgvUsers.FocusedColumn == colSurnameInitials)
                {
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        ///     Добавление выбранных в CheckedComboBox пользователей к текущей роли
        /// </summary>
        private void UsersEditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(riccbeUsers.GetCheckedItems().ToString()))
            {
                return;
            }
            var query = riccbeUsers.Items.Cast<CheckedListBoxItem>()
                .Where(t => t.CheckState == CheckState.Checked)
                .Select(i => (int)i.Value)
                .SelectMany(pId => DBAppContext.Where<User>(p => p.Id == pId))
                .ToList();
            cgvUsers.BeginDataUpdate();
            try
            {
                foreach (var user in query)
                {
                    CurrentRole.Users.Add(user);
                    bsRoleUsers.Add(user);
                }
            }
            catch (Exception ex)
            {
                DBException.WriteLog(ex);
            }
            finally
            {
                cgvUsers.EndDataUpdate();
            }
        }

        /// <summary>
        ///     Настройка строки добавления пользователей к роли в таблице gcUsers
        /// </summary>
        /// <remarks>
        ///     При клике на строку добавления нового пользователя в роль
        ///     появляется список всех пользователей, не добавленных к данной роли, таким образом
        ///     можно добавить сразу несколько сотрудников в роль вместо одного при стандартной обработке
        /// </remarks>
        private void UsersCustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (e.RowHandle != GridControl.NewItemRowHandle)
            {
                return;
            }
            e.RepositoryItem = e.Column != colSurnameInitials ? new RepositoryItem() : riccbeUsers;
        }

        #endregion

        #region Работа с таблицей со списком всех пользователей

        private void AllUsersValidateRow(object sender, ValidateRowEventArgs e)
        {
            var user = e.Row as User;
            if (user == null)
            {
                return;
            }
            if (!user.SurnameInitials.IsEmptyAfterTrim())
            {
                return;
            }
            user.SurnameInitials = user.LastName;
            if (!user.FirstName.IsEmptyAfterTrim())
                user.SurnameInitials += " " + user.FirstName[0];
            if (!user.MiddleName.IsEmptyAfterTrim())
                user.SurnameInitials += "." + user.MiddleName[0] + ".";
        }

        private void UsersBeforePopup(object sender, CancelEventArgs e)
        {
            var user = (User)cgvAllUsers.GetFocusedRow();
            if (user.IsLocked)
            {
                bbiUnlock.Visibility = BarItemVisibility.Always;
                bbiLockUser.Visibility = BarItemVisibility.Never;
            }
            else
            {
                bbiUnlock.Visibility = BarItemVisibility.Never;
                bbiLockUser.Visibility = BarItemVisibility.Always;
            }
        }

        private void AllUsersCellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column != colIsLocked)
            {
                return;
            }
            if ((bool)e.Value)
            {
                LockUser();
            }
            else
            {
                UnlockUser();
            }
        }

        private void LockUserClick(object sender, ItemClickEventArgs e)
        {
            LockUser();
        }

        private void UnlockUserClick(object sender, ItemClickEventArgs e)
        {
            UnlockUser();
        }

        private void LockUser()
        {
            CurrentUser.IsLocked = true;
            MessageWindow.GetInstance("Пользователь заблокирован.", MessageType.Info);
        }

        private void UnlockUser()
        {
            CurrentUser.IsLocked = false;
            MessageWindow.GetInstance("Пользователь разблокирован.", MessageType.Info);
        }

        private void CheckBeforeDrawImage(object sender, EventRowHandlerArgs e)
        {
            var user = (User)((GridView)sender).GetRow(e.RowHandler);
            e.Cancel = user == null || !user.IsLocked;
        }

        private void RoleDoCheckBeforeDrawIndicator(object sender, EventRowHandlerArgs e)
        {
            var role = (Role)((GridView)sender).GetRow(e.RowHandler);
            e.Cancel = role == null || !role.IsAdmin;
        }

        /// <summary>
        ///     Показать <see cref="T:PopupMenu" /> для данного грида
        /// </summary>
        private void UserPopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            var hitInfo = cgvAllUsers.CalcHitInfo(e.Point);
            if (hitInfo == null)
            {
                return;
            }
            bbiChangePassword.Enabled = cgvAllUsers.RowCount != 0;
            bbiRemovePassword.Enabled = cgvAllUsers.RowCount != 0;
            bbiLockUser.Enabled = cgvAllUsers.RowCount != 0;
            bbiUnlock.Enabled = cgvAllUsers.RowCount != 0;
            pmUsers.ShowPopup(cgcAllUsers.PointToScreen(e.Point));
        }

        private void NewPasswordUserClick(object sender, ItemClickEventArgs e)
        {

            try
            {
                var user = DBUser.Users_Select_Id(CurrentUser.Id);
                if (user.UpdatePassword(new ChangePasswordForm().ChangePassword(user.UserPassword, true)))
                {
                    MessageWindow.GetInstance("Пароль успешно изменён.", MessageType.Info);
                }
            }
            catch (Exception)
            {
                MessageWindow.GetInstance("Пожалуйста, сохраните изменения!", MessageType.Attention);
            }
        }

        private void RemovePasswordUserClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (CurrentUser.Users_RemovePassword_Id())
                {
                    MessageWindow.GetInstance("Пароль успешно удалён.", MessageType.Info);
                }
            }
            catch (Exception)
            {
                MessageWindow.GetInstance("Пожалуйста, сохраните изменения!", MessageType.Attention);
            }
        }

        #endregion

        private void RoleCurrentChanged(object sender, EventArgs e)
        {
            bsRoleUsers.DataSource = ((Role)bsRole.Current).Users.ToList();
        }
    }
}