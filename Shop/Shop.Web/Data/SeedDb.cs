namespace Shop.Web.Data
{
    using Microsoft.AspNetCore.Identity;
    using Entities;
    using Shop.Web.Helpers;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public class SeedDb
    {
        private readonly DataContext context;
        private readonly IUserHelper userHelper;
        private Random random;

        public SeedDb(DataContext context,IUserHelper userHelper)
        {
            this.context = context;
            this.userHelper = userHelper;
            this.random = new Random();
        }

        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();

            await this.userHelper.CheckRoleAsync("Admin");
            await this.userHelper.CheckRoleAsync("Customer");

            if (!this.context.Countries.Any())
            {
                var cities = new List<City>();
                cities.Add(new City { Name = "Trujillo" });
                cities.Add(new City { Name = "Lima" });
                cities.Add(new City { Name = "Arequipa" });

                this.context.Countries.Add(new Country
                {
                    Cities = cities,
                    Name = "Perú"
                });

                await this.context.SaveChangesAsync();
            }



            var user = await this.userHelper.GetUserByEmailAsync("mectoy2013@gmail.com");

            if (user == null)
            {
                user = new User
                {
                    FirstName =     "Jose",
                    LastName =      "Ponciano",
                    Email =             "mectoy2013@gmail.com",
                    UserName = "mectoy2013@gmail.com",
                    Address = "Calle Luna Calle Sol",
                    PhoneNumber = "350 634 2747",
                    CityId = this.context.Countries.FirstOrDefault().Cities.FirstOrDefault().Id,
                    City = this.context.Countries.FirstOrDefault().Cities.FirstOrDefault()

                };

                var result = await this.userHelper.AddUserAsync(user, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }

                await this.userHelper.AddUserToRoleAsync(user, "Admin");
            }
            //si el usuario esta creado
            var isInRole = await this.userHelper.IsUserInRoleAsync(user, "Admin");
            if (!isInRole)
            {
                await this.userHelper.AddUserToRoleAsync(user, "Admin");
            }


            if (!this.context.Products.Any())
            {
                this.AddProduct("IphoneX",user);
                this.AddProduct("Magic mouse",user);
                this.AddProduct("IWatch",user);
                await this.context.SaveChangesAsync();
            }
        }

        private void AddProduct(string name,User user)
        {
            this.context.Products.Add(new Product
            {
                Name = name,
                Price = this.random.Next(100),
                IsAvailabe = true,
                Stock = this.random.Next(100),
                User=user
            });
        }
    }
}
