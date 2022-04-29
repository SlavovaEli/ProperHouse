using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ProperHouse.Infrastructure.Data;
using ProperHouse.Infrastructure.Data.Models;

namespace ProperHouse.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {        
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;
                        
            SeedCategories(services);
            SeedAdministrator(services);
            SeedUsers(services);
            SeedOwners(services);
            SeedProperties(services);
            
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

        private static void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync("Administrator"))
                    {
                        return;
                    }

                    var role = new IdentityRole { Name = "Administrator" };

                    await roleManager.CreateAsync(role);

                    const string adminEmail = "admin@prop.com";
                    const string adminPassword = "admin12";

                    var user = new User
                    {
                        Email = adminEmail,
                        UserName = adminEmail,
                        FullName = "Admin"
                    };

                    await userManager.CreateAsync(user, adminPassword);

                    await userManager.AddToRoleAsync(user, role.Name);
                })
                .GetAwaiter()
                .GetResult();
        }

        private static void SeedUsers(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var dbContext = services.GetRequiredService<ProperHouseDbContext>();

            Task
                .Run(async () =>
                {                   

                    var firstUser = new User
                    {
                        Email = "first@user.com",
                        UserName = "first@user.com",
                        FullName = "first user"
                    };                    

                    var secondUser = new User
                    {
                        Email = "second@user.com",
                        UserName = "second@user.com",
                        FullName = "second user"
                    };

                    var thirdUser = new User
                    {
                        Email = "third@user.com",
                        UserName = "third@user.com",
                        FullName = "third user"
                    };

                    await userManager.CreateAsync(firstUser, "first123");
                    await userManager.CreateAsync(secondUser, "second123");
                    await userManager.CreateAsync(thirdUser, "third123");                    

                })
                .GetAwaiter()
                .GetResult();
        }
        
        private static void SeedOwners(IServiceProvider services)
        {
            var dbContext = services.GetRequiredService<ProperHouseDbContext>();

            var firstUser = dbContext.Users
                .Where(u => u.Email == "first@user.com" && u.UserName == "first@user.com")
                .FirstOrDefault();

            var secondUser = dbContext.Users
                .Where(u => u.Email == "second@user.com" && u.UserName == "second@user.com")
                .FirstOrDefault();

            if(!dbContext.Owners.Any(o => o.UserId == firstUser.Id))
            {
                var firstOwner = new Owner
                {
                    Name = "first user",
                    PhoneNumber = "+359 222222222",
                    UserId = firstUser.Id
                };
                dbContext.Owners.Add(firstOwner);
            }

            if(!dbContext.Owners.Any(o => o.UserId == secondUser.Id))
            {
                var secondOwner = new Owner
                {
                    Name = "second user",
                    PhoneNumber = "+359 333333333",
                    UserId = secondUser.Id
                };
                dbContext.Owners.Add(secondOwner);
            }         
                       
            
            dbContext.SaveChanges();
        }

        private static void SeedProperties(IServiceProvider services)
        {
            var dbContext = services.GetRequiredService<ProperHouseDbContext>();            

            if (dbContext.Properties.Any())
            {
                return;
            }

            dbContext.Properties.AddRange(new List<Property>()
            {
                new Property() //1
                {
                    Town = "Veliko Turnovo",
                    Quarter = "Center",
                    CategoryId = 1,
                    Category = dbContext.Categories.Where(c => c.Id == 1).FirstOrDefault(), 
                    Capacity = 2,
                    Area = 40,
                    Floor = "3",
                    Description = "Great location in the old bulgarian capital Veliko Turnovo",
                    OwnerId = 1,
                    Owner = dbContext.Owners.Where(o => o.Id == 1).FirstOrDefault(),
                    Price = 40,
                    IsPublic = true,
                    ImageUrl = "https://th.bing.com/th/id/OIP.PEtNZgGg___93s978B4t6wHaEn?w=306&h=191&c=7&r=0&o=5&dpr=1.5&pid=1.7",
                    

                },
                new Property() //2
                {
                    Town = "Varna",
                    Quarter = "Morska gradina",
                    CategoryId = 3,
                    Category = dbContext.Categories.Where(c => c.Id == 3).FirstOrDefault(),
                    Capacity = 9,
                    Area = 110,
                    Floor = "7",
                    Description = "Great apartment near the sea with beautiful view",
                    OwnerId = 1,
                    Owner = dbContext.Owners.Where(o => o.Id == 1).FirstOrDefault(),
                    Price = 200,
                    IsPublic = true,
                    ImageUrl = "https://th.bing.com/th/id/OIP.7FjeiDeQGo7idcdEXV0X3gHaE8?w=305&h=202&c=7&r=0&o=5&dpr=1.5&pid=1.7"

                },
                new Property() //3
                {
                    Town = "Plovdiv",
                    Quarter = "Kapana",
                    CategoryId = 5,
                    Category = dbContext.Categories.Where(c => c.Id == 5).FirstOrDefault(),
                    Capacity = 10,
                    Area = 200,
                    Floor = "2",
                    Description = "Cozy house right in the heart of Kapana",
                    OwnerId = 2,
                    Owner = dbContext.Owners.Where(o => o.Id == 2).FirstOrDefault(),
                    Price = 200,
                    IsPublic = true,
                    ImageUrl = "https://th.bing.com/th/id/OIP.Qv6AEfk2USHEkHbQJvJLaAHaE7?w=263&h=180&c=7&r=0&o=5&dpr=1.5&pid=1.7"

                },
                new Property() //4
                {
                    Town = "Sofia",
                    Quarter = "Lozenets",
                    CategoryId = 1,
                    Category = dbContext.Categories.Where(c => c.Id == 1).FirstOrDefault(),
                    Capacity = 3,
                    Area = 60,
                    Floor = "15",
                    Description = "Unique studio in Lozenets sky",
                    OwnerId = 2,
                    Owner = dbContext.Owners.Where(o => o.Id == 2).FirstOrDefault(),
                    Price = 60,
                    IsPublic = true,
                    ImageUrl = "https://th.bing.com/th/id/OIP.1mFTfBvCbrm49twTqB5sYAAAAA?w=179&h=180&c=7&r=0&o=5&dpr=1.5&pid=1.7"

                },
                new Property() //5
                {
                    Town = "Sofia",
                    Quarter = "Center",
                    CategoryId = 3,
                    Category = dbContext.Categories.Where(c => c.Id == 3).FirstOrDefault(),
                    Capacity = 5,
                    Area = 80,
                    Floor = "3",
                    Description = "Comfortable apartment downtown Sofia",
                    OwnerId = 2,
                    Owner = dbContext.Owners.Where(o => o.Id == 2).FirstOrDefault(),
                    Price = 50,
                    IsPublic = true,
                    ImageUrl = "https://th.bing.com/th/id/OIP.2sKwEWCfhlGjposSAG-Q2wHaE7?w=262&h=180&c=7&r=0&o=5&dpr=1.5&pid=1.7"

                },
                new Property() //6
                {
                    Town = "Sozopol",
                    Quarter = "Old Town",
                    CategoryId = 5,
                    Category = dbContext.Categories.Where(c => c.Id == 5).FirstOrDefault(),
                    Capacity = 16,
                    Area = 250,
                    Floor = "2",
                    Description = "Amazing old house in the beautiful town of Sozopol",
                    OwnerId = 1,
                    Owner = dbContext.Owners.Where(o => o.Id == 1).FirstOrDefault(),
                    Price = 300,
                    IsPublic = true,
                    ImageUrl = "https://th.bing.com/th/id/OIP.IOpTNY2lbOw7ltmCNeW00AHaF8?w=256&h=205&c=7&r=0&o=5&dpr=1.5&pid=1.7"
                },
                new Property() //7
                {
                    Town = "Varna",
                    Quarter = "Galata",
                    CategoryId = 5,
                    Category = dbContext.Categories.Where(c => c.Id == 5).FirstOrDefault(),
                    Capacity = 12,
                    Area = 230,
                    Floor = "2",
                    Description = "Comfortable house at the beach near Varna",
                    OwnerId = 1,
                    Owner = dbContext.Owners.Where(o => o.Id == 1).FirstOrDefault(),
                    Price = 250,
                    IsPublic = true,
                    ImageUrl = "https://th.bing.com/th/id/OIP.VK2GSvDwmAgCqMqjG1dbJgHaE7?w=228&h=180&c=7&r=0&o=5&dpr=1.5&pid=1.7"
                },
                new Property() //8
                {
                    Town = "Plovdiv",
                    Quarter = "Old Town",
                    CategoryId = 5,
                    Category = dbContext.Categories.Where(c => c.Id == 5).FirstOrDefault(),
                    Capacity = 12,
                    Area = 280,
                    Floor = "2",
                    Description = "Unique house in the very heart of Old Plovdiv town",
                    OwnerId = 1,
                    Owner = dbContext.Owners.Where(o => o.Id == 1).FirstOrDefault(),
                    Price = 250,
                    IsPublic = true,
                    ImageUrl = "https://th.bing.com/th/id/OIP.mB4gicXehsz2f7yvRM1TzwEgDY?w=225&h=180&c=7&r=0&o=5&dpr=1.5&pid=1.7"
                }
            });

            dbContext.SaveChanges();
        }
    }
}
