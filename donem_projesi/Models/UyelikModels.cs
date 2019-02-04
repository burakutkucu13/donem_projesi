using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace donem_projesi.Models
{
    public class Uye
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Kullanıcı Adı")]
        public string KullaniciAdi { get; set; }

        [Required]
        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        public string Sifre { get; set; }

    }

    public class UyeOl : Uye
    {
        [Required]
        [Display(Name = "Ad")]
        public string Ad { get; set; }
        
        [Required]
        [Display(Name = "Soyad")]
        public string SoyAd { get; set; }

        [Required]
        [Display(Name = "Şifre Tekrar")]
        [DataType(DataType.Password)]
        [Compare("Sifre")]
        public string Sifre2 { get; set; }

        [Required]
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    
    }

    public class UyeYorum : Uye
    {
        public string Yorum_Id { get; set; }

        public int yorum_onay { get; set; }

        public string Yorum { get; set; }

        public DateTime yorum_tarihi { get; set; }

    }

}