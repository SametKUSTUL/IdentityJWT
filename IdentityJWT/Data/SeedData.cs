using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityJWT.Data
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<AppIdentityDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<AppliicationUser>>();

            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string roleAdmin = "admin";
            string roleEditor = "editor";


            context.Database.EnsureCreated(); // Veritabanını oluşturdu.


            if (!context.Users.Any())
            {
                AppliicationUser user = new AppliicationUser()
                {
                    UserName = "Samet",
                    Email = "sametkustul@gmail.com",
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                await userManager.CreateAsync(user, "@Password123");

                if (!context.Roles.Any())
                {
                    if (await roleManager.FindByNameAsync(roleAdmin) == null)
                    {
                        await roleManager.CreateAsync(new IdentityRole(roleAdmin)); // Role tablosuna admin eklendi
                    }

                    if (await roleManager.FindByNameAsync(roleEditor) == null)
                    {
                        await roleManager.CreateAsync(new IdentityRole(roleEditor)); // Role tablosuna editor eklendi
                    }

                    await userManager.AddToRoleAsync(user, roleAdmin);


                }
            }

        }
    }
}
