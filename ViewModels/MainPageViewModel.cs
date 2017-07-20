using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadManager.ViewModels
{
    class MainPageViewModel : IMainPageViewModel
    {
        IPostIncrementalLoadingCollection postData;
        public MainPageViewModel(IPostIncrementalLoadingCollection postData)
        {
            this.postData = postData;
        }
        public IPostIncrementalLoadingCollection PostData => postData;
    }
}
