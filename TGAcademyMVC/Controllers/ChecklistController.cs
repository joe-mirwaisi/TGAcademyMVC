using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TGAcademyMVC.Models;
using TGAcademyMVC.Data;
using System.Xml;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TGAcademyMVC.Controllers
{
    public class ChecklistController : Controller
    {

        public IActionResult Index()
        {
            return View(new Checklist());
        }
       
    }
}