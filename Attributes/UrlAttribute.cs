using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DownloadManager.Literals;

namespace DownloadManager.Attributes
{
    class UrlAttribute : Attribute
    {
        public string Url { get; set; }

        public RequestMethod Method { get; set; }
    }
}
