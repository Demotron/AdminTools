using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CommonLibrary.GridControlFolder;
using DevExpress.Utils;
using DevExpress.Utils.Drawing;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Container;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Skins;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Data;

namespace CommonLibrary
{
    /// <summary>
    ///     Общие методы для данного приложения
    /// </summary>
    public static class CommonUtilities
    {
        /// <summary>
        /// Находится ли приложение в режиме DesignMode
        /// </summary>
        public static bool IsDesignMode
        {
            get
            {
                return Process.GetCurrentProcess().ProcessName == "devenv";
            }
        }

        /// <summary>
        ///     Получить активную форму для данного приложения
        /// </summary>
        public static Form GetActiveForm()
        {
            var activeForm = Form.ActiveForm;
            if (activeForm != null && activeForm.IsMdiContainer && activeForm.ActiveMdiChild != null)
            {
                activeForm = activeForm.ActiveMdiChild;
            }
            return activeForm;
        }

        /// <summary>
        /// Массив целочисленных значений перевести в строку с разделителей пробел
        /// </summary>
        /// <param name="separator">Разделитель между номерами вагонов</param>
        /// <param name="numbers">Массив чисел</param>
        /// <returns>Строка, содержащая массив чисел</returns>
        public static string ArrayToString(char separator = ' ', params int[] numbers)
        {
            var str = new StringBuilder();
            foreach (var number in numbers)
            {
                str.Append(number + separator.ToString());
            }
            return str.ToString().TrimEnd(separator);
        }

        /// <summary>
        ///     Получить доступ к TextEdit в всплывающем окне у SearchLookUp
        /// </summary>
        /// <param name="popupForm">SearchLookUp</param>
        public static TextEdit FindTextInputField(this PopupSearchLookUpEditForm popupForm)
        {
            if (popupForm == null)
            {
                return null;
            }
            var foundControls = popupForm.Controls.Find("teFind", true);
            if (foundControls.Length == 0)
            {
                return null;
            }
            return (TextEdit)foundControls[0];
        }

        /// <summary>
        ///     Добавить новую кнопку в контекстное меню
        /// </summary>
        /// <param name="bsi">Кнопка меню, к которой нужно добавить кнопку</param>
        /// <param name="name">Название кнопки</param>
        /// <param name="click">Обработчик нажатия</param>
        /// <param name="glyph">Изображение на кнопке</param>
        /// <param name="tag">Tag</param>
        public static BarButtonItem AddBarButton(this BarSubItem bsi, string name, ItemClickEventHandler click, object tag = null, Bitmap glyph = null)
        {
            var addBarButton = new BarButtonItem
            {
                Caption = name,
                Glyph = glyph,
                Tag = tag
            };
            addBarButton.ItemClick += click;
            bsi.AddItem(addBarButton);
            return addBarButton;
        }

        /// <summary>
        /// Содержит ли строка русские символы
        /// </summary>
        /// <param name="name">Строка</param>
        public static bool IsEnglishOnly(string name)
        {
            return Regex.IsMatch(name, "^[a-zA-Z0-9]*$");
        }

        /// <summary>
        /// Является ли значение null или DBNULL
        /// </summary>
        public static bool IsNull(this object value)
        {
            return value == null || Convert.IsDBNull(value);
        }

        /// <summary>
        ///     Проверяет, является ли строка пустой, после отбрасывания пробельных символов
        /// </summary>
        public static bool IsEmptyAfterTrim(this string s)
        {
            return String.IsNullOrEmpty(s) || String.IsNullOrEmpty(s.Trim());
        }

        /// <summary>
        /// Получить название колонки дерева или таблицы для отображения в форме кастомизации
        /// </summary>
        /// <param name="editor">Контрол</param>
        /// <param name="col">Колонка</param>
        /// <returns>Дружественное пользователю название</returns>
        public static string ColumnToString(this EditorContainer editor, object col)
        {
            var tree = editor as TreeList;
            if (tree != null)
            {
                var column = (DataColumnInfo)col;
                var result = column.Caption;
                return String.IsNullOrEmpty(result) ? column.ColumnName : result;
            }
            var grid = editor as GridControl;
            if (grid != null)
            {
                var column = (GridColumn)col;
                var result = column.Caption;
                if (!Regex.IsMatch(result, "^[a-zA-Z0-9]*$"))
                {
                    return String.IsNullOrEmpty(result) ? column.FieldName : result;
                }
                var table = grid.DataSource as DataTable;
                if (table != null)
                {
                    result = table.Columns[column.FieldName].Caption;
                }
                return String.IsNullOrEmpty(result) ? column.FieldName : result;
            }
            throw new ArgumentException("Неизвестный тип контрола");
        }

        #region Процедуры для работы с CommonGridView

        /// <summary>
        ///     Отобразить столбец таблицы со скином заголовков колонок
        /// </summary>
        public static void HeaderDrawCell(this CommonGridView view, GridColumn column, RowCellCustomDrawEventArgs e)
        {
            if (e.Column != column || e.RowHandle == GridControl.AutoFilterRowHandle)
            {
                return;
            }
            var p = new GridSkinElementsPainter(view);
            var args = new HeaderObjectInfoArgs();
            args.Assign(new ObjectInfoArgs(e.Cache, e.Bounds, ObjectState.Normal));
            args.Graphics = e.Graphics;
            args.Caption = e.DisplayText;
            p.Column.DrawObject(args);
            e.Appearance.DrawString(e.Cache, e.DisplayText, e.Bounds);
            e.Handled = true;
        }

