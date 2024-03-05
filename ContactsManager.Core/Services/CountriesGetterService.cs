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
    public class CountriesGetterService : ICountriesGetterService
    {
            
        private readonly ICountriesRepository _countriesRepository;


        public CountriesGetterService(ICountriesRepository countriesRepository)
        {
            _countriesRepository = countriesRepository;

        }
        public Task<List<CountryResponse>> GetAllCountries()
        {
            throw new NotImplementedException();
        }

        public Task<CountryResponse?> GetCountryByID(Guid? countryId)
        {
            throw new NotImplementedException();
        }
    }
}
