using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EnergetskiPregled.Data.Migrations
{
    public partial class AddTrasparentBuildingElemets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBEs_TBEFrames_TBEFrameId",
                table: "TBEs");

            migrationBuilder.DropForeignKey(
                name: "FK_TBEs_TBEHeatCorrectionFactors_TBEHeatCorrectionFactorId",
                table: "TBEs");

            migrationBuilder.DropForeignKey(
                name: "FK_TBEs_TBEMaterials_TBEMaterialId",
                table: "TBEs");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TBEMaterials",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(int),
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "TBEHeatCorrectionFactors",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "TBEMaterialId",
                table: "TBEs",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "TBEHeatCorrectionFactorId",
                table: "TBEs",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "TBEFrameId",
                table: "TBEs",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_TBEs_TBEFrames_TBEFrameId",
                table: "TBEs",
                column: "TBEFrameId",
                principalTable: "TBEFrames",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_TBEs_TBEHeatCorrectionFactors_TBEHeatCorrectionFactorId",
                table: "TBEs",
                column: "TBEHeatCorrectionFactorId",
                principalTable: "TBEHeatCorrectionFactors",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_TBEs_TBEMaterials_TBEMaterialId",
                table: "TBEs",
                column: "TBEMaterialId",
                principalTable: "TBEMaterials",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBEs_TBEFrames_TBEFrameId",
                table: "TBEs");

            migrationBuilder.DropForeignKey(
                name: "FK_TBEs_TBEHeatCorrectionFactors_TBEHeatCorrectionFactorId",
                table: "TBEs");

            migrationBuilder.DropForeignKey(
                name: "FK_TBEs_TBEMaterials_TBEMaterialId",
                table: "TBEs");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "TBEHeatCorrectionFactors");

            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "TBEMaterials",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<int>(
                name: "TBEMaterialId",
                table: "TBEs",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TBEHeatCorrectionFactorId",
                table: "TBEs",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TBEFrameId",
                table: "TBEs",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TBEs_TBEFrames_TBEFrameId",
                table: "TBEs",
                column: "TBEFrameId",
                principalTable: "TBEFrames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TBEs_TBEHeatCorrectionFactors_TBEHeatCorrectionFactorId",
                table: "TBEs",
                column: "TBEHeatCorrectionFactorId",
                principalTable: "TBEHeatCorrectionFactors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TBEs_TBEMaterials_TBEMaterialId",
                table: "TBEs",
                column: "TBEMaterialId",
                principalTable: "TBEMaterials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
