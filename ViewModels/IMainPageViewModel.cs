﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadManager.ViewModels
{
    interface IMainPageViewModel
    {
        IPostIncrementalLoadingCollection PostData { get; }
    }
}
