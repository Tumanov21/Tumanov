using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.ViewsModels.Supp
{
    public class SortViewsModels
    {
        public SortState EmailSort { get; private set; }
        public SortState CompanySort { get; private set; }
        public SortState Current { get; private set; }

        public SortViewsModels(SortState sort)
        {
            EmailSort = sort == SortState.EmailAsc ? SortState.EmailDecs : SortState.EmailAsc;
            CompanySort = sort == SortState.CompanyAsc ? SortState.CompanyDecs : SortState.CompanyAsc;
        }
    }
}
