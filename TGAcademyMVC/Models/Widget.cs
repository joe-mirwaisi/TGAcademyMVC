using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TGAcademyMVC.Models
{
    public class Widget
    {
        public Guid ID { get; set; }

        //Enitity Framwork will not allow storing an XML file, but we can save the XML as a string that we later parse out as XML
        public string xml { get; set; }
        public string UserID { get; set; }
        public string Key { get; set; }
        public bool Key_Value { get; set; }
       

    }
}
