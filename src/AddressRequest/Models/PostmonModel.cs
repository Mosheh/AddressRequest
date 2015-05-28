using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace AddressRequest.Models
{
    [DataContract]
    public class PostmonModel
    {
        public PostmonModel()
        {
            
        }
        public string complemento { get; set; }
        public string bairro { get; set; }
        public string cidade { get; set; }
        public string logradouro { get; set; }
        public string cep { get; set; }
        public Estado_info estado_info { get; set; }
        public Cidade_info cidade_info { get; set; }
        public string estado { get; set; }

    }
}
