﻿using System;
using System.Collections.Generic;
using System.Text;
using CarWashPOI.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarWashPOI.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<WashLocation> WashLocations { get; set; }
        public DbSet<LatLon> Coordinates { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<UserRating> UsersRatings { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserRating>()
                .HasKey(x => new { x.UserId, x.RatingId });

            builder.Entity<UserRating>()
                .HasOne(ur => ur.Rating)
                .WithMany(u => u.RatingUsers)
                .HasForeignKey(ur => ur.RatingId);

            builder.Entity<UserRating>()
                .HasOne(ur => ur.User)
                .WithMany(r => r.UserRatings)
                .HasForeignKey(ur => ur.UserId);

            base.OnModelCreating(builder);
        }
    }
}
