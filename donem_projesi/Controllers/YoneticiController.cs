using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using donem_projesi.Models;
using donem_projesi.DAL;

namespace donem_projesi.Controllers
{
    public class YoneticiController : Controller
    {
        SatisContext veritabani = new SatisContext();
        // GET: Yonetici
        
        
        public ActionResult Yonetici_Sayfasi()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Yonetici_Sayfasi(Yonetici yonetici)
        {
            if (ModelState.IsValid)
            {
                if (yonetici.Ad == "Burak" && yonetici.Sifre == "Burak@1388")
                    ViewBag.yonetici_mi = 1;
                return View();
            }
            else
            {
                return View();
            }
            
        }

        public ActionResult Yorumlar(string detay) //Kullanıcı Yorumları...
        {
            var yorumlar = veritabani.UyeYorum.Where(u => u.yorum_onay == 0).ToList();
            ViewBag.detay = detay;

            if (detay != null) //Yönetici yapılan yoruma ait detay linkine tıklarsa, yorum yapılan işyerine ait bilgileri göster...
            {
                var isyeri = veritabani.IsYeri.Where(u => u.Id.ToString() == detay).FirstOrDefault();//Hangi işyerine yorum yapıldı bilgisi...
                ViewBag.kategoriAdi = isyeri.Kategori.Ad;
                ViewBag.isim = isyeri.Ad;
                ViewBag.telefon = isyeri.Tel;
                ViewBag.adres = isyeri.Adres;
                ViewBag.il = isyeri.Il;
                ViewBag.ilce = isyeri.Ilce;
            }
            return View(yorumlar);
        }

        public ActionResult Yorum_Onay(int id, string isyeri_id)
        {
            veritabani.UyeYorum.Where(u => u.Id == id && u.Yorum_Id == isyeri_id).FirstOrDefault().yorum_onay = 1;
            veritabani.SaveChanges();
            return RedirectToAction("Yorumlar");
        }

        public ActionResult Yorum_Kaldir(int id, string isyeri_id)
        {
            UyeYorum silinecek_yorum = veritabani.UyeYorum.Where(u => u.Id == id && u.Yorum_Id == isyeri_id).FirstOrDefault();
            veritabani.UyeYorum.Remove(silinecek_yorum);
            veritabani.SaveChanges();
            return RedirectToAction("Yorumlar");
        }

        public ActionResult Isyeri_istekleri(string harita_id) //Kullanıcı İş Yeri Ekleme İstekleri...
        {
            var isyerleri = veritabani.IsYeri.Where(u => u.isyeri_onay == 0).ToList();
            if (harita_id != null)
            {
                var isyeri = veritabani.IsYeri.Where(u => u.Id.ToString() == harita_id).FirstOrDefault();
                ViewBag.koordinat = isyeri.koordinat;

                ViewBag.kategoriAdi = isyeri.Kategori.Ad;
                ViewBag.isim = isyeri.Ad;
                ViewBag.telefon = isyeri.Tel;
                ViewBag.adres = isyeri.Adres;
                ViewBag.il = isyeri.Il;
                ViewBag.ilce = isyeri.Ilce;
            }
            return View(isyerleri);
        }

        public ActionResult Isyeri_Onay(string isyeri_id)
        {
            veritabani.IsYeri.Where(u => u.Id.ToString() == isyeri_id).FirstOrDefault().isyeri_onay = 1;
            veritabani.SaveChanges();
            return RedirectToAction("Isyeri_istekleri");
        }

        public ActionResult Isyeri_Kaldir(string isyeri_id)
        {
            IsYeri silinecek_isyeri = veritabani.IsYeri.Where(u => u.Id.ToString() == isyeri_id).FirstOrDefault();
            veritabani.IsYeri.Remove(silinecek_isyeri);
            veritabani.SaveChanges();
            return RedirectToAction("Isyeri_istekleri");
        }

    }
}