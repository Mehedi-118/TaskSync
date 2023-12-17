using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PTSL.Ovidhan.Common.Dictionaries;
using PTSL.Ovidhan.Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTSL.Ovidhan.Api.Helpers
{
    public static class ConfigureServiceExtensions
    {
        public static void ConfigureDatabases(
           this IServiceCollection services,
           string identicaMyCoreAuthConfigDatabaseConnection)
        {
            services.AddDbContext<GENERICReadOnlyCtx>(item => item.UseSqlServer(identicaMyCoreAuthConfigDatabaseConnection), ServiceLifetime.Scoped);
            services.AddDbContext<GENERICWriteOnlyCtx>(item => item.UseSqlServer(identicaMyCoreAuthConfigDatabaseConnection), ServiceLifetime.Scoped);

            using (IServiceScope serviceScope = services.BuildServiceProvider().CreateScope())
            {
                using (GENERICWriteOnlyCtx authContext = serviceScope.ServiceProvider.GetRequiredService<GENERICWriteOnlyCtx>())
                {
                    authContext.Database.EnsureCreated();

                    #region Configuration Dictionaries (Authentication Operation,Login Type,Organization Relationship,Residence Relationship)
                    ConfigurationDictionaries configurationDictionaries = new ConfigurationDictionaries();

                    //configurationDictionaries.AuthenticationOperationDictionary = authContext.Set<AuthenticationOperation>()
                    //    .ToDictionary(x => x.Id, x => x.Name);

                    //configurationDictionaries.LoginTypeDictionary = authContext.Set<LoginType>()
                    //    .ToDictionary(x => x.Id, x => x.Name);

                    //configurationDictionaries.OrganizationRelationshipDictionary = authContext.Set<OrganizationRelationship>()
                    //    .ToDictionary(x => x.Id, x => x.Name);

                    //configurationDictionaries.ResidenceRelationshipDictionary = authContext.Set<ResidenceRelationship>()
                    //    .ToDictionary(x => x.Id, x => x.Name);

                    services.AddSingleton(configurationDictionaries);
                    #endregion

                }
            }
        }
    }
}
