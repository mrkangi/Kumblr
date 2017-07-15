﻿// Generated by Xamasoft JSON Class Generator
// http://www.xamasoft.com/json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DownloadManager.Attributes;

namespace DownloadManager.Entities
{
    [Url(Url = "http://headers.{jsontest}.com/", Method = Literals.RequestMethod.GET)]
    public partial class HttpHeader : EntityBase
    {

        [JsonProperty("X-Cloud-Trace-Context")]
        public string XCloudTraceContext { get; set; }

        [JsonProperty("Upgrade-Insecure-Requests")]
        public string UpgradeInsecureRequests { get; set; }

        [JsonProperty("Accept-Language")]
        public string AcceptLanguage { get; set; }

        [JsonProperty("Host")]
        public string Host { get; set; }

        [JsonProperty("Referer")]
        public string Referer { get; set; }

        [JsonProperty("User-Agent")]
        public string UserAgent { get; set; }

        [JsonProperty("Accept")]
        public string Accept { get; set; }
    }

}
