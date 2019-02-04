using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using donem_projesi.Models;
using donem_projesi.DAL;

using donem_projesi.Controllers;

using Microsoft.AspNet.Identity; //UserManager sınıfı için..
using Microsoft.AspNet.Identity.EntityFramework;//UserStore sınıfı için..
using Microsoft.Owin.Security;//IAuthenticationManager arayüzü için

namespace donem_projesi.Controllers
{
    public class SiteController : Controller
    {
        SatisContext veritabani = new SatisContext();

        UserManager<ApplicationUser> UserManager { get; set; }

        // GET: Site
        public ActionResult AnaSayfa()
        {
            //Select List İtem elemanlarını belirleme -->> İl Kategorileri
            int i, j;
            var isyerleri = veritabani.IsYeri.Where(u=>u.isyeri_onay == 1).ToList();
            for (i = 0; i < isyerleri.Count; ++i)
            {
                string il = isyerleri[i].Il;
                for (j = 0; j < isyerleri.Count; ++j)
                {
                    if (i != j)
                    {
                        if (il == isyerleri[j].Il)
                        {
                            isyerleri[j].Il = "";
                        }
                    }
                }
            }

            var il_kategori = isyerleri.Where(u => u.Il != "").Select(k => new SelectListItem
            {
                Selected = false,
                Text = k.Il,
                Value = k.Il
            }).ToList();

            ViewBag.ilKategori = il_kategori; //Select List İtem --> İller

            //Select List İtem elemanlarını belirleme -->> İlçe Kategorileri
            var isyerleri_listesi = veritabani.IsYeri.Where(u => u.isyeri_onay == 1).ToList();

            for (i = 0; i < isyerleri_listesi.Count; ++i)
            {
                string ilce = isyerleri_listesi[i].Ilce;
                for (j = 0; j < isyerleri_listesi.Count; ++j)
                {
                    if (i != j)
                    {
                        if (ilce == isyerleri_listesi[j].Ilce)
                        {
                            isyerleri_listesi[j].Ilce = "";
                        }
                    }
                }
            }
            var ilce_kategori = isyerleri_listesi.Where(u => u.Ilce != "").Select(k => new SelectListItem
            {
                Selected = false,
                Text = k.Ilce,
                Value = k.Ilce
            }).ToList();

            ViewBag.ilceKategori = ilce_kategori; //Select List İtem --> İlceler

            //Ana sayfa ilk kez açıldığında listelenecek kategorileri belirleme
            string ilk_il = il_kategori[0].Text; //Kocaeli
            string ilk_ilce = ilce_kategori[0].Text; //Kartepe
            
            //İlk önce kayıtlı olan birinci il ve ilçedeki tüm kategorileri getir..
            List<Kategori> kategori = veritabani.IsYeri.Where(u => u.Il == ilk_il && u.Ilce == ilk_ilce && u.isyeri_onay == 1).ToList().Select(k => new Kategori
            {
                Id = k.Kategori.Id,
                Ad = k.Kategori.Ad
            }).ToList();
            //Aynı il ve ilcedeki kategorilerin adlarnı boşalt. Aynı kategori tekrar gözükmesin..(alt alta cafe, cafe gibi)
            int m, n;
            for (m = 0; m < kategori.Count; ++m)
            {
                string kategori_Adi = kategori[m].Ad;
                for (n = 0; n < kategori.Count; n++)
                {
                    if (m != n)
                    {
                        if (kategori_Adi == kategori[n].Ad)
                        {
                            kategori[n].Ad = "";
                        }

                    }
                }
            }
            //İsmi boş olmayanları(yani birbirinden farklı olanları) göster..
            List<Kategori> kategori_listesi = kategori.Where(u => u.Ad != "").ToList();

            ViewBag.Il = ilk_il; //Resimlerin üzerine tıklanıldığında detay sayfasına hangi il ve 
            ViewBag.Ilce = ilk_ilce;//ilçede arama yapılması gerektiğini belirleyen kısım..

            return View(kategori_listesi);

        }

