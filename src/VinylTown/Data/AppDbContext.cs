﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using VinylTown.Domain;

namespace VinylTown.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{ }

	public DbSet<Product> Products { get; set; }
	public DbSet<ProductAuthor> ProductAuthors { get; set; }
	public DbSet<ProductGenre> ProductGenres { get; set; }
	public DbSet<CostumerCart> CostumerCarts { get; set; }
	public DbSet<CartItem> CartItems { get; set; }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);
		builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}
}

