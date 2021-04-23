using Microsoft.AspNetCore.Identity;
using Quacker.Dal.Entities;
using Quacker.Dal.SeedInterfaces;
using Quacker.Dal.UserRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quacker.Dal.SeedService
{
    public class UserSeedService : IUserSeedService
    {
        private readonly UserManager<User> userManager;

        public UserSeedService(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task SeedUserAsync()
        {

            // Create test users
            if (!userManager.Users.Any())
            {
                await SeedSingleUserAsync(new User
                {
                    DisplayName = "Marsh Holland",
                    HasImage = true,
                    Email = "user1@quacker.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "user1@quacker.com"
                });
                await SeedSingleUserAsync(new User
                {
                    DisplayName = "Minerva Harrett",
                    HasImage = true,
                    Email = "user2@quacker.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "user2@quacker.com"
                });
                await SeedSingleUserAsync(new User
                {
                    DisplayName = "Daisy Alvarado",
                    HasImage = true,
                    Email = "user3@quacker.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "user3@quacker.com"
                });
                await SeedSingleUserAsync(new User
                {
                    DisplayName = "Frank Kelley",
                    HasImage = true,
                    Email = "user4@quacker.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "user4@quacker.com"
                });
                await SeedSingleUserAsync(new User
                {
                    DisplayName = "Yvette Goodman",
                    HasImage = true,
                    Email = "user5@quacker.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "user5@quacker.com"
                });
                await SeedSingleUserAsync(new User
                {
                    DisplayName = "Shawn Curtis",
                    HasImage = true,
                    Email = "user6@quacker.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "user6@quacker.com"
                });
                await SeedSingleUserAsync(new User
                {
                    DisplayName = "Gaylord Mcgee",
                    HasImage = true,
                    Email = "user7@quacker.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "user7@quacker.com"
                });
                await SeedSingleUserAsync(new User
                {
                    DisplayName = "Alec Reid",
                    HasImage = true,
                    Email = "user8@quacker.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "user8@quacker.com"
                });
                await SeedSingleUserAsync(new User
                {
                    DisplayName = "Neal Mack",
                    HasImage = true,
                    Email = "user9@quacker.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "user9@quacker.com"
                });
                await SeedSingleUserAsync(new User
                {
                    DisplayName = "Britney Abbott",
                    HasImage = true,
                    Email = "user10@quacker.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "user10@quacker.com"
                });
                await SeedSingleUserAsync(new User
                {
                    DisplayName = "Angela Gray",
                    HasImage = true,
                    Email = "user11@quacker.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "user11@quacker.com"
                });
                await SeedSingleUserAsync(new User
                {
                    DisplayName = "Tabitha Dean",
                    HasImage = true,
                    Email = "user12@quacker.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "user12@quacker.com"
                });
                await SeedSingleUserAsync(new User
                {
                    DisplayName = "Marmaduke Schwartz",
                    HasImage = true,
                    Email = "user13@quacker.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "user13@quacker.com"
                });
                await SeedSingleUserAsync(new User
                {
                    DisplayName = "Pearl Moreno",
                    HasImage = true,
                    Email = "user14@quacker.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "user14@quacker.com"
                });
                await SeedSingleUserAsync(new User
                {
                    DisplayName = "Danielle Wintringham",
                    HasImage = true,
                    Email = "user15@quacker.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "user15@quacker.com"
                });
                await SeedSingleUserAsync(new User
                {
                    DisplayName = "Hughie Taylor",
                    HasImage = true,
                    Email = "user16@quacker.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "user16@quacker.com"
                });
                await SeedSingleUserAsync(new User
                {
                    DisplayName = "Gregory Walker",
                    HasImage = true,
                    Email = "user17@quacker.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "user17@quacker.com"
                });
                await SeedSingleUserAsync(new User
                {
                    DisplayName = "Albert French",
                    HasImage = true,
                    Email = "user18@quacker.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "user18@quacker.com"
                });
                await SeedSingleUserAsync(new User
                {
                    DisplayName = "Duncan Barnes",
                    HasImage = true,
                    Email = "user19@quacker.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "user19@quacker.com"
                });
                await SeedSingleUserAsync(new User
                {
                    DisplayName = "Beatrice Johnston",
                    HasImage = true,
                    Email = "user20@quacker.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "user20@quacker.com"
                });
            }

            // Create admin
            if (!(await userManager.GetUsersInRoleAsync(Roles.Administrator)).Any())
            {
                var user = new User
                {
                    DisplayName = "Adminisztrátor Aladár",
                    Email = "admin@quacker.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "admin@quacker.com"
                };

                var createResult = await userManager.CreateAsync(user, "Admin123!");
                var addToRoleResult = await userManager.AddToRoleAsync(user, Roles.Administrator);

                if (!createResult.Succeeded || !addToRoleResult.Succeeded)
                {
                    throw new ApplicationException("Administrator could not be created:" +
                        String.Join(", ", createResult.Errors.Concat(addToRoleResult.Errors).Select(e => e.Description)));
                }
            }
        }

        private async Task SeedSingleUserAsync(User user)
        {
            var createResult = await userManager.CreateAsync(user, "User123!");

            if (!createResult.Succeeded)
            {
                throw new ApplicationException("User could not be created:" +
                    String.Join(", ", createResult.Errors.Select(e => e.Description)));
            }

        }
    }
}
