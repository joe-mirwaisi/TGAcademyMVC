using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace TGAcademyMVC.Models
{
    public class Checklist
    {
        public Guid ID { get; set; }

        //Enitity Framwork will not allow storing an XML file, but we can save the XML as a string that we later parse out as XML
        public string ChecklistXml { get; set; }
        public Guid UserID { get; set; }
        public bool Key1 { get; set; }
        public bool Key2 { get; set; }
        public bool Key3 { get; set; }
        public bool Key4 { get; set; }
        public bool Key5 { get; set; }
    }
}
