using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RineServer.Models;

[assembly: HostingStartup(typeof(RineServer.Areas.Identity.IdentityHostingStartup))]
namespace RineServer.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<RineServerContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("RineServerContextConnection")));

                services.AddDefaultIdentity<IdentityUser>()
                    .AddEntityFrameworkStores<RineServerContext>();
            });
        }
    }
}