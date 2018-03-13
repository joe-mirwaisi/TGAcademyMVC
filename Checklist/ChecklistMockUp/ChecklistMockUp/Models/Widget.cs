using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChecklistMockUp.Models
{
    public class Widget
    {
        public Guid Id { get; set; }
        public string WidgetXML { get; set; }
        public string KeyName { get; set; }
        public bool KeyValue { get; set; }
        public string UserEmail { get; set; }

    }
}