using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Models.Data
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Supplier> Suppliers { get; set; }
        public Company()
        {
            Suppliers = new List<Supplier>();
        }
    }
}
