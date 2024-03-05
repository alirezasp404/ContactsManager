using ContactsManager.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManager.Core.ServiceContracts
{
    public interface ICountriesGetterService
    {
        /// <summary>
        /// return all countries form the list
        /// </summary>
        /// <returns>
        /// all countries from the list as List
        /// </returns>
        Task<List<CountryResponse>> GetAllCountries();
        /// <summary>
        /// return a country object based on the given country id
        /// </summary>
        /// <param name="countryId">CountryId (guid) to search</param>
        /// <returns>matching country as CountryResponse object</returns>
        Task<CountryResponse?> GetCountryByID(Guid? countryId);
    }
}
