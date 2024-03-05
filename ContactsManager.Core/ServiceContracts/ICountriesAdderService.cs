using ContactsManager.Core.DTO;


namespace ContactsManager.Core.ServiceContracts
{
    public interface ICountriesAdderService
    {/// <summary>
     /// adds a country object to the list of countries
     /// </summary>
     /// <param name="countryAddRequest"></param>
     /// <returns> returns the country object after adding it</returns>
        Task<CountryResponse> AddCountry(CountryAddRequest? countryAddRequest);
    }
}
