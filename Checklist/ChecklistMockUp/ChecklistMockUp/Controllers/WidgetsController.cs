using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using ChecklistMockUp.DAL;
using ChecklistMockUp.Models;


namespace ChecklistMockUp.Controllers
{
    public class WidgetsController : Controller
    {
        private ApplicaitonDbContext db = new ApplicaitonDbContext();

        private XmlDocument xml
        {
            get
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(@"C:\Projects\TAP\Checklist\ChecklistMockUp\ChecklistMockUp\App_Data\Checklist1.xml");
                return doc;
            }
        }



        // GET: Widgets
        public ActionResult Index()
        {
            Dictionary<string, bool> checklistValues = new Dictionary<string, bool>();

            string userEmail = "abell@techtonicgroup.com";

            var allMatchingEmail = db.Widgets.Where(w => w.UserEmail == userEmail).ToListAsync();
            allMatchingEmail.Wait();
            if (allMatchingEmail.Result.Count() > 0)
            {
                //right now we have nothing to manage more than one possible checklist
                foreach (XmlNode n in xml.DocumentElement.ChildNodes)
                {
                    string nodeAttribute = n.Attributes[0].Value;
                    if (allMatchingEmail.Result.Where(c => c.KeyName == nodeAttribute).Any())
                    {
                        checklistValues.Add(n.InnerText, true);
                    }
                    else
                    {
                        checklistValues.Add(n.InnerText, false);
                    }

                }
            }
            else
            {
                //we have no entries yet for this user so we can assume all checkbox values are false
                foreach (XmlNode n in xml.DocumentElement.ChildNodes)
                {
                    checklistValues.Add(n.InnerText, false);
                }
            }

            return View(checklistValues);
        }

        [HttpPost]
        public ActionResult Update(Dictionary<string, bool> checklistValues)
        {
            //this will eventually be retrived from the Membership provider for the currently logged in user
            string userEmail = "abell@techtonicgroup.com";

            var allMatchingEmail = db.Widgets.Where(w => w.UserEmail == userEmail).ToListAsync();
            allMatchingEmail.Wait();

            foreach(KeyValuePair<string,bool> kvp in checklistValues)
            {
                if(allMatchingEmail.Result.Where(w => w.KeyName == kvp.Key).Any())
                {
                    allMatchingEmail.Result.Where(w => w.KeyName == kvp.Key).FirstOrDefault().KeyValue = kvp.Value;
                }
                else
                {
                    Widget widget = new Widget { KeyName = kvp.Key, KeyValue = kvp.Value, UserEmail = userEmail, WidgetXML = xml.ToString() };
                    db.Widgets.Add(widget);
                }
                db.SaveChanges();
            }
            
            return View(checklistValues);
        }


    }
}
