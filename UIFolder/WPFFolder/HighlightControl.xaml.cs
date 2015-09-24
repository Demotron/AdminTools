using System;
using System.Windows;
using System.Windows.Input;

namespace CommonLibrary
{
    /// <summary>
    ///     Форма подсветки контрола на форме при администрировании
    /// </summary>
    public partial class HighlightControl
    {
        private static HighlightControl instance;

        /// <summary>
        ///     Нужно ли отрисовывать подсветку контрола
        /// </summary>
        public static bool IsPaint = true;

        private static bool topmost;

        private HighlightControl(int x, int y, int width, int height)
        {
            InitializeComponent();
            HighlightBorder.WindowStartupLocation = WindowStartupLocation.Manual;
            ChangeLocationSize(x, y, width, height);
        }

        /// <summary>
        ///     Показать форму поверх остальных окон
        /// </summary>
        public static bool TopMost
        {
            set
            {
                topmost = value;
                if (instance != null)
                {
                    instance.Topmost = value;
                }
            }
        }

        /// <summary>
        ///     Создать форму для подсветки элементов управления на форме
        /// </summary>
        /// <remarks>
        ///     В конструктор передаются координаты для отрисовки
        /// </remarks>
        /// <param name="x">координаты x на экране</param>
        /// <param name="y">координаты y на экране</param>
        /// <param name="width">ширина элемента</param>
        /// <param name="height">высота элемента</param>
        public static void GetInstance(int x, int y, int width, int height)
        {
            if (!IsPaint)
            {
                return;
            }
            if (instance == null)
            {
                instance = new HighlightControl(x, y, width, height);
            }
            else
            {
                instance.ChangeLocationSize(x, y, width, height);
            }
        }

        /// <summary>
        ///     Закрыть форму
        /// </summary>
        public static void CloseForm()
        {
            if (instance != null)
            {
                instance.Close();
            }
        }

        /// <summary>
        ///     Перериовать элемент на экране
        /// </summary>
        /// <param name="x">координата х</param>
        /// <param name="y">координата y</param>
        /// <param name="width">ширина подсветки</param>
        /// <param name="height">высота подсветки</param>
        private void ChangeLocationSize(int x, int y, int width, int height)
        {
            HighlightBorder.Hide();
            HighlightBorder.Left = x;
            HighlightBorder.Top = y;
            HighlightBorder.Width = width;
            HighlightBorder.Height = height;
            Topmost = topmost;
            Show();
            Activate();
        }

        private void HighlightControlClosed(object sender, EventArgs e)
        {
            topmost = false;
            instance = null;
        }

        private void BorderMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
    }
}