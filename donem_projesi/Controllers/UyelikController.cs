using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using donem_projesi.Models;
using Microsoft.AspNet.Identity; //UserManager sınıfı için..
using Microsoft.AspNet.Identity.EntityFramework;//UserStore sınıfı için..
using Microsoft.Owin.Security;//IAuthenticationManager arayüzü için

using donem_projesi.DAL;

namespace donem_projesi.Controllers
{
    public class UyelikController : Controller
    {
        ApplicationDbContext _db = new ApplicationDbContext();
        UserManager<ApplicationUser> UserManager { get; set; }//UserManager sınıfı da generic bir sınıftır ve generic tanımlamasında kullanıcı model sınıfının tanımlanmasını ister...
        RoleManager<IdentityRole> RoleManager { get; set; }//RoleManager sınıfı da generic bir sınıftır ve generic tanımlamasında kullanıcı model sınıfının tanımlanmasını ister..

        SatisContext veritabani = new SatisContext();

        IAuthenticationManager AuthenticationManager
        {
            //Kullanıcının sisteme giriş yapması, giriş yapan kullanıcını bilgilerine erişim, sistemden çıkış yapma işlemleri 
            //IAuthenticationManager interface'inin kalıtımını alan bu özellik üzerinden yapılır...
            //Bu özellik kimlik doğrulama işlemlerini barındırır.

            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public UyelikController()
        {
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

        }

        #region Üye Ol

        public ActionResult UyeOl()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UyeOl(UyeOl yeni_kullanici)
        {
            if (ModelState.IsValid) //Girdiği bilgiler doğruysa
            {
                //Aynı kullanıcı adlı bir kişi daha var mı kontrolü...
                var bul = _db.Users.Where(u => u.UserName == yeni_kullanici.KullaniciAdi).Select(k => new UyeOl
                    {
                        KullaniciAdi = yeni_kullanici.KullaniciAdi
                    });

                if (bul.Count() == 0) //Aynı kullanıcı adlı kişi yoksa işleme devam et..
                {
                    ApplicationUser kullanici = new ApplicationUser
                    {
                        UserName = yeni_kullanici.KullaniciAdi,
                        Email = yeni_kullanici.Email,
                        PasswordHash = yeni_kullanici.Sifre
                    };
                    _db.Users.Add(kullanici);
                    _db.SaveChanges();

                    //UserManager.AddPassword(kullanici.Id, yeni_kullanici.Sifre);
                    //_db.SaveChanges();

                    //Uyarı bildir...
                    string onay = "<script lang='JavaScript'>alert('TAMAM');</script>";
                    return RedirectToAction("AnaSayfa", "Site");
                }
                else //Aynı kullanıcı adlı kişi varsa
                {
                    ViewBag.hata = "<script lang='JavaScript'>alert('Aynı kullanıcı adli kişi mevcut olduğundan başka bir kulanıcı adı belirtiniz!');</script>";
                    return View();
                }
            }
            else
            {
                ViewBag.mesaj = "<script lang='JavaScript'>alert('Eksik veya Hatalı Bilgi Girişi');</script>";
            }
            return View();
        }

        #endregion

        #region Üye Girişi

        public ActionResult UyeGiris()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UyeGiris(Uye kullanici)
        {
            if (ModelState.IsValid)
            {
                var kullanıcı_bul = UserManager.FindByName(kullanici.KullaniciAdi);
                if (kullanıcı_bul == null)  //aradığı isimde kullanıcı yoksa
                {
                    ViewBag.mesaj = "<script lang='JavaScript'>alert('Aradığınız isimde bir kullanıcı bulunamadı!!!');</script>";
                }
                else //aradığı isimde kullanıcı varsa şifre kontrolü yaptır. 
                {
                    if (kullanıcı_bul.PasswordHash == kullanici.Sifre)//Şifreyi de doğru yazarsa UyeSayfası'na id'si ile beraber yönlendir.
                    {
                        return RedirectToAction("UyeSayfasi", "Uyelik", new { id = kullanıcı_bul.Id }); //Kullanıcını ad ve şifre
                    }
                    else //Şifreyi yanlış yazarsa aynı sayfaya yönlendir..
                    {
                        ViewBag.mesaj = "<script lang='JavaScript'>alert('Belirtilen kullanıcıya ait şifre bilgisi yanlış!!!');</script>";
                    }
                }
                return View();
            }
            return View();

        }

        #endregion

        public ActionResult UyeSayfasi(string id, string istek)
        {
            if (istek == "1")
            {
                ViewBag.alert = "<script lang='JavaScript'>alert('İş yeri ekleme isteğiniz başarıyla alınmıştır.Yönetici uygun gördüğü takdirde ekleme işlemi gerçekleşecektir.');</script>";
            }
            //Üye girişi yapılınca sağ üst köşede gözükecek kullanıcı adının belirlenmesi.
            ViewBag.kullaniciAdi = UserManager.FindById(id).UserName;

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

            //UyeSayfasi ilk kez açıldığında sol tarafta alt alta listelenecek kategorileri belirleme
            string ilk_il = il_kategori[0].Text; //Kocaeli
            string ilk_ilce = ilce_kategori[0].Text; //Kartepe

            //İlk önce kayıtlı olan birinci il ve ilçedeki tüm kategorileri getir..
            List<Kategori> kategori = veritabani.IsYeri.Where(u => u.Il == ilk_il && u.Ilce == ilk_ilce && u.isyeri_onay == 1).ToList().Select(k => new Kategori
            {
                Id = k.Kategori.Id,
                Ad = k.Kategori.Ad
            }).ToList();
            //Aynı il ve ilcedeki kategorilerin adlarını boşalt. Aynı kategori tekrar gözükmesin..(alt alta cafe, cafe gibi)
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
        public ActionResult UyeSayfasi(string id, FormCollection veri)
        {
            ViewBag.kullaniciAdi = UserManager.FindById(id).UserName;

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

            ViewBag.Il = s1; //Resimlerin üzerine tıklanıldığında detay sayfasına hangi il ve 
            ViewBag.Ilce = s2; //ilçede arama yapılması gerektiğini belirleyen kısım..

            return View(kategori_listesi);
        }

        public ActionResult UyeDetay(string id, string il, string ilce, string kullaniciAdi, string uyari_mesaji) //id-->>Kategori id..
        {
            ViewBag.kullaniciId = UserManager.FindByName(kullaniciAdi).Id; //Kullanıcı id'si...
            ViewBag.kullaniciAdi = kullaniciAdi;

            ViewBag.Il = il;
            ViewBag.Ilce = ilce;

            var liste = veritabani.IsYeri.Where(u => u.Il == il && u.Ilce == ilce && u.isyeri_onay == 1).ToList();
            var detay_liste = liste.Where(u => u.KategoriId == Convert.ToInt32(id)).ToList();
            ViewBag.Sinif = detay_liste[0].Kategori.Ad;

            ViewBag.alert = uyari_mesaji;
            return View(detay_liste);
        }

        public ActionResult Yorum_Harita(string isyeri_id, string kullaniciAdi) //Yorumları ve haritaları göster linkine tıklarsa...
        {
            ViewBag.kullaniciId = UserManager.FindByName(kullaniciAdi).Id; //Kullanıcı id'si...
            var isyeri_bilgileri = veritabani.IsYeri.Where(u => u.Id.ToString() == isyeri_id).FirstOrDefault();
            ViewBag.kullaniciAdi = kullaniciAdi;
            var detay_liste = veritabani.IsYeri.Where(u => u.Il == isyeri_bilgileri.Il && u.Ilce == isyeri_bilgileri.Ilce && u.KategoriId == isyeri_bilgileri.KategoriId && u.isyeri_onay == 1).ToList();

            ViewBag.yorum_harita_goster = 1; //UyeDetay View'ına gidince yorumlar kısmını açması için 1 yaptık...

            //Hangi il , ilçe ve kategoride oldugunu göstermek için...
            ViewBag.Il = isyeri_bilgileri.Il; ViewBag.Ilce = isyeri_bilgileri.Ilce; ViewBag.Sinif = isyeri_bilgileri.Kategori.Ad;
            /////////////////////////////////////////////
            ViewBag.KategoriAdi = isyeri_bilgileri.Kategori.Ad;
            ViewBag.isim = isyeri_bilgileri.Ad;
            ViewBag.Telefon = isyeri_bilgileri.Tel;
            ViewBag.Adres = isyeri_bilgileri.Adres;

            var yorum_listesi = veritabani.UyeYorum.Where(u => u.Yorum_Id == isyeri_id && u.yorum_onay == 1).ToList();
            ViewBag.yorumlar = yorum_listesi;

            ViewBag.koordinat = isyeri_bilgileri.koordinat;
            return View("UyeDetay", detay_liste);
        }

        [HttpPost]
        public ActionResult Yorum_Harita(string isyeri_id, string kullaniciAdi, FormCollection yorum) //Yorum Gönder butonuna tıklarsa...
        {
            ViewBag.kullaniciId = UserManager.FindByName(kullaniciAdi).Id; //Kullanıcı id'si...
            string mesaj = "";
            //aynı şeyler...
            var isyeri_bilgileri = veritabani.IsYeri.Where(u => u.Id.ToString() == isyeri_id).FirstOrDefault();
            ViewBag.kullaniciAdi = kullaniciAdi;
            var detay_liste = veritabani.IsYeri.Where(u => u.Il == isyeri_bilgileri.Il && u.Ilce == isyeri_bilgileri.Ilce && u.KategoriId == isyeri_bilgileri.KategoriId && u.isyeri_onay == 1).ToList();

            ViewBag.yorum_harita_goster = 1; //UyeDetay View'ına gidince yorumlar kısmını açması için 1 yaptık...

            //Hangi il , ilçe ve kategoride oldugunu göstermek için...
            ViewBag.Il = isyeri_bilgileri.Il; ViewBag.Ilce = isyeri_bilgileri.Ilce; ViewBag.Sinif = isyeri_bilgileri.Kategori.Ad;
            //İşyeri bilgileri -->> sol alttaki
            ViewBag.KategoriAdi = isyeri_bilgileri.Kategori.Ad; ViewBag.isim = isyeri_bilgileri.Ad; ViewBag.Telefon = isyeri_bilgileri.Tel; ViewBag.Adres = isyeri_bilgileri.Adres;
            //  *** UyeYorum sınıfına kullanıcı adını, id sini, yorum id'sini(hangi iş yerine ait yorum olduğunu) ve yorumunu
            //  *** yorum onay 0 olacak şekilde ekle ***
            string kullanici_yorumu = yorum["text_area"];
            if (kullanici_yorumu == "")
            {
                mesaj = "<script lang='JavaScript'>alert('İçerik boş olamaz!');</script>";
            }
            else
            {
                DateTime tarih = DateTime.Now;
                //mesaj = "<script lang='JavaScript'>alert('Yorumunuz site yöneticisine gönderilmiştir.Eğer yorumunuzu uygun görürse ekranda gözükecektir.');</script>";
                mesaj = "<script lang='JavaScript'>alert('Yorumunuz başarılı bir şekilde alınmıştır.Site yöneticisi uygun gördüğü takdirde, en kısa zamanda diğer yorumlar arasındaki yerini alacaktır');</script>";

                var kullanici_bilgileri = UserManager.FindByName(kullaniciAdi); //Hangi kullanıcı yorum yaptı bilgisi...
                UyeYorum yeni_yorum = new UyeYorum();
                yeni_yorum.KullaniciAdi = kullanici_bilgileri.UserName;
                yeni_yorum.Sifre = kullanici_bilgileri.PasswordHash;
                yeni_yorum.Yorum_Id = isyeri_id;
                yeni_yorum.Yorum = kullanici_yorumu;
                yeni_yorum.yorum_onay = 0;
                yeni_yorum.yorum_tarihi = tarih;

                veritabani.UyeYorum.Add(yeni_yorum);
                veritabani.SaveChanges();
            }

            ViewBag.alert = mesaj;
            var yorum_listesi = veritabani.UyeYorum.Where(u => u.Yorum_Id == isyeri_id && u.yorum_onay == 1).ToList();
            ViewBag.yorumlar = yorum_listesi;
            return View("UyeDetay", detay_liste);
        }

        public ActionResult Isyeri_Ekle(string kategoriAdi, string il, string ilce, string kullaniciAdi,string mesaj)
        {
            IsYeri yeni_isyeri = new IsYeri();
            yeni_isyeri.Il = il;
            yeni_isyeri.Ilce = ilce;
            yeni_isyeri.KategoriId = veritabani.Kategori.Where(u => u.Ad == kategoriAdi).FirstOrDefault().Id;
            yeni_isyeri.Kategori = veritabani.Kategori.Where(u => u.Ad == kategoriAdi).FirstOrDefault();
            yeni_isyeri.isyeri_onay = 0;

            ViewBag.kullaniciAdi = kullaniciAdi;
            return View(yeni_isyeri);
        }

        [HttpPost]
        public ActionResult Isyeri_Ekle(IsYeri yeni_isyeri, string kategoriAdi, string kullaniciAdi)
        {
            yeni_isyeri.KategoriId = veritabani.Kategori.Where(u => u.Ad == kategoriAdi).FirstOrDefault().Id;
            yeni_isyeri.Kategori = veritabani.Kategori.Where(u => u.Ad == kategoriAdi).FirstOrDefault();
            yeni_isyeri.ekleyen_kisi = kullaniciAdi;

            if (ModelState.IsValid)
            {
                veritabani.IsYeri.Add(yeni_isyeri);
                veritabani.SaveChanges();
                return RedirectToAction("UyeSayfasi", new { id = UserManager.FindByName(kullaniciAdi).Id, istek = 1 });
            }
            else
            {
                ViewBag.alert = "<script lang='JavaScript'>alert('Eksik veya hatalı bilgi girişi!!!');</script>";
                ViewBag.kullaniciAdi = kullaniciAdi;
                return View(yeni_isyeri);
            }
        }

    }
}