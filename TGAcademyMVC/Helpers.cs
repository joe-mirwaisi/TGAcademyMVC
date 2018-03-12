using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
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
using System.Xml;
using TGAcademyMVC.Data;
using TGAcademyMVC.Models;
using TGAcademyMVC.Services;

namespace TGAcademyMVC
{
    public static class Helpers
    {
        public static void CreateRoles(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            //initializing custom roles 
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            string[] roleNames = { "Prospective_Student", "Current_Student", "Mentor", "Admin", };

            foreach (var roleName in roleNames)
            {
                Task<bool> roleExist = roleManager.RoleExistsAsync(roleName);
                roleExist.Wait();
                if (!roleExist.Result)
                {
                    //create the roles and seed them to the database
                    //Task<ApplicationRole>  newRole = roleManager.CreateAsync(new ApplicationRole(roleName));
                    //newRole.Wait();
                }
            }

            ApplicationUser[] testUsers = {
                new ApplicationUser { UserName = "test_ps", Email = "test_ps@techtonicgroup.com", },
                new ApplicationUser { UserName = "test_cs", Email = "test_cs@techtonicgroup.com", },
                new ApplicationUser { UserName = "test_mentor", Email = "test_mentor@techtonicgroup.com", },
                new ApplicationUser { UserName = "test_admin", Email = "test_admin@techtonicgroup.com", }
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
                //AddBasicChecklistToUser(user.Result);
                var assignToRole = userManager.AddToRoleAsync(testUsers[i], roleNames[i]);
                assignToRole.Wait();
            }
        }

        public static void AddBasicChecklistToUser(ApplicationDbContext context)
        {
            //loads up the checklist xml document and stores it as a string because the database will only hold it that way.
            XmlDocument doc = new XmlDocument();
            doc.Load("AppData/Checklist.xml");
            string xmlString = doc.InnerXml;

            Task<bool> usersExist = context.Users.AnyAsync();
            usersExist.Wait();
            if (usersExist.Result)
            {
                //Task<ApplicationUser> user = context.Users.Where(u => u.Email == "test_ps@techtonicgroup.com").FirstOrDefaultAsync();
                //Console.WriteLine("Just some stuff to pause on");
                //user.Wait();
                //var allUsers = context.Users.ToListAsync();
                ////Task<List<ApplicationUser>> allUsers = context.Users.AnyAsync();
                //allUsers.Wait();
                //foreach (ApplicationUser user in allUsers.Result)
                //{
                //    //no entries for this user, we need to create one.
                //    if (!context.Checklists.Where(u => u.UserID == user.Id.ToString()).Any()) ;
                //    {
                Widget checklist = new Widget();
                checklist.UserID = "3aae850c-aefa-47e5-b492-2b266266a08d";
                checklist.xml = xmlString;
                checklist.Key = "Key1";
                checklist.Key_Value = true;
                context.Widgets.Add(checklist);
                context.SaveChanges();
                    //}
                //}
            }
        }
    }
}
