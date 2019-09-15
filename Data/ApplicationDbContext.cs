using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp1.Models;

namespace WebApp1.Data
{
    public class ApplicationDbContext : IdentityDbContext<UserModel>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UserModel>(b =>
            {
                // Primary key
                b.HasKey(u => u.Id);

                // Indexes for "normalized" username and email, to allow efficient lookups
                b.HasIndex(u => u.NormalizedUserName).HasName("UserNameIndex").IsUnique();
                b.HasIndex(u => u.NormalizedEmail).HasName("EmailIndex");

                // Maps to the AspNetUsers table
                b.ToTable("AspNetUsers");

                // A concurrency token for use with the optimistic concurrency checking
                b.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

                // Limit the size of columns to use efficient database types
                b.Property(u => u.UserName).HasMaxLength(256);
                b.Property(u => u.NormalizedUserName).HasMaxLength(256);
                b.Property(u => u.Email).HasMaxLength(256);
                b.Property(u => u.NormalizedEmail).HasMaxLength(256);
                b.Property(u => u.RememberMe).HasDefaultValue(false);
                // Each User can have many UserTokens
                b.HasMany<TUserToken>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();

                b.HasOne<ContactsModel>().WithMany(p => p.Contacts).IsRequired();
                b.HasMany<MessageModel>().WithOne(p => p.SenderID).HasForeignKey(fk => fk.ID).IsRequired();
                b.HasMany<CommentsModel>().WithOne().HasForeignKey(fk => fk.ID).IsRequired();

                b.ToTable("UsersTable");
            });

            builder.Entity<ContactsModel>(b =>
            {
                b.HasKey(k => k.ContactId);
                b.Property(u => u.Name).HasMaxLength(256);
                b.Property(u => u.Email).HasMaxLength(256);
               // b.HasOne(p => p.ContactId).WithOne(c => c.Contacts);
                b.HasMany<UserModel>(u => u.Contacts).WithOne(c => c.Contacts).HasForeignKey(fk => fk.Id);
                b.ToTable("ContactsTable");
            });

            builder.Entity<MessageModel>(b =>
            {
                b.HasKey(k => k.ID);
                b.Property(u => u.Message);
                b.Property(u => u.IsSeen).HasDefaultValue(false);
                b.Property(u => u.Date).HasDefaultValue(DateTime.Now);
                b.HasOne(u => u.SenderID).WithOne().HasForeignKey<UserModel>(fk => fk.Id);
                b.ToTable("MessagesTable");
            });

            builder.Entity<CommentsModel>(b =>
            {
                b.HasKey(k => k.ID);
                b.Property(u => u.Comment);
                b.Property(u => u.Date).HasDefaultValue(DateTime.Now);
                b.HasOne(u => u.CommenterID).WithOne().HasForeignKey<UserModel>(fk => fk.Id);
                b.ToTable("CommentsTable");
            });
        }
        public DbSet<ContactsModel> Contacts { get; set; }
        public DbSet<MessageModel> Messages { get; set; }
        public DbSet<CommentsModel> Comments { get; set; }        
    }
}


