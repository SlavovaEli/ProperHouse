using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ProperHouse.Infrastructure.Data;
using ProperHouse.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProperHouse.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;
                        
            SeedCategories(services);
            
            return app;
        }

        private static void SeedCategories(IServiceProvider services)
        {
            var dbContext = services.GetRequiredService<ProperHouseDbContext>();

            if(dbContext.Categories.Any())
            {
                return;
            }

             dbContext.Categories.AddRange(new [] 
            { 
                new Category { Name = "Studio"},
                new Category { Name = "One bedroom property"},
                new Category { Name = "Two bedroom property"},
                new Category { Name = "Three bedroom property"},
                new Category { Name = "Big property"}               

            });

             dbContext.SaveChanges();
        }
    }
}
