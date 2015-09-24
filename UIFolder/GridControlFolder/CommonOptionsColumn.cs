using System.ComponentModel;
using DevExpress.Utils.Serializing;
using DevExpress.XtraGrid.Columns;

namespace CommonLibrary.GridControlFolder
{
    public class CommonOptionsColumn : OptionsColumn
    {
        public CommonOptionsColumn()
        {
            AllowQuickHide = true;
            ShowInQuickHide = true;
            ShowEditorInPopupMenu = true;
        }

        [Description("����� �� ���� ������� ��������������� � QuickHide."), DefaultValue(true), XtraSerializableProperty]
        public bool AllowQuickHide { get; set; }

        [Description("����� �� ������� � QuickHide."), DefaultValue(true), XtraSerializableProperty]
        public bool ShowInQuickHide { get; set; }

        [Description("����� �� �������� �������� � ������� ����� ����������� ����."), DefaultValue(true), XtraSerializableProperty]
        public bool ShowEditorInPopupMenu { get; set; }
    }
}