using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SerapisMedicalAPI.Data;
using SerapisMedicalAPI.Interfaces;
using SerapisMedicalAPI.Model;
using Swashbuckle.AspNetCore.Swagger;

namespace SerapisMedicalAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        //THIS IS FOR CLICKATELL
        //API KEY : AfMDkjnITRaKOQKiV6mN_g==
        //https://localhost:44371/api/communication/callback


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            //services.AddSingleton<IMongoClient>(c =>
            //{
            //    var login = "";
            //    var password = Uri.EscapeDataString("");
            //    var server = "";

            //    return new MongoClient(
            //        string.Format("mongodb+srv://{0}:{1}@{2}/test?retryWrites=true&w=majority", login, password, server));
            //});

            //services.AddScoped(c =>
            //    c.GetService<IMongoClient>().StartSession());

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.Configure<Settings>(options =>
            {
                options.ConnectionString = Configuration.GetSection("MongoConnection:ConnectionString").Value;
                options.Database = Configuration.GetSection("MongoConnection:Database").Value;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "My API",  Version="v1"} );
            });
            services.AddTransient<IDoctorRepository, DoctorRepository>();
            services.AddTransient<IPracticeRepository, PracticeRepository>();
            services.AddTransient<IPatientRepository, PatientRepository>();
            services.AddTransient<IBookingRepository, BookingRepository>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IMailing, MailRepository>();
            services.AddTransient<IMessagingRepository, MessagingRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("CorsPolicy");
            app.UseSwagger();
            app.UseSwaggerUI(c => 
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API version 2");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
