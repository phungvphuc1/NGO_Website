using Microsoft.EntityFrameworkCore;
using NGOWebApp.Data.ConfigData.Seeders;
using NGOWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGOWebApp.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }
        public virtual DbSet<Account> GetAccounts { get; set; }
        public virtual DbSet<ContactUs> GetContactUs { get; set; }
        public virtual DbSet<Donate> GetDonates { get; set; }
        public virtual DbSet<DonateCategory> GetDonateCategories { get; set; }
        public virtual DbSet<Fund> GetFunds { get; set; }
        public virtual DbSet<Interested> GetInteresteds { get; set; }
        public virtual DbSet<Partner> GetPartners { get; set; }
        public virtual DbSet<Photos> GetPhotos { get; set; }
        public virtual DbSet<Programs> GetPrograms { get; set; }
        public virtual DbSet<Query> GetQueries { get; set; }
        public virtual DbSet<Reply> GetReplies { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            #region DefaultValues

            //account
           
            modelBuilder.Entity<Account>().Property(a => a.RoleId).HasDefaultValue(2);
            modelBuilder.Entity<Account>().Property(a => a.Avatar).HasDefaultValue("images/avatar.jpg");
            modelBuilder.Entity<Account>().Property(a => a.CreatedAt).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Account>().Property(a => a.Status).HasDefaultValue(1);

            //DonateCategory
            modelBuilder.Entity<DonateCategory>().Property(a => a.Status).HasDefaultValue(1);


            //contacUs
            modelBuilder.Entity<ContactUs>().Property(a => a.CreatedAt).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<ContactUs>().Property(a => a.Status).HasDefaultValue(1);

            //Donate
            modelBuilder.Entity<Donate>().Property(a => a.CreatedAt).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Donate>().Property(a => a.Status).HasDefaultValue(1);

            //Fund
            modelBuilder.Entity<Fund>().Property(a => a.CreatedAt).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Fund>().Property(a => a.Status).HasDefaultValue(1);

            //Interested
            modelBuilder.Entity<Interested>().Property(a => a.CreatedAt).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Interested>().Property(a => a.Status).HasDefaultValue(1);

            //Partner
            modelBuilder.Entity<Partner>().Property(a => a.CreatedAt).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Partner>().Property(a => a.UpdatedAt).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Partner>().Property(a => a.Status).HasDefaultValue(1);

            //Programs
            modelBuilder.Entity<Programs>().Property(a => a.CreatedAt).HasDefaultValueSql("getdate()");                      
            modelBuilder.Entity<Programs>().Property(a => a.Status).HasDefaultValue(1);
            modelBuilder.Entity<Programs>().Property(a => a.ExpectedAmount).HasDefaultValue((double)0);

            //Query
            modelBuilder.Entity<Query>().Property(a => a.CreatedAt).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Query>().Property(a => a.Status).HasDefaultValue(1);

            //Reply
            modelBuilder.Entity<Reply>().Property(a => a.CreatedAt).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Reply>().Property(a => a.Status).HasDefaultValue(1);

            #endregion

            #region RelationShip
            //one to one

            //(Accoun-roles)
            modelBuilder.Entity<Account>().HasOne(a => a.GetRole);
            //Fund - Donate
            modelBuilder.Entity<Fund>().HasOne(f => f.GetDonate).WithOne(d => d.GetFund).HasForeignKey<Fund>(f => f.DonateId);

            //one to many

            //(account- donate)
            modelBuilder.Entity<Account>().HasMany(a => a.GetDonates).WithOne(d => d.GetAccount);
            //(account-interested)
            modelBuilder.Entity<Account>().HasMany(a => a.GetInteresteds).WithOne(i => i.GetAccount);
            //(Account-Query)
            modelBuilder.Entity<Account>().HasMany(a => a.GetQueries).WithOne(q => q.GetAccount);
            //(Account-Reply)
            modelBuilder.Entity<Account>().HasMany(a => a.GetReplies).WithOne(r => r.GetAccount);
            //Category-partner
            modelBuilder.Entity<DonateCategory>().HasMany(a => a.GetPartners).WithOne(r => r.GetDonateCategory);
            //(Partner-Donate)
            modelBuilder.Entity<Partner>().HasMany(p => p.GetDonates).WithOne(d => d.GetPartner);
            //(Partner-Program)
            modelBuilder.Entity<Partner>().HasMany(p => p.GetPrograms).WithOne(d => d.GetPartner);
            //(Query-Reply)
            modelBuilder.Entity<Query>().HasMany(q => q.GetReplies).WithOne(r => r.GetQuery);

            //(Program-Interested)
            modelBuilder.Entity<Programs>().HasMany(p => p.GetInteresteds).WithOne(i => i.GetPrograms);
            //(Program-Photos)
            modelBuilder.Entity<Programs>().HasMany(p => p.GetPhotos).WithOne(p => p.GetPrograms);
            //(Program-Donate)
            modelBuilder.Entity<Programs>().HasMany(p => p.GetDonates).WithOne(p => p.GetPrograms);
            #endregion

            //Aply seedders
            AppSeeders.CallSeeder(modelBuilder);

        }
    }
}
