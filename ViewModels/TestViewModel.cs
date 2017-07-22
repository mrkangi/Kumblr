using DownloadManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace DownloadManager.ViewModels
{
    class TestViewModel : ObservableObject, IMediaPanelViewModel
    {
        static Random rand = new Random();
        public TestViewModel()
        {
            IsImage = rand.Next(100) >= 50;
        }
        public string Title => "This is Title";

        public string Describ => "This is Describ";

        public string Avator => null;

        public bool IsImage { get; set; }

        public bool IsVideo => !IsImage;

        public List<string> Images => new List<string>() { "https://i.stack.imgur.com/303rh.png", "http://images2015.cnblogs.com/blog/731716/201609/731716-20160915153252883-1855674390.gif", "http://cute-n-tiny.com/wp-content/uploads/2009/02/rcp101-kitten-puppy.jpg", "http://www.qbaobei.com/UploadFiles/shtk/2012/12/201212071141473736.jpg" };

        public string VideoUrl => "http://xz.duoyi.com/videos/2017_campus_brand_ten.mp4";

        public string Content => null;

        public ImageSource VideoCover => new BitmapImage(new Uri("http://images2015.cnblogs.com/blog/731716/201609/731716-20160915153252883-1855674390.gif"));
    }
}
