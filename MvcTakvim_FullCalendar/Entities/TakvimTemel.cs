using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcTakvim_FullCalendar.Models
{
    public class TakvimTemel
    {
         //FullCalendar'ın çalışabilmesi için temel nesne(isimleri değiştirilirse çalışmaz.)
        public int id { get; set; }
        public string title { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string color { get; set; }
        public bool allDay { get; set; }
    }
}