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
		public DbSet<Project> Projects { get; set; }
		public DbSet<MaterialCategory> MaterialCategorys { get; set; }
		public DbSet<Material> Materials { get; set; }
		public DbSet<MaterialThickness> MaterialThicknesses { get; set; }
		public DbSet<NonTrasparentBuildingElemet> NonTrasparentBuildingElemets { get; set; }
		public DbSet<TBEFrameCategory> TBEFrameCategorys { get; set; }
		public DbSet<TBEFrame> TBEFrames { get; set; }
		public DbSet<TBEMaterial> TBEMaterials { get; set; }
		public DbSet<TBEHeatCorrectionFactor> TBEHeatCorrectionFactors { get; set; }
		public DbSet<TBE> TBEs { get; set; }

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

			//TBEMaterial
			builder.Entity<TBEMaterial>().ToTable("TBEMaterials");
			builder.Entity<TBEMaterial>().Property(x => x.Name)
				.IsRequired(true)
				.HasMaxLength(100);
			builder.Entity<TBEMaterial>().Property(x => x.Ug)
				.IsRequired(true);
			builder.Entity<TBEMaterial>().Property(x => x.g)
				.IsRequired(true);

			//TBEHeatCorrectionFactor
			builder.Entity<TBEHeatCorrectionFactor>().ToTable("TBEHeatCorrectionFactors");
			builder.Entity<TBEHeatCorrectionFactor>().Property(x => x.Name)
				.IsRequired(true)
				.HasMaxLength(100);
			builder.Entity<TBEHeatCorrectionFactor>().Property(x => x.PsiG)
				.IsRequired(true);


			//TBEFrameCategory
			builder.Entity<TBEFrameCategory>().ToTable("TBEFrameCategorys");
			builder.Entity<TBEFrameCategory>().Property(x => x.Name)
				.IsRequired(true)
				.HasMaxLength(100);
			builder.Entity<TBEFrameCategory>().Property(x => x.IsArchived)
				.IsRequired(true);


			//TBEFrame
			builder.Entity<TBEFrame>().ToTable("TBEFrames");
			builder.Entity<TBEFrame>().Property(x => x.Name)
				.IsRequired(true)
				.HasMaxLength(100);
			builder.Entity<TBEFrame>()
				.HasOne(x => x.Category)
				.WithMany()
				.HasForeignKey(x => x.CategoryId)
				.IsRequired(true)
				.OnDelete(DeleteBehavior.Restrict);
			builder.Entity<TBEFrame>().Property(x => x.Uf)
				.IsRequired(true);
			builder.Entity<TBEFrame>().Property(x => x.g)
				.IsRequired(true);


			//TBE
			builder.Entity<TBE>().ToTable("TBEs");
			builder.Entity<TBE>()
				.HasOne(x => x.TBEMaterial)
				.WithMany()
				.HasForeignKey(x => x.TBEMaterialId)
				.OnDelete(DeleteBehavior.SetNull);
			builder.Entity<TBE>()
				.HasOne(x => x.TBEFrame)
				.WithMany()
				.HasForeignKey(x => x.TBEFrameId)
				.OnDelete(DeleteBehavior.SetNull);
			builder.Entity<TBE>()
				.HasOne(x => x.TBEHeatCorrectionFactor)
				.WithMany()
				.HasForeignKey(x => x.TBEHeatCorrectionFactorId)
				.OnDelete(DeleteBehavior.SetNull);
	}
	}
}
