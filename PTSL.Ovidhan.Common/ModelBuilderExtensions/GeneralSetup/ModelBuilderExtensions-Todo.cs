using Microsoft.EntityFrameworkCore;

using PTSL.Ovidhan.Common.Entity.Tasks;
using PTSL.Ovidhan.Common.ModelBuilderExtensions;

namespace PTSL.GENERIC.Common.ModelBuilderExtensions;

public static partial class EntityModelBuilderExtensions
{
    public static void ConfigureTodo(this ModelBuilder builder)
    {
        builder.Entity<Todo>(e =>
        {
            e.ToTable($"{nameof(Todo)}s", SchemaNames.GENERAL_SETUP);
        });

        builder.Entity<Todo>().HasQueryFilter(q => !q.IsDeleted && q.IsActive);
    }
}