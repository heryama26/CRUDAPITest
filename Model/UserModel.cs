using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace CRUDAPITest.Model
{
    public class MyDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public MyDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                // connect to sql server with connection string from app settings
                options.UseSqlServer(Configuration.GetConnectionString("ConsString"));
            }
        }
        public DbSet<UserModel> tbl_user { get; set; } = null!;
    }
    public class UserModel
    {
        [Key]
        public int userid { get; set; }
        public string namalengkap { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public char status { get; set; }
    }
}
