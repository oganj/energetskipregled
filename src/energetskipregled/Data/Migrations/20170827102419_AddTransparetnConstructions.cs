using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EnergetskiPregled.Data.Migrations
{
    public partial class AddTransparetnConstructions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBEFrameCategorys",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsArchived = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBEFrameCategorys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBEHeatCorrectionFactors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PsiG = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBEHeatCorrectionFactors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBEMaterials",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Ug = table.Column<float>(nullable: false),
                    g = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBEMaterials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBEFrames",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Uf = table.Column<float>(nullable: false),
                    g = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBEFrames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBEFrames_TBEFrameCategorys_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "TBEFrameCategorys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TBEs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Af = table.Column<float>(nullable: false),
                    Ag = table.Column<float>(nullable: false),
                    Height = table.Column<float>(nullable: false),
                    Lg = table.Column<float>(nullable: false),
                    POS = table.Column<string>(nullable: true),
                    TBEFrameId = table.Column<int>(nullable: false),
                    TBEHeatCorrectionFactorId = table.Column<int>(nullable: false),
                    TBEMaterialId = table.Column<int>(nullable: false),
                    Width = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBEs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBEs_TBEFrames_TBEFrameId",
                        column: x => x.TBEFrameId,
                        principalTable: "TBEFrames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBEs_TBEHeatCorrectionFactors_TBEHeatCorrectionFactorId",
                        column: x => x.TBEHeatCorrectionFactorId,
                        principalTable: "TBEHeatCorrectionFactors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBEs_TBEMaterials_TBEMaterialId",
                        column: x => x.TBEMaterialId,
                        principalTable: "TBEMaterials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBEs_TBEFrameId",
                table: "TBEs",
                column: "TBEFrameId");

            migrationBuilder.CreateIndex(
                name: "IX_TBEs_TBEHeatCorrectionFactorId",
                table: "TBEs",
                column: "TBEHeatCorrectionFactorId");

            migrationBuilder.CreateIndex(
                name: "IX_TBEs_TBEMaterialId",
                table: "TBEs",
                column: "TBEMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_TBEFrames_CategoryId",
                table: "TBEFrames",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBEs");

            migrationBuilder.DropTable(
                name: "TBEFrames");

            migrationBuilder.DropTable(
                name: "TBEHeatCorrectionFactors");

            migrationBuilder.DropTable(
                name: "TBEMaterials");

            migrationBuilder.DropTable(
                name: "TBEFrameCategorys");
        }
    }
}
