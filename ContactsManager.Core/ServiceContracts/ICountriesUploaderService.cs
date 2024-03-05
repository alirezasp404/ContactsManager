using Microsoft.AspNetCore.Http;


namespace ContactsManager.Core.ServiceContracts
{
    public interface ICountriesUploaderService
    {
        /// <summary>
        /// uploads countries from excel file into database
        /// </summary>
        /// <param name="formFile">excel file with list of countries</param>
        /// <returns>returns number of countries added</returns>
        Task<int> UploadCountriesFromExcelFile(IFormFile formFile);
    }
}
