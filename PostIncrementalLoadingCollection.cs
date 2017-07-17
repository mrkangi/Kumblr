using DontPanic.TumblrSharp.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace DownloadManager
{
    class PostIncrementalLoadingCollection : IncrementalLoadingBase<BasePost>
    {
        protected override bool HasMoreItemsOverride()
        {
            return true;
        }

        protected override async Task<IList<BasePost>> LoadMoreItemsOverrideAsync(CancellationToken c, uint count)
        {
            return await Tumblr.Client.GetDashboardPostsAsync();
        }
    }

    class TestModel
    {
        public string Value { get; set; }
    }

    class TestIncrementalLoadingCollection : IncrementalLoadingBase<TestModel>
    {
        HttpClient client = new HttpClient();

        protected override bool HasMoreItemsOverride()
        {
            return true;
        }

        protected override async Task<IList<TestModel>> LoadMoreItemsOverrideAsync(CancellationToken c, uint count)
        {
            var values = JArray.Parse(await client.GetStringAsync("http://localhost:57239/api/values")).Values<string>().Select(item => new TestModel() { Value = item }).ToList();
            return values;
        }
    }
}
