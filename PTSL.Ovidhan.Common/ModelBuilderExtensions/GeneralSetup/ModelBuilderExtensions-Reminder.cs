using Microsoft.EntityFrameworkCore;

using PTSL.Ovidhan.Common.Entity.GeneralSetup;
using PTSL.Ovidhan.Common.ModelBuilderExtensions;

namespace PTSL.GENERIC.Common.ModelBuilderExtensions;

public static partial class EntityModelBuilderExtensions
{
    public static void ConfigureReminder(this ModelBuilder builder)
    {
        builder.Entity<Reminder>(e =>
        {
            e.ToTable($"{nameof(Reminder)}s", SchemaNames.GENERAL_SETUP);
        });

        builder.Entity<Reminder>().HasQueryFilter(q => !q.IsDeleted && q.IsActive);
    }
}