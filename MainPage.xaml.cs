using DownloadManager.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace DownloadManager
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private async void ComplexListItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            HttpHeader header = await EntityFactory.ToOne<HttpHeader>(urlSegPlaceholder: new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("jsontest", "jsontest") });
            //var item = new ListViewItem()
            //{
            //    Content = new ComplexListItem()
            //    {
            //        Title = header.Host,
            //        Describe = header.Referer,
            //    }
            //};
            var item = new MediaPanelControl()
            {
                Type = Literals.MediaType.IMAGE,
                Width = 300,
                Height = 400,
            };
            item.AddImage("http://www.uwpcommunitytoolkit.com/en/master/resources/images/Controls-DropShadowPanel.png");
            item.AddImage("http://www.uwpcommunitytoolkit.com/en/master/resources/images/Controls-HamburgerMenu.gif");
            item.AddImage("http://www.uwpcommunitytoolkit.com/en/master/resources/images/Controls-DropShadowPanel.png");
            item.AddImage("http://www.uwpcommunitytoolkit.com/en/master/resources/images/Controls-HamburgerMenu.gif");
            item.AddImage("http://www.uwpcommunitytoolkit.com/en/master/resources/images/Controls-DropShadowPanel.png");
            item.AddImage("http://www.uwpcommunitytoolkit.com/en/master/resources/images/Controls-HamburgerMenu.gif");
            item.AddImage("http://www.uwpcommunitytoolkit.com/en/master/resources/images/Controls-DropShadowPanel.png");
            contentRoot.Children.Add(item);
            item = new MediaPanelControl()
            {
                Type = Literals.MediaType.VIDEO,
                Width = 300,
                Height = 400,
            };
            item.ShowVideo("http://xz.duoyi.com/videos/employee_interview.mp4");
            contentRoot.Children.Add(item);
            Debug.WriteLine("helloworld");
        }

        private async void ComplexListItem_Holding(object sender, HoldingRoutedEventArgs e)
        {
            MessageDialog dialog = new MessageDialog("Holding");
            await dialog.ShowAsync();
        }
    }
}
