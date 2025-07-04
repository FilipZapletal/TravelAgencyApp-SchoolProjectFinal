using Microsoft.AspNetCore.Identity;
using TravelAgencyApp.Models;

namespace TravelAgencyApp.Data
{
    public class IdentitySeeder
    {
        public static async Task SeedRolesAndAdmin(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            string[] roleNames = { "Admin", "User", "Worker" };

            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                    await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            var adminEmail = "admin@admin.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                var user = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, "Admin123!");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
            var workerEmail = "worker@agency.com";
            var workerUser = await userManager.FindByEmailAsync(workerEmail);
            if (workerUser == null)
            {
                var user = new ApplicationUser
                {
                    UserName = workerEmail,
                    Email = workerEmail,
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(user, "Worker123!");
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(user, "Worker");
            }

        }
    }
}
