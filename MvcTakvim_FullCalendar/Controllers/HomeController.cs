using MvcTakvim_FullCalendar.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcTakvim_FullCalendar.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        TakvimContext db = new TakvimContext();

        //TAKVİM ETKİNLİK LİSTELEME
        public JsonResult GetTakvimEtkinlik(string start, string end)
        { //Entity sorungusunda tarihi çevirmemize imkan vermediği için  oluşan listeyi terkar çekip tarileri çevirdik hkn 01.08.2018

            DateTime tarihStart = Convert.ToDateTime(start);
            DateTime tarihEnd = Convert.ToDateTime(end);
            List<TakvimTemel> EtkinlikListe = (from e in db.Takvim
                                               where e.Baslangic > tarihStart && e.Bitis < tarihEnd && e.iseAktif == true
                                               select new
                                               {
                                                   ID = e.ID,
                                                   Renk = e.Renk,
                                                   TumGun = e.iseTumGun,
                                                   Bitis = e.Bitis,
                                                   Baslangic = e.Baslangic,
                                                   Baslik = e.Baslik
                                               }).ToList().Select(x => new TakvimTemel()
                                               {
                                                   id = x.ID,
                                                   color = x.Renk,
                                                   allDay = x.TumGun,
                                                   start = string.Format("{0:yyyy-MM-dd HH:mm:ss}", x.Baslangic),
                                                   end = string.Format("{0:yyyy-MM-dd HH:mm:ss}", x.Bitis),
                                                   title = x.Baslik
                                               }).ToList();

            return Json(EtkinlikListe, JsonRequestBehavior.AllowGet);
        }

        //TAKVİM ETKİNLİK EKLEME
        Random random = new Random();
        public JsonResult EtkinlikEkle(TakvimTemel etkinlik)
        {
            if (etkinlik.title != null)
            {
                Takvim yeniEtkinlik = new Takvim();

                yeniEtkinlik.Baslik = etkinlik.title;
                yeniEtkinlik.Baslangic = Convert.ToDateTime(etkinlik.start);
                yeniEtkinlik.Bitis = Convert.ToDateTime(etkinlik.end);
                yeniEtkinlik.Renk = etkinlik.color;
                yeniEtkinlik.iseTumGun = etkinlik.allDay;
                yeniEtkinlik.iseAktif = true;
                yeniEtkinlik.OlusturulmaTarihi = DateTime.Now;
                db.Takvim.Add(yeniEtkinlik);
                db.SaveChanges();
            }
            return Json(true);
        }

        //TAKVİM ETKİNLİK SİLME
        public JsonResult EtkinlikSil(int id)
        {
            if (id != 0)
            {
                Takvim SilinecekEtkinlik = db.Takvim.Find(id);
                SilinecekEtkinlik.iseAktif = false;
                db.Entry(SilinecekEtkinlik).State = EntityState.Modified;
                db.SaveChanges();
            }
            return Json(true, JsonRequestBehavior.AllowGet);


        }

        //TAKVİM ETKİNLİK TARİH GÜNCELLEME
        public JsonResult EtkinlikTarihGuncelle(int id, string start, string end)
        {
            if (id != 0)
            {
                Takvim GuncellenecekEtkinlik = db.Takvim.Find(id);
                GuncellenecekEtkinlik.Baslangic = Convert.ToDateTime(start);
                GuncellenecekEtkinlik.Bitis = Convert.ToDateTime(end);
                db.Entry(GuncellenecekEtkinlik).State = EntityState.Modified;
                db.SaveChanges();
            }
            return Json(true, JsonRequestBehavior.AllowGet);

        }
    }
}