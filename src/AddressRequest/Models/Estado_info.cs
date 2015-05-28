using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace AddressRequest.Models
{
    public class Estado_info
    {
        public Estado_info()
        {
 
        }
        [DataMember()]
        public string area_km2 { get; set; }
        public string codigo_ibge { get; set; }
        public string nome { get; set; }
    }
}
