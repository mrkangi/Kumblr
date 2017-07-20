using DontPanic.TumblrSharp.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadManager.ViewModels
{
    public class MediaPanelViewModel : ObservableObject, IMediaPanelViewModel
    {
        BasePost post;

        public MediaPanelViewModel()
        {
            this.post = null;
        }

        public MediaPanelViewModel(BasePost post)
        {
            this.post = post;
        }

        public string Title => post?.BlogName;
        public string Describ => post?.SourceTitle;
        public string Avator => null;

        public bool IsImage => post?.Type == DontPanic.TumblrSharp.PostType.Photo;

        public bool IsVideo => post?.Type == DontPanic.TumblrSharp.PostType.Video;

        public List<string> Images => IsImage ? (post as PhotoPost)?.PhotoSet.Select(item => item.AlternateSizes[item.AlternateSizes.Length - 1].ImageUrl).ToList() : new List<string>();

        public string VideoUrl => IsVideo ? (post as VideoPost)?.VideoUrl : null;

        public string Content => null;
    }
}
