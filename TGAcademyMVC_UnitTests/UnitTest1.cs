using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;


using TGAcademyMVC;
using TGAcademyMVC.Data;
using TGAcademyMVC.Models;

namespace TGAcademyMVC_UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddDbContext<ApplicationDbContext>(options => 
            options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=aspnet-TGAcademyMVC-895368D8-AF00-429F-A46B-A1F3FAE043A4;Trusted_Connection=True;MultipleActiveResultSets=true"));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            Startup.CreateRoles(services);
        }
    }
}
