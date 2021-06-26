using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StandWeb.Data.Migrations
{
    public partial class ImportacaoProjeto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataRegisto",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Morada = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    CodPostal = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Telemovel = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Marcas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marcas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Carros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Modelo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ano = table.Column<int>(type: "int", nullable: false),
                    Preco = table.Column<int>(type: "int", nullable: false),
                    Cilindrada = table.Column<int>(type: "int", nullable: false),
                    Potencia = table.Column<int>(type: "int", nullable: false),
                    Combustivel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MarcaFK = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carros_Marcas_MarcaFK",
                        column: x => x.MarcaFK,
                        principalTable: "Marcas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fotografias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fotografia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataFoto = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Local = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarroFK = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fotografias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fotografias_Carros_CarroFK",
                        column: x => x.CarroFK,
                        principalTable: "Carros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Gostos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataCompra = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CarroFK = table.Column<int>(type: "int", nullable: false),
                    ClienteFK = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gostos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gostos_Carros_CarroFK",
                        column: x => x.CarroFK,
                        principalTable: "Carros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Gostos_Clientes_ClienteFK",
                        column: x => x.ClienteFK,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "CodPostal", "Email", "Morada", "Nome", "Telemovel", "UserName" },
                values: new object[,]
                {
                    { 1, "2305 - 515 PAIALVO", "Marisa.Freitas@iol.pt", "Largo do Pelourinho", "Marisa Vieira", "967197885", null },
                    { 9, "2300 - 390 TOMAR", "Mariline.Ribeiro@iol.pt", "Rua Corredora do Mestre (Palhavã de Cima)", "Mariline Santos", "964106478", null },
                    { 8, "2300 - 551 TOMAR", "Paula.Vieira@clix.pt", "Praça Infante Dom Henrique", "Paula Soares", "961937768", null },
                    { 7, "7630 - 782 ZAMBUJEIRA DO MAR", "Alexandre.Dias@hotmail.com", "Rua João Pedro Costa", "Alexandre Vieira", "961493756", null },
                    { 10, "2300 - 635 TOMAR", "Roberto.Vieira@sapo.pt", "Largo do Flecheiro", "Roberto Pinto", "964685937", null },
                    { 5, "2305 - 127 ASSEICEIRA TMR", "Mariline.Martins@sapo.pt", "Zona Industrial", "Mariline Marques", "967212144", null },
                    { 4, "2300 - 324 TOMAR", "Paula.Lopes@iol.pt", "Bairro Pimenta", "Paula Silva", "967517256", null },
                    { 2, "2300 - 551 TOMAR", "Fátima.Machado@gmail.com", "Praça Infante Dom Henrique", "Fátima Ribeiro", "963737476", null },
                    { 6, "2475 - 013 BENEDITA", "Marcos.Rocha@sapo.pt", "Rua de Bacelos", "Marcos Rocha", "962125638", null }
                });

            migrationBuilder.InsertData(
                table: "Marcas",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { 7, "Porsche" },
                    { 1, "Buggati" },
                    { 2, "Pagani" },
                    { 3, "McLaren" },
                    { 4, "Lamborghini" },
                    { 5, "Koenigsegg" },
                    { 6, "Ferrari" },
                    { 8, "Rimac" }
                });

            migrationBuilder.InsertData(
                table: "Carros",
                columns: new[] { "Id", "Ano", "Cilindrada", "Combustivel", "MarcaFK", "Modelo", "Potencia", "Preco" },
                values: new object[,]
                {
                    { 1, 2010, 8000, "Gasolina", 1, "Bugatti Veyron", 1400, 2300000 },
                    { 2, 2018, 9000, "Gasolina", 1, "Bugatti Chiron", 1600, 2800000 },
                    { 3, 2021, 10000, "Gasolina", 1, "Bugatti Divo", 1700, 5000000 },
                    { 12, 2017, 8000, "Gasolina", 2, "Paggani Huayra", 1300, 300000 },
                    { 4, 2019, 8000, "Hibrido", 3, "McLaren P1", 1600, 1300000 },
                    { 5, 2020, 7000, "Hibrido", 3, "McLaren Senna", 1200, 1000000 },
                    { 6, 2021, 9000, "Hibrido", 4, "Lamborgini Sian", 1500, 3700000 },
                    { 7, 2021, 8000, "Hibrido", 5, "Koenigsegg Gemera", 1500, 1900000 },
                    { 8, 2019, 9000, "Gasolina", 5, "Koenigsegg Jesko", 1700, 2500000 },
                    { 9, 2018, 5000, "Hibrido", 6, "Ferrari Laferrari ", 850, 2000000 },
                    { 10, 2013, 6000, "Gasolina", 7, "Porche Carrera gt", 800, 1900000 },
                    { 11, 2021, 10000, "Eletrico", 8, "Rimac Nevera", 2000, 100000 }
                });

            migrationBuilder.InsertData(
                table: "Fotografias",
                columns: new[] { "Id", "CarroFK", "DataFoto", "Fotografia", "Local" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2019, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "veyron.jpg", "Stand" },
                    { 11, 11, new DateTime(2018, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "nevera.jpg", "Stand" },
                    { 10, 10, new DateTime(2020, 12, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "CarreraGT.jpg", "Stand" },
                    { 9, 9, new DateTime(2011, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "LaFerrari.jpg", "Stand" },
                    { 8, 8, new DateTime(2020, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "jesko.jpg", "Stand" },
                    { 7, 7, new DateTime(2012, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "gemera.jpg", "Stand" },
                    { 5, 5, new DateTime(2019, 3, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "senna.jpg", "Stand" },
                    { 6, 6, new DateTime(2013, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "sian.jpg", "Stand" },
                    { 12, 12, new DateTime(2017, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Huayra.jpg", "Stand" },
                    { 3, 3, new DateTime(2019, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "divo.jpg", "Stand" },
                    { 2, 2, new DateTime(2019, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "chiron.jpg", "Stand" },
                    { 4, 4, new DateTime(2021, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "P1.jpg", "Stand" }
                });

            migrationBuilder.InsertData(
                table: "Gostos",
                columns: new[] { "Id", "CarroFK", "ClienteFK", "DataCompra" },
                values: new object[,]
                {
                    { 4, 4, 5, new DateTime(2008, 8, 7, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 5, 6, new DateTime(2012, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, 11, 8, new DateTime(2018, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 6, 7, new DateTime(2010, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 3, 4, new DateTime(2011, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, 7, 8, new DateTime(2011, 2, 9, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, 8, 9, new DateTime(2013, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, 2, new DateTime(2019, 12, 9, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, 9, 10, new DateTime(2011, 7, 9, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, 10, 5, new DateTime(2017, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 1, 1, 1, new DateTime(2019, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, 11, 8, new DateTime(2018, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Carros_MarcaFK",
                table: "Carros",
                column: "MarcaFK");

            migrationBuilder.CreateIndex(
                name: "IX_Fotografias_CarroFK",
                table: "Fotografias",
                column: "CarroFK");

            migrationBuilder.CreateIndex(
                name: "IX_Gostos_CarroFK",
                table: "Gostos",
                column: "CarroFK");

            migrationBuilder.CreateIndex(
                name: "IX_Gostos_ClienteFK",
                table: "Gostos",
                column: "ClienteFK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fotografias");

            migrationBuilder.DropTable(
                name: "Gostos");

            migrationBuilder.DropTable(
                name: "Carros");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Marcas");

            migrationBuilder.DropColumn(
                name: "DataRegisto",
                table: "AspNetUsers");
        }
    }
}
