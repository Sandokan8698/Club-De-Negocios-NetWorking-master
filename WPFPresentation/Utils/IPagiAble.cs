using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFPresentation.Utils
{
    public interface IPagiAble
    {
        bool IsLoading { get; }

        int GetTotalItems();

        void Paginate(int page, int pageSize);

        void SetPaginator(IPaginator paginator);
    }
}
