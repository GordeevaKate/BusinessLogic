using Microsoft.EntityFrameworkCore;
using DatabaseImplement.Models;

namespace DatabaseImplement
{
    public class KursachDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                  optionsBuilder.UseSqlServer(@"Data Source=25.57.59.231\NEWMSSQLSERVER;Initial Catalog=KursachDatabase;User Id = sa; Password = 123;");
            }
            base.OnConfiguring(optionsBuilder);
        }

        public virtual DbSet<Client> Clients { set; get; }
        public virtual DbSet<Agent> Agents { set; get; }
        public virtual DbSet<Dogovor> Dogovors { set; get; }
        public virtual DbSet<Dogovor_Reis> Dogovor_Reiss { set; get; }
        public virtual DbSet<Raion> Raions { set; get; }
        public virtual DbSet<Reis> Reiss { set; get; }
        public virtual DbSet<User> Users { set; get; }
        public virtual DbSet<Zarplata> Zarplatas { set; get; }
    }
}
