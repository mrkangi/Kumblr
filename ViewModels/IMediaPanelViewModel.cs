using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadManager.ViewModels
{
    interface IMediaPanelViewModel
    {
        string Title { get; }
        string Describ { get; }
        string Avator { get; }

        bool IsImage { get; }

        bool IsVideo { get; }

        List<string> Images { get; }

        string VideoUrl { get; }

        string Content { get; }
    }
}
