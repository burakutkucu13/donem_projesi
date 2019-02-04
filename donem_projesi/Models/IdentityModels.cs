using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace donem_projesi.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        //IdentityUser sınıfının kalıtımını alan kullanıcı model sınıfı...
        //Uygulama içinde kullanılan, kullanıcı bilgilerinin tanımlandığı sınıftır.
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //veritabanına erişim burada(IdentityDbContext sınıfının kalıtımını alan sınıfta) yapılır.
        //ApplicationDbContext sınıfının kullanıcı veri türü ApplicationUser'dır.  
        //ApplicationDbContext sınıfı bir Entity Framework DbSet'i olduğu için Entity'de yapılan kayıt işlemleri(sorgulama,silme,listeleme,düzenleme) aynı şekilde yapılmaktadır.
        //ApplicationUser(IdentityUser sınıfının kalıtımını alan kullanıcı model sınıfı) türünden Users isminde bir DbSet bulunmaktadır. 
        //IdentityRole türünden de Roles isminde bir DbSet bulunmaktadır. 
        //Users özelliği üzerinden veritabanındaki kullanıcı bilgilerine, Roles özelliği üzerinden veritabanındaki rol bilgilerine ulaşıp bu bilgiler üzerinden işlemler yapılır.......
        //Microsoft.AspNet.Identity.EntityFramework bak!!!!!!!!!

        //IdentityDbContext<TUser> sınıfı generic? bir sınıf olup kullanıcı model sınıfının tanımlanmasını ister.
        //TUser da tanımlanacak olan sınıfın Microsoft.AspNet.Identity.EntityFramework.IdentityUser sınıfı türünden olması gerekir.

        public ApplicationDbContext() : base("SiteUyeOl") {  }

        
    }
}