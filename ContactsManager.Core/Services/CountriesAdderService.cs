using ContactsManager.Core.Domain.Entities;
using ContactsManager.Core.Domain.RepositoryContracts;
using ContactsManager.Core.DTO;
using ContactsManager.Core.ServiceContracts;


namespace ContactsManager.Core.Services
{
    public class CountriesAdderService : ICountriesAdderService
    {

        private readonly ICountriesRepository _countriesRepository;


        public CountriesAdderService(ICountriesRepository countriesRepository)
        {
            _countriesRepository = countriesRepository;

        }
        public async Task<CountryResponse> AddCountry(CountryAddRequest? countryAddRequest)
        {
            if (countryAddRequest == null)
            {
                throw new ArgumentNullException(nameof(countryAddRequest));
            }
            if (countryAddRequest.CountryName == null)
            {
                throw new ArgumentException(nameof(countryAddRequest.CountryName));
            }
            if (await _countriesRepository.GetCountryByCountryName(countryAddRequest.CountryName) != null)
            {
                throw new ArgumentException("Given country name already exist");
            }
            var country = countryAddRequest.ToCountry();
            country.CountryID = Guid.NewGuid();
            Country countyResponse = await _countriesRepository.AddCountry(country);
            return countyResponse.ToCountryResponse();
        }
    }
}
