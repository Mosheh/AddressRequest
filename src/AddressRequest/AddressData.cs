using AddressRequest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AddressRequest.Extensions;
namespace AddressRequest
{
    public class AddressData
    {
        public AddressData()
        {
            StateInf = new StateInformation();
            CityInfo = new CityInformation();
        }

        internal void FillBy(PostmonModel model)
        {
            if (model == null)
                throw new Exception("Modelo inválido");
            if(CityInfo ==null)
                throw new Exception("Modelo do cidade inválido");
            if(StateInf == null)
                throw new Exception("Modelo do estado inválido");
            this.Complementary = model.complemento;
            this.Neighborhood = model.bairro;
            this.City = model.cidade;
            this.Street = model.logradouro;
            this.ZipCode = model.cep;
            this.State = model.estado;
            SetStateInf(model.estado_info.area_km2.To<decimal>(), model.estado_info.codigo_ibge, model.estado_info.nome);
            SetCityInformation(model.cidade_info.area_km2.To<decimal>(), model.cidade_info.codigo_ibge);
        }

        /// <summary>
        /// Complemento
        /// </summary>
        public string Complementary { get; set; }

        /// <summary>
        /// Bairro
        /// </summary>
        public string Neighborhood { get; set; }

        /// <summary>
        /// Cidade
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Logradouro
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// CEP
        /// </summary>
        public string ZipCode { get; set; }
        /// <summary>
        /// Estado
        /// </summary>
        public string State { get; set; }

        public StateInformation StateInf { get; private set; }
        public CityInformation CityInfo { get; private set; }

        public void SetStateInf(decimal areaKm2, string ibgeCode, string name)
        {
            StateInf.AreaKm2 = areaKm2;
            StateInf.IBGECode = ibgeCode;
            StateInf.Name = name;
        }

        public void SetCityInformation(decimal areaKm2, string ibgeCode)
        {
            this.CityInfo.AreaKm2 = areaKm2;
            this.CityInfo.IBGECode = ibgeCode;
        }

        internal void FillBy(Models.ViaCEP.ViaCEPModel viaCEPModel)
        {
            if (viaCEPModel == null)
                throw new Exception("Modelo inválido");

            this.ZipCode = viaCEPModel.cep;
            this.State = viaCEPModel.uf;
            this.City = viaCEPModel.localidade;
            this.Neighborhood = viaCEPModel.bairro;
            this.Complementary = viaCEPModel.complemento;
            this.CityInfo.IBGECode = viaCEPModel.ibge;
            
        }
    }
}
