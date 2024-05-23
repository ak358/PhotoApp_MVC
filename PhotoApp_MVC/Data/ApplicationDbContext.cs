using Microsoft.EntityFrameworkCore;
using PhotoApp_MVC.Models;
using PhotoApp_MVC.ViewModels;
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

        modelBuilder.Entity<User>()
        .HasIndex(u => u.EmailAdress)
        .IsUnique();

        // Add roles
        modelBuilder.Entity<Role>().HasData(
            new Role { Id = 1, Name = "Admin" },
            new Role { Id = 2, Name = "User" }
        );

        // Add users
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Name = "AdminUser", EmailAdress = "admin@example.com", Password = "password", RoleId = 1 },
            new User { Id = 2, Name = "RegularUser", EmailAdress = "user@example.com", Password = "password", RoleId = 2 }
        );

        // Add categories
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "アナウンサー", UserId = 1 },
            new Category { Id = 2, Name = "北海道", UserId = 2 }
        );

        modelBuilder.Entity<PhotoPost>().HasData(
                new PhotoPost
                {
                    Id = 1,
                    Title = "シマエナガさん",
                    Description = "冬の北海道でよく見られるちいさな鳥です。" +
                    "「雪の妖精」と呼ばれています。",
                    ImageUrl = "images/bird_shimaenaga.png",
                    CategoryId = 2,
                    UserId = 1,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new PhotoPost
                {
                    Id = 2,
                    Title = "ペンギンのアナウンサー",
                    Description = "朝のニュースをお伝えします。",
                    ImageUrl = "images/animal_chara_radio_penguin.png",
                    CategoryId = 1,
                    UserId = 2,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                }

            );

    }

public DbSet<Contact> Contact { get; set; } = default!;
}