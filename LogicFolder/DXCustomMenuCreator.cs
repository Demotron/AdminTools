using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CommonLibrary.AdminFolder;
using CommonLibrary.Properties;
using CommonLibrary.UserFolder;
using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Container;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Localization;
using ServerInformation;

namespace CommonLibrary.LogicFolder
{
    public class DXCustomMenuCreator
    {
        private List<UserLayout> layouts;
        private readonly Form controlForm;
        private readonly DXPopupMenu menu;
        private readonly EditorContainer editor;
        private readonly DXSubMenuItem dxsmiRemove;
        private readonly DXSubMenuItem dxsmiSaved;
        private readonly DXMenuItem bbiDeleteStates;

        private IEnumerable<UserLayout> Layouts
        {
            get
            {
                if (layouts != null)
                {
                    return layouts;
                }
                using (var db = new ApplicationEntitie(0))
                {
                    layouts = db.UserLayouts
                        .Where(ul => ul.UserId == DBUser.Working.Id && ul.TableName == TableName)
                        .ToList();
                }
                return layouts;
            }
        }

        private string TableName
        {
            get { return controlForm.Name + "." + editor.Name; }
        }

        /// <summary>
        /// Скрыть стандартные кнопки из меню Грида
        /// </summary>
        public static void HideCustomizationColumns(DXPopupMenu menu)
        {
            var showColumns = menu.Items
                .Cast<DXMenuItem>()
                .FirstOrDefault(i => i.Tag is GridStringId && (GridStringId)i.Tag == GridStringId.MenuColumnColumnCustomization);
            if (showColumns != null)
                showColumns.Visible = false;
        }

        public static RepositoryItem DXCreateBarItemByFocusedColumn(GridColumn column)
        {
            if (column.ColumnEdit != null)
                return column.ColumnEdit;
            if (column.ColumnType == typeof(string))
                return new RepositoryItemTextEdit();
            if (column.ColumnType == typeof(int))
                return new RepositoryItemTextEdit()
                {
                    Mask = { EditMask = @"N00" }
                };
            return new RepositoryItemTextEdit();
        }

        /// <summary>
        /// Создать настройки меню для сохранения и загрузки информации о внешнем виде таблиц и деревьев
        /// </summary>
        /// <param name="_editor">Таблица или дерево</param>
        /// <param name="_menu">Меню</param>
        public static void DXCreateLayoutMenuItems(EditorContainer _editor, ref DXPopupMenu _menu)
        {
            var layoutWorker = _editor as IShowLayoutWorkMenu;
            if (layoutWorker != null && !layoutWorker.GetShowLayoutWorkMenu())
            {
                return;
            }
            var instance = new DXCustomMenuCreator(_editor, ref _menu);
            instance.DXCreateLayoutMenuItems();
        }

        /// <summary>
        /// Добавить в меню пункты по выбору способа выделения ячеек
        /// </summary>
        /// <param name="menu">Меню</param>
        /// <param name="selectCustom">Метод для обработки случая выборки нескольких ячеек</param>
        /// <param name="selectRow">Метод для обработки случая выборки нескольких строк</param>
        /// <param name="selectRows">Метод для обработки случая выборки одной строки</param>
        public static void DXCreateSelectSelectionMenu(DXPopupMenu menu, EventHandler selectCustom, EventHandler selectRow, EventHandler selectRows)
        {
            var dxsmiSelect = new DXSubMenuItem("Способы выделения ячеек")
            {
                Image = Resources.table_select
            };
            var dxmiSelectRows = new DXMenuItem("Колонка для выделения")
            {
                Image = Resources.check_box_list
            };
            var dxmiSelectCustom = new DXMenuItem("Выделение нескольких ячеек")
            {
                Image = Resources.table_select_group
            };
            var dxmiSelectRow = new DXMenuItem("Выделение строк")
            {
                Image = Resources.table_select_row
            };

            dxmiSelectCustom.Click += selectCustom;
            dxmiSelectRow.Click += selectRow;
            dxmiSelectRows.Click += selectRows;

            dxsmiSelect.Items.Add(dxmiSelectRow);
            dxsmiSelect.Items.Add(dxmiSelectRows);
            dxsmiSelect.Items.Add(dxmiSelectCustom);

            menu.Items.Add(dxsmiSelect);
        }

        private DXCustomMenuCreator(EditorContainer _editor, ref DXPopupMenu _menu)
        {
            editor = _editor;
            menu = _menu;
            controlForm = editor.FindForm();
            dxsmiSaved = new DXSubMenuItem("Сохраненные состояния")
            {
                Image = Resources.layout_save
            };
            bbiDeleteStates = new DXMenuItem("Удалить выбранные")
            {
                Image = Resources.Remove
            };
            dxsmiRemove = new DXSubMenuItem("Удалить состояния")
            {
                Image = Resources.layout_delete
            };
        }

