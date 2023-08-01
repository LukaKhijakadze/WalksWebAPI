using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WalksAPI.Database.Context
{
    public class WalksAuthDbContext : IdentityDbContext
    {
        public WalksAuthDbContext(DbContextOptions<WalksAuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var writerId = "4de53d81-02fa-4ee2-ada6-905347b41225";
            var readerId = "b6d916cc-a9b2-40c3-a1aa-3eb2ccd371c7";

            var roles = new List<IdentityRole>()
            {
                new IdentityRole()
                {
                    Id = readerId,
                    ConcurrencyStamp = readerId,
                    Name = "Reader",
                    NormalizedName = "READER",
                },
                new IdentityRole()
                {
                    Id = writerId,
                    ConcurrencyStamp = writerId,
                    Name = "Writer",
                    NormalizedName = "WRITER",
                },
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
