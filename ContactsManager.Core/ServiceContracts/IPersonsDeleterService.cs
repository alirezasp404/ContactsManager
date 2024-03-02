

namespace ContactsManager.Core.ServiceContracts
{
    /// <summary>
    /// Represents business logic for manipulating Person entity
    /// </summary>
    public interface IPersonsDeleterService
    {
        /// <summary>
        /// deletes a person based on id
        /// </summary>
        /// <param name="PersonId"></param>
        /// <returns></returns>
        Task<bool> DeletePerson(Guid? personId);
    }
}
