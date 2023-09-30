using System.Threading;
using System.Threading.Tasks;

using EntityFrameworkCore.Triggers;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using PTSL.GENERIC.Common.ModelBuilderExtensions;
using PTSL.Ovidhan.Common.ModelBuilderExtensions;

namespace PTSL.Ovidhan.Common.Entity
{
    public class GENERICWriteOnlyCtx : IdentityDbContext<User, Role, string>
    {
        public GENERICWriteOnlyCtx(DbContextOptions<GENERICWriteOnlyCtx> options)
            : base(options)
        {
        }

        public GENERICWriteOnlyCtx()
            : base()
        {
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return this.SaveChangesWithTriggersAsync(
                    base.SaveChangesAsync,
                    acceptAllChangesOnSuccess: true,
                    cancellationToken: cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.ConfigureRefreshToken();

            modelBuilder.ConfigureReminder();
            modelBuilder.ConfigureTodo();
            modelBuilder.ConfigureCategory();

        }
    }
}
