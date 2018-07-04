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

        [Test]
        public void TestarUFGOPorCEP()
        {
            var service = new AddressService(ServiceEnum.ViaCEP);
            var address = service.GetAddress("74477-207");
            var State = "GO";
            Assert.AreEqual(State, address.State);
        }

        [Test]
        public void TestarConsultaComCepSemHífen()
        {
            var service = new AddressService(ServiceEnum.ViaCEP);
            var address = service.GetAddress("74477207");
            var State = "GO";
            Assert.AreEqual(State, address.State);
        }

        [Test]
        public void TestarMensagemCEPInvalido()
        {
            var service = new AddressService(ServiceEnum.ViaCEP);
            try
            {
                var address = service.GetAddress("11111111");
            }
            catch (Exception ex) { throw ex; }
        }

        [Test]
        public void TestarBairroPeloCEP()
        {
            var service = new AddressService(ServiceEnum.ViaCEP);
            var address = service.GetAddress("74922330");
            var Bairro = "Jardim Olímpico";
            Assert.AreEqual(Bairro, address.Neighborhood);
        }

        [Test]
        public void TestarEnderecoEstadosUnidosNovaYork()
        {
            var service = new AddressService(ServiceEnum.TargetLock);
            var address = service.GetAddress("11742");
            var Bairro = "Suffolk";
            Assert.AreEqual(Bairro, address.Neighborhood);
        }

        [Test]
        public void TestarBairroNullCEPGenerico()
        {
            var service = new AddressService(ServiceEnum.ViaCEP);
            var address = service.GetAddress("77500000");
            Assert.IsNullOrEmpty(address.Neighborhood);
        }

        [Test]
        public void TestarBairroNaoNullo()
        {
            var service = new AddressService(ServiceEnum.ViaCEP);
            var address = service.GetAddress("74922330");
            Assert.IsNotNull(address.Neighborhood);
        }

        [Test]
        public void TestarUF()
        {
            var service = new AddressService(ServiceEnum.ViaCEP);
            var address = service.GetAddress("74922330");
            var State = "TO";
            Assert.AreNotEqual(State, address.State);
        }


    }
}
