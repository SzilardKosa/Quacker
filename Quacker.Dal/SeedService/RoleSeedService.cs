using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Quacker.Dal.SeedInterfaces;
using Microsoft.AspNetCore.Identity;
using Quacker.Dal.UserRoles;

namespace Quacker.Dal.SeedService
{
    public class RoleSeedService : IRoleSeedService
    {
        private readonly RoleManager<IdentityRole<int>> roleManager;

        public RoleSeedService(RoleManager<IdentityRole<int>> roleManager)
        {
            this.roleManager = roleManager;
        }

        public async Task SeedRoleAsync()
        {
            if (!await roleManager.RoleExistsAsync(Roles.Administrator))
                await roleManager.CreateAsync(new IdentityRole<int> { Name = Roles.Administrator });
        }
    }
}