        private void DXCreateLayoutMenuItems()
        {
            var ribeNewStateName = new RepositoryItemButtonEdit
            {
                AutoHeight = false,
                NullText = @"Введите имя"
            };
            ribeNewStateName.Buttons.Clear();
            ribeNewStateName.Buttons.Add(new EditorButton(ButtonPredefines.Glyph)
            {
                Image = Resources.save,
                ToolTip = "Сохранить состояние с введённым именем"
            });
            var dxmeNewLayout = new DXEditMenuItem
            {
                Image = Resources.layout_add,
                Edit = ribeNewStateName,
                Width = 100
            };
            var dxdmiNewState = new DXSubMenuItem("Сохранить состояние")
            {
                BeginGroup = true,
                Image = Resources.saveas
            };
            var dxmiDefaultState = new DXMenuItem("Начальное состояние")
            {
                Image = Resources.layout_default
            };
            var dxmiLastState = new DXMenuItem("Последнее сохранённое")
            {
                Image = Resources.layout_link
            };

            //Создаём обработчики событий для меню
            menu.BeforePopup += BeforePopupMenuLayout;
            ribeNewStateName.ButtonClick += NewStateNameButtonClick;
            bbiDeleteStates.Click += DeleteStatesItemClick;
            dxmiDefaultState.Click += DefaultStateItemClick;
            dxmiLastState.Click += LastStateItemClick;

            //Распределяем кнопки по меню
            dxdmiNewState.Items.Add(dxmeNewLayout);
            menu.Items.Add(dxdmiNewState);
            menu.Items.Add(dxsmiSaved);
            menu.Items.Add(dxsmiRemove);
            menu.Items.Add(dxmiDefaultState);
            menu.Items.Add(dxmiLastState);
        }

        private void BeforePopupMenuLayout(object sender = null, EventArgs e = null)
        {
            foreach (var layout in Layouts.Where(l => l.LayoutType == 0 && l.UserId == DBUser.Working.Id))
            {
                AddStateItemToMenu(layout.Id, layout.LayoutName);
            }
            if (dxsmiRemove.Items.Count != 0)
            {
                dxsmiRemove.Items.Add(bbiDeleteStates);
            }
            dxsmiSaved.Enabled = dxsmiSaved.Items.Count != 0;
            dxsmiRemove.Enabled = dxsmiRemove.Items.Count != 0;
        }

        private void NewStateNameButtonClick(object sender, ButtonPressedEventArgs e)
        {
            var layout = DBAdmin.UserLayout_Insert(controlForm.Name, editor.Name, ((ButtonEdit)sender).MaskBox.MaskBoxText, editor.GetLayoutData(), 0);
            layouts.Add(layout);
            MessageWindow.GetInstance("Настройка состояния успешно сохранена", MessageType.Info);
            menu.HidePopup();
        }

        private void DeleteStatesItemClick(object sender, EventArgs eventArgs)
        {
            if (!dxsmiRemove.Items.OfType<DXEditMenuItem>().Any(item => item.EditValue != null && (bool)item.EditValue))
            {
                MessageWindow.GetInstance("Не выбрано ни одно состояние");
            }
            else
            {
                using (var db = new ApplicationEntitie(0))
                {
                    var database = db;
                    foreach (var state in dxsmiRemove.Items
                        .OfType<DXEditMenuItem>()
                        .Where(item => (bool)item.EditValue)
                        .Select(item => database.UserLayouts.Find((int)item.Tag)))
                    {
                        db.UserLayouts.Remove(state);
                    }
                    db.SaveChanges();
                    MessageWindow.GetInstance("Выбранные состояния успешно удалены.", MessageType.Info);
                }
            }
        }

        private void AddStateItemToMenu(int id, string name)
        {
            var riceStates = new RepositoryItemCheckEdit
            {
                AutoWidth = true,
                Caption = string.Empty,
                GlyphAlignment = HorzAlignment.Far,
                NullStyle = StyleIndeterminate.Unchecked,
                Appearance = { BackColor = Color.Transparent }
            };
            AddBarButton(dxsmiSaved, name, UserStateItemClick, id);
            var checkStateBtn = new DXEditMenuItem
            {
                Caption = name,
                Edit = riceStates,
                Tag = id
            };
            dxsmiRemove.Items.Add(checkStateBtn);
        }

        private void UserStateItemClick(object sender, EventArgs e)
        {
            editor.AcceptLayoutForControl(Layouts.FirstOrDefault(state => state.Id == Convert.ToInt32(((DXMenuItem)sender).Tag)));
        }

        private void DefaultStateItemClick(object sender, EventArgs eventArgs)
        {
            controlForm.AcceptDefaultLayoutForControl(editor.Name, editor);
        }

        private void LastStateItemClick(object sender, EventArgs eventArgs)
        {
            var data = controlForm.GetLastLayout(editor.Name);
            editor.RestoreLayoutFromStream(data);
        }

        /// <summary>
        ///     Добавить новую кнопку в контекстное меню
        /// </summary>
        /// <param name="dxsmi">Кнопка меню, к которой нужно добавить кнопку</param>
        /// <param name="name">Название кнопки</param>
        /// <param name="click">Обработчик нажатия</param>
        /// <param name="tag">Tag</param>
        /// <param name="glyph">Изображение на кнопке</param>
        private static void AddBarButton(DXSubMenuItem dxsmi, string name, EventHandler click, object tag = null, Bitmap glyph = null)
        {
            var addBarButton = new DXMenuItem
            {
                Caption = name,
                Image = glyph,
                Tag = tag
            };
            addBarButton.Click += click;
            dxsmi.Items.Add(addBarButton);
        }
    }
}
