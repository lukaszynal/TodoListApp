using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TodoListASPNETmvc.Data;

[assembly: HostingStartup(typeof(TodoListASPNETmvc.Areas.Identity.IdentityHostingStartup))]
namespace TodoListASPNETmvc.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<TodoListUsersDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("TodoListUsersDbContextConnection")));

                services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                    .AddEntityFrameworkStores<TodoListUsersDbContext>();
            });
        }
    }
}
