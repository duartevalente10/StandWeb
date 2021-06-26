using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StandWeb.Models
{
    public class Gostos
    {
        /// <summary>
        /// PK para a tabela do relacionamento entre Carros e Comprador
        /// </summary>
        [Key]
        public int Id { get; set; }


        /// <summary>
        /// Data de Compra
        /// </summary>
        public DateTime DataCompra { get; set; }

        //*************************************************************

        /// <summary>
        /// FK para o Carro do Comprador
        /// </summary>
        [ForeignKey(nameof(Carro))]
        public int CarroFK { get; set; }
        public Carros Carro { get; set; }


        /// <summary>
        /// FK para o Comprador do Carro
        /// </summary>
        [ForeignKey(nameof(Cliente))]  // [ForeignKey("Cliente")]
        public int ClienteFK { get; set; }
        public Clientes Cliente { get; set; }
    }
}
