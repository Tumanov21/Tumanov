using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Site.Models;
using Site.Models.Data;
using Site.ViewsModels.Supp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Site.Controllers
{
    public class SupplierController : Controller
    {

        private readonly ApplicationContext db;
        public SupplierController(ApplicationContext context)
        {
            db = context;
            if (db.Suppliers.Count()==0)
            {
                
            }
        }

        public async Task<IActionResult> Search(int? company, string email, SortState sort, int page =1)
        {
            int pageSize = 6;
            
            IQueryable<Supplier> sullpier = db.Suppliers.Include(c => c.Company);

            //Filter
            if (company!=null && company!=0)
            {
                sullpier = sullpier.Where(p => p.Company.Id == company);
            }
            if (!string.IsNullOrEmpty(email))
            {
                sullpier = sullpier.Where(p => p.Email.Contains(email));
            }

            //Sort
            switch (sort)
            {
                case SortState.EmailDecs:
                    sullpier = sullpier.OrderByDescending(c => c.Email);
                    break;
                case SortState.CompanyAsc:
                    sullpier = sullpier.OrderBy(c => c.Company);
                    break;
                case SortState.CompanyDecs:
                    sullpier = sullpier.OrderByDescending(c => c.Company);
                    break;
                case SortState.EmailAsc:
                    break;
                default:
                    sullpier = sullpier.OrderBy(c => c.Email);
                    break;
            }

            //Paging
            var count = await sullpier.CountAsync();
            var items = await sullpier.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            IndexViewsModels index = new IndexViewsModels()
            {
                FilterViewsModels = new FilterViewsModels(db.Companies.ToList(), company, email),
                SortViewsModels = new SortViewsModels(sort),
                PageViewsModels = new PageViewsModels(count,page,pageSize),
                Suppliers = items
            };
            return View(index);
        }
        #region CRUD

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id!=null)
            {
                var supplier = await db.Suppliers.Include(c => c.Company).FirstOrDefaultAsync(c => c.Id == id);
                if (supplier!=null)
                {
                    return View(supplier);
                }
                return View();
            }
            return RedirectToAction("Search");
        }

        [HttpPost]
        public IActionResult Details()
        {
            return RedirectToAction("Search");
        }

        [Authorize(Roles ="admin")]
        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                db.Suppliers.Add(supplier);
                await db.SaveChangesAsync();
                return RedirectToAction("Search");
            }
            return View(supplier);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id!=null)
            {
                var user = await db.Suppliers.Include(c=>c.Company).AsNoTracking().FirstOrDefaultAsync(c => c.Id==id);
                if (user!=null)
                {
                    return View(user);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Supplier supplier)
        {
            db.Suppliers.Update(supplier);
            await db.SaveChangesAsync();
            return RedirectToAction("Search");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id!=null)
            {
                var user = await db.Suppliers.Include(c=>c.Company).FirstOrDefaultAsync(c => c.Id == id);
                if (user!=null)
                {
                    return View(user);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id!=null)
            {
                Supplier user = await db.Suppliers.Include(c=>c.Company).AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
                if (user!=null)
                {
                    db.Suppliers.Remove(user);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Search");
                }
            }
            return NotFound();
        }
        #endregion
    }
}
