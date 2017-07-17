using DontPanic.TumblrSharp;
using DontPanic.TumblrSharp.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadManager
{
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
