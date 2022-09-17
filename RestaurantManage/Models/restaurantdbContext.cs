using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RestaurantManage.ModelView;

#nullable disable

namespace RestaurantManage.Models
{
    public partial class restaurantdbContext : DbContext
    {
        public restaurantdbContext()
        {
        }

        public restaurantdbContext(DbContextOptions<restaurantdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Restaurant> Restaurants { get; set; }
        public virtual DbSet<Restaurantmenu> Restaurantmenus { get; set; }
        public virtual DbSet<Restaurantmenucustomer> Restaurantmenucustomers { get; set; }
        public virtual DbSet<CSVReport> CSVReports { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("Server=localhost;port=3306;user=root;password=1475963;database=restaurantdb;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CSVReport>(entity =>
            {
                entity.ToTable("ReportCSV");
                entity.HasNoKey();
                entity.Property(e => e.RestaurantName).HasColumnType("varchar(255)");
                entity.Property(e => e.TheBestSellingMeal).HasColumnType("varchar(255)");
                entity.Property(e => e.MostPurchasedCustomer).HasColumnType("varchar(255)");
                entity.Property(e => e.NumberOfOrderedCustomer).HasColumnType("long");
                entity.Property(e => e.ProfitInNis).HasColumnType("float");
                entity.Property(e => e.ProfitInUsd).HasColumnType("float");



            });
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customer");

                entity.HasIndex(e => e.CustomerId, "Id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Archived).HasColumnType("tinyint");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Restaurant>(entity =>
            {
                entity.ToTable("restaurant");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Archived).HasColumnType("tinyint");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Restaurantmenu>(entity =>
            {
                entity.ToTable("restaurantmenu");

                entity.HasIndex(e => e.RestaurantmenuId, "Id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.RestaurantId, "Rest_restaurantmenu_idx");

                entity.Property(e => e.RestaurantmenuId).HasColumnName("restaurantmenuId");

                entity.Property(e => e.Archived).HasColumnType("tinyint");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.RestaurantId).HasColumnName("restaurantId");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.Restaurantmenus)
                    .HasForeignKey(d => d.RestaurantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Rest_restaurantmenu");
            });

            modelBuilder.Entity<Restaurantmenucustomer>(entity =>
            {
                entity.ToTable("restaurantmenucustomer");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.CustomerId, "customer_RMC_idx");

                entity.HasIndex(e => e.RestaurantmenuId, "restaurantmenucustomerId_idx");

                entity.Property(e => e.RestaurantmenuId).HasColumnName("restaurantmenuId");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Restaurantmenucustomers)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("customer_RMC");

                entity.HasOne(d => d.Restaurantmenu)
                    .WithMany(p => p.Restaurantmenucustomers)
                    .HasForeignKey(d => d.RestaurantmenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Restaurantmenu_RMC");
            });
            modelBuilder.Entity<Customer>().HasQueryFilter(a => a.Archived);
            modelBuilder.Entity<Restaurant>().HasQueryFilter(a => a.Archived);
            modelBuilder.Entity<Restaurantmenu>().HasQueryFilter(a => a.Archived);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
