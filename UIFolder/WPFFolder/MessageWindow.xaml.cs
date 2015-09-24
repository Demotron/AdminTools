using System;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Application = System.Windows.Forms.Application;
using FlowDirection = System.Windows.FlowDirection;

namespace CommonLibrary
{
    /// <summary>
    ///     Interaction logic for MessageWindow.xaml
    /// </summary>
    public partial class MessageWindow
    {
        private static MessageWindow instance;
        private readonly Timer cancelTimer = new Timer();

        public MessageWindow(string caption, MessageType info)
        {
            InitializeComponent();
            cancelTimer.Interval = 3000;
            cancelTimer.Tick += DoWork;
            txtCaption.Inlines.Clear();
            txtCaption.Inlines.Add(caption);
            var myTypeface = new Typeface("Arial");
            var ft = new FormattedText(caption + "Отмена", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, myTypeface, 13, Brushes.Black);
            Width = ft.Width + 95;
            Loaded += MessageWindowLoaded;
            Top = 5;
            var bitmap = Properties.Resources.info;
            using (var memory = new MemoryStream())
            {
                switch (info)
                {
                    case MessageType.Error:
                        borderParent.BorderBrush = Brushes.Red;
                        borderParent.Background = new SolidColorBrush(Color.FromRgb(255, 221, 221));
                        bitmap = Properties.Resources.error;
                        break;
                    case MessageType.Info:
                        borderParent.BorderBrush = Brushes.Green;
                        borderParent.Background = new SolidColorBrush(Color.FromRgb(218, 247, 225));
                        break;
                    case MessageType.Attention:
                        borderParent.BorderBrush = Brushes.Orange;
                        borderParent.Background = new SolidColorBrush(Color.FromRgb(250, 230, 210));
                        bitmap = Properties.Resources.warning;
                        break;
                    case MessageType.Loading:
                        Width += 50;

                        //                        loadingApple.Visibility = Visibility.Visible;
                        //                        loadingApple.Play();
                        return;
                }
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                image.Source = bitmapImage;
            }
            cancelTimer.Start();
        }

        private void MessageWindowLoaded(object sender, RoutedEventArgs e)
        {
            if (Application.OpenForms.Count == 0)
            {
                return;
            }
            var screenBound = Screen.FromControl(Application.OpenForms[0])
                .WorkingArea;
            Left = screenBound.Left + screenBound.Width/2 - Width/2;
        }

        /// <summary>
        ///     Создать окно для отмены текущего действия
        /// </summary>
        /// <param name="caption">заголовок подписи для формы отмены, которая будет работать в виде MessageBox для информации</param>
        /// <param name="info">тип изображения</param>
        /// <param name="show">нужно ли сразу показывать сообщение</param>
        public static void GetInstance(string caption, MessageType info = MessageType.Error, bool show = true)
        {
            if (instance != null)
            {
                instance.Close();
            }
            instance = new MessageWindow(caption, info);
            if (show)
            {
                instance.Show();
            }
        }

        private void DoWork(object sender, EventArgs e)
        {
            cancelTimer.Stop();
            Close();
        }

        private void HighlightBorderClosed(object sender, EventArgs e)
        {
            instance = null;
        }

        private void GifMediaEnded(object sender, RoutedEventArgs e)
        {
            loadingApple.Position = new TimeSpan(0, 0, 1);
            loadingApple.Play();
        }
    }
}