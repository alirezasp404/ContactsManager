using ContactsManager.Core.Domain.RepositoryContracts;
using ContactsManager.Core.DTO;
using ContactsManager.Core.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManager.Core.Services
{
    public class CountriesAdderService : ICountriesAdderService
    {

        private readonly ICountriesRepository _countriesRepository;


        public CountriesAdderService(ICountriesRepository countriesRepository)
        {
            _countriesRepository = countriesRepository;

        }
        public Task<CountryResponse> AddCountry(CountryAddRequest? countryAddRequest)
        {
            throw new NotImplementedException();
        }
    }
}
