using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProperHouse.Infrastructure.Data
{
    public class ProperHouseDbContext : IdentityDbContext
    {
        public ProperHouseDbContext(DbContextOptions<ProperHouseDbContext> options)
            : base(options)
        {
        }
    }
}