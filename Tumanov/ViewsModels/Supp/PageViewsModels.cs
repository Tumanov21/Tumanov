using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.ViewsModels.Supp
{
    public class PageViewsModels
    {
        public int PageNumber { get; set; }
        public int TotalPage { get; set; }
        public PageViewsModels(int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            TotalPage = (int)Math.Ceiling(count / (double)pageSize);
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageNumber > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageNumber < TotalPage);
            }
        }
    }
}
