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

        private async void MenuButton1_Click(object sender, RoutedEventArgs e)
        {
            var user = await Tumblr.Client.GetUserInfoAsync();


            var following = await Tumblr.Client.GetFollowingAsync();

            var dashboard = await Tumblr.Client.GetDashboardPostsAsync();

            foreach (var item in dashboard)
            {
                if (item.Type == PostType.Video)
                {
                    var v = new MediaPanelControl()
                    {
                        Type = Literals.MediaType.VIDEO,
                        Width = 300,
                        Height = 400,
                    };
                    v.ShowVideo((item as VideoPost)?.VideoUrl);
                    contentRoot.Children.Add(v);
                }

                else if (item.Type == PostType.Photo)
                {
                    var img = new MediaPanelControl()
                    {
                        Type = Literals.MediaType.IMAGE,
                        Width = 300,
                        Height = 400,
                    };
                    var images = (item as PhotoPost).PhotoSet;
                    foreach (var image in images)
                    {
                        img.AddImage(image.OriginalSize.ImageUrl);
                    }
                    contentRoot.Children.Add(img);

                }
            }
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


        }
    }


    public static class Tumblr
    {
        public static string Blog { get; set; }
        public static TumblrClient Client;
        public const string CONSUMER_KEY = "xx";
        public const string CONSUMER_SECRET = "xx";
        const string OAUTH_TOKEN = "xx";
        const string OAUTH_TOKEN_SECRET = "xx";

        static Tumblr()
        {
            // create our client
            Client = new TumblrClientFactory().Create<TumblrClient>(CONSUMER_KEY, CONSUMER_SECRET, new DontPanic.TumblrSharp.OAuth.Token(OAUTH_TOKEN, OAUTH_TOKEN_SECRET));
        }
    }
}
