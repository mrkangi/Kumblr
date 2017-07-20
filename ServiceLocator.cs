using DownloadManager.Services;
using DownloadManager.ViewModels;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadManager
{
    public class ServiceLocator
    {
        IServiceCollection collection = new ServiceCollection();

        IServiceProvider svp = null;
        private ServiceLocator()
        {
            collection.TryAddSingleton<ISocketDataService, SocketDataService>();
            collection.TryAddSingleton<ITumblrService, TumblrService>();
            collection.TryAddTransient<IMediaPanelViewModel, TestViewModel>();
            collection.TryAddTransient<IMainPageViewModel, MainPageViewModel>();
            //collection.TryAddTransient<IMediaPanelViewModel, MediaPanelViewModel>();
            //collection.TryAddTransient<IPostIncrementalLoadingCollection, PostIncrementalLoadingCollection>();
            collection.TryAddTransient<IPostIncrementalLoadingCollection, TestIncrementalLoadingCollection>();


            svp = collection.BuildServiceProvider();
        }

        private static ServiceLocator locater;
        public static ServiceLocator Current
        {
            get { return locater = locater ?? new ServiceLocator(); }
        }

        public T Get<T>()
        {
            return svp.GetService<T>();
        }
    }
}
