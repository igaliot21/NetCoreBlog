using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetCoreBlog.Data;
using NetCoreBlog.Models;

namespace NetCoreBlog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            try
            {
                var scope = host.Services.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                context.Database.EnsureCreated();

                var adminRole = new IdentityRole("Admin");
                if (!context.Roles.Any())
                {
                    // create a role
                    roleManager.CreateAsync(adminRole).GetAwaiter().GetResult();
                }

                if (!context.Users.Any(u => u.UserName == "Admin"))
                {
                    // create an Admin
                    var adminUser = new AppUser()
                    {
                        UserName = "Admin",
                        Email = "Admin@test.com"
                    };
                    var userResult = userManager.CreateAsync(adminUser, "ayanami").GetAwaiter().GetResult();
                    // add role to user
                    userManager.AddToRoleAsync(adminUser, adminRole.Name);
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
            
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
