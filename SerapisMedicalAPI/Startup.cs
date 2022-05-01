﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Honeycomb.OpenTelemetry;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SerapisMedicalAPI.Data;
using SerapisMedicalAPI.Data.Supabase;
using SerapisMedicalAPI.Helpers.Config;
using SerapisMedicalAPI.Interfaces;
using SerapisMedicalAPI.Model;
using SerapisMedicalAPI.Model.DoctorModel.Practice;
using SerapisMedicalAPI.Model.PatientModel;
using SerapisMedicalAPI.Model.PracticeModel;
using SerapisMedicalAPI.Services.SymptomsChecker;
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
            /*services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });*/
            

            services.AddScoped(c =>
                c.GetService<IMongoClient>().StartSession());

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
            
            
            //Transients
            services.AddTransient<IDoctorRepository, DoctorRepository>();
            services.AddTransient<IPracticeRepository, PracticeRepository>();
            services.AddTransient<IPatientRepository, PatientRepository>();
            services.AddTransient<IBookingRepository, BookingRepository>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IMailing, MailRepository>();
            services.AddTransient<IMessagingRepository, MessagingRepository>();
            services.AddTransient<ISymptomCheckerRepository, SymptomCheckerRepository>();
            services.AddTransient<IAccountSupabaseRepository, AccountSupabaseRepository>();
            services.AddTransient<IAccountSupabaseRepository, AccountSupabaseRepository>();
            
            
            //Singletons
            services.AddSingleton<ISymptomsCheckerService, SymptomsCheckerService>();
            services.AddTransient<Context>();
            services.AddTransient<CassandraContext>();
            services.AddControllers();
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });
            //
            services.Configure<SupabaseConfig>(Configuration.GetSection("Supabase"));


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
            app.UseForwardedHeaders();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Run(async (context) =>
            {
                //Run DB service here
            });

            //app.UseMvc();
        }
    }
}
