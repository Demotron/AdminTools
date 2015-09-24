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

        [Description("ћожет ли быть колонка кастомизирована в QuickHide."), DefaultValue(true), XtraSerializableProperty]
        public bool AllowQuickHide { get; set; }

        [Description("¬идна ли колонка в QuickHide."), DefaultValue(true), XtraSerializableProperty]
        public bool ShowInQuickHide { get; set; }

        [Description("ћожно ли изменить значение у колонки через контекстное меню."), DefaultValue(true), XtraSerializableProperty]
        public bool ShowEditorInPopupMenu { get; set; }
    }
}