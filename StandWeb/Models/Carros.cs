using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StandWeb.Models
{
    /// <summary>
    /// descrição de cada um dos Carros
    /// </summary>
    public class Carros
    {

        public Carros()
        {
            // inicializar a lista de Fotografias de cada um dos carros
            ListasDeFotografias = new HashSet<Fotografias>();
            // inicializar a lista de Clientes
            ListaClientes = new HashSet<Gostos>();
        }

        /// <summary>
        /// Identificador do Carro
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Identificador do modelo do carro
        /// </summary>
        public string Modelo { get; set; }

        /// <summary>
        /// Ano do carro
        /// </summary>
        public int Ano { get; set; }

        /// <summary>
        /// Preço do carro
        /// </summary>
        public int Preco { get; set; }

        /// <summary>
        /// Cilindrada do carro
        /// </summary>
        public int Cilindrada { get; set; }

        /// <summary>
        /// Potência do carro
        /// </summary>
        public int Potencia { get; set; }

        /// <summary>
        /// Tipo de Combustivel do carro
        /// Gasoleo 
        /// Gasolina
        /// </summary>
        public string Combustivel { get; set; }

        // ********************************************************

        /// <summary>
        /// FK para a Marca do carro
        /// </summary>
        [ForeignKey(nameof(Marca))]  // esta 'anotação' indica que o atributo 'MarcaFK' está a executar o mesmo que o atributo 'Marca', e que representa uma FK para a classe Marcas
        public int MarcaFK { get; set; }   // atributo para ser usado no SGBD e no C#. Representa a FK para a Marca do carro
        public Marcas Marca { get; set; }   // atributo para ser usado no C#. Representa a FK para a Marca do carro


        // #########################################################

        // associar os carros às suas fotografias
        /// <summary>
        /// Lista das Fotografias do carro
        /// </summary>
        public ICollection<Fotografias> ListasDeFotografias { get; set; }

        // ##########################################################

        // associar os Carros aos seus Clientes
        /// <summary>
        /// Lista dos Clientes associado ao carro
        /// </summary>
        public ICollection<Gostos> ListaClientes { get; set; }

    }
}
