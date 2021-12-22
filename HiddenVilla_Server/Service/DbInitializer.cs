using DataAccess.Data;
using HiddenVilla_Server.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HiddenVilla_Server.Service
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(
            ApplicationDbContext db,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this._db = db;
            this._userManager = userManager;
            this._roleManager = roleManager;
        }
        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Any())
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                //logging
            }

            if (_db.Roles.Any(x => x.Name == Common.SD.Role_Admin)) return;

            _roleManager.CreateAsync(new IdentityRole(Common.SD.Role_Admin)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(Common.SD.Role_Customer)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(Common.SD.Role_Employee)).GetAwaiter().GetResult();


            _userManager.CreateAsync(new IdentityUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true
            },"Admin123*").GetAwaiter().GetResult();

            var user = _db.Users.First(x => x.Email == "admin@gmail.com");
            _userManager.AddToRoleAsync(user, Common.SD.Role_Admin).GetAwaiter().GetResult();

        }
            
    }
}