        /// <summary>
        /// Получить номер строки для объекта из таблицы
        /// </summary>
        /// <param name="view">Представление для таблицы</param>
        /// <param name="obj">Объект, для которого ищется номер строки</param>
        /// <returns></returns>
        public static int FindRowHandleByRowObject(this GridView view, object obj)
        {
            if (obj == null)
            {
                return GridControl.InvalidRowHandle;
            }
            for (var i = 0; i < view.DataRowCount; i++)
            {
                if (obj.Equals(view.GetRow(i)))
                {
                    return i;
                }
            }
            return GridControl.InvalidRowHandle;
        }

        /// <summary>
        ///     Получить номер строки для строки из таблицы
        /// </summary>
        /// <param name="view">Представление для таблицы</param>
        /// <param name="row">DataRow из DataSource для таблицы</param>
        /// <returns>Порядковый номер строки</returns>
        public static int FindRowHandleByDataRow(this GridView view, DataRow row)
        {
            if (row == null)
            {
                return GridControl.InvalidRowHandle;
            }
            for (var i = 0; i < view.DataRowCount; i++)
            {
                if (view.GetDataRow(i) == row)
                {
                    return i;
                }
            }
            return GridControl.InvalidRowHandle;
        }

        /// <summary>
        /// Установка изображения для отображения в индикаторе
        /// </summary>
        /// <param name="view"></param>
        /// <param name="_indicatorImage">Изображение</param>
        public static void SetIndicatorImage(this CommonGridView view, Bitmap _indicatorImage)
        {
            view.IndicatorImage = _indicatorImage;
        }

        /// <summary>
        /// Получить номер строки под координатами мыши
        /// </summary>
        /// <param name="view">Вид таблицы</param>
        /// <param name="x">Координата по оси X</param>
        /// <param name="y">Координата по оси Y</param>
        /// <returns>Возвращает номер строки</returns>
        public static int GetRowAt(this GridView view, int x, int y)
        {
            return view.CalcHitInfo(new Point(x, y)).RowHandle;
        }

        /// <summary>
        /// Вернуть в виде текста содержание всех выделенные ячеек у грида
        /// </summary>
        public static string GetSelectedValues(this GridView view)
        {
            if (view.SelectedRowsCount == 0)
            {
                return view.GetFocusedDisplayText();
            }

            const string cellDelimiter = "\t";
            const string lineDelimiter = "\r\n";
            var result = string.Empty;

            // iterate cells and compose a tab delimited string of cell values
            if (view.OptionsSelection.MultiSelectMode != GridMultiSelectMode.CellSelect)
                for (var i = 0; i < view.SelectedRowsCount; i++)
                {
                    var rowHandle = view.GetSelectedRows()[i];
                    for (var j = 0; j < view.VisibleColumns.Count; j++)
                    {
                        result += view.GetRowCellDisplayText(rowHandle, view.VisibleColumns[j]);
                        if (j != view.VisibleColumns.Count - 1)
                            result += cellDelimiter;
                    }
                    if (i != view.SelectedRowsCount - 1)
                        result += lineDelimiter;
                }
            else
            {
                for (var i = 0; i < view.SelectedRowsCount; i++)
                {
                    var rowHandle = view.GetSelectedRows()[i];
                    var cells = view.GetSelectedCells(rowHandle);

                    for (var j = 0; j < cells.Length; j++)
                    {
                        result += view.GetRowCellDisplayText(rowHandle, cells[j]);
                        if (j != cells.Length - 1)
                            result += cellDelimiter;
                    }
                    if (i != view.SelectedRowsCount - 1)
                        result += lineDelimiter;
                }
            }
            return result;
        }

        /// <summary>
        ///     Показать номера строк слева на индикаторе
        /// </summary>
        public static void RowNumberCustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (!e.Info.IsRowIndicator || e.RowHandle < 0)
            {
                return;
            }
            e.Info.ImageIndex = -1;
            e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            e.Appearance.TextOptions.VAlignment = VertAlignment.Center;
            e.Info.DisplayText = (e.RowHandle + 1).ToString(CultureInfo.InvariantCulture);
            e.Painter.DrawObject(e.Info);
        }

        #endregion

        public static bool HasEventListeners<T>(this object obj, string name)
        {
            var fieldInfo = typeof(T).GetField(name, BindingFlags.NonPublic | BindingFlags.Instance);
            if (fieldInfo == null)
            {
                return false;
            }
            var handler = fieldInfo.GetValue(obj) as Delegate;
            if (handler == null)
            {
                return false;
            }
            var subscribers = handler.GetInvocationList();
            return subscribers.Length != 0;
        }

        public static void Raise<T>(this EventHandler<T> handler, object sender, T args) where T : EventArgs
        {
            if (handler != null)
                handler(sender, args);
        }
    }
}