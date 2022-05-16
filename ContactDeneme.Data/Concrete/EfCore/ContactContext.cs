using ContactDeneme.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactDeneme.Data.Concrete
{
    public partial class ContactContext : DbContext
    {
        public ContactContext(DbContextOptions options) : base(options)
        {

        }

        public virtual DbSet<Contact> Contacts { get; set; } = null!;

        public virtual DbSet<ContactInfo> ContactInfos { get; set; } = null!;

        public virtual DbSet<Region> Regions { get; set; } = null!;


     

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>(entity =>
            {
                entity.HasOne(d => d.Region)
                .WithMany(p => p.Contacts)
                .HasForeignKey(d => d.RegionId)
                .OnDelete(DeleteBehavior.ClientSetNull);
              

            });


            modelBuilder.Entity<ContactInfo>(entity =>
            {
                entity.HasKey(e => e.InfoId);

                entity.Property(e => e.Telephone).IsFixedLength();

                entity.HasOne(d => d.Contact)
                .WithOne(p => p.ContactInfo)
                .HasForeignKey<ContactInfo>(d => d.ContactId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            });


            modelBuilder.Entity<Region>(entity =>
            {
                entity.Property(e=> e.RegionId).ValueGeneratedNever();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
