using DownloadManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Reflection.Metadata;
using DownloadManager.Attributes;
using System.Net.Http;

namespace DownloadManager
{
    class EntityFactory
    {
        public static async Task<List<T>> ToList<T>(
            List<KeyValuePair<string, string>> queryString = null,
            List<KeyValuePair<string, string>> forms = null,
            List<KeyValuePair<string, string>> urlSegPlaceholder = null) where T : EntityBase
        {
            string response = await Request<T>(queryString, forms, urlSegPlaceholder);

            if (response != null)
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(response);
            }
            else
                return new List<T>();
        }

        private static string BuildQueryString(List<KeyValuePair<string, string>> querys)
        {
            if (querys == null) return "";
            return string.Join("&", querys.Select(item => $"{item.Key}={item.Value}"));
        }

        private static string ReplacePlaceholder(string url, List<KeyValuePair<string, string>> urlSegPlaceholder)
        {
            foreach (var kv in urlSegPlaceholder)
            {
                url = url.Replace($"{{{kv.Key}}}", kv.Value);
            }
            return url;
        }

        public static async Task<T> ToOne<T>(
            List<KeyValuePair<string, string>> queryString = null,
            List<KeyValuePair<string, string>> forms = null,
            List<KeyValuePair<string, string>> urlSegPlaceholder = null) where T : EntityBase
        {
            string response = await Request<T>(queryString, forms, urlSegPlaceholder);

            if (response != null)
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(response);
            }
            else
                return default(T);
        }

        private static async Task<string> Request<T>(
            List<KeyValuePair<string, string>> queryString = null,
            List<KeyValuePair<string, string>> forms = null,
            List<KeyValuePair<string, string>> urlSegPlaceholder = null) where T : EntityBase
        {
            var urlAttr = (UrlAttribute)typeof(T).GetTypeInfo().GetCustomAttribute(typeof(UrlAttribute));
            HttpClient client = new HttpClient();

            

            string response = null;
            string url = $"{ReplacePlaceholder(urlAttr.Url, urlSegPlaceholder)}?{BuildQueryString(queryString)}";
            switch (urlAttr.Method)
            {
                case Literals.RequestMethod.GET:
                    {
                        response = await client.GetStringAsync(url);
                    }; break;
                case Literals.RequestMethod.POST:
                    {
                        response = await (await client.PostAsync(url, new FormUrlEncodedContent(forms))).Content.ReadAsStringAsync();
                    }; break;
            }

            return response;
        }
    }
}
