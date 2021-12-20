using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Site.Models;
using Site.ViewsModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Site.Controllers
{
    [Authorize(Roles ="admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<User> _userManager;
        public UsersController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index() => View(_userManager.Users.ToList());

        

        #region CRUD
        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateUsersViewsModels create)
        {
            if (ModelState.IsValid)
            {
                var user = new User { Email = create.Email, UserName = create.Email, Year = create.Year };
                var result = await _userManager.CreateAsync(user, create.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(create);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            //if (user!=null)
            //{
            //    EditUsersViewsModels edit = new EditUsersViewsModels { Id = user.Id, Email = user.Email, Year = user.Year };
            //    return View(user);
            //}
            //return NotFound();
            if (user == null)
            {
                return NotFound();
            }
            EditUsersViewsModels model = new EditUsersViewsModels { Id = user.Id, Email = user.Email, Year = user.Year };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUsersViewsModels edit)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(edit.Id);
                if (user!=null)
                {
                    user.Email = edit.Email;
                    user.UserName = edit.Email;
                    user.Year = edit.Year;

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return View(edit);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user!=null)
            {
                var result = await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Password
        [HttpGet]
        public async Task<IActionResult> Password(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user==null)
            {
                return NotFound();
            }
            PasswordUsersViewsModels password = new PasswordUsersViewsModels { Id = user.Id, Email = user.Email };
            return View(password);
        }

        [HttpPost]
        public async Task<IActionResult> Password(PasswordUsersViewsModels password)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(password.Id);
                if (user!=null)
                {
                    var _passwordValidator = HttpContext.RequestServices.GetService(typeof(IPasswordValidator<User>)) as IPasswordValidator<User>;
                    var _passwordHasher = HttpContext.RequestServices.GetService(typeof(IPasswordHasher<User>)) as IPasswordHasher<User>;

                    var result = await _passwordValidator.ValidateAsync(_userManager, user, password.NewPassword);
                    if (result.Succeeded)
                    {
                        user.PasswordHash = _passwordHasher.HashPassword(user, password.NewPassword);
                        await _userManager.UpdateAsync(user);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "NotFound");
                }
            }
            return View(password);
        }
        #endregion
    }
}
