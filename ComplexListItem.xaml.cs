using System;
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
    public sealed partial class ComplexListItem : UserControl
    {
        public ComplexListItem()
        {
            this.InitializeComponent();
            (this.Content as FrameworkElement).DataContext = this;
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }


        public static readonly DependencyProperty TitleProperty =
      DependencyProperty.Register("Title", typeof(string),
        typeof(ComplexListItem), new PropertyMetadata(null));

        public string Describe
        {
            get { return (string)GetValue(DescribeProperty); }
            set { SetValue(DescribeProperty, value); }
        }
        public static readonly DependencyProperty DescribeProperty =
      DependencyProperty.Register("Describe", typeof(string),
        typeof(ComplexListItem), new PropertyMetadata(null));
    }
}