        [HttpPost]
        public ActionResult AnaSayfa(FormCollection veri)  //Select List ten il ve ilçeyi seçince...
        {
            //Select List İtem elemanlarını belirleme -->> İl Kategorileri
            int i, j;
            var isyerleri = veritabani.IsYeri.Where(u => u.isyeri_onay == 1).ToList();
            for (i = 0; i < isyerleri.Count; ++i)
            {
                string il = isyerleri[i].Il;
                for (j = 0; j < isyerleri.Count; ++j)
                {
                    if (i != j)
                    {
                        if (il == isyerleri[j].Il)
                        {
                            isyerleri[j].Il = "";
                        }
                    }
                }
            }

            var il_kategori = isyerleri.Where(u => u.Il != "").Select(k => new SelectListItem
            {
                Selected = false,
                Text = k.Il,
                Value = k.Il
            }).ToList();

            ViewBag.ilKategori = il_kategori; //Select List İtem --> İller

            //Select List İtem elemanlarını belirleme -->> İlçe Kategorileri
            var isyerleri_listesi = veritabani.IsYeri.Where(u => u.isyeri_onay == 1).ToList();

            for (i = 0; i < isyerleri_listesi.Count; ++i)
            {
                string ilce = isyerleri_listesi[i].Ilce;
                for (j = 0; j < isyerleri_listesi.Count; ++j)
                {
                    if (i != j)
                    {
                        if (ilce == isyerleri_listesi[j].Ilce)
                        {
                            isyerleri_listesi[j].Ilce = "";
                        }
                    }
                }
            }
            var ilce_kategori = isyerleri_listesi.Where(u => u.Ilce != "").Select(k => new SelectListItem
            {
                Selected = false,
                Text = k.Ilce,
                Value = k.Ilce
            }).ToList();

            ViewBag.ilceKategori = ilce_kategori; //Select List İtem --> İlceler

            //Site post edildiğinde listelenecek kategorileri belirleme -->>  seçili il ve ilçeye göre aynı işlemleri tekrarla.
            string s1 = veri["Kategori_Il"];
            string s2 = veri["Kategori_Ilce"];

            List<Kategori> kategori = veritabani.IsYeri.Where(u => u.Il == s1 && u.Ilce == s2 && u.isyeri_onay == 1).ToList().Select(k => new Kategori
            {
                Id = k.Kategori.Id,
                Ad = k.Kategori.Ad
            }).ToList();

            int m, n;
            for (m = 0; m < kategori.Count; ++m)
            {
                string kategori_Adi = kategori[m].Ad;
                for (n = 0; n < kategori.Count; n++)
                {
                    if (m != n)
                    {
                        if (kategori_Adi == kategori[n].Ad)
                        {
                            kategori[n].Ad = "";
                        }

                    }
                }
            }

            List<Kategori> kategori_listesi = kategori.Where(u => u.Ad != "").ToList();

            ViewBag.Il = s1; //Resimlerin üzerine tıklanıldığında detay sayfasına hangi il ve 
            ViewBag.Ilce = s2; //ilçede arama yapılması gerektiğini belirleyen kısım..

            return View(kategori_listesi);
        }


        #region İş Yeri Listesi

        public ActionResult Detay(string id, string il, string ilce)  //id-->>Kategori id..
        {
            ViewBag.Il = il;
            ViewBag.Ilce = ilce;

            var liste = veritabani.IsYeri.Where(u => u.Il == il && u.Ilce == ilce && u.isyeri_onay == 1).ToList();
            var detay_liste = liste.Where(u => u.KategoriId == Convert.ToInt32(id)).ToList();
            ViewBag.Sinif = detay_liste[0].Kategori.Ad;

            return View(detay_liste);
        }

        public ActionResult Yorum_Harita(string isyeri_id) //Yorumları ve haritaları göster linkine tıklarsa...
        {
            var isyeri_bilgileri = veritabani.IsYeri.Where(u => u.Id.ToString() == isyeri_id).FirstOrDefault();

            var detay_liste = veritabani.IsYeri.Where(u => u.Il == isyeri_bilgileri.Il && u.Ilce == isyeri_bilgileri.Ilce && u.KategoriId == isyeri_bilgileri.KategoriId && u.isyeri_onay == 1).ToList();
            
            ViewBag.yorum_harita_goster = 1;
            ViewBag.Il = isyeri_bilgileri.Il;
            ViewBag.Ilce = isyeri_bilgileri.Ilce;
            ViewBag.Sinif = isyeri_bilgileri.Kategori.Ad;
            /////////////////////////////////////////////
            ViewBag.KategoriAdi = isyeri_bilgileri.Kategori.Ad;
            ViewBag.isim = isyeri_bilgileri.Ad;
            ViewBag.Telefon = isyeri_bilgileri.Tel;
            ViewBag.Adres = isyeri_bilgileri.Adres;

            var yorum_listesi = veritabani.UyeYorum.Where(u => u.Yorum_Id == isyeri_id && u.yorum_onay == 1).ToList();
            ViewBag.yorumlar = yorum_listesi;
            ViewBag.koordinat = isyeri_bilgileri.koordinat;
            return View("Detay",detay_liste);

        }

        [HttpPost]
        public ActionResult Yorum_Harita(string isyeri_id,string ad) //Yorum göndere tıklarsa...
        {
            ViewBag.mesaj = "<script lang='JavaScript'>alert('Yorum Yazabilmek İçin Üye Girişi Yapmanız Gerekmektedir!!!');</script>";

            var isyeri_bilgileri = veritabani.IsYeri.Where(u => u.Id.ToString() == isyeri_id).FirstOrDefault();

            var detay_liste = veritabani.IsYeri.Where(u => u.Il == isyeri_bilgileri.Il && u.Ilce == isyeri_bilgileri.Ilce && u.KategoriId == isyeri_bilgileri.KategoriId && u.isyeri_onay == 1).ToList();

            ViewBag.yorum_harita_goster = 1;
            ViewBag.Il = isyeri_bilgileri.Il;
            ViewBag.Ilce = isyeri_bilgileri.Ilce;
            ViewBag.Sinif = isyeri_bilgileri.Kategori.Ad;
            /////////////////////////////////////////////
            ViewBag.KategoriAdi = isyeri_bilgileri.Kategori.Ad;
            ViewBag.isim = isyeri_bilgileri.Ad;
            ViewBag.Telefon = isyeri_bilgileri.Tel;
            ViewBag.Adres = isyeri_bilgileri.Adres;

            var yorum_listesi = veritabani.UyeYorum.Where(u => u.Yorum_Id == isyeri_id && u.yorum_onay == 1).ToList();
            ViewBag.yorumlar = yorum_listesi;
            return View("Detay", detay_liste);

        }


        #endregion

    }
}