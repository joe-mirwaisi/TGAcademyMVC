using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TGAcademyMVC.Data;
using TGAcademyMVC.Models;
using TGAcademyMVC.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.OAuth;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;



namespace TGAcademyMVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new RequireHttpsAttribute());
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();

            AuthenticationBuilder ab = new AuthenticationBuilder(services);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = "GitHub";
            })
       .AddCookie()
       .AddOAuth("GitHub", options =>
       {
           options.ClientId = "90b03f348d041ec189f0";
           options.ClientSecret = "a2ba9b2efa3b7c6b9ee3e26fc63f044017a0bd69";
           options.CallbackPath = new PathString("/signin-github");

           options.AuthorizationEndpoint = "https://github.com/login/oauth/authorize";
           options.TokenEndpoint = "https://github.com/login/oauth/access_token";
           options.UserInformationEndpoint = "https://api.github.com/user";

           options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
           options.ClaimActions.MapJsonKey(ClaimTypes.Name, "name");
           options.ClaimActions.MapJsonKey("urn:github:login", "login");
           options.ClaimActions.MapJsonKey("urn:github:url", "html_url");
           options.ClaimActions.MapJsonKey("urn:github:avatar", "avatar_url");

           options.Events = new OAuthEvents
           {
               OnCreatingTicket = async context =>
               {
                   var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
                   request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                   request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);

                   var response = await context.Backchannel.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, context.HttpContext.RequestAborted);
                   response.EnsureSuccessStatusCode();

                   var user = JObject.Parse(await response.Content.ReadAsStringAsync());

                   context.RunClaimActions(user);
               }
           };
       });

            var serviceProvider = services.BuildServiceProvider();
            CreateRoles(serviceProvider);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void CreateRoles(IServiceProvider serviceProvider)
        {
            //initializing custom roles 
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            string[] roleNames = { "Prospective_Student", "Current_Student", "Mentor", "Admin", };
            Task<IdentityResult> roleResult;

            foreach (var roleName in roleNames)
            {
                Task<bool> roleExist = roleManager.RoleExistsAsync(roleName);
                if (!roleExist.Result)
                {
                    //create the roles and seed them to the database: Question 1
                    roleResult = roleManager.CreateAsync(new IdentityRole(roleName));
                    roleResult.Wait();
                }
            }

            ApplicationUser[] testUsers = {
            new ApplicationUser { UserName = "test_ps", Email = "test_ps@techtonicgroup.com", },
            new ApplicationUser { UserName = "test_cs", Email = "test_cs@techtonicgroup.com", },
            new ApplicationUser { UserName = "test_mentor", Email = "test_mentor@techtonicgroup.com", },
            new ApplicationUser { UserName = "test_admin", Email = "test_admin@techtonicgroup.com", }
        };
            //{new ApplicationUser( UserName = "test_ps", Email = "test_ps@techtonicgroup.com" )};

            //Ensure you have these values in your appsettings.json file
            string userPWD = "Password123!";
            for (int i = 0; i < testUsers.Length; i++)
            {
                Task<ApplicationUser> user = userManager.FindByEmailAsync(testUsers[i].Email);
                user.Wait();
                if (user == null)
                {
                    var newTestUser = userManager.CreateAsync(testUsers[i], userPWD);
                    newTestUser.Wait();
                    var assignToRole = userManager.AddToRoleAsync(testUsers[i], roleNames[i]);
                    assignToRole.Wait();
                }
            }
        }
    }
}
