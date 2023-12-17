using Microsoft.EntityFrameworkCore;

using PTSL.Ovidhan.Common.Entity.GeneralSetup;
using PTSL.Ovidhan.Common.ModelBuilderExtensions;

namespace PTSL.GENERIC.Common.ModelBuilderExtensions;

public static partial class EntityModelBuilderExtensions
{
    public static void ConfigureCategory(this ModelBuilder builder)
    {
        builder.Entity<Category>(e =>
        {
            e.ToTable($"{nameof(Category)}s", SchemaNames.GENERAL_SETUP);
        });

        builder.Entity<Category>().HasQueryFilter(q => !q.IsDeleted && q.IsActive);
    }
}