using System;
using Application.ServiceInterfaces;
using Infrastructure.IoC;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using WebApp.Extensions;

namespace WebApp
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddServicesAPP();

            services.AddResponseCaching();

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                //.AddRazorPagesOptions(options => options.Conventions.AddPageRoute("/Account/Login", ""))
                .AddJsonOptions(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

           

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment hostingEnvironment, IEmailService emailService)
        {
            app.UseRewriter(new RewriteOptions()
                .AddRedirectToHttps(StatusCodes.Status301MovedPermanently));

            if (hostingEnvironment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseCustomExceptionMiddleware();

                app.UseStatusCodePagesWithReExecute("/Errors/Exception/{0}");

                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    const int durationInSeconds = 60 * 60 * 24 * 365;
                    ctx.Context.Response.Headers[HeaderNames.CacheControl] = "private,max-age=" + durationInSeconds;
                }
            });

            app.UseResponseCaching();

           
          

            app.UseMvc();
        }
    }
}
