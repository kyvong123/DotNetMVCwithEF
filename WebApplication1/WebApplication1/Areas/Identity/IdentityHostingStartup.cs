using System;
using System.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApplication1.Areas.Identity.Data;
using WebApplication1.Data;

[assembly: HostingStartup(typeof(WebApplication1.Areas.Identity.IdentityHostingStartup))]
namespace WebApplication1.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public IConfiguration Configuration { get; }
        public void Configure(IWebHostBuilder builder)
        {
            string mySqlConnectionStr = Configuration.GetConnectionString("AuthDBContextConnection");
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<AuthDBContext>(options =>
                    options.UseMySql(
                        "Server=DESKTOP-CM92NS0;Database=MVCAuthDB;User Id=lekyvong;Password=Lkv@03091967;port=3306",
                        new MySqlServerVersion(new Version(8, 0, 11))
                     )
            );
                services.AddDefaultIdentity<ApplicationUser>(options => {
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.SignIn.RequireConfirmedAccount = false;
                })
                .AddEntityFrameworkStores<AuthDBContext>();
            });
        }
    }
}