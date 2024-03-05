using ContactsManager.Core.Domain.Entities;


namespace ContactsManager.Core.Domain.RepositoryContracts
{

    /// <summary>
    /// Represents data access logic for managing Person entity
    /// </summary>
    public interface ICountriesRepository
    {
        /// <summary>
        /// adds a new country object to the data store
        /// </summary>
        /// <param name="country">Country object to add</param>
        /// <returns>Returns the country object after adding it to the data store</returns>
        Task<Country> AddCountry(Country country);
        /// <summary>
        /// returns all countries in the data store
        /// </summary>
        /// <returns>All countries from the table</returns>
        Task<List<Country>?> GetAllCountries();
        /// <summary>
        /// Returns a country object based on the given country id;
        /// otherwise,it returns null
        /// </summary>
        /// <param name="id">CountryId to search</param>
        /// <returns>Matching country or null</returns>
        Task<Country?> GetCountryByCountryId(Guid id);
        /// <summary>
        /// Returns a country object based on the given country name
        /// </summary>
        /// <param name="countryName">Country name to search</param>
        /// <returns>Matching country or null</returns>
        Task<Country?> GetCountryByCountryName(string countryName);
    }

}
