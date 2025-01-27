﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;

namespace Restaurants.Infrastructure.Persistance;

public class RestaurantsDbContext : IdentityDbContext<User>
{
    public RestaurantsDbContext(DbContextOptions<RestaurantsDbContext> options) : base (options){ }
    internal DbSet<Restaurant> Restaurants { get; set; }
    internal DbSet<Dish> Dishes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Restaurant>()
            .OwnsOne(a => a.Address);

        modelBuilder.Entity<Restaurant>()
            .HasMany(d => d.Dishes)
            .WithOne()
            .HasForeignKey(f => f.RestaurantId);

        modelBuilder.Entity<User>()
            .HasMany(o => o.OwnedRestaurants)
            .WithOne(r => r.Owner)
            .HasForeignKey(r => r.OwnerId);
    }
}
