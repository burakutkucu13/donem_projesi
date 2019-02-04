using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using donem_projesi.Models;
using System.Data.Entity; //CreateDatabaseIfNotExists için gerekli

namespace donem_projesi.DAL
{
    public class SatisInitializer : CreateDatabaseIfNotExists<SatisContext>
    {
        //kategori ve ürün kayıtlarını ekleme kodlarının veritabanını ilk kez oluşturduğumuzda çalışması için bu kalıtımı aldı.
        //veri tabanı oluştuktan sonra bu sınıfın çalışması için web.config'de(<entityFramework> içinde) ayarlamalar yapılmalıdır.

        protected override void Seed(SatisContext context)
        {
            var kategoriYiyecekIcecek = new Kategori { Ad = "Yiyecek & İçecek" };
            var kategoriOzel = new Kategori { Ad = "Özel" };
            var kategoriTerzi = new Kategori { Ad = "Terzi" };
            var kategoriBisikletTamircisi = new Kategori { Ad = "Bisiklet Tamircisi" };
            var kategoriKırtasiye = new Kategori { Ad = "Kırtasiye" };
            var kategoriBerber = new Kategori { Ad = "Berber" };
            var kategoriAyakkabiTamircisi = new Kategori { Ad = "Ayakkabi Tamircisi" };
            var kategoriArabaHaliYikama = new Kategori { Ad = "Araba & Halı Yıkama" };
            var kategoriElektronikEsyaTamircisi = new Kategori { Ad = "Elektronik Eşya Tamircisi" };

            context.Kategori.Add(kategoriYiyecekIcecek);
            context.Kategori.Add(kategoriOzel);
            context.Kategori.Add(kategoriTerzi);
            context.Kategori.Add(kategoriBisikletTamircisi);
            context.Kategori.Add(kategoriKırtasiye);
            context.Kategori.Add(kategoriBerber);
            context.Kategori.Add(kategoriAyakkabiTamircisi);
            context.Kategori.Add(kategoriArabaHaliYikama);
            context.Kategori.Add(kategoriElektronikEsyaTamircisi);

            context.SaveChanges();

            List<IsYeri> isYeri = new List<IsYeri>
            { 
                new IsYeri{Ad="Çiğ Köfteci Sait", Adres="Kartepe", Tel="12312312312", KategoriId=kategoriYiyecekIcecek.Id, Il="Kocaeli", Ilce="Kartepe",isyeri_onay=1,ekleyen_kisi="Burak",koordinat="39.86156903970107,32.83813489062504"},
                new IsYeri{Ad="Meşhur Adıyaman Çiğ Köftecisi", Adres="Kartepe", Tel="12312312312", KategoriId=kategoriYiyecekIcecek.Id, Il="Kocaeli", Ilce="Kartepe",isyeri_onay=1,ekleyen_kisi="Burak",koordinat="39.86156903970107,32.83813489062504"},                
                new IsYeri{Ad="yemek_B", Adres="İzmit", Tel="12312312312", KategoriId=kategoriYiyecekIcecek.Id, Il="Kocaeli", Ilce="İzmit",isyeri_onay=1,ekleyen_kisi="Burak",koordinat="39.86156903970107,32.83813489062504"},
                new IsYeri{Ad="Boyacı Samet Usta", Adres="Keçiören", Tel="12312312312", KategoriId=kategoriOzel.Id, Il="Ankara", Ilce="Keçiören",isyeri_onay=1,ekleyen_kisi="Burak",koordinat="39.86156903970107,32.83813489062504"},
                new IsYeri{Ad="askl", Adres="Kartepe", Tel="12312312312", KategoriId=kategoriTerzi.Id, Il="Kocaeli", Ilce="Kartepe",isyeri_onay=1,ekleyen_kisi="Burak",koordinat="39.86156903970107,32.83813489062504"},
                new IsYeri{Ad="sdfsdf", Adres="Kartepe", Tel="12312312312", KategoriId=kategoriKırtasiye.Id, Il="Kocaeli", Ilce="Kartepe",isyeri_onay=1,ekleyen_kisi="Burak",koordinat="39.86156903970107,32.83813489062504"},
                new IsYeri{Ad="sdfaafd", Adres="Keçiören", Tel="12312312312", KategoriId=kategoriOzel.Id, Il="Ankara", Ilce="Keçiören",isyeri_onay=1,ekleyen_kisi="Burak",koordinat="39.86156903970107,32.83813489062504"},
                new IsYeri{Ad="fdda", Adres="Kartepe", Tel="12312312312", KategoriId=kategoriBerber.Id, Il="Kocaeli", Ilce="Kartepe",isyeri_onay=1,ekleyen_kisi="Burak",koordinat="39.86156903970107,32.83813489062504"},
                new IsYeri{Ad="nvbnaskl", Adres="Kartepe", Tel="12312312312", KategoriId=kategoriBisikletTamircisi.Id, Il="Kocaeli", Ilce="Kartepe",isyeri_onay=1,ekleyen_kisi="Burak",koordinat="39.86156903970107,32.83813489062504"},
                new IsYeri{Ad="bnbnsdfsdf", Adres="Kartepe", Tel="12312312312", KategoriId=kategoriAyakkabiTamircisi.Id, Il="Kocaeli", Ilce="Kartepe",isyeri_onay=1,ekleyen_kisi="Burak",koordinat="39.86156903970107,32.83813489062504"},
                new IsYeri{Ad="cvcbnvsdfaafd", Adres="Keçiören", Tel="12312312312", KategoriId=kategoriOzel.Id, Il="Ankara", Ilce="Keçiören",isyeri_onay=1,ekleyen_kisi="Burak",koordinat="39.86156903970107,32.83813489062504"},
                new IsYeri{Ad="nbvcfdda", Adres="Kartepe", Tel="12312312312", KategoriId=kategoriBerber.Id, Il="Kocaeli", Ilce="Kartepe",isyeri_onay=1,ekleyen_kisi="Burak",koordinat="39.86156903970107,32.83813489062504"},
                new IsYeri{Ad="cvcbnvsdfaafd", Adres="Kartepe", Tel="12312312312", KategoriId=kategoriArabaHaliYikama.Id, Il="Kocaeli", Ilce="Kartepe",isyeri_onay=1,ekleyen_kisi="Burak",koordinat="39.86156903970107,32.83813489062504"},
                new IsYeri{Ad="nbvcfdda", Adres="Kartepe", Tel="12312312312", KategoriId=kategoriElektronikEsyaTamircisi.Id, Il="Kocaeli", Ilce="Kartepe",isyeri_onay=1,ekleyen_kisi="Burak",koordinat="39.86156903970107,32.83813489062504"},
                new IsYeri{Ad="Elektrikçi Mehmet Usta", Adres="Kartepe", Tel="12312312312", KategoriId=kategoriOzel.Id, Il="Kocaeli", Ilce="Kartepe",isyeri_onay=1,ekleyen_kisi="Burak",koordinat="39.86156903970107,32.83813489062504"},
                new IsYeri{Ad="Marangoz Ahmet", Adres="Kartepe", Tel="12312312312", KategoriId=kategoriOzel.Id, Il="Kocaeli", Ilce="Kartepe",isyeri_onay=1,ekleyen_kisi="Burak",koordinat="39.86156903970107,32.83813489062504"},
                new IsYeri{Ad="Tesisatçı Mustafa Usta", Adres="Kartepe", Tel="12312312312", KategoriId=kategoriOzel.Id, Il="Kocaeli", Ilce="Kartepe",isyeri_onay=1,ekleyen_kisi="Burak",koordinat="39.86156903970107,32.83813489062504"},
                new IsYeri{Ad="Boyacı Sami Usta", Adres="Kartepe", Tel="12312312312", KategoriId=kategoriOzel.Id, Il="Kocaeli", Ilce="Kartepe",isyeri_onay=1,ekleyen_kisi="Burak",koordinat="39.86156903970107,32.83813489062504"},
                new IsYeri{Ad="Elektrikçi Selim Usta", Adres="Kartepe", Tel="12312312312", KategoriId=kategoriOzel.Id, Il="Kocaeli", Ilce="Kartepe",isyeri_onay=1,ekleyen_kisi="Burak",koordinat="39.86156903970107,32.83813489062504"},
                new IsYeri{Ad="Marangoz Hakan", Adres="Kartepe", Tel="12312312312", KategoriId=kategoriOzel.Id, Il="Kocaeli", Ilce="Kartepe",isyeri_onay=1,ekleyen_kisi="Burak",koordinat="39.86156903970107,32.83813489062504"},
                new IsYeri{Ad="Tesisatçı Özer Usta", Adres="Kartepe", Tel="12312312312", KategoriId=kategoriOzel.Id, Il="Kocaeli", Ilce="Kartepe",isyeri_onay=1,ekleyen_kisi="Burak",koordinat="39.86156903970107,32.83813489062504"},
                new IsYeri{Ad="Boyacı Samet Usta", Adres="Kartepe", Tel="12312312312", KategoriId=kategoriOzel.Id, Il="Kocaeli", Ilce="Kartepe",isyeri_onay=1,ekleyen_kisi="Burak",koordinat="39.86156903970107,32.83813489062504"}
            };

            isYeri.ForEach(urun => context.IsYeri.Add(urun));
            context.SaveChanges();

        }

    }
}