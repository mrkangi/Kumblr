using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace DownloadManager.ViewModels
{
    interface IMediaPanelViewModel
    {
        string Title { get; }
        string Describ { get; }
        string Avator { get; }

        bool IsImage { get; }

        bool IsVideo { get; }

        string Type { get; }
        Color TypeColor { get; }

        List<string> Images { get; }

        string VideoUrl { get; }

        ImageSource VideoCover { get; }

        string Content { get; }
    }
}
