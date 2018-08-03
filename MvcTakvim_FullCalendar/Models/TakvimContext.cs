using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MvcTakvim_FullCalendar.Models
{
    public class TakvimContext:DbContext
    {
        public TakvimContext():base("TakvimDB")
        {
            
        }
        public DbSet<Takvim> Takvim{ get; set; }
    }
}