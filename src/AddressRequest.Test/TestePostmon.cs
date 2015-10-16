using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AddressRequest.Test
{
    
    public class TestPostmon
    {
        [Test]
        public void TestarRequisiçãoDeEndereço()
        {
            var service = new AddressService(ServiceEnum.Postmon);
            var address = service.GetAddress("74223-170");

            Assert.AreEqual("Rua T 61", address.Street);
        }
    }
}
