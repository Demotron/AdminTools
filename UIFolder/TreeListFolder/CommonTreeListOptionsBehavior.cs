using System;
using System.ComponentModel;
using CommonLibrary.ColumnsPopupFormFolder;
using DevExpress.Utils.Controls;
using DevExpress.Utils.Serializing;
using DevExpress.XtraTreeList;

namespace CommonLibrary.TreeListFolder
{
    public class CommonTreeListOptionsBehavior : TreeListOptionsBehavior
    {
        private bool allowQuickCustomisation;
        private bool allowMove;
        private Action<bool> onAllowMoveChanged;
        private Action onQuickCustomisation;
        private readonly QuickCustomizationIcon quickCustomizationIcon;

        public CommonTreeListOptionsBehavior(CommonTreeList _treeList)
            : base(_treeList)
        {
            allowQuickCustomisation = true;
            allowMove = true;
            quickCustomizationIcon = new QuickCustomizationIcon();
        }

        [Description("Может ли пользователь кастомизировать колонки как ему хочется"), DefaultValue(true), XtraSerializableProperty]
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
                onQuickCustomisation();
                OnChanged(new BaseOptionChangedEventArgs("AllowQuickCustomisation", prevValue, allowQuickCustomisation));
            }
        }

        [Description("Может ли пользователь передвигать колонки как ему хочется"), DefaultValue(true), XtraSerializableProperty]
        public bool AllowMove
        {
            get { return allowMove; }
            set
            {
                if (allowMove == value)
                {
                    return;
                }
                var prevValue = allowMove;
                allowMove = value;
                onAllowMoveChanged(value);
                OnChanged(new BaseOptionChangedEventArgs("AllowMove", prevValue, allowMove));
            }
        }

        public void AcceptChangeEventAction(Action<bool> methodMove, Action methodCustomisation)
        {
            onAllowMoveChanged = methodMove;
            onQuickCustomisation = methodCustomisation;
        }

        [Browsable(false)]
        internal QuickCustomizationIcon QuickCustomizationIcons
        {
            get { return allowQuickCustomisation ? quickCustomizationIcon : null; }
        }
    }
}