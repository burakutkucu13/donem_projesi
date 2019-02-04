using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel; //model özelliklerini belirtmeye yarar.(DisplayName..)
using System.ComponentModel.DataAnnotations;

namespace donem_projesi.Models
{
    public class IsYeri
    {
        public int Id { get; set; }

        public int KategoriId { get; set; } //bu isim olmazsa olmaz!!!başka isimde tanımlanırsa(ör. kategoriiiId)urunlerin view'i 
        //oluşturulurken kategori.ad değil de kategoriiiId değişkeni gelecektir.iki model arasındaki bağlantının bir anlamı olmaz... 

        [Required]
        [Display(Name = "İsim")]
        public string Ad { get; set; }

        [Required]
        [Display(Name = "Telefon")]
        [MaxLength(11, ErrorMessage = "11 Karakterden fazla giremezsiniz!")]
        [MinLength(11, ErrorMessage = "11 Karakterden az giremezsiniz!")]
        public string Tel { get; set; }

        [Required]
        [Display(Name = "Adres")]
        public string Adres { get; set; }

        public virtual Kategori Kategori { get; set; } //Kategori modeline erişim sağlandı.
        //Kategori modelinde ayrıca urunlere bağlantı sağlanması zorunlu değildir!!!! 

        [Required]
        [Display(Name="İl")]
        public string Il { get; set; }

        [Required]
        [Display(Name = "İlçe")]
        public string Ilce { get; set; }

        [Required]
        [Display(Name = "Koordinat Bilgileri")]
        public string koordinat { get; set; }

        public int isyeri_onay{ get; set; }

        //[Required]
        public string ekleyen_kisi { get; set; }

    }
}