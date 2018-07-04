using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AddressRequest.Models.TargetLock
{
    internal class TargetLockAddressModel
    {
        public string post_code { get; set; }
        public string post_code_type { get; set; }
        public string locality { get; set; }
        public string admin_level_1_long { get; set; }
        public string admin_level_1_short { get; set; }
        public string admin_level_2 { get; set; }
        public string country { get; set; }
        public string country_iso2 { get; set; }
        public string country_iso3 { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
    }
}
