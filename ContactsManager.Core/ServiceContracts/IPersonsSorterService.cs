using ContactsManager.Core.DTO;
using ContactsManager.Core.Enums;


namespace ContactsManager.Core.ServiceContracts
{
    /// <summary>
    /// Represents business logic for manipulating Person entity
    /// </summary>
    public interface IPersonsSorterService
    {
        /// <summary>
        /// Returns sorted list of persons
        /// </summary>
        /// <param name="allPersons">Represents list of persons to sort</param>
        /// <param name="sortBy">Name of the property(key) ,based on which the persons should be sorted </param>
        /// <param name="sortOrder">ASC or DESC</param>
        /// <returns>Returns sorted persons as List of PersonResponse</returns>
        Task<List<PersonResponse>> GetSortedPersons(List<PersonResponse> allPersons, string sortBy, SortOrderOptions sortOrder);
    }
}
