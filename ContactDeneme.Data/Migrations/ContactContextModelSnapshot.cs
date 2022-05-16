﻿// <auto-generated />
using ContactDeneme.Data.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ContactDeneme.Data.Migrations
{
    [DbContext(typeof(ContactContext))]
    partial class ContactContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ContactDeneme.Entity.Contact", b =>
                {
                    b.Property<int>("ContactId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ContactId"), 1L, 1);

                    b.Property<string>("Company")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("RegionId")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ContactId");

                    b.HasIndex("RegionId");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("ContactDeneme.Entity.ContactInfo", b =>
                {
                    b.Property<int>("InfoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InfoId"), 1L, 1);

                    b.Property<int>("ContactId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Telephone")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("char(10)")
                        .IsFixedLength();

                    b.HasKey("InfoId");

                    b.HasIndex("ContactId")
                        .IsUnique();

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.HasIndex("Telephone")
                        .IsUnique()
                        .HasFilter("[Telephone] IS NOT NULL");

                    b.ToTable("ContactInfos");
                });

            modelBuilder.Entity("ContactDeneme.Entity.Region", b =>
                {
                    b.Property<int>("RegionId")
                        .HasColumnType("int");

                    b.Property<string>("RegionName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("RegionId");

                    b.HasIndex("RegionName")
                        .IsUnique();

                    b.ToTable("Regions");
                });

            modelBuilder.Entity("ContactDeneme.Entity.Contact", b =>
                {
                    b.HasOne("ContactDeneme.Entity.Region", "Region")
                        .WithMany("Contacts")
                        .HasForeignKey("RegionId")
                        .IsRequired();

                    b.Navigation("Region");
                });

            modelBuilder.Entity("ContactDeneme.Entity.ContactInfo", b =>
                {
                    b.HasOne("ContactDeneme.Entity.Contact", "Contact")
                        .WithOne("ContactInfo")
                        .HasForeignKey("ContactDeneme.Entity.ContactInfo", "ContactId")
                        .IsRequired();

                    b.Navigation("Contact");
                });

            modelBuilder.Entity("ContactDeneme.Entity.Contact", b =>
                {
                    b.Navigation("ContactInfo")
                        .IsRequired();
                });

            modelBuilder.Entity("ContactDeneme.Entity.Region", b =>
                {
                    b.Navigation("Contacts");
                });
#pragma warning restore 612, 618
        }
    }
}