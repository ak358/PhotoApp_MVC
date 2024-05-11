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

    }

}