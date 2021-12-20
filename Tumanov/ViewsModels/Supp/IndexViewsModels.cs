using Site.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.ViewsModels.Supp
{
    public class IndexViewsModels
    {
        public IEnumerable<Supplier> Suppliers { get; set; }
        public PageViewsModels PageViewsModels { get; set; }
        public SortViewsModels SortViewsModels { get; set; }
        public FilterViewsModels FilterViewsModels { get; set; }
    }
}
