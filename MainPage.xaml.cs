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

using DontPanic.TumblrSharp;
using DontPanic.TumblrSharp.Client;
using DontPanic.TumblrSharp.OAuth;
using System.Net.Sockets;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using System.Text;
using System.Net.Http;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace DownloadManager
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        int port = 8000;
        public MainPage()
        {
            this.InitializeComponent();
            //scroller.LayoutUpdated += Scroller_LayoutUpdated;
            listener.Control.KeepAlive = true;
            listener.ConnectionReceived += async (s, args) =>
            {
                Debug.WriteLine("Got connection");
                using (var dr = new DataReader(args.Socket.InputStream))
                {
                    dr.InputStreamOptions = InputStreamOptions.Partial;

                    await dr.LoadAsync(12);
                    var input = dr.ReadString(12);

                    Debug.WriteLine("received: " + input);
                }

                using (var dw = new DataWriter(args.Socket.OutputStream))
                {
                    dw.WriteString(@"HTTP/1.1 200 OK
Cache-Control: private
Connection: Keep-Alive
Content-Length: 19
Content-Type: text/html; charset=UTF-8
Server: kumblr

<h1>HelloWorld</h1>
");
                    await dw.StoreAsync();
                    dw.DetachStream();
                }
            };
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
            item.OnImageDoubleTapped += MediaPanelControl_OnImageDoubleTapped;
            item.AddImage("http://www.uwpcommunitytoolkit.com/en/master/resources/images/Controls-DropShadowPanel.png");
            item.AddImage("http://www.uwpcommunitytoolkit.com/en/master/resources/images/Controls-HamburgerMenu.gif");
            item.AddImage("http://www.uwpcommunitytoolkit.com/en/master/resources/images/Controls-DropShadowPanel.png");
            item.AddImage("http://www.uwpcommunitytoolkit.com/en/master/resources/images/Controls-HamburgerMenu.gif");
            item.AddImage("http://www.uwpcommunitytoolkit.com/en/master/resources/images/Controls-DropShadowPanel.png");
            item.AddImage("http://www.uwpcommunitytoolkit.com/en/master/resources/images/Controls-HamburgerMenu.gif");
            item.AddImage("http://www.uwpcommunitytoolkit.com/en/master/resources/images/Controls-DropShadowPanel.png");
            contentRoot.Items.Add(item);
            item = new MediaPanelControl()
            {
                Type = Literals.MediaType.VIDEO,
                Width = 300,
                Height = 400,
            };
            item.ShowVideo("http://xz.duoyi.com/videos/employee_interview.mp4");
            contentRoot.Items.Add(item);
            Debug.WriteLine("helloworld");
        }

        private async void ComplexListItem_Holding(object sender, HoldingRoutedEventArgs e)
        {
            MessageDialog dialog = new MessageDialog("Holding");
            await dialog.ShowAsync();
        }

        private async void MenuButton1_Click(object sender, RoutedEventArgs e)
        {
            //var user = await Tumblr.Client.GetUserInfoAsync();


            //var following = await Tumblr.Client.GetFollowingAsync();

            //var dashboard = await Tumblr.Client.GetDashboardPostsAsync();

            //foreach (var item in dashboard)
            //{
            //    if (item.Type == PostType.Video)
            //    {
            //        var v = new MediaPanelControl()
            //        {
            //            Type = Literals.MediaType.VIDEO,
            //            Width = 300,
            //            Height = 400,
            //        };
            //        v.ShowVideo((item as VideoPost)?.VideoUrl);
            //        contentRoot.Children.Add(v);
            //    }

            //    else if (item.Type == PostType.Photo)
            //    {
            //        var img = new MediaPanelControl()
            //        {
            //            Type = Literals.MediaType.IMAGE,
            //            Width = 300,
            //            Height = 400,
            //        };
            //        var images = (item as PhotoPost).PhotoSet;
            //        foreach (var image in images)
            //        {
            //            img.AddImage(image.OriginalSize.ImageUrl);
            //        }
            //        contentRoot.Children.Add(img);

            //    }
            //}
            //OAuthClient oauthClient = new OAuthClient(
            //new DontPanic.TumblrSharp.HmacSha1HashProvider(),
            //Tumblr.CONSUMER_KEY,
            //Tumblr.CONSUMER_SECRET);

            //// get a request token
            //// replace "noted://" with your own callback URI
            //Token requestToken = await oauthClient.GetRequestTokenAsync("kumblr://");

            //var authenticateUrl = "https://www.tumblr.com/oauth/authorize?oauth_token=" + requestToken.Key;


            //// depending on your project, you might open up a web browser a different way
            //WebView webView = new WebView(WebViewExecutionMode.SeparateThread);
            //webView.Source = new Uri(authenticateUrl);
            //webView.StartBringIntoView();
            //ContentDialog web = new ContentDialog();
            //web.Content = webView;
            //web.FullSizeDesired = true;
            //web.IsPrimaryButtonEnabled = true;
            //await web.ShowAsync();
            ContentDialog web = new ContentDialog();
            await listener.BindServiceNameAsync(port.ToString());
            Debug.WriteLine("Bound to port: " + port.ToString());
            HttpClient hc = new HttpClient();
            //var str = await hc.GetStringAsync("");

            WebView webView = new WebView(WebViewExecutionMode.SeparateThread);
            webView.Source = new Uri("http://127.0.0.1:8000");

            web.Content = webView;
            web.FullSizeDesired = true;
            web.IsSecondaryButtonEnabled = true;
            web.IsPrimaryButtonEnabled = true;
            await web.ShowAsync();


        }

        PostIncrementalLoadingCollection postData;

        TestIncrementalLoadingCollection testData = new TestIncrementalLoadingCollection();

        private void Scroller_LayoutUpdated(object sender, object e)
        {
        }

        static StreamSocketListener listener = new StreamSocketListener();

        private void MediaPanelControl_OnImageDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            var img = sender as Image;
            contentContainer.Children.Add(new Image()
            {
                Source = img.Source,
            });
            contentContainer.Tapped += ContentContainer_Tapped;
            contentContainer.Visibility = Visibility.Visible;
        }

        private void ContentContainer_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var grid = (sender as Grid);
            grid.Visibility = Visibility.Collapsed;
            grid.Children.Clear();
        }
    }
}
