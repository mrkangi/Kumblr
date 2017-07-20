using DontPanic.TumblrSharp;
using DontPanic.TumblrSharp.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadManager.Services
{
    class TumblrService : ITumblrService
    {
        public TumblrClient Client { get; }
        const string CONSUMER_KEY = "xx";
        const string CONSUMER_SECRET = "xx";
        const string OAUTH_TOKEN = "xx";
        const string OAUTH_TOKEN_SECRET = "xx";

        public TumblrService()
        {
            Client = new TumblrClientFactory().Create<TumblrClient>(CONSUMER_KEY, CONSUMER_SECRET, new DontPanic.TumblrSharp.OAuth.Token(OAUTH_TOKEN, OAUTH_TOKEN_SECRET));
        }
    }
}
