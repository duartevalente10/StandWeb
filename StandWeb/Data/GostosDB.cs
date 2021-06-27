using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StandWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace StandWeb.Data
{ 
    public class ApplicationUser : IdentityUser
        {
            /// <summary>
            /// recolhe a data de registo de um utilizador
            /// </summary>
            public DateTime DataRegisto { get; set; }

            // /// <summary>
            // /// se fizerem isto, estão a adicionar todos os atributos do 'Cliente'
            // /// à tabela de autenticação
            // /// </summary>
        }

    public class GostosDB : IdentityDbContext<ApplicationUser>
    {

        // Install-Package Microsoft.EntityFrameworkCore -Version 5.0.4
        // construtor da classe GostosDB
        // indicar onde está a BD à qual estas classes (tabelas) serão associadas
        // ver o conteúdo do ficheiro 'startup.cs'
        public GostosDB(DbContextOptions<GostosDB> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            // insert DB seed

            // dados para definição dos 'Roles'
            modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole { Id = "c", Name = "Cliente", NormalizedName = "CLIENTE" },
            new IdentityRole { Id = "g", Name = "Gestor", NormalizedName = "GESTOR" }
            );

            //dados Marcas
            modelBuilder.Entity<Marcas>().HasData(
               new Marcas { Id = 1, Nome = "Buggati" },
               new Marcas { Id = 2, Nome = "Pagani" },
               new Marcas { Id = 3, Nome = "McLaren" },
               new Marcas { Id = 4, Nome = "Lamborghini" },
               new Marcas { Id = 5, Nome = "Koenigsegg" },
               new Marcas { Id = 6, Nome = "Ferrari" },
               new Marcas { Id = 7, Nome = "Porsche" },
               new Marcas { Id = 8, Nome = "Rimac" }
            );

            //dados Clientes
            modelBuilder.Entity<Clientes>().HasData(
               new Clientes { Id = 1, Nome = "Marisa Vieira", Morada = "Largo do Pelourinho", CodPostal = "2305 - 515 PAIALVO", Email = "Marisa.Freitas@iol.pt", Telemovel = "967197885" },
               new Clientes { Id = 2, Nome = "Fátima Ribeiro", Morada = "Praça Infante Dom Henrique", CodPostal = "2300 - 551 TOMAR", Email = "Fátima.Machado@gmail.com", Telemovel = "963737476" },
               new Clientes { Id = 4, Nome = "Paula Silva", Morada = "Bairro Pimenta", CodPostal = "2300 - 324 TOMAR", Email = "Paula.Lopes@iol.pt", Telemovel = "967517256" },
               new Clientes { Id = 5, Nome = "Mariline Marques", Morada = "Zona Industrial", CodPostal = "2305 - 127 ASSEICEIRA TMR", Email = "Mariline.Martins@sapo.pt", Telemovel = "967212144" },
               new Clientes { Id = 6, Nome = "Marcos Rocha", Morada = "Rua de Bacelos", CodPostal = "2475 - 013 BENEDITA", Email = "Marcos.Rocha@sapo.pt", Telemovel = "962125638" },
               new Clientes { Id = 7, Nome = "Alexandre Vieira", Morada = "Rua João Pedro Costa", CodPostal = "7630 - 782 ZAMBUJEIRA DO MAR", Email = "Alexandre.Dias@hotmail.com", Telemovel = "961493756" },
               new Clientes { Id = 8, Nome = "Paula Soares", Morada = "Praça Infante Dom Henrique", CodPostal = "2300 - 551 TOMAR", Email = "Paula.Vieira@clix.pt", Telemovel = "961937768" },
               new Clientes { Id = 9, Nome = "Mariline Santos", Morada = "Rua Corredora do Mestre (Palhavã de Cima)", CodPostal = "2300 - 390 TOMAR", Email = "Mariline.Ribeiro@iol.pt", Telemovel = "964106478" },
               new Clientes { Id = 10, Nome = "Roberto Pinto", Morada = "Largo do Flecheiro", CodPostal = "2300 - 635 TOMAR", Email = "Roberto.Vieira@sapo.pt", Telemovel = "964685937" }
            );

            //dados Carros
            modelBuilder.Entity<Carros>().HasData(
               new Carros { Id = 1, Modelo = "Bugatti Veyron", Ano = 2010, Preco = 2300000, Cilindrada = 8000, Potencia = 1400, Combustivel = "Gasolina", MarcaFK = 1 },
               new Carros { Id = 2, Modelo = "Bugatti Chiron", Ano = 2018, Preco = 2800000, Cilindrada = 9000, Potencia = 1600, Combustivel = "Gasolina", MarcaFK = 1 },
               new Carros { Id = 3, Modelo = "Bugatti Divo", Ano = 2021, Preco = 5000000, Cilindrada = 10000, Potencia = 1700, Combustivel = "Gasolina", MarcaFK = 1 },
               new Carros { Id = 4, Modelo = "McLaren P1", Ano = 2019, Preco = 1300000, Cilindrada = 8000, Potencia = 1600, Combustivel = "Hibrido", MarcaFK = 3 },
               new Carros { Id = 5, Modelo = "McLaren Senna", Ano = 2020, Preco = 1000000, Cilindrada = 7000, Potencia = 1200, Combustivel = "Hibrido", MarcaFK = 3 },
               new Carros { Id = 6, Modelo = "Lamborgini Sian", Ano = 2021, Preco = 3700000, Cilindrada = 9000, Potencia = 1500, Combustivel = "Hibrido", MarcaFK = 4 },
               new Carros { Id = 7, Modelo = "Koenigsegg Gemera", Ano = 2021, Preco = 1900000, Cilindrada = 8000, Potencia = 1500, Combustivel = "Hibrido", MarcaFK = 5 },
               new Carros { Id = 8, Modelo = "Koenigsegg Jesko", Ano = 2019, Preco = 2500000, Cilindrada = 9000, Potencia = 1700, Combustivel = "Gasolina", MarcaFK = 5 },
               new Carros { Id = 9, Modelo = "Ferrari Laferrari ", Ano = 2018, Preco = 2000000, Cilindrada = 5000, Potencia = 850, Combustivel = "Hibrido", MarcaFK = 6 },
               new Carros { Id = 10, Modelo = "Porche Carrera gt", Ano = 2013, Preco = 1900000, Cilindrada = 6000, Potencia = 800, Combustivel = "Gasolina", MarcaFK = 7 },
               new Carros { Id = 11, Modelo = "Rimac Nevera", Ano = 2021, Preco = 100000, Cilindrada = 10000, Potencia = 2000, Combustivel = "Eletrico", MarcaFK = 8 },
               new Carros { Id = 12, Modelo = "Paggani Huayra", Ano = 2017, Preco = 300000, Cilindrada = 8000, Potencia = 1300, Combustivel = "Gasolina", MarcaFK = 2 }
            );

            //dados Fotografias
            modelBuilder.Entity<Fotografias>().HasData(
               new Fotografias { Id = 1, Fotografia = "veyron.jpg", Local = "Stand", DataFoto = new DateTime(2019, 5, 20), CarroFK = 1 },
               new Fotografias { Id = 2, Fotografia = "chiron.jpg", Local = "Stand", DataFoto = new DateTime(2019, 5, 20), CarroFK = 2 },
               new Fotografias { Id = 3, Fotografia = "divo.jpg", Local = "Stand", DataFoto = new DateTime(2019, 11, 18), CarroFK = 3 },
               new Fotografias { Id = 4, Fotografia = "P1.jpg", Local = "Stand", DataFoto = new DateTime(2021, 1, 17), CarroFK = 4 },
               new Fotografias { Id = 5, Fotografia = "senna.jpg", Local = "Stand", DataFoto = new DateTime(2019, 3, 7), CarroFK = 5 },
               new Fotografias { Id = 6, Fotografia = "sian.jpg", Local = "Stand", DataFoto = new DateTime(2013, 10, 21), CarroFK = 6 },
               new Fotografias { Id = 7, Fotografia = "gemera.jpg", Local = "Stand", DataFoto = new DateTime(2012, 10, 1), CarroFK = 7 },
               new Fotografias { Id = 8, Fotografia = "jesko.jpg", Local = "Stand", DataFoto = new DateTime(2020, 10, 1), CarroFK = 8 },
               new Fotografias { Id = 9, Fotografia = "LaFerrari.jpg", Local = "Stand", DataFoto = new DateTime(2011, 1, 4), CarroFK = 9 },
               new Fotografias { Id = 10, Fotografia = "CarreraGT.jpg", Local = "Stand", DataFoto = new DateTime(2020, 12, 6), CarroFK = 10 },
               new Fotografias { Id = 11, Fotografia = "nevera.jpg", Local = "Stand", DataFoto = new DateTime(2018, 12, 23), CarroFK = 11 },
               new Fotografias { Id = 12, Fotografia = "Huayra.jpg", Local = "Stand", DataFoto = new DateTime(2017, 1, 5), CarroFK = 12 }
            );

            //dados Gostos
            modelBuilder.Entity<Gostos>().HasData(
               new Gostos { Id = 1, DataCompra = new DateTime(2019, 6, 15), CarroFK = 1, ClienteFK = 1 },
               new Gostos { Id = 2, DataCompra = new DateTime(2019, 12, 9), CarroFK = 2, ClienteFK = 2 },
               new Gostos { Id = 3, DataCompra = new DateTime(2011, 5, 21), CarroFK = 3, ClienteFK = 4 },
               new Gostos { Id = 4, DataCompra = new DateTime(2008, 8, 7), CarroFK = 4, ClienteFK = 5 },
               new Gostos { Id = 5, DataCompra = new DateTime(2012, 10, 20), CarroFK = 5, ClienteFK = 6 },
               new Gostos { Id = 6, DataCompra = new DateTime(2010, 11, 30), CarroFK = 6, ClienteFK = 7 },
               new Gostos { Id = 7, DataCompra = new DateTime(2011, 2, 9), CarroFK = 7, ClienteFK = 8 },
               new Gostos { Id = 8, DataCompra = new DateTime(2013, 6, 21), CarroFK = 8, ClienteFK = 9 },
               new Gostos { Id = 9, DataCompra = new DateTime(2011, 7, 9), CarroFK = 9, ClienteFK = 10 },
               new Gostos { Id = 10, DataCompra = new DateTime(2017, 5, 20), CarroFK = 10, ClienteFK = 5 },
               new Gostos { Id = 11, DataCompra = new DateTime(2018, 3, 5), CarroFK = 11, ClienteFK = 8 },
               new Gostos { Id = 12, DataCompra = new DateTime(2018, 3, 5), CarroFK = 11, ClienteFK = 8 }
            );

        }

        //Representar as Tabelas da BD
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Carros> Carros { get; set; }
        public DbSet<Marcas> Marcas { get; set; }
        public DbSet<Fotografias> Fotografias { get; set; }
        public DbSet<Gostos> Gostos { get; set; }

    }
}
