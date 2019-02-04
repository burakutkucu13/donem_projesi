using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel; //model özelliklerini belirtmeye yarar.(DisplayName..)
using System.ComponentModel.DataAnnotations;

namespace donem_projesi.Models
{
    public class Yonetici
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Yönetici Adı")]
        public string Ad { get; set; }

        [Required]
        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        public string Sifre { get; set; }
        
    }
}