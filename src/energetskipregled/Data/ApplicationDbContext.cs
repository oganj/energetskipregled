using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EnergetskiPregled.Models;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EnergetskiPregled.Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public DbSet<Player> Players { get; set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			// Customize the ASP.NET Identity model and override the defaults if needed.
			// For example, you can rename the ASP.NET Identity table names and more.
			// Add your customizations after calling base.OnModelCreating(builder);
			
			builder.Entity<Player>().ToTable("Players");

			//Player -> User association table
			builder.Entity<Player>()
				.HasOne(x => x.LastModifiedBy)
				.WithMany()
				.HasForeignKey(x => x.LastModifiedById)
				.IsRequired(true)
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<Player>()
				.HasOne(x => x.CreatedBy)
				.WithMany()
				.HasForeignKey(x => x.CreatedById)
				.IsRequired(true)
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<Player>()
				.HasOne(x => x.User)
				.WithMany(x => x.Players)
				.HasForeignKey(x => x.UserId)
				.IsRequired(true)
				.OnDelete(DeleteBehavior.Restrict);

			//Player properties
			builder.Entity<Player>()
				.Property(x => x.Name)
				.HasMaxLength(100);

			builder.Entity<Player>()
				.Property(x => x.Name)
				.IsRequired(true);

			//MaterialCategory
			builder.Entity<MaterialCategory>().ToTable("MaterialCategories");
			builder.Entity<MaterialCategory>().Property(x => x.Name)
				.IsRequired(true)
				.HasMaxLength(100);

			//Material
			builder.Entity<Material>().ToTable("Materials");
			builder.Entity<Material>().Property(x => x.Name)
				.IsRequired(true)
				.HasMaxLength(100);
			builder.Entity<Material>()
				.HasOne(x => x.Category)
				.WithMany()
				.HasForeignKey(x => x.CategoryId)
				.IsRequired(true)
				.OnDelete(DeleteBehavior.Restrict);

			//Project
			builder.Entity<Project>().ToTable("Projects");
			builder.Entity<Project>().Property(x => x.Name)
				.IsRequired(true)
				.HasMaxLength(100);
			builder.Entity<Project>()
				.HasOne(x => x.User)
				.WithMany(x => x.Projects)
				.HasForeignKey(x => x.UserId)
				.IsRequired(true)
				.OnDelete(DeleteBehavior.Restrict);
			builder.Entity<Project>().Property(x => x.LastModifiedAt)
				.IsConcurrencyToken();
			

			//MaterialThickness
			builder.Entity<MaterialThickness>().ToTable("MaterialThicknesses");
			builder.Entity<MaterialThickness>()
				.HasOne(x => x.BuildingElement)
				.WithMany(x => x.MaterialsUsed)
				.HasForeignKey(x => x.BuildingElementId)
				.IsRequired(true);
			builder.Entity<MaterialThickness>()
				.HasOne(x => x.Material)
				.WithMany(x => x.BuildingElements)
				.HasForeignKey(x => x.MaterialId);
			builder.Entity<MaterialThickness>()
				.HasOne(x => x.Material);

			//NonTrasparentBuildingElemet
			builder.Entity<NonTrasparentBuildingElemet>().ToTable("NonTrasparentBuildingElemets");
			builder.Entity<NonTrasparentBuildingElemet>().Property(x => x.Name)
				.IsRequired(true)
				.HasMaxLength(100);
			builder.Entity<NonTrasparentBuildingElemet>().Property(x => x.Code)
				.IsRequired(true)
				.HasMaxLength(100);
			builder.Entity<NonTrasparentBuildingElemet>().Property(x => x.LastModifiedAt)
				.IsConcurrencyToken();
			builder.Entity<NonTrasparentBuildingElemet>()
				.HasOne(x => x.Project)
				.WithMany(x => x.NonTransparentBuildingElements)
				.HasForeignKey(x => x.ProjectId);
		}
	}
}
