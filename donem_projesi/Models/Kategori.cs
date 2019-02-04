using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel; //model özelliklerini belirtmeye yarar.(DisplayName..)

namespace donem_projesi.Models
{
    public class Kategori
    {
        public int Id { get; set; }

        [DisplayName("Kategori Adı")] //Urun ile bağlantı sağlandıktan sonra id den sonra gelen ilk değişken(Ad) esas alınır.
        public string Ad { get; set; } //urun'un view'ini oluşturduğumuzda urunun kategoriyle bağlantı noktası bu olacaktır.

        //public virtual ICollection<IsYeri> IsYerleri { get; set; } //Urun modeline erişim sağlandı.
        ////Bir kategoride birden fazla urun olacağından ıcollection olarak tanımlandı.
    }
}