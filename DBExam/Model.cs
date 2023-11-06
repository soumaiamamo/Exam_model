using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

class ExamContext : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>()
            .HasOne(_ => _.MainCategory)
            .WithMany()
            .HasForeignKey(_ => _.ID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Order>().HasKey(_ => new { _.CustomerID, _.FoodItemID });

        modelBuilder.Entity<Order>()
            .HasOne(_ => _.FoodItem)
            .WithMany()
            .HasForeignKey(_ => _.FoodItemID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Order>()
            .HasOne(_ => _.Customer)
            .WithMany()
            .HasForeignKey(_ => _.CustomerID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Order>()
            .HasOne(_ => _.Employee)
            .WithMany()
            .HasForeignKey(_ => _.EmployeeID)
            .OnDelete(DeleteBehavior.Cascade);
    }

    public DbSet<FoodItem> FoodItems { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<MainCategory> MainCategories { get; set; } = null!;
    public DbSet<Employee> Employees { get; set; } = null!;
}

class FoodItem
{
    public int ID { get; set; }  // Convention
    public string Name { get; set; } = null!;
    public Category? Category { get; set; }
    public int CategoryID { get; set; }
    public decimal Price { get; set; }
    public string? Unit { get; set; }
    public MainCategory? MainCategory { get; set; }

    // Constructors
    public FoodItem(int iD, string name, int categoryID, decimal price)
    {
        ID = iD;
        Name = name;
        CategoryID = categoryID;
        Price = price;
    }
    public FoodItem(int iD, string name, int categoryID, decimal price, string unit)
        : this(iD, name, categoryID, price) => Unit = unit;
}

class Customer
{
    public int ID { get; set; }   // Convention
    public string? Name { get; set; }
    public DateTime DateTime { get; set; }
    public int? TableNumber { get; set; }
}

class Order
{
    public Customer Customer { get; set; } = null!;
    public int CustomerID { get; set; }

    public FoodItem? FoodItem { get; set; }
    public int FoodItemID { get; set; }

    public int Quantity { get; set; }

    public Employee? Employee { get; set; }
    public int EmployeeID { get; set; }
}

class MainCategory
{
    public int ID { get; set; }
    public string Name { get; set; } = null!;
}

class Employee
{
    public int ID { get; set; }

    [MaxLength(50)]
    public string Name { get; set; } = null!;
}
