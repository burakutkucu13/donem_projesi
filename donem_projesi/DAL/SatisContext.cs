using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using donem_projesi.Models; //modeller üzerinden veri setleri oluştururken gerekli
using System.Data.Entity.ModelConfiguration.Conventions; //PluralizingTableNameConvention için gerekli
using System.Data.Entity; //DbContext için gerekli

namespace donem_projesi.DAL
{
    public class SatisContext : DbContext
    {
        //veri tabanı işlemlerini yonetecek merkezi sınıf.modellerle veri tabanı arasındaki bağlantıyı sağlar.

        public SatisContext() : base("SatisVeritabani") { }
        //dbcontextin const.na bu veriyi gönderdi.connectionstring'in adı olacaktır.

        public DbSet<Kategori> Kategori { get; set; }
        public DbSet<IsYeri> IsYeri { get; set; }
        public DbSet<UyeYorum> UyeYorum { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) //tablo isimlerini çoğullayarak oluşturmasını engelledik.
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public System.Data.Entity.DbSet<donem_projesi.Models.Yonetici> Yoneticis { get; set; }


    }

}