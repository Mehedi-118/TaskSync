using Microsoft.Extensions.DependencyInjection;

using PTSL.GENERIC.Service.Services.Output;
using PTSL.Ovidhan.Business.Businesses.Implementation;
using PTSL.Ovidhan.Business.Businesses.Implementation.GeneralSetup;
using PTSL.Ovidhan.Business.Businesses.Implementation.UserEntitys;
using PTSL.Ovidhan.Business.Businesses.Interface;
using PTSL.Ovidhan.Business.Businesses.Interface.GeneralSetup;
using PTSL.Ovidhan.Business.Businesses.Interface.UserEntitys;
using PTSL.Ovidhan.Business.TokenHelper;
using PTSL.Ovidhan.Common.Helper;
using PTSL.Ovidhan.DAL.Repositories.Implementation.GeneralSetup;
using PTSL.Ovidhan.DAL.Repositories.Implementation.UserEntitys;
using PTSL.Ovidhan.DAL.Repositories.Interface.GeneralSetup;
using PTSL.Ovidhan.DAL.Repositories.Interface.UserEntitys;
using PTSL.Ovidhan.DAL.UnitOfWork;
using PTSL.Ovidhan.Service.Services.Implementation;
using PTSL.Ovidhan.Service.Services.Implementation.GeneralSetup;
using PTSL.Ovidhan.Service.Services.Implementation.SystemUser;
using PTSL.Ovidhan.Service.Services.Interface;
using PTSL.Ovidhan.Service.Services.Interface.GeneralSetup;
using PTSL.Ovidhan.Service.Services.Interface.SystemUser;
using PTSL.Ovidhan.Service.Services.UserEntitys;

namespace PTSL.Ovidhan.Api.Helpers;

public static class DependencyResolver
{
    public static IServiceCollection AddDependencyResolver(this IServiceCollection services)
    {
        //UOW
        services.AddScoped<IGENERICUnitOfWork, GENERICUnitOfWork>();

        // Repository
        AddScopedForRepository(services);

        // Services
        AddScopedForService(services);

        // Business
        AddScopedForBusiness(services);

        services.AddSingleton<FileHelper>();
        services.AddSingleton<IGenerateJSONWebToken, GenerateJSONWebToken>();

        return services;
    }

    private static void AddScopedForBusiness(IServiceCollection services)
    {
        services.AddScoped<IUserBusiness, UserBusiness>();
        services.AddScoped<IRefreshTokenBusiness, RefreshTokenBusiness>();
        services.AddScoped<IUserRolesBusiness, UserRolesBusiness>();


        services.AddScoped<ICategoryBusiness, CategoryBusiness>();
        services.AddScoped<ITodoBusiness, TodoBusiness>();
        services.AddScoped<IReminderBusiness, ReminderBusiness>();
    }

    private static void AddScopedForService(IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRefreshTokenService, RefreshTokenService>();
        services.AddScoped<IUserRolesService, UserRolesService>();
        services.AddScoped<ICacheService, CacheService>();
        services.AddScoped<IMessageService, MessageService>();

        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ITodoService, TodoService>();
        services.AddScoped<IReminderService, ReminderService>();

    }

    private static void AddScopedForRepository(IServiceCollection services)
    {
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ITodoRepository, TodoRepository>();
        services.AddScoped<IReminderRepository, ReminderRepository>();

    }
}
