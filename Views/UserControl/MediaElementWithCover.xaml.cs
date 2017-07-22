using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace DownloadManager
{
    public sealed partial class MediaElementWithCover : UserControl
    {
        public MediaElementWithCover()
        {
            this.InitializeComponent();
        }

        public ImageSource CoverImage
        {
            get { return (ImageSource)GetValue(CoverImageProperty); }
            set { SetValue(CoverImageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CoverUrl.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CoverImageProperty =
            DependencyProperty.Register("CoverImage", typeof(ImageSource), typeof(MediaElementWithCover), null);




        public MediaElement Media
        {
            get { return (MediaElement)GetValue(MediaProperty); }
            set { SetValue(MediaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Media.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MediaProperty =
            DependencyProperty.Register("Media", typeof(MediaElement), typeof(MediaElementWithCover), null);


        private void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            coverImage.Tapped -= Image_Tapped;
            Media.Height = coverImage.ActualHeight;
            Media.Width = coverImage.ActualWidth;
            Media.CurrentStateChanged += Media_CurrentStateChanged;
            grid.Children.Add(Media);
            Media.Play();
        }

        private void Media_CurrentStateChanged(object sender, RoutedEventArgs e)
        {
            if (sender is MediaElement)
            {
                var m = sender as MediaElement;
                if (m.CurrentState == MediaElementState.Paused)
                {
                    m.CurrentStateChanged -= Media_CurrentStateChanged;
                    m.Play();
                }
            }
        }
    }
}
