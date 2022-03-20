using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProperHouse.Data
{
    public class ProperHouseDbContext : IdentityDbContext
    {
        public ProperHouseDbContext(DbContextOptions<ProperHouseDbContext> options)
            : base(options)
        {
        }
    }
}