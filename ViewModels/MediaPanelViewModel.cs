using DontPanic.TumblrSharp.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI;

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
        public bool IsText => post?.Type == DontPanic.TumblrSharp.PostType.Text;


        public List<string> Images => IsImage ? (post as PhotoPost)?.PhotoSet.Select(item => item.AlternateSizes[0].ImageUrl).ToList() : new List<string>();

        public string VideoUrl => IsVideo ? (post as VideoPost)?.VideoUrl : null;

        private readonly string scriptForHeight = @"<html>
<head>
    <style>
        html,
        body {{
            margin: 0;
            padding: 0;
            height: 100%;
        }}
    </style>
    <script>
        function getHeight() {{
            var height = document.getElementById('wrapper').offsetHeight;
            window.external.notify('' + height);
            }}
    </script>
</head>
<body>
    <div id='wrapper'>
        {0}
    </div>
</body>
</html>";

        public string Content => string.Format(scriptForHeight, IsImage ? (post as PhotoPost)?.Caption : IsVideo ? (post as VideoPost)?.Caption : IsText ? (post as TextPost)?.Body : null);

        public ImageSource VideoCover => IsVideo ? new BitmapImage(new Uri((post as VideoPost)?.ThumbnailUrl)) : null;

        public string Type => post?.Type.ToString().ToLower();

        public Color TypeColor => typeColorMap.ContainsKey(post.Type) ? typeColorMap[post.Type] : Colors.Gray;

        private static Dictionary<DontPanic.TumblrSharp.PostType, Color> typeColorMap = new Dictionary<DontPanic.TumblrSharp.PostType, Color>()
        {
            [DontPanic.TumblrSharp.PostType.Video] = Colors.LightGreen,
            [DontPanic.TumblrSharp.PostType.Photo] = Colors.LightSkyBlue,
        };

    }
}
