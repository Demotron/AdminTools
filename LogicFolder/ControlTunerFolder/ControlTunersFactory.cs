using System;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;

namespace CommonLibrary.ControlTunerFolder
{
    /// <summary>
    ///     Фабрика для получения объекта
    ///     <see cref="T:ControlTuner" />
    ///     по типу звена
    /// </summary>
    public static class ControlTunersFactory
    {
        /// <summary>
        ///     Определить тип передаваемого контрола и вернуть его в виде класса, унаследованного от
        ///     <see cref="T:ControlTuner" />
        /// </summary>
        /// <param name="control">Текущий выделенный контрол</param>
        /// <returns>
        ///     Возвращает класс, описывающий переданный объект. Возвращаются следующие классы:
        ///     <list type="bullet">
        ///         <item>
        ///             <description>
        ///                 <see cref="T:EditorContainerTuner" />
        ///                 - возвращается для объектов, унаследованных от GridControl или TreeList
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 <see cref="T:ColumnTuner" />
        ///                 - возвращается для колонок типа GridColumn и TreeListColumn
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 <see cref="T:PivotGridControlTuner" />
        ///                 - возвращается для пивота
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 <see cref="T:PivotGridFieldTuner" />
        ///                 - возвращается для полей пивота
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 <see cref="T:RibbonControlTuner" />
        ///                 - возвращается для рибона
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 <see cref="T:RibbonPageCategoryTuner" />
        ///                 - возвращается для групп страниц в рибоне
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 <see cref="T:RibbonPageTuner" />
        ///                 - возвращается для страниц в рибоне
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 <see cref="T:RibbonPageGroup" />
        ///                 - возвращается для групп на странице в рибоне
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 <see cref="T:BarButtonItemLink" />
        ///                 - возвращается для элементов на рибоне с возможностью редактирования
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 <see cref="T:BarItem" />
        ///                 - возвращается для всех остальных элементов на рибоне без возможности редактирования
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 <see cref="T:System.Windows.Forms.Control" />
        ///                 - возвращается для всех остальных контролов
        ///             </description>
        ///         </item>
        ///     </list>
        /// </returns>
        /// <remarks>
        ///     Выделены определённые типы объектов на форме, которые обладают
        ///     похожим функционалом. Для каждой группы таких объектов создан класс, с помощью
        ///     которого мы получаем доступ к данному контролу на экране и подсвечиваем его
        /// </remarks>
        public static ControlTuner GetControlTuner(this object control)
        {
            if (control is GridControl || control is TreeList)
            {
                return new EditorContainerTuner(control);
            }

            if (control is GridColumn || control is TreeListColumn)
            {
                return new ColumnTuner(control);
            }

            if (control is PivotGridControl)
            {
                return new PivotGridControlTuner(control);
            }

            if (control is PivotGridField)
            {
                return new PivotGridFieldTuner(control);
            }

            if (control is RibbonControl)
            {
                return new RibbonControlTuner(control);
            }

            if (control is RibbonPageCategory)
            {
                return new RibbonPageCategoryTuner(control);
            }

            if (control is RibbonPage)
            {
                return new RibbonPageTuner(control);
            }

            if (control is RibbonPageGroup)
            {
                return new RibbonPageGroupTuner(control);
            }

            if (control is BarButtonItemLink)
            {
                return new BarButtonItemLinkTuner(control);
            }

            if (control is BarItem)
            {
                return new BarItemTuner(control);
            }

            if (control is DockPanel)
            {
                return new DockPanelTuner(control);
            }

            if (control is Control)
            {
                return new OtherControlTuner(control);
            }

            throw new SystemException("Контрол не опознан тип" + control.GetType().Name);
        }
    }
}