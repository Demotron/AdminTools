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
    ///     ������� ��� ��������� �������
    ///     <see cref="T:ControlTuner" />
    ///     �� ���� �����
    /// </summary>
    public static class ControlTunersFactory
    {
        /// <summary>
        ///     ���������� ��� ������������� �������� � ������� ��� � ���� ������, ��������������� ��
        ///     <see cref="T:ControlTuner" />
        /// </summary>
        /// <param name="control">������� ���������� �������</param>
        /// <returns>
        ///     ���������� �����, ����������� ���������� ������. ������������ ��������� ������:
        ///     <list type="bullet">
        ///         <item>
        ///             <description>
        ///                 <see cref="T:EditorContainerTuner" />
        ///                 - ������������ ��� ��������, �������������� �� GridControl ��� TreeList
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 <see cref="T:ColumnTuner" />
        ///                 - ������������ ��� ������� ���� GridColumn � TreeListColumn
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 <see cref="T:PivotGridControlTuner" />
        ///                 - ������������ ��� ������
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 <see cref="T:PivotGridFieldTuner" />
        ///                 - ������������ ��� ����� ������
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 <see cref="T:RibbonControlTuner" />
        ///                 - ������������ ��� ������
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 <see cref="T:RibbonPageCategoryTuner" />
        ///                 - ������������ ��� ����� ������� � ������
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 <see cref="T:RibbonPageTuner" />
        ///                 - ������������ ��� ������� � ������
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 <see cref="T:RibbonPageGroup" />
        ///                 - ������������ ��� ����� �� �������� � ������
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 <see cref="T:BarButtonItemLink" />
        ///                 - ������������ ��� ��������� �� ������ � ������������ ��������������
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 <see cref="T:BarItem" />
        ///                 - ������������ ��� ���� ��������� ��������� �� ������ ��� ����������� ��������������
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 <see cref="T:System.Windows.Forms.Control" />
        ///                 - ������������ ��� ���� ��������� ���������
        ///             </description>
        ///         </item>
        ///     </list>
        /// </returns>
        /// <remarks>
        ///     �������� ����������� ���� �������� �� �����, ������� ��������
        ///     ������� ������������. ��� ������ ������ ����� �������� ������ �����, � �������
        ///     �������� �� �������� ������ � ������� �������� �� ������ � ������������ ���
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

            throw new SystemException("������� �� ������� ���" + control.GetType().Name);
        }
    }
}