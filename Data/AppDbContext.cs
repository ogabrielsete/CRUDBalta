using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TesteBalta.Models;

namespace TesteBalta.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

        public DbSet<Aluno> AlunoDB {  get; set; }
        public DbSet<Assinatura> AssinaturaDB { get; set; } 

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Aluno>().HasKey(x => x.Id);
            builder.Entity<Assinatura>().HasKey(x => x.Id);
            base.OnModelCreating(builder);
        }
    }
}
