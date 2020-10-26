using System;
using System.Collections.Generic;
using System.Text;
using FITorg.Data.EntityModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FITorg.Data
{
    
    public class MyContext : IdentityDbContext<AppUser,AppRole,int>
    {
        public MyContext(DbContextOptions<MyContext> x) : base(x)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }


        public DbSet<Trener> Trener { get; set; }
        public DbSet<Sportas> Sportas { get; set; }
        public DbSet<Trening> Trening { get; set; }
        public DbSet<Vjezba> Vjezba { get; set; }
        public DbSet<TreningVjezba> TreningVjezba { get; set; }
        public DbSet<Drzava> Drzava { get; set; }
        public DbSet<Grad> Grad { get; set; }
        public DbSet<Spol> Spol { get; set; }
        public DbSet<Administrator> Administrator { get; set; }
        public DbSet<Napredak> Napredak { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
