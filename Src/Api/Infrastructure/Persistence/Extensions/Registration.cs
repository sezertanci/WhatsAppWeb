using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using Persistence.Menagers;
using Persistence.Repositories;

namespace Persistence.Extensions
{
    public static class Registration
    {
        public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WhatsAppWebContext>(conf =>
            {
                var connString = configuration.GetConnectionString("SqlServerConnectionString");

                conf.UseSqlServer(connString, opt =>
                {
                    opt.EnableRetryOnFailure();
                });
            });

            services.AddScoped<IChatRepository, ChatRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IGroupMessageRepository, GroupMessageRepository>();
            services.AddScoped<IGroupUserRepository, GroupUserRepository>();
            services.AddScoped<IUserChatRepository, UserChatRepository>();
            services.AddScoped<IUserChatMessageRepository, UserChatMessageRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserFriendRepository, UserFriendRepository>();

            services.AddScoped<IUserFriendService, UserFriendManager>();
            services.AddScoped<IUserChatMessageService, UserChatMessageManager>();
            services.AddScoped<IUserChatService, UserChatManager>();
            services.AddScoped<IGroupUserService, GroupUserManager>();
            services.AddScoped<IGroupMessageService, GroupMessageManager>();

            return services;
        }
    }
}
