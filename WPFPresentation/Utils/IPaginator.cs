using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPFPresentation.Utils
{
    public interface IPaginator
    {
       

        void SetData();

        void ResetData();

        void MoveForWard();

        void MoveBackWard();

        void SetPaginable(IPagiAble paginable);

        int ItemsPerPage { get; set; }

        int ActualPage { get;set; }
    }

    public enum PaginatorMovement
    {
        Forward,
        Backward
    }
}
