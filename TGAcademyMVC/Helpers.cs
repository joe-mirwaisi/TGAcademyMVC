using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using TGAcademyMVC.Data;
using TGAcademyMVC.Models;
using TGAcademyMVC.Services;

namespace TGAcademyMVC
{
    public static class Helpers
    {
        public static void CreateRoles(IServiceCollection services)
        {
            /*
            var serviceProvider = services.BuildServiceProvider();

            //initializing custom roles 
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            string[] roleNames = { "Applicant", "Administrator", "Student", "Instructur", };
            Task<IdentityResult> roleResult;

            foreach (var roleName in roleNames)
            {
                Task<bool> roleExist = roleManager.RoleExistsAsync(roleName);
                roleExist.Wait();
                if (!roleExist.Result)
                {
                    //create the roles and seed them to the database: Question 1
                    roleResult = roleManager.CreateAsync(new IdentityRole(roleName));
                    roleResult.Wait();
                }
            }

            ApplicationUser[] testUsers = {
                new ApplicationUser { UserName = "applicant", Email = "applicant@techtonicgroup.com", },
                new ApplicationUser { UserName = "administrator", Email = "administrator@techtonicgroup.com", },
                new ApplicationUser { UserName = "student", Email = "student@techtonicgroup.com", },
                new ApplicationUser { UserName = "instructor", Email = "instructor@techtonicgroup.com", }
            };

            //Ensure you have these values in your appsettings.json file
            string userPWD = "Password123!";
            for (int i = 0; i < testUsers.Length; i++)
            {
                Task<ApplicationUser> user = userManager.FindByEmailAsync(testUsers[i].Email);
                user.Wait();
                if (user.Result == null)
                {
                    var newTestUser = userManager.CreateAsync(testUsers[i], userPWD);
                    newTestUser.Wait();
                }
                else
                {
                    testUsers[i].Id = user.Result.Id;
                }

                var assignToRole = userManager.AddToRoleAsync(testUsers[i], roleNames[i]);
                assignToRole.Wait();
            }
            */
        }
    }
}
