using GraphQL.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Username).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            });

            // Employee configuration
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.EmployeeCode).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.IsActive).HasDefaultValue(true);

                // Configure the relationship
                entity.HasOne(e => e.Manager)
                    .WithMany(u => u.Employees)
                    .HasForeignKey(e => e.ManagerId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Seed data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Users
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "john.doe",
                    Email = "john.doe@company.com",
                    FirstName = "John",
                    LastName = "Doe",
                    FullName = "John Doe",
                    Address = "123 Main St, New York, NY",
                    ContactNumber = "+1-555-0101",
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "System"
                },
                new User
                {
                    Id = 2,
                    Username = "jane.smith",
                    Email = "jane.smith@company.com",
                    FirstName = "Jane",
                    LastName = "Smith",
                    FullName = "Jane Smith",
                    Address = "456 Oak Ave, Los Angeles, CA",
                    ContactNumber = "+1-555-0102",
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "System"
                }
            );

            // Seed Employees
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    EmployeeCode = "EMP001",
                    FirstName = "Alice",
                    LastName = "Johnson",
                    FullName = "Alice Johnson",
                    Email = "alice.johnson@company.com",
                    Department = "Engineering",
                    Position = "Senior Developer",
                    Salary = 95000,
                    HireDate = new DateTime(2022, 1, 15),
                    ContactNumber = "+1-555-0201",
                    Address = "789 Pine Rd, San Francisco, CA",
                    IsActive = true,
                    ManagerId = 1,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "System"
                },
                new Employee
                {
                    Id = 2,
                    EmployeeCode = "EMP002",
                    FirstName = "Bob",
                    LastName = "Wilson",
                    FullName = "Bob Wilson",
                    Email = "bob.wilson@company.com",
                    Department = "Engineering",
                    Position = "Junior Developer",
                    Salary = 65000,
                    HireDate = new DateTime(2023, 3, 20),
                    ContactNumber = "+1-555-0202",
                    Address = "321 Elm St, Seattle, WA",
                    IsActive = true,
                    ManagerId = 1,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "System"
                },
                new Employee
                {
                    Id = 3,
                    EmployeeCode = "EMP003",
                    FirstName = "Carol",
                    LastName = "Martinez",
                    FullName = "Carol Martinez",
                    Email = "carol.martinez@company.com",
                    Department = "Marketing",
                    Position = "Marketing Manager",
                    Salary = 85000,
                    HireDate = new DateTime(2021, 6, 10),
                    ContactNumber = "+1-555-0203",
                    Address = "654 Maple Dr, Boston, MA",
                    IsActive = true,
                    ManagerId = 2,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "System"
                }
            );
        }
    }
}
