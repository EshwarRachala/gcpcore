using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Firebase.Authentication.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

namespace localmarket {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddMvc ();
            // Register the Swagger generator
            services.AddSwaggerGen (c => {

                c.SwaggerDoc ("v1", new Info {
                    Version = "v1",
                        Title = "Local Market API",
                        Description = "Local Market API",
                        TermsOfService = "None",
                        Contact = new Contact {
                            Name = "Eshwar Rachala",
                                Email = string.Empty,
                                Url = "https://twitter.com/eswarrachala"
                        },
                        License = new License {
                            Name = "Local market",
                                Url = "https://example.com/license"
                        }
                });
                // var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                // var xmlPath = Path.Combine (AppContext.BaseDirectory, xmlFile);
                // c.IncludeXmlComments (xmlPath);
            });

            services.AddCors (options => {
                options.AddPolicy ("AllowEverything",
                    builder => builder
                    .AllowAnyHeader ()
                    .AllowAnyMethod ()
                    .AllowAnyOrigin () // You'll want to restrict the policy to only your allowed origins (i.e. the address of the site hitting the API)
                    .AllowCredentials ()
                );
            });

            services.AddFirebaseAuthentication (Configuration["FirebaseAuthentication:Issuer"],
                Configuration["FirebaseAuthentication:Audience"]);

            // services
            //     .AddAuthentication (JwtBearerDefaults.AuthenticationScheme)
            //     .AddJwtBearer (options => {
            //         options.Authority = "https://securetoken.google.com/localmarket-213804";
            //         options.TokenValidationParameters = new TokenValidationParameters {
            //             ValidateIssuer = true,
            //             ValidateAudience = true, 
            //             ValidateLifetime = true,
            //             ValidateIssuerSigningKey=true,
            //             ValidIssuer = "https://securetoken.google.com/localmarket-213804",
            //             ValidAudience = "localmarket-213804"   
            //         };
            //     });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }

            loggerFactory.AddConsole ();

            app.UseCors ("AllowEverything");

            // var issuerKeyProvider = new FirebaseIssuerKeyProvider ();
            // var signingKeys = issuerKeyProvider.GetSigningKeys ().Result;

            // // The projectId can be found in Firebase project settings
            // var firebaseProjectId = "localmarket-213804";

            // if (string.IsNullOrEmpty (firebaseProjectId))
            //     throw new Exception ("Need your project Id from Firebase settings up in here");

            app.UseStaticFiles ();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger ();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI (c => {
                c.SwaggerEndpoint ("/swagger/v1/swagger.json", "Local Market API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseAuthentication ();

            app.UseMvcWithDefaultRoute ();
            app.UseMvc ();
        }
    }
}