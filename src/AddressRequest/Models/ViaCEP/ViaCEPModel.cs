using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AddressRequest.Models.ViaCEP
{
    public class ViaCEPModel
    {
        public string cep { get; set; }
        public string logradouro { get; set; }
        public string complemento { get; set; }
        public string bairro { get; set; }
        public string localidade { get; set; }
        public string uf { get; set; }
        public string ibge { get; set; }
    }
}
