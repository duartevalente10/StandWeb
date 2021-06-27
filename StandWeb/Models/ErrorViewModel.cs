using System;
using System.Collections.Generic;

namespace StandWeb.Models
{

    /// <summary>
    /// lista os dados dos carros a serem disponibilizados na API
    /// </summary>
    public class CarrosAPIViewModel
    {
        /// <summary>
        /// identificador do Carro
        /// </summary>
        public int IdCarro { get; set; }

        /// <summary>
        /// Nome do Carro
        /// </summary>
        public string NomeCarro { get; set; }
    }

    /// <summary>
    /// ViewModel para transportar os dados das Fotografias
    /// dos carros, na API
    /// </summary>
    public class FotosAPIViewModel
    {
        /// <summary>
        /// id da Foto
        /// </summary>
        public int IdFoto { get; set; }

        /// <summary>
        /// nome do ficheiro da foto
        /// </summary>
        public string NomeFoto { get; set; }

        /// <summary>
        /// local onde a foto foi tirada
        /// </summary>
        public string LocalFoto { get; set; }

        /// <summary>
        /// data em que a foto foi tirada
        /// </summary>
        public string DataFoto { get; set; }

        /// <summary>
        /// nome do Carro
        /// </summary>
        public string ModeloCarro { get; set; }
    }

    /// <summary>
    /// classe para permitir o transporte do Controller para a View, e vice-versa
    /// irá transportar os dados das Fotografias e dos IDs dos Carros que pertencem 
    /// à pessoa autenticada
    /// </summary>
    public class FotosCarros
    {

        /// <summary>
        /// lista de todas as fotografias de todos os carros
        /// </summary>
        public ICollection<Fotografias> ListaFotografias { get; set; }

        /// <summary>
        /// lista dos IDs dos carros que pertencem à pessoa autenticada
        /// </summary>
        public ICollection<int> ListaCarros { get; set; }

    }

    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
