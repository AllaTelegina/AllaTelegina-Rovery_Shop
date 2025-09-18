using Backend_asp.net.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend_asp.net.DataBase
{
    public class AplicationContext:DbContext
    {
        public DbSet<UserRovery> userRoverys { get; set; }
        public  AplicationContext(DbContextOptions<AplicationContext> option) : base(option)
        {
        }
    }
}
