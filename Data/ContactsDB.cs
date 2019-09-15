using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp1.Models;

namespace WebApp1.Data
{
    public class ContactsDBContext : DbContext
    {
        public ContactsDBContext(DbContextOptions<ContactsDBContext> options)
            : base(options)
        { }
            protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContactsModel>()
                .HasKey(c => c.ContactId);
        }
    

        public DbSet<ContactsModel> Contacts { get; set; }
        public DbSet<MessageModel> Messages { get; set; }
    }
}
