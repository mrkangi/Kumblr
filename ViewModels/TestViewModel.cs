using DownloadManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadManager.ViewModels
{
    class TestViewModel : ObservableObject, ITestViewModel
    {
        public TestViewModel()
        {
        }
        public TestIncrementalLoadingCollection TestData { get; } = new TestIncrementalLoadingCollection();
    }
}
