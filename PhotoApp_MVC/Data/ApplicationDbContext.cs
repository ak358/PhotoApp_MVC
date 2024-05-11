using Microsoft.EntityFrameworkCore;
using PhotoApp_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<PhotoPost> PhotoPosts { get; set; }
    public DbSet<Role> Roles { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PhotoPost>()
            .HasOne(p => p.User)
            .WithMany(u => u.PhotoPosts)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<PhotoPost>()
            .HasOne(p => p.Category)
            .WithMany(c => c.PhotoPosts)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Category>()
            .HasOne(c => c.User)
            .WithMany(u => u.Categories)
            .OnDelete(DeleteBehavior.Restrict);

            // Add roles
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin" },
                new Role { Id = 2, Name = "User" }
            );

            // Add users
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "AdminUser", EmailAdress = "admin@example.com", Password = "adminpassword", RoleId = 1 },
                new User { Id = 2, Name = "RegularUser", EmailAdress = "user@example.com", Password = "userpassword", RoleId = 2 }
            );

            // Add categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Category 1", UserId = 1 },
                new Category { Id = 2, Name = "Category 2", UserId = 2 }
            );

            // Add photo posts
            modelBuilder.Entity<PhotoPost>().HasData(
                new PhotoPost { Id = 1, Title = "Post 1", CategoryId = 1, UserId = 1 },
                new PhotoPost { Id = 2, Title = "Post 2", CategoryId = 2, UserId = 2 }
            );
    }

}