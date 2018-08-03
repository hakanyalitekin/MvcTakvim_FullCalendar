using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcTakvim_FullCalendar.Models
{
    public class Takvim
    {
        public int ID { get; set; }
        public string Baslik { get; set; }
        public DateTime Baslangic { get; set; }
        public DateTime Bitis { get; set; }
        public string Renk { get; set; }
        public bool iseTumGun { get; set; }
        public bool iseAktif { get; set; }
        public DateTime OlusturulmaTarihi { get; set; }
    }
}