using System;
using LMSModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace LMSDataAccessLayer.Contexts
{
    public class LibraryContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=LibraryManagementSystem;Username=postgress;Password=hareshwaran");
        }
        public DbSet<Book> Book {get; set;}
        public DbSet<Borrow> Borrow {get; set;}
        public DbSet<Copy> Copy {get; set;}
        public DbSet<Fine> Fine {get; set;}
        public DbSet<Member> Member {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(b =>
            {
                b.HasKey(b => b.book_Id).HasName("PK_Book");
            });

            modelBuilder.Entity<Copy>(c =>
            {
                c.HasKey(c => c.copyId).HasName("PK_Copy");
                c.HasOne(c => c.book_Id)
                .WithMany(b => )
            });
        }
    }
}