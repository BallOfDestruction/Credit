using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Web.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public DbSet<FileModel> Files { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<CmsModel> CmsModels { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TokenModel> TokenModels { get; set; }
        public DbSet<Credit> Credits { get; set; }
        public DbSet<AvialableCredit> AvialableCredits { get; set; }



        public IQueryable<T> GetDbSet<T>(T type)
            where T : class
        {
            return Set<T>()?.AsQueryable();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
