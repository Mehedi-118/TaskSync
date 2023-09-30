using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using PTSL.GENERIC.Common.ModelBuilderExtensions;
using PTSL.Ovidhan.Common.ModelBuilderExtensions;

namespace PTSL.Ovidhan.Common.Entity
{
    public class GENERICReadOnlyCtx : IdentityDbContext<User, Role, string>
    {
        public GENERICReadOnlyCtx(DbContextOptions<GENERICReadOnlyCtx> options)
            : base(options) => ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

        public GENERICReadOnlyCtx()
            : base() => ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

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
