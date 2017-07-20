using DontPanic.TumblrSharp.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadManager.Services
{
    interface ITumblrService
    {
        TumblrClient Client { get; }
    }
}
