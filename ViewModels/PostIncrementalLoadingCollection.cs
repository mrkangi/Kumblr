using DontPanic.TumblrSharp.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using DownloadManager.Services;

namespace DownloadManager.ViewModels
{
    class PostIncrementalLoadingCollection : IncrementalLoadingBase<IMediaPanelViewModel>, IPostIncrementalLoadingCollection
    {
        ITumblrService tumblr;
        int totalCount = 0;
        public PostIncrementalLoadingCollection(ITumblrService tumblr)
        {
            this.tumblr = tumblr;
        }

        protected override bool HasMoreItemsOverride()
        {
            return true;
        }

        protected override async Task<IList<IMediaPanelViewModel>> LoadMoreItemsOverrideAsync(CancellationToken c, uint count)
        {
            var list = (await tumblr.Client.GetDashboardPostsAsync(startIndex: Count)).Select(item => (IMediaPanelViewModel)new MediaPanelViewModel(item)).ToList();
            totalCount++;
            return list;
        }
    }

    class TestModel
    {
        public string Value { get; set; }
    }

    class TestIncrementalLoadingCollection : IncrementalLoadingBase<IMediaPanelViewModel>, IPostIncrementalLoadingCollection
    {
        HttpClient client = new HttpClient();

        protected override bool HasMoreItemsOverride()
        {
            return true;
        }

        protected override async Task<IList<IMediaPanelViewModel>> LoadMoreItemsOverrideAsync(CancellationToken c, uint count)
        {
            var values = JArray.Parse(await client.GetStringAsync("http://localhost:57239/api/values")).Values<string>().Select(item => (IMediaPanelViewModel)new TestViewModel() { }).ToList();
            return values;
        }
    }
}
