using Microsoft.EntityFrameworkCore;
using ProperHouse.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProperHouse.Test.Mocks
{
    public class DatabaseMock
    {
        public static ProperHouseDbContext Instance
        {
            get
            {
                var options = new DbContextOptionsBuilder<ProperHouseDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                    .Options;

                var dbContext = new ProperHouseDbContext(options);

                return dbContext;
            }
        }
    }


}
