using DLWMS.Core;
using Microsoft.EntityFrameworkCore;

namespace DLWMS.Repository
{
    public class DLWMSDBContext : DbContext
    {
        public DLWMSDBContext(DbContextOptions<DLWMSDBContext> contextOptions) //konekcija na bazu DbContextOptions<DLWMSDBContext> contextOptions (gdje se nalazi baza i to
            : base(contextOptions) { }
        public DbSet<Student> Studenti { get; set; }
        public DbSet<Predmet> Predmeti { get; set; }

        //Ako klasa ovdje nije navedena ona entity frmeworku ne postoji
    }
}