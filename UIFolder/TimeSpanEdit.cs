using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;

namespace CommonLibrary
{
    [UserRepositoryItem("RegisterTimeSpanEdit")]
    public sealed class RepositoryItemTimeSpanEdit : RepositoryItemTimeEdit
    {
        public const string TimeSpanEditName = "TimeSpanEdit";
        private bool allowDayInput;

        public RepositoryItemTimeSpanEdit()
        {
            allowDayInput = false;
            UpdateFormats();
        }

        static RepositoryItemTimeSpanEdit()
        {
            RegisterTimeSpanEdit();
        }

        public override string EditorTypeName
        {
            get { return TimeSpanEditName; }
        }

        [Browsable(false)]
        public override FormatInfo EditFormat
        {
            get { return base.EditFormat; }
        }

        [Browsable(false)]
        public override FormatInfo DisplayFormat
        {
            get { return base.DisplayFormat; }
        }

        [Browsable(false)]
        public override MaskProperties Mask
        {
            get { return base.Mask; }
        }

        [Browsable(false)]
        public new string EditMask
        {
            get
            {
                var mask = "HH:mm:ss";
                if (AllowDayInput)
                {
                    mask = "d." + mask;
                }
                return mask;
            }
        }

        [Category(CategoryName.Behavior), DefaultValue(false)]
        public bool AllowDayInput
        {
            get { return allowDayInput; }
            set
            {
                if (allowDayInput == value)
                {
                    return;
                }
                allowDayInput = value;
                UpdateFormats();
            }
        }

        public static void RegisterTimeSpanEdit()
        {
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(TimeSpanEditName,
                typeof (TimeSpanEdit), typeof (RepositoryItemTimeSpanEdit),
                typeof (BaseSpinEditViewInfo), new ButtonEditPainter(), true));
        }

        private void UpdateFormats()
        {
            EditFormat.FormatString = EditMask;
            DisplayFormat.FormatString = EditMask;
            Mask.EditMask = EditMask;
        }

        public override void Assign(RepositoryItem item)
        {
            BeginUpdate();
            try
            {
                base.Assign(item);
                var source = item as RepositoryItemTimeSpanEdit;
                if (source == null)
                {
                    return;
                }
                AllowDayInput = source.AllowDayInput;
            }
            finally
            {
                EndUpdate();
            }
        }

        public override string GetDisplayText(FormatInfo format, object editValue)
        {
            if (editValue is TimeSpan)
            {
                return TimeSpanHelper.TimeSpanToString((TimeSpan) editValue);
            }
            if (editValue is string)
            {
                return editValue.ToString();
            }
            return GetDisplayText(null, new TimeSpan(0));
        }

        internal string GetFormatMaskAccessFunction(string editMask, CultureInfo managerCultureInfo)
        {
            return GetFormatMask(editMask, managerCultureInfo);
        }
    }

    public class TimeSpanEdit : TimeEdit
    {
        public TimeSpanEdit()
        {
            fOldEditValue = fEditValue = new TimeSpan(0);
        }

        static TimeSpanEdit()
        {
            RepositoryItemTimeSpanEdit.RegisterTimeSpanEdit();
        }

        public override string EditorTypeName
        {
            get { return RepositoryItemTimeSpanEdit.TimeSpanEditName; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemTimeSpanEdit Properties
        {
            get { return base.Properties as RepositoryItemTimeSpanEdit; }
        }

        public override object EditValue
        {
            get
            {
                if (Properties.ExportMode == ExportMode.DisplayText)
                {
                    return Properties.GetDisplayText(null, base.EditValue);
                }
                return base.EditValue;
            }
            set
            {
                if (value is DateTime)
                {
                    var time = ((DateTime) value);
                    base.EditValue = new TimeSpan(time.Ticks);
                }
                else if (value is TimeSpan)
                {
                    base.EditValue = value;
                }
                else if (value is string)
                {
                    base.EditValue = TimeSpanHelper.Parse((string) value);
                }
                else
                {
                    base.EditValue = new TimeSpan(0, 0, 0);
                }
            }
        }
    }

    //    public class CustomTimeEditMaskProperties : TimeEditMaskProperties
    //    {
    //        public CustomTimeEditMaskProperties() : base() { }
    //        public virtual MaskManager CreatePatchedMaskManager()
    //        {
    //            CultureInfo managerCultureInfo = this.Culture;
    //            if (managerCultureInfo == null)
    //                managerCultureInfo = CultureInfo.CurrentCulture;
    //            string editMask = this.EditMask;
    //            if (editMask == null)
    //                editMask = string.Empty;
    //            return new CustomDateTimeMaskManager(editMask, false, managerCultureInfo, true);
    //        }
    //    }

    internal static class TimeSpanHelper
    {
        /// <summary>
        ///     hh is 00-Int32.Maximum
        ///     mm is 00-59
        ///     ss is 00-59
        ///     This is a neccessary workaround for the formating issues of TimeSpan.
        ///     TimeSpan.ToString() supports negative values but will also show milliseconds
        ///     TimeSpan.ToString("T") does not support negative values
        ///     TimeSpan.ToString(@"hh:mm:ss") does not support negative values
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static String TimeSpanToString(TimeSpan t)
        {
            var hours = Math.Abs((int) t.TotalHours)
                .ToString("00");
            var minutes = Math.Abs(t.Minutes)
                .ToString("00");
            var seconds = Math.Abs(t.Seconds)
                .ToString("00");
            return String.Format("{0}:{1}:{2}", hours, minutes, seconds);
        }

        public static TimeSpan Parse(string value)
        {
            return TimeSpan.Parse(value);
        }
    }
}