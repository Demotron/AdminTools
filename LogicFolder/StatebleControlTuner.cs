using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
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
    public static class StatebleControlTuner
    {
        /// <summary>
        ///     Применить настройку внешнего вида для объекта из объекта
        ///     <see cref="T:System.Data.DataRow" />
        /// </summary>
        /// <param name="control">Объект для сохранения внешнего вида</param>
        /// <param name="layout">
        ///     Объект типа
        ///     <see cref="T:System.Data.DataRow" />
        /// </param>
        public static void AcceptLayoutForControl(this Control control, UserLayout layout)
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
            if (layout == null || sxs == null)
            {
                return;
            }
            sxs.RestoreLayoutSerializingFromStream(layout.Layout);
        }

        /// <summary>
        ///     Загрузить внешний вид объекта из массива байтов
        /// </summary>
        /// <param name="control">Объект для применения настроек</param>
        /// <param name="layout">Массив байтов</param>
        public static void RestoreLayoutFromStream(this Control control, byte[] layout)
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
            if (layout == null || layout.Count() == 0)
            {
                return;
            }
            var stream = new MemoryStream(layout);
            try
            {
                sxs.RestoreLayoutFromStream(stream);
            }
            catch (Exception ex)
            {
                throw new Exception("Wrong data format", ex);
            }
        }

        /// <summary>
        ///     Загрузить внешний вид объекта из массива байтов
        /// </summary>
        /// <param name="sxs">Объект для применения настроек</param>
        /// <param name="layout">Массив байтов</param>
        internal static void RestoreLayoutSerializingFromStream(this ISupportXtraSerializer sxs, byte[] layout)
        {
            if (sxs == null || layout == null || layout.Count() == 0)
            {
                return;
            }
            var stream = new MemoryStream(layout);
            try
            {
                sxs.RestoreLayoutFromStream(stream);
            }
            catch (Exception ex)
            {
                throw new Exception("Wrong data format", ex);
            }
        }

        /// <summary>
        ///     Получить настройки для внешнего вида объекта в виде массива байтов
        /// </summary>
        /// <param name="control">Контрол, для которого необходимо получить Layout в виде массива байтов</param>
        /// <returns>Возвращает массив байтов</returns>
        public static byte[] GetLayoutData(this Control control)
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
                return null;
            }
            var stream = new MemoryStream();
            sxs.SaveLayoutToStream(stream);
            return stream.GetBuffer();
        }

        /// <summary>
        ///     Можно ли сохранить состояние для данного контрола, и просматриваем всех предков данного контрола,
        ///     пока не найдём тот, для которого можно сохранить состояние или пока не просмотрим все контролы
        /// </summary>
        /// <param name="control">Контрол</param>
        public static Control IsParentStatebleControl(this Control control)
        {
            if (control == null)
            {
                return null;
            }
            if (control is GridControl || control is PivotGridControl || control is TreeList)
            {
                return control;
            }
            var ctrl = IsParentStatebleControl(control.Parent);
            return ctrl;
        }

        /// <summary>
        ///     Вернуть список всех контролов, для которых можно сохранить настройки внешнего вида
        /// </summary>
        /// <param name="controlCollection">Список всех контролов</param>
        // ReSharper disable once ReturnTypeCanBeEnumerable.Local
        private static Control[] GetStatebleControls(IEnumerable controlCollection)
        {
            var res = new List<Control>();
            foreach (Control control in controlCollection)
            {
                if (control is GridControl || control is PivotGridControl || control is TreeList)
                {
                    res.Add(control);
                }
                res.AddRange(GetStatebleControls(control.Controls));
            }
            return res.ToArray();
        }

        /// <summary>
        ///     Сохранить состояние контролов перед закрытием формы
        /// </summary>
        /// <param name="controls">список контролов</param>
        public static void SaveControlsStatesBeforeClose(Control.ControlCollection controls)
        {
            foreach (var stateble in GetStatebleControls(controls))
            {
                stateble.SaveLastLayout();
            }
        }

        /// <summary>
        ///     Загрузить последнее состояние контролов перед показом формы
        /// </summary>
        /// <param name="controls">список контролов</param>
        public static void LoadControlsStatesBeforeShow(Control.ControlCollection controls)
        {
            foreach (var stateble in GetStatebleControls(controls))
            {
                var data = stateble.FindForm().GetLastLayout(stateble.Name);
                RestoreLayoutSerializingFromStream(stateble as ISupportXtraSerializer, data);
            }
        }
    }
}