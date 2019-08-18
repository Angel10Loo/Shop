using Microsoft.AspNetCore.Identity;
using Shop.Web.Data.Entities;
using Shop.Web.Helpper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext context;
        private readonly IUserHelper userHelper;
        private Random random;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            this.context = context;
            this.userHelper = userHelper;
            this.random = new Random();
        }

        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();

            var user = await this.userHelper.GetUserByEmailAsync("angelgonzalez7652@gmail.com");
            if (user == null)
            {
                user = new User
                {
                    FirstName = "Angel",
                    LastName = "Gonzalez",
                    Email = "angelgonzalez7652@gmail.com",
                    UserName = "angelgonzalez7652@gmail.com"
                };

              var result = await this.userHelper.AddUserAsync(user, "Angel123.");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }
            }

      

                if (!this.context.Products.Any())
            {
                this.AddProduct("Iphon 6sPlus", user);
                this.AddProduct("Tarjeta Nividia", user);
                this.AddProduct("Televisor Plasma", user);
                await this.context.SaveChangesAsync();
            }
        }

        private void AddProduct(string name, User user)
           
        {
            this.context.Products.Add(new Product
            {
                Name = name,
                Price = this.random.Next(100),
                IsAvailabe = true,
                Stock = this.random.Next(100),
                User = user
                
            });

        }
    }
}
