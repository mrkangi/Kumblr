using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using static DownloadManager.Literals;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace DownloadManager
{
    public sealed partial class MediaPanelControl : UserControl
    {
        public void AddImage(string src)
        {
            var bmp = new BitmapImage(new Uri(src)) { AutoPlay = true };

            images.Children.Add(
                new Image()
                {
                    Source = bmp,
                });
        }

        public void ShowVideo(string src)
        {
            video.DownloadProgressChanged += Video_DownloadProgressChanged;
            video.Source = new Uri(src);
        }

        private void Video_DownloadProgressChanged(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine(video.DownloadProgress);
        }

        private MediaType type;
        internal MediaType Type
        {
            get { return type; }
            set
            {
                type = value;
                switch (type)
                {
                    case MediaType.IMAGE:
                        {
                            images.Visibility = Visibility.Visible;
                            videoPanel.Visibility = Visibility.Collapsed;
                        }; break;
                    case MediaType.VIDEO:
                        {
                            images.Visibility = Visibility.Collapsed;
                            videoPanel.Visibility = Visibility.Visible;
                        }; break;
                }
            }
        }



        public MediaPanelControl()
        {
            this.InitializeComponent();
            video.ManipulationMode = ManipulationModes.TranslateX | ManipulationModes.TranslateY;
        }

        private void video_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (video.CurrentState == MediaElementState.Playing)
                video.Pause();
            if (video.CurrentState == MediaElementState.Paused)
                video.Play();
        }

        private void video_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            //if (Math.Abs(e.Delta.Translation.X) < 5 && Math.Abs(e.Delta.Translation.Y) < 5)
            //{
            //    Debug.WriteLine("没有移动");
            //}
            //if (Math.Abs(e.Delta.Translation.X) < 5)
            //{
            //    if (e.Delta.Translation.Y > 0)
            //        Debug.WriteLine("向上移动");
            //    else
            //        Debug.WriteLine("向下移动");
            //}

            //if (Math.Abs(e.Delta.Translation.Y) < 5)
            //{
            //    if (e.Delta.Translation.X > 0)
            //        Debug.WriteLine("向右移动");
            //    else
            //        Debug.WriteLine("向左移动");
            //}
        }

        private void video_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {

            var absX = Math.Abs(e.Cumulative.Translation.X);
            var absY = Math.Abs(e.Cumulative.Translation.Y);

            if (absX < 10 && absY < 10)
            {
                Debug.WriteLine("没有移动");
            }
            else if (absX < absY)
            {
                if (e.Cumulative.Translation.Y < 0)
                    Debug.WriteLine("向上移动");
                else
                    Debug.WriteLine("向下移动");
            }
            else if (absX > absY)
            {
                if (e.Cumulative.Translation.X > 0)
                {//向右滑动
                    video.Position += new TimeSpan(0, 0, 10);
                }
                else
                {//向左滑动
                    video.Position -= new TimeSpan(0, 0, 10);
                }

            }
        }

        private void video_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            //video.Width = Window.Current.Bounds.Width;
            //video.Height = Window.Current.Bounds.Height;
            video.IsFullWindow = !video.IsFullWindow;

        }
    }
}
