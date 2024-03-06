using ContactsManager.Core.Domain.Entities;
using ContactsManager.Core.Domain.RepositoryContracts;
using ContactsManager.Core.DTO;
using ContactsManager.Core.ServiceContracts;


namespace ContactsManager.Core.Services
{
    public class CountriesGetterService : ICountriesGetterService
    {
            
        private readonly ICountriesRepository _countriesRepository;


        public CountriesGetterService(ICountriesRepository countriesRepository)
        {
            _countriesRepository = countriesRepository;

        }
        public async Task<List<CountryResponse>> GetAllCountries()
        {

            return (await _countriesRepository.GetAllCountries()).Select(c => c.ToCountryResponse()).ToList();
        }

        public async Task<CountryResponse?> GetCountryByID(Guid? countryID)
        {
            if (countryID == null) { return null; }

            Country? countryResponseFromList = await _countriesRepository.GetCountryByCountryId(countryID.Value);

            if (countryResponseFromList == null) { return null; }

            return countryResponseFromList.ToCountryResponse();

        }
    }
}
