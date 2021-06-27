using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StandWeb.Models
{
    public class Marcas{
        public Marcas()
        {
            // procurar os Carros de cada Marca e criar, para cada Marca, uma lista com os seus carros
            ListaDeCarros = new HashSet<Carros>();
        }

        /// <summary>
        /// Identificador de cada uma das Marcas
        /// </summary>
        [Key] // identifica este atributo como PK
        public int Id { get; set; }

        /// <summary>
        /// identifica o nome da Marca
        /// </summary>
        public string Nome { get; set; }

        //*********************************************************

        // uma marca está associada a muitos carros
        // uma marca tem uma lista de carros
        /// <summary>
        /// Lista dos carros que são da marca
        /// </summary>
        public ICollection<Carros> ListaDeCarros { get; set; }
    }
}
