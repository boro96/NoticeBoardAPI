using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoticeBoardAPI.Entities
{
    public class NoticeBoardDbContext : DbContext
    {
        public NoticeBoardDbContext(DbContextOptions options): base(options)
        {

        }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Advert> Adverts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Relationship
            modelBuilder.Entity<Address>()
                .HasMany(a => a.Users)
                .WithOne(b => b.Address)
                .HasForeignKey(foreignKey => foreignKey.AddressId);

            modelBuilder.Entity<Advert>()
                .HasOne(a => a.Category)
                .WithMany(b => b.Adverts)
                .HasForeignKey(foreignKey => foreignKey.CategoryId);

            modelBuilder.Entity<User>()
                .HasMany(a => a.Adverts)
                .WithOne(b => b.User)
                .HasForeignKey(foreignKey => foreignKey.UserId);

            modelBuilder.Entity<User>()
                .HasMany(a => a.Comments)
                .WithOne(b => b.User)
                .HasForeignKey(foreignKey => foreignKey.UserId)
                .OnDelete(DeleteBehavior.Restrict);
                

            modelBuilder.Entity<Comment>()
                .HasOne(a => a.Advert)
                .WithMany(b => b.Comments)
                .HasForeignKey(foreignKey => foreignKey.AdvertId);

            //Model Configuration
            modelBuilder.Entity<Address>(address =>
            {
                address.Property(a => a.City).HasMaxLength(150).IsRequired();
                address.Property(b => b.Street).HasMaxLength(100).IsRequired();
                address.Property(c => c.ApartamentNumber).HasMaxLength(10).IsRequired();
                address.Property(d => d.PostalCode).IsRequired();
            });
            modelBuilder.Entity<User>(user =>
            {
                user.Property(a => a.Email).IsRequired();
                user.Property(b => b.FirstName).HasColumnType("varchar(150)").IsRequired();
                user.Property(c => c.LastName).HasMaxLength(200).IsRequired();
                user.Property(d => d.Nationality).HasMaxLength(150).IsRequired();
            });
            modelBuilder.Entity<Category>(category =>
            {
                category.Property(a => a.Name).IsRequired();
            });
            modelBuilder.Entity<Advert>(advert =>
            {
                advert.Property(a => a.Description).HasColumnType("varchar(500)").IsRequired();
                advert.Property(b => b.PublicationDate).HasPrecision(3);
                advert.Property(c => c.PublicationDate).HasDefaultValueSql("getdate()");
            });
            modelBuilder.Entity<Comment>(comment =>
            {
                comment.Property(a => a.Message).HasMaxLength(600).IsRequired();
                comment.Property(b => b.PublicationDate)
                .HasPrecision(3)
                .HasDefaultValueSql("getdate()");
                
            });
        }
    }
}
