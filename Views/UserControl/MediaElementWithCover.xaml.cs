﻿using System;
using System.Collections.Generic;
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

        public string CoverUrl
        {
            get { return (string)GetValue(CoverUrlProperty); }
            set { SetValue(CoverUrlProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CoverUrl.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CoverUrlProperty =
            DependencyProperty.Register("CoverUrl", typeof(string), typeof(MediaElementWithCover), null);




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
            Media.Height = coverImage.ActualHeight;
            Media.Width = coverImage.ActualWidth;
            this.Content = Media;
            Media.Play();
        }
    }
}