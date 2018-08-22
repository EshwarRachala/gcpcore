using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }
            
            app.UseStaticFiles();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger ();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI (c => {
                c.SwaggerEndpoint ("/swagger/v1/swagger.json", "Local Market API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseMvc ();
        }
    }
}