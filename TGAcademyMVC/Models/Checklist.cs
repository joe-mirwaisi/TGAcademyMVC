using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

using TGAcademyMVC.Data;
using Microsoft.EntityFrameworkCore;

namespace TGAcademyMVC.Models
{
    public class Checklist
    {
        private IHttpContextAccessor _contextAccessor;
        private HttpContext _context { get { return _contextAccessor.HttpContext; } }
        public Dictionary<string, bool> ChecklistValues
        {
            get
            {
                return GetChecklistValues();
            }
        }

        private XmlDocument checklistXml
        {
            get
            {
                XmlDocument doc = new XmlDocument();
                doc.Load("./AppData/Checklist.xml");
                return doc;
            }
        }
           
        
        public Checklist() { }

        public Dictionary<string,bool> GetChecklistValues()
        {
            Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
            if(_context != null)
            {
                if(_context.User != null)
                {
                    var userID = _context.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    using (var context = new ApplicationDbContext())
                    {
                        var hasEntry = context.Widgets.Where(w => w.UserID == userID).AnyAsync();
                        hasEntry.Wait();
                        if (hasEntry.Result)
                        {
                            var allCheckedTrue = context.Widgets.Where(u => u.UserID == userID).ToListAsync();
                            allCheckedTrue.Wait();
                            foreach (XmlNode n in checklistXml)
                            {
                                if (allCheckedTrue.Result.Where(c => c.Key == n.InnerText).Any())
                                {
                                    dictionary.Add(n.InnerText, true);
                                }
                                else
                                {
                                    dictionary.Add(n.InnerText, false);
                                }

                            }
                        }
                        else
                        {
                            foreach (XmlNode n in checklistXml)
                            {
                                dictionary.Add(n.InnerText, false);
                            }
                        }
                    }
                }
            }
            return dictionary;
                
        }
                  

    }
}
