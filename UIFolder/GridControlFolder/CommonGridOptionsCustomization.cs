using System.ComponentModel;
using CommonLibrary.ColumnsPopupFormFolder;
using DevExpress.Utils.Controls;
using DevExpress.Utils.Serializing;
using DevExpress.XtraGrid.Views.Grid;

namespace CommonLibrary.GridControlFolder
{
    public class CommonGridOptionsCustomization : GridOptionsCustomization
    {
        public delegate void EventAfterCarNumberFindChanged(bool value);

        public delegate void EventAfterSelectionChanged(bool value);

        private bool allowQuickCustomisation;
        private bool allowSelectionColumn;
        private bool allowAdd;
        private bool allowDelete;
        private bool showSelectedRowMenu;
        private EventAfterSelectionChanged onSelectionChanged;
        private readonly QuickCustomizationIcon quickCustomizationIcon;

        public CommonGridOptionsCustomization()
        {
            allowAdd = true;
            allowDelete = true;
            allowQuickCustomisation = true;
            allowSelectionColumn = false;
            showSelectedRowMenu = true;
            quickCustomizationIcon = new QuickCustomizationIcon();
        }

        [Description("����� �� ������������ �������� �������� � ���������� ����� � ������� ������������ ����"), DefaultValue(true), XtraSerializableProperty]
        public bool ShowSelectedRowMenu
        {
            get { return showSelectedRowMenu; }
            set
            {
                if (showSelectedRowMenu == value)
                {
                    return;
                }
                var prevValue = showSelectedRowMenu;
                showSelectedRowMenu = value;
                OnChanged(new BaseOptionChangedEventArgs("ShowSelectedRowMenu", prevValue, showSelectedRowMenu));
            }
        }

        [Description("����� �� ������������ ��������� ������ � �������"), DefaultValue(true), XtraSerializableProperty]
        public bool AllowAdd
        {
            get { return allowAdd; }
            set
            {
                if (allowAdd == value)
                {
                    return;
                }
                var prevValue = allowAdd;
                allowAdd = value;
                OnChanged(new BaseOptionChangedEventArgs("AllowAdd", prevValue, allowAdd));
            }
        }

        [Description("����� �� ������������ ������� ������ �� �������"), DefaultValue(true), XtraSerializableProperty]
        public bool AllowDelete
        {
            get { return allowDelete; }
            set
            {
                if (allowDelete == value)
                {
                    return;
                }
                var prevValue = allowDelete;
                allowDelete = value;
                OnChanged(new BaseOptionChangedEventArgs("AllowDelete", prevValue, allowDelete));
            }
        }

        [Description("����� �� ������������ ��������������� ������� ��� ��� �������"), DefaultValue(true), XtraSerializableProperty]
        public bool AllowQuickCustomisation
        {
            get { return allowQuickCustomisation; }
            set
            {
                if (allowQuickCustomisation == value)
                {
                    return;
                }
                var prevValue = allowQuickCustomisation;
                allowQuickCustomisation = value;
                OnChanged(new BaseOptionChangedEventArgs("AllowQuickCustomisation", prevValue, allowQuickCustomisation));
            }
        }

        [Browsable(false)]
        internal QuickCustomizationIcon QuickCustomizationIcons
        {
            get { return allowQuickCustomisation ? quickCustomizationIcon : null; }
        }

        [Description("�������� �� ������� ��� ������ �����"), DefaultValue(false), XtraSerializableProperty]
        public bool AllowSelectionColumn
        {
            get { return allowSelectionColumn; }
            set
            {
                if (allowSelectionColumn == value)
                {
                    return;
                }
                var prevValue = allowSelectionColumn;
                allowSelectionColumn = value;
                onSelectionChanged(value);
                OnChanged(new BaseOptionChangedEventArgs("AllowQuickCustomisation", prevValue, allowSelectionColumn));
            }
        }

        public void AcceptSelectionChangeEvent(EventAfterSelectionChanged methodSelection)
        {
            onSelectionChanged = methodSelection;
        }
    }
}