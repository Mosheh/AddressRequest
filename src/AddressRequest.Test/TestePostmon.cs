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
        public void TestarRequisiçãoDeEndereçoPostmon()
        {
            var service = new AddressService(ServiceEnum.Postmon);
            var address = service.GetAddress("74223-170");
            Assert.AreEqual("Rua T 61", address.Street);
        }

        [Test]
        public void TestarRequisiçãoDeEndereçoViaCEP()
        {
            var service = new AddressService(ServiceEnum.ViaCEP);
            var address = service.GetAddress("74223-170");
            Assert.AreEqual("Rua T 61", address.Street);
        }

        [Test]
        public void TestarRequisiçãoDeEndereçoIBGE()
        {
            var service = new AddressService(ServiceEnum.ViaCEP);
            var address = service.GetAddress("74223-170");
            var ibgeGoiania = "5208707";
            Assert.AreEqual(ibgeGoiania, address.CityInfo.IBGECode);
        }

        [Test]
        public void TestarRemocaoDeCaracterEspecialDoCep()
        {
            var service = new AddressService(ServiceEnum.Postmon);
            var address = service.RemoveCaracter("74223-170");
            Assert.AreEqual(address, "74223170");

        }

        [Test]
        public void TestarQuantidadeDeCaracter()
        {
            var service = new AddressService(ServiceEnum.Postmon);
            var isValid = service.ValidQuantityCaracter("74223-170");
            Assert.AreEqual(isValid, true);
        }

        [Test]
        public void TestarRequisicaoDeEnderecoPorArea()
        {
            var service = new AddressService(ServiceEnum.ViaCEP);
            var address = service.GetAddress("74477-207");
            var bairro = "Conjunto Primavera";
            Assert.AreEqual(bairro, address.Neighborhood);
        }
    }
}
