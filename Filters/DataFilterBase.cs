using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadManager.Filters
{
    abstract class DataFilterBase<T>
    {
        private DataFilterBase<T> next;

        public DataFilterBase(DataFilterBase<T> next)
        {
            this.next = next;
        }

        public T Filtration(T data)
        {
            return this.InnerFiltration(this.next.Filtration(data));
        }

        protected abstract T InnerFiltration(T data);
    }
}
