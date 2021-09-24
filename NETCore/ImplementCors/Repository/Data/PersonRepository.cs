using ImplementCors.Base.Url;
using Microsoft.AspNetCore.Http;
using NETCore.Model;
using NETCore.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ImplementCors.Repository.Data
{
    public class PersonRepository:GeneralRepository<Person,string>
    {
        private readonly Address address;
        private readonly string request;
        //private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient httpClient;

        public PersonRepository(Address address, string request = "Persons/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            //_contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _contextAccessor.HttpContext.Session.GetString("JWToken"));
        }


        public async Task<List<PersonVM>> GetPersonVM()
        {
            List<PersonVM> entities = new List<PersonVM>();
            using (var response = await httpClient.GetAsync(request + "GetPerson"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<PersonVM>>(apiResponse);
            }
            return entities;
        }

         public async Task<PersonVM> GetPersonVM(string NIK)
        {
            PersonVM personVM = new PersonVM();
            using (var response = await httpClient.GetAsync(request + "GetPerson/"+NIK))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                personVM = JsonConvert.DeserializeObject<PersonVM>(apiResponse);
            }
            return personVM;
        }


        //public async Task<PersonVM> Register(PersonVM personVM)
        //{
        //    StringContent content = new StringContent(JsonConvert.SerializeObject(personVM), Encoding.UTF8, "application/json");

        //    using (var response = await httpClient.PostAsync(request + "Register", content))
        //    {
        //        string apiResponse = await response.Content.ReadAsStringAsync();
        //        personVM = JsonConvert.DeserializeObject<PersonVM>(apiResponse);
        //    }
        //    return personVM;
        //}

        public string Register(PersonVM register)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(register), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(request + "Register", content).Result.Content.ReadAsStringAsync().Result;
            return result;
        }






        //public async Task<List<District>> GetByProvinceId(int provinceId)
        //{
        //    List<District> entities = new List<District>();

        //    using (var response = await httpClient.GetAsync(request + "getbyprovinceid/" + provinceId))
        //    {
        //        string apiResponse = await response.Content.ReadAsStringAsync();
        //        entities = JsonConvert.DeserializeObject<List<District>>(apiResponse);
        //    }
        //    return entities;
        //}

    }
}
