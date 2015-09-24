using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CommonLibrary.ControlTunerFolder;
using CommonLibrary.UserFolder;
using DevExpress.XtraBars;
using DevExpress.XtraTreeList;
using ServerInformation;
using ServerInformation.ExceptionFolder;

namespace CommonLibrary.AdminFolder
{
    /// <summary>
    ///     Форма настройки свойств контролов для форм в приложении
    /// </summary>
    /// <remarks>Реализована с использованием паттерна проектирования Одиночка</remarks>
    /// <a href="https://ru.wikipedia.org/wiki/Одиночка_(шаблон_проектирования)">Одиночка(шаблон проектирования)</a>
    public partial class AdminTool : CommonChildForm
    {
        public static AdminTool Instance;

        /// <summary>
        ///     Были ли внесены изменения для текущей формы
        /// </summary>
        private bool isChangedProperty;

        /// <summary>
        ///     Старые размеры главной формы
        /// </summary>
        private readonly Rectangle parentBound;

        /// <summary>
        ///     Старое состояние главной формы
        /// </summary>
        private readonly FormWindowState parentState;

        private AdminTool(string formName)
        {
            InitializeComponent();

            DBUser.DesignMode = true;

            //Помещаем окно редактора контролов справа на экране
            var screenBound = Screen.FromControl(FormControls.MainForm).WorkingArea;
            Size = new Size(Width, screenBound.Height);
            Location = new Point(screenBound.Location.X + screenBound.Width - Size.Width, 0);

            //Запоминаем старые данные для главной формы
            parentBound = new Rectangle(FormControls.MainForm.Location, FormControls.MainForm.Size);
            parentState = FormControls.MainForm.WindowState;

            //Загружаем список всех запущенных форм в выпадающий список
            bsForms.DataSource = Application.OpenForms
                .OfType<CommonChildForm>()
                .Where(form => form.SaveState && form != this)
                .Cast<Form>()
                .Union(new[]
                {
                    FormControls.MainForm
                })
                .Select(f => new KeyValuePair<string, string>(f.Name, f.Text))
                .ToList();
            lueForms.EditValue = formName;
        }

        private new Form ActiveForm
        {
            get
            {
                return Application
                    .OpenForms
                    .Cast<Form>()
                    .FirstOrDefault(f => f.Name.Equals(lueForms.EditValue));
            }
        }

        public static void GetInstace(Form activeForm)
        {
            Instance = Instance ?? (Instance = new AdminTool(activeForm.Name));
            Instance.Show();
        }

        private void AdminToolLoad(object sender, EventArgs e)
        {
            //Помещаем форму в левую часть экрана
            var screenBound = Screen.FromControl(FormControls.MainForm).WorkingArea;
            FormControls.MainForm.WindowState = FormWindowState.Normal;
            FormControls.MainForm.Size = new Size(screenBound.Width - Size.Width, screenBound.Height);
            FormControls.MainForm.Location = screenBound.Location;
        }

        private void AdminToolShown(object sender, EventArgs e)
        {
            try
            {
                Focus();

                //Загружаем список всех ролей в выпадающий список
#if DEBUG
                lueRoles.Properties.DataSource = DBAppContext.Roles_SelectAll();
#else
                lueRoles.Properties.DataSource = DBAppContext.Roles_SelectAll()
                    .Where(r => !r.IsAdmin)
                    .ToArray();
#endif
            }
            catch (Exception ex)
            {
                DBException.WriteLog(ex);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
            MessageWindow.GetInstance(@"Выберите роль для загрузки настроек", MessageType.Info);
            CloseWaitForm();
        }

        private void FormsEditValueChanged(object sender, EventArgs e)
        {
            ActiveForm.Activate();
            LoadRolesForActiveForm();
        }

        private void RolesEditValueChanged(object sender, EventArgs e)
        {
            LoadRolesForActiveForm();
        }

        /// <summary>
        ///     Подсвечиваем контрол на форме при фокусе его в дереве
        /// </summary>
        private void ControlsFocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            if (e.Node.Id == TreeList.AutoFilterNodeId)
            {
                return;
            }
            FocuseControl(((DataRowView)tlControls.GetDataRecordByNode(e.Node)).Row["Control"].GetControlTuner());
        }

        /// <summary>
        ///     Перерисуем контрол при изменении его свойств в PropertyGrid
        /// </summary>
        private void FilterPropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            isChangedProperty = true;
            if (e.ChangedItem == null || e.ChangedItem.Label == null)
            {
                return;
            }
            var row = ((DataRowView)tlControls.GetDataRecordByNode(tlControls.FocusedNode));
            if (row == null)
            {
                return;
            }
            var control = row["Control"];
            FocuseControl(control.GetControlTuner());
        }

        /// <summary>
        ///     Выделенный в дереве контрол необходимо активировать и подсветить на форме
        /// </summary>
        private void FocuseControl(ControlTuner control)
        {
            control.ActivateParent();
            pgFilter.SelectedObject = control.Control;
            pgFilter.Enabled = lueRoles.EditValue != null && ActiveForm != null;
            if (!control.Visible)
            {
                HighlightControl.CloseForm();
                return;
            }
            var activeSize = control.Size;
            var activeLocation = control.PointToScreenLocation;
            HighlightControl.GetInstance(activeLocation.X, activeLocation.Y, activeSize.Width, activeSize.Height);
        }

