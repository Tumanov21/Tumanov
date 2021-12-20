using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Models
{
    public class RoleInitializer
    {
        public static async Task InitializerAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "admin@mail.ru";
            string passwordAdmin = "!23Qwe";
            string userEmail = "user@mail.ru";
            string passwordUser = "!23Qwe";

            if (await roleManager.FindByNameAsync("admin")==null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }

            if (await roleManager.FindByNameAsync("employee") ==null)
            {
                await roleManager.CreateAsync(new IdentityRole("employee"));
            }

            if (await roleManager.FindByNameAsync("user")==null)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }

            if (await roleManager.FindByNameAsync(adminEmail)==null)
            {
                var admin = new User { Email = adminEmail, UserName = adminEmail };
                var user = new User { Email = userEmail, UserName = userEmail };
                var resultUser = await userManager.CreateAsync(user, passwordUser);
                if (resultUser.Succeeded)
                {
                    await userManager.CreateAsync(user, "user");
                }
                var resultAdmin = await userManager.CreateAsync(admin, passwordAdmin);
                if (resultAdmin.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }
    }
}
