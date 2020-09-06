using Application.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Application.ServiceInterfaces;
using Application.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.IoC
{
    public static class DependencyContainerAPP
    {
        public static IServiceCollection AddServicesAPP(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddHttpClient<IHttpClientService, HttpClientService>();

            services.AddSingleton<IEmailService, EmailService>();

          

            services.AddSingleton<IFileService, FileService>();

            var mappingConfig = new MapperConfiguration(options => options.AddProfile(new AutoMapperProfilesApp()));
            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }
    }
}