        /// <summary>
        ///     При изменении активной формы необходимо обновить таблицу контролов
        /// </summary>
        /// <param name="_activeForm">Активная форма</param>
        public void ChangeActiveForm(Form _activeForm)
        {
            if (bsForms.List.Cast<KeyValuePair<string, string>>()
                .All(f => f.Key != _activeForm.Name))
            {
                bsForms.Add(new KeyValuePair<string, string>(_activeForm.Name, _activeForm.Text));
            }
            lueForms.EditValue = _activeForm.Name;
        }

        /// <summary>
        ///     При изменении активной формы необходимо обновить таблицу контролов
        /// </summary>
        public void CloseActiveForm(Form form)
        {
            if (!bsForms.List.Cast<KeyValuePair<string, string>>()
                .Any(obj => obj.Key.Equals(form.Name)))
            {
                return;
            }
            tlControls.DataSource = null;
            var item = bsForms.List.Cast<KeyValuePair<string, string>>()
                .First(obj => obj.Key.Equals(form.Name));
            bsForms.Remove(item);
        }

        /// <summary>
        ///     Загрузить настройки роли для активной формы
        /// </summary>
        private void LoadRolesForActiveForm()
        {
            if (lueRoles.EditValue == null || ActiveForm == null)
            {
                return;
            }
            HighlightControl.IsPaint = false;
            bbiSave.Enabled = true;
            bbiDefault.Enabled = true;
            bbiRestore.Enabled = true;
            try
            {
                var name = ActiveForm.Name;
                tlControls.DataSource = ActiveForm.GetDataTableControl();

                //Обновим форму до первоначальных настроек
                var xml = DBAppContext.RolesRule_GetFormXML(name, (int)lueRoles.EditValue);
                ActiveForm.LoadAllControlsState(xml);
                isChangedProperty = false;
            }
            catch (Exception ex)
            {
                DBException.WriteLog(ex);
            }
            finally
            {
                HighlightControl.IsPaint = true;
            }
        }

        /// <summary>
        ///     Сохранить сделанные для ролей изменения в базу данных
        /// </summary>
        private void SaveItemClick(object sender, ItemClickEventArgs e)
        {
            if (!isChangedProperty || lueRoles.EditValue == null || ActiveForm == null)
            {
                return;
            }
            var currentRoleId = (int) lueRoles.EditValue;
            var xmls = ActiveForm.SaveXmlFormRule();
            var setting = DBAppContext
                .Where<RolesRule>(rr => rr.FormName.Equals(ActiveForm.Name) && rr.RoleId == currentRoleId)
                .FirstOrDefault();
            if (setting != null)
            {
                setting.ControlsXML = xmls;
            }
            else
            {
                var ruleRole = new RolesRule
                {
                    ControlsXML = xmls,
                    RoleId = currentRoleId,
                    FormName = ActiveForm.Name
                };
                DBAppContext.RolesRules.Add(ruleRole);
            }
            MessageWindow.GetInstance("Настройки для роли успешно сохранены.", MessageType.Info);
        }

        /// <summary>
        ///     Вернуть внешний вид активной формы в последнее сохранённое состояние
        /// </summary>
        private void RestoreItemClick(object sender, ItemClickEventArgs e)
        {
            if (!isChangedProperty)
            {
                return;
            }
            isChangedProperty = false;
            LoadRolesForActiveForm();
        }

        /// <summary>
        ///     Вернуть внешний вид активной формы в стандартное значение, которые было до всех изменений свойств контролов
        /// </summary>
        private void DefaultItemClick(object sender, ItemClickEventArgs e)
        {
            if (ActiveForm == null)
            {
                return;
            }
            ActiveForm.LoadDefaultStates();
            isChangedProperty = true;
        }

        private void AdminToolFormClosed(object sender, FormClosedEventArgs e)
        {
            DBUser.DesignMode = false;
            HighlightControl.CloseForm();
            Instance = null;

            //Возвращаем размеры и расположение главной формы в начальное положение
            FormControls.MainForm.Size = parentBound.Size;
            FormControls.MainForm.Location = parentBound.Location;
            FormControls.MainForm.WindowState = parentState;

            //Обновляем информацию в базе данных о созданных правилах для форм
            try
            {
                FormControls.MainForm.LoadLastFormSettings();
            }
            catch (Exception ex)
            {
                DBException.WriteLog(ex);
            }
        }

        private void ControlsPopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            var hitInfo = tlControls.CalcHitInfo(e.Point);
            if (hitInfo.Node == null)
            {
                return;
            }
            pmControls.ShowPopup(tlControls.PointToScreen(e.Point));
        }

        private void ExpandButtonClick(object sender, ItemClickEventArgs e)
        {
            tlControls.FocusedNode.ExpandAll();
        }
    }
}