﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Aula.Migrations
{
    public partial class PedidoEProdutoObrigatorios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItensPedido_Pedido_PedidoId",
                table: "ItensPedido");

            migrationBuilder.DropForeignKey(
                name: "FK_ItensPedido_Produtos_ProdutoId",
                table: "ItensPedido");

            migrationBuilder.AlterColumn<int>(
                name: "ProdutoId",
                table: "ItensPedido",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PedidoId",
                table: "ItensPedido",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ItensPedido_Pedido_PedidoId",
                table: "ItensPedido",
                column: "PedidoId",
                principalTable: "Pedido",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItensPedido_Produtos_ProdutoId",
                table: "ItensPedido",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItensPedido_Pedido_PedidoId",
                table: "ItensPedido");

            migrationBuilder.DropForeignKey(
                name: "FK_ItensPedido_Produtos_ProdutoId",
                table: "ItensPedido");

            migrationBuilder.AlterColumn<int>(
                name: "ProdutoId",
                table: "ItensPedido",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "PedidoId",
                table: "ItensPedido",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_ItensPedido_Pedido_PedidoId",
                table: "ItensPedido",
                column: "PedidoId",
                principalTable: "Pedido",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ItensPedido_Produtos_ProdutoId",
                table: "ItensPedido",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
