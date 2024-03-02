using ContactsManager.Core.DTO;


namespace ContactsManager.Core.ServiceContracts
{
    /// <summary>
    /// Represents business logic for manipulating Person entity
    /// </summary>
    public interface IPersonsGetterService
    {
        Task<List<PersonResponse>> GetAllPersons();
        /// <summary>
        /// returns the person objects based on the given personId
        /// </summary>
        /// <param name="personId">Person id to search</param>
        /// <returns>Returns matching person object</returns>
        Task<PersonResponse> GetPersonByPersonID(Guid? personId);
        /// <summary>
        /// return all  person object that matches with the given search field and search string 
        /// </summary>
        /// <param name="searchBy">Search field to search</param>
        /// <param name="searchString">Search string to search</param>
        /// <returns></returns>
        Task<List<PersonResponse>> GetFilteredPersons(string searchBy, string searchString);

        /// <summary>
        /// returns persons as CSV
        /// </summary>
        /// <returns>Returns the memory stream with CSV data</returns>
        Task<MemoryStream> GetPersonCSV();
        /// <summary>
        /// Returns persons as Excel
        /// </summary>
        /// <returns>returns memory stream with Excel data</returns>
        Task<MemoryStream> GetPersonsExcel();
    }
}
