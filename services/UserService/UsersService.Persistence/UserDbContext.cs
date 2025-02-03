using Microsoft.EntityFrameworkCore;
using UsersService.Core.Models;

namespace UsersService.Persistence
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}