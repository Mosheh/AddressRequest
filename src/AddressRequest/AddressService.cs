﻿using AddressRequest.Models;
using AddressRequest.Models.TargetLock;
using AddressRequest.Models.ViaCEP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace AddressRequest
{
    public class AddressService
    {
        public AddressService(ServiceEnum service, bool checkInternetConnection=false)
        {
            Service = service;
            this.CheckInternetConnection = checkInternetConnection;
            ServicesUri = new Dictionary<ServiceEnum, string>();
            ServicesUri.Add(ServiceEnum.Postmon, "http://api.postmon.com.br/v1/cep/{0}");
            ServicesUri.Add(ServiceEnum.ViaCEP, "http://viacep.com.br/ws/{0}/json");
            ServicesUri.Add(ServiceEnum.TargetLock, "https://api.targetlock.io/v1/post-code/{0}");
        }

        public AddressData GetAddress(string zipCode)
        {
            CheckConnection();
            ValidZipCode(zipCode);
            var addressDataUrl =string.Format( ServicesUri[this.Service],zipCode);
            try
            {
                switch (Service)
                {
                    case ServiceEnum.Postmon:
                        return GetAddressDataPostmon(addressDataUrl);
                    case ServiceEnum.ViaCEP:
                        return GetAddressDataViaCEP(addressDataUrl);
                    case ServiceEnum.TargetLock:
                        return GetAddressTargetLock(addressDataUrl);
                    default:
                        return GetAddressDataPostmon(addressDataUrl);
                }                                                 
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        private AddressData GetAddressTargetLock(string addressDataUrl)
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var httpRequest = HttpWebRequest.Create(addressDataUrl) as HttpWebRequest;
            httpRequest.Method = "GET";
            httpRequest.KeepAlive = false;
            httpRequest.ProtocolVersion = HttpVersion.Version10;
            httpRequest.ContentType = "application/json";
            httpRequest.Accept = "application/json";
            using (HttpWebResponse response = httpRequest.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(response.StatusDescription);

                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    DataContractJsonSerializer dataContractSerializer = new DataContractJsonSerializer(typeof(ViaCEPModel));
                    var jsonText = reader.ReadToEnd();
                    var jscriptSerializer = new JavaScriptSerializer();
                    var targetModel = jscriptSerializer.Deserialize<TargetLockAddressModel>(jsonText);
                    var addressData = new AddressData();
                    addressData.FillBy(targetModel);
                    return addressData;
                }
            }
        }

        private void CheckConnection()
        {
            if (this.CheckInternetConnection)
                if (!HasInternetConnection())
                    throw new Exception("Falha de conexão com a internet");
        }

        private AddressData GetAddressDataViaCEP(string addressDataUrl)
        {
            var httpRequest = HttpWebRequest.Create(addressDataUrl) as HttpWebRequest;

            using (HttpWebResponse response = httpRequest.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(response.StatusDescription);

                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    DataContractJsonSerializer dataContractSerializer = new DataContractJsonSerializer(typeof(ViaCEPModel));
                    var jsonText = reader.ReadToEnd();
                    var jscriptSerializer = new JavaScriptSerializer();
                    var viaCEPModel = jscriptSerializer.Deserialize<ViaCEPModel>(jsonText);
                    var addressData = new AddressData();
                    addressData.FillBy(viaCEPModel);
                    return addressData;
                }
            }
        }

        private static AddressData GetAddressDataPostmon(string addressDataUri)
        {
            var httpRequest = HttpWebRequest.Create(addressDataUri) as HttpWebRequest;

            using (HttpWebResponse response = httpRequest.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(response.StatusDescription);

                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    DataContractJsonSerializer dataContractSerializer = new DataContractJsonSerializer(typeof(PostmonModel));
                    var jsonText = reader.ReadToEnd();
                    var jscriptSerializer = new JavaScriptSerializer();
                    var postmonModel = jscriptSerializer.Deserialize<PostmonModel>(jsonText);
                    var addressData = new AddressData();
                    addressData.FillBy(postmonModel);
                    return addressData;
                }
            }
        }

        public string RemoveCaracter(string zipCode)
        {
            zipCode = zipCode.Replace("-", "");

            return zipCode;
        }

        public bool ValidQuantityCaracter(string zipCode)
        {
            var zip = RemoveCaracter(zipCode);

            return zip.Length == 8;
        }

        private void ValidZipCode(string zipCode)
        {
            
        }
        public static bool HasInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        ServiceEnum Service { get; set; }
        bool CheckInternetConnection { get; set; }
        public Dictionary<ServiceEnum, string> ServicesUri = new Dictionary<ServiceEnum, string>();
    }
}
