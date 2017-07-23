using Microsoft.Toolkit.Uwp.UI.Controls;
using Microsoft.Toolkit.Uwp.UI.Extensions;
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
        public event DoubleTappedEventHandler OnImageDoubleTapped;

        private void Video_DownloadProgressChanged(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine(video.DownloadProgress);
        }

        public MediaPanelControl()
        {
            this.InitializeComponent();
            //(this.Content as FrameworkElement).DataContext = this;
            video.ManipulationMode = ManipulationModes.TranslateX | ManipulationModes.TranslateY;
        }

        private static MediaElement PlayingMedia = null;


        private void video_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //if (PlayingMedia != null && !PlayingMedia.Equals(video)) PlayingMedia.Pause();
            //PlayingMedia = video;
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
            if (video.CurrentState == MediaElementState.Playing)
                video.Pause();
            if (video.CurrentState == MediaElementState.Paused)
                video.Play();
        }




        public string Poster
        {
            get { return (string)GetValue(PosterProperty); }
            set { SetValue(PosterProperty, value); }
        }


        public static readonly DependencyProperty PosterProperty =
      DependencyProperty.Register("Poster", typeof(string),
        typeof(ComplexListItem), new PropertyMetadata(null));

        public string PosterDescribe
        {
            get { return (string)GetValue(PosterDescribeProperty); }
            set { SetValue(PosterDescribeProperty, value); }
        }
        public static readonly DependencyProperty PosterDescribeProperty =
      DependencyProperty.Register("PosterDescribe", typeof(string),
        typeof(ComplexListItem), new PropertyMetadata(null));

        private void video_CurrentStateChanged(object sender, RoutedEventArgs e)
        {
            var media = sender as MediaElement;
            if (media.CurrentState == MediaElementState.Playing)
            {
                if (PlayingMedia != null && !PlayingMedia.Equals(media)) PlayingMedia.Pause();
                PlayingMedia = media;
            }
        }

        private void WebView_ScriptNotify(object sender, NotifyEventArgs e)
        {
            string result = e.Value.ToString();
            (sender as WebView).Height = double.Parse(result);
        }

        private async void WebView_DOMContentLoaded(WebView sender, WebViewDOMContentLoadedEventArgs args)
        {
            try { var rst = await (sender as WebView).InvokeScriptAsync("getHeight", null); } catch { }
        }
    }
    class MyProperties
    {
        // "HtmlString" attached property for a WebView
        public static readonly DependencyProperty HtmlStringProperty =
           DependencyProperty.RegisterAttached("HtmlString", typeof(string), typeof(MyProperties), new PropertyMetadata("", OnHtmlStringChanged));

        // Getter and Setter
        public static string GetHtmlString(DependencyObject obj) { return (string)obj.GetValue(HtmlStringProperty); }
        public static void SetHtmlString(DependencyObject obj, string value) { obj.SetValue(HtmlStringProperty, value); }

        // Handler for property changes in the DataContext : set the WebView
        private static void OnHtmlStringChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            WebView wv = d as WebView;
            if (wv != null)
            {
                wv.NavigateToString((string)e.NewValue);
            }
        }
    }
}
