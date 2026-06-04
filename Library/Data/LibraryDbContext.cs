using Library.Repositories;
using Library.Services;
using Microsoft.EntityFrameworkCore;
using Library.Models;

namespace LibraryContext
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Copy> Copies { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Borrow> Borrows { get; set; }
        public DbSet<Fine> Fines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>()
                .HasKey(b => b.book_Id);

            modelBuilder.Entity<Book>()
                .HasMany(b => b.Copies)
                .WithOne(c => c.Book)
                .HasForeignKey(c => c.book_Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Copy>()
                .HasKey(c => c.copyId);

            modelBuilder.Entity<Copy>()
                .HasMany(c => c.Borrows)
                .WithOne(b => b.Copy)
                .HasForeignKey(b => b.book_copyId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Member>()
                .HasKey(m => m.member_id);

            modelBuilder.Entity<Member>()
                .HasMany(m => m.Borrows)
                .WithOne(b => b.Member)
                .HasForeignKey(b => b.member_id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Borrow>()
                .HasKey(b => b.borrow_id);

            modelBuilder.Entity<Borrow>()
                .HasOne(b => b.Copy)
                .WithMany(c => c.Borrows)
                .HasForeignKey(b => b.book_copyId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Borrow>()
                .HasOne(b => b.Member)
                .WithMany(m => m.Borrows)
                .HasForeignKey(b => b.member_id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Fine>()
                .HasKey(f => f.Borrow_id);

            modelBuilder.Entity<Fine>()
                .HasOne<Borrow>()
                .WithOne()
                .HasForeignKey<Fine>(f => f.Borrow_id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}