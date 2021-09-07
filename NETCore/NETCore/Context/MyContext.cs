using Microsoft.EntityFrameworkCore;
using NETCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.Context
{
    public class MyContext:DbContext    //merupakan gateway ke database
    {
        public MyContext(DbContextOptions<MyContext>options):base(options)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server = DESKTOP-BJ8DRIN; Database = NETCore ; User ID =sa; Password= sapassword;Trusted_Connection=True");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasOne(account => account.Account).WithOne(person => person.Person)
                .HasForeignKey<Account>(account => account.NIK);

            modelBuilder.Entity<Account>()
               .HasOne(profiling => profiling.Profiling).WithOne(account => account.Account)
               .HasForeignKey<Profiling>(profiling => profiling.NIK);

            modelBuilder.Entity<Profiling>()
                .HasOne(education => education.Education).WithMany(profiling => profiling.Profilings);

            modelBuilder.Entity<AccountRole>()
        .HasKey(ar => new { ar.AccountId, ar.RoleId });
            modelBuilder.Entity<AccountRole>()
                .HasOne(ar => ar.Account)
                .WithMany(a => a.AccountRoles)
                .HasForeignKey(ar => ar.AccountId);
            modelBuilder.Entity<AccountRole>()
                .HasOne(ar => ar.Role)
                .WithMany(r => r.AccountRoles)
                .HasForeignKey(ar => ar.RoleId);

            modelBuilder.Entity<University>()
                .HasMany(education => education.Educations).WithOne(university => university.University);
            //modelBuilder.Entity<Person>().ToTable("tb_tr_accounts");
            //modelBuilder.Entity<Account>().ToTable("tb_tr_accounts");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        public DbSet<Person> Persons { get; set; } //Database set
        public DbSet<Education> Educations { get; set; }
        public DbSet<Profiling> Profilings { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }







    }
}
