using DownloadManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadManager.ViewModels
{
    class TestViewModel : ObservableObject, IMediaPanelViewModel
    {
        public string Title => "This is Title";

        public string Describ => "This is Describ";

        public string Avator => null;

        public bool IsImage => false;

        public bool IsVideo => true;

        public List<string> Images => null;

        public string VideoUrl => "http://xz.duoyi.com/videos/2017_campus_brand_ten.mp4";

        public string Content => null;
    }
}
