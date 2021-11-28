using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rocky_DataAccess.Data;
using Rocky_Models;
using Rocky_Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocky_DataAccess.Initializer
{
    public class DBInitializer : IDBInitializer
    {
        public DBInitializer(DBApplicationContext dBApplicationContext, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _dBApplicationContext = dBApplicationContext ?? throw new ArgumentNullException(nameof(dBApplicationContext));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        }

        private readonly DBApplicationContext _dBApplicationContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole>  _roleManager;

        public void Initialize()
        {
            try
            {
                if (_dBApplicationContext.Database.GetPendingMigrations().Count() > 0)
                    _dBApplicationContext.Database.Migrate();
            }
            catch (Exception ex)
            {
            }

            if (!_roleManager.RoleExistsAsync(WC.AdminRole).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(WC.AdminRole)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(WC.CustomerRole)).GetAwaiter().GetResult();
            }
            else
                return;

            _userManager.CreateAsync(new ApplicationUser()
            {
                Email = "admin@gmail.com",
                FullName = "Admin",
                EmailConfirmed = true,
                PhoneNumber = "1111111",
                UserName = "admin@gmail.com"
            }, "Admin123*").GetAwaiter().GetResult();

            ApplicationUser user = _dBApplicationContext.ApplicationUser.FirstOrDefault(x => x.Email == "admin@gmail.com");
            _userManager.AddToRoleAsync(user, WC.AdminRole).GetAwaiter().GetResult();
        }
    }
}
