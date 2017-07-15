using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadManager
{
    class Literals
    {
        public enum RequestMethod
        {
            GET,
            POST,
            DELETE,
            PUT,
        }

        public enum MediaType
        {
            VIDEO,
            IMAGE,
            TEXT
        }
    }
}
