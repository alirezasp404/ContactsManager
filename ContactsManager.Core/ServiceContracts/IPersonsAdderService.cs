

using ContactsManager.Core.DTO;

namespace ContactsManager.Core.ServiceContracts
{
    /// <summary>
    /// Represents business logic for manipulating Person entity
    /// </summary>
    public interface IPersonsAdderService
    {
        /// <summary>
        /// add a person to existing persons
        /// </summary>
        /// <param name="personAddRequest">Person to add</param>
        /// <returns> Returns the same person into PersonResponse</returns>
        Task<PersonResponse> AddPerson(PersonAddRequest personAddRequest);
    }
}
