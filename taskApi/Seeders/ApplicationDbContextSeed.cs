using Microsoft.AspNetCore.Identity;
using taskApi.Data;

namespace taskApi.Seeders
{
    public static class ApplicationDbContextSeed
    {

        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager)
        {
            var defaultUser = new ApplicationUser { UserName = "admin", Email = "admin@example.com" };

            if (await userManager.FindByEmailAsync(defaultUser.Email) == null)
            {
                var result = await userManager.CreateAsync(defaultUser, "Admin.c0m");
            }
        }


    }
}
