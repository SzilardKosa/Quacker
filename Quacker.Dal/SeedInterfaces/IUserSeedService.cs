using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Quacker.Dal.SeedInterfaces
{
    public interface IUserSeedService
    {
        Task SeedUserAsync();
    }
}
