using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TodoListASPNETmvc.Data
{
    public class TodoListUsersDbContext : IdentityDbContext<IdentityUser>
    {
        public TodoListUsersDbContext(DbContextOptions<TodoListUsersDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
