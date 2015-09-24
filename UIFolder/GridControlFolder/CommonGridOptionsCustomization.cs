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

        [Description("ћожет ли пользователь измен€ть значени€ у выделенных строк с помощью всплывающего меню"), DefaultValue(true), XtraSerializableProperty]
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

        [Description("ћожет ли пользователь добавл€ть записи в тиблицу"), DefaultValue(true), XtraSerializableProperty]
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

        [Description("ћожет ли пользователь удал€ть записи из таблицы"), DefaultValue(true), XtraSerializableProperty]
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

        [Description("ћожет ли пользователь кастомизировать колонки как ему хочетс€"), DefaultValue(true), XtraSerializableProperty]
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

        [Description("ƒоступна ли колонка дл€ выбора строк"), DefaultValue(false), XtraSerializableProperty]
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