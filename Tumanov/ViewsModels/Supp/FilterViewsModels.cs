using Microsoft.AspNetCore.Mvc.Rendering;
using Site.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.ViewsModels.Supp
{
    public class FilterViewsModels
    {
        public FilterViewsModels(List<Company> companies, int? company ,string email)
        {
            companies.Insert(0, new Company { Name = "All", Id = 0 });
            Company = new SelectList( companies, "Id", "Name", email);
            SelectedCompanies = company;
            SelectedName = email;
        }
        public SelectList Company { get; set; }
        public int? SelectedCompanies { get; set; }
        public string SelectedName { get; set; }
    }
}
