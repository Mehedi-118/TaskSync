using Microsoft.EntityFrameworkCore;
using PTSL.Ovidhan.Common.Entity;
using PTSL.Ovidhan.Common.Entity.GeneralSetup;
using PTSL.Ovidhan.Common.Entity.UserEntitys;

using System;
using System.Collections.Generic;
using System.Text;

namespace PTSL.Ovidhan.Common.ModelBuilderExtensions
{
    public static partial class EntityModelBuilderExtensions
    {
        public static void ConfigureRefreshToken(this ModelBuilder builder)
        {
            builder.Entity<RefreshToken>(ac =>
            {
                ac.ToTable(nameof(RefreshToken), "System");

            });

            builder.Entity<RefreshToken>().HasQueryFilter(q => !q.IsRevoked);
        }
    }
}
