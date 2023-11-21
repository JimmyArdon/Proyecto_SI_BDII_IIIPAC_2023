using Microsoft.AspNetCore.Identity;

namespace WebApiPrestamos.Entities
{
    public class ApplicationDbContextData
    {
        public static async Task LoadDataAsync(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ILoggerFactory loggerFactory)
        {
            try
            {
                if (!roleManager.Roles.Any())
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                    await roleManager.CreateAsync(new IdentityRole("User"));
                }

                if (!userManager.Users.Any())
                {
                    var userAdmin = new IdentityUser
                    {
                        Email = "jperez@gmail.com",
                        UserName = "jperez@gmail.com"
                    };
                    await userManager.CreateAsync(userAdmin, "Cur0c@2020");
                    await userManager.AddToRoleAsync(userAdmin, "Admin");

                    var normalUser = new IdentityUser
                    {
                        Email = "pepito@gmail.com",
                        UserName = "pepito@gmail.com"
                    };
                    await userManager.CreateAsync(normalUser, "Cur0c@2020");
                    await userManager.AddToRoleAsync(normalUser, "User");
                }
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<ApplicationDbContext>();
                logger.LogError(e.Message);
            }
        }
    }
}
