using ContactsManager.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManager.Core.Domain.RepositoryContracts
{
    /// <summary>
    /// Represents data access logic for managing  Person entity
    /// </summary>
    public interface IPersonsRepository
    {
        /// <summary>
        /// Adds a person object to the data store
        /// </summary>
        /// <param name="person">Person object to add</param>
        /// <returns>Returns the person object after adding it to the table</returns>
        Task<Person> AddPerson(Person person);
        /// <summary>
        /// Returns all persons in the data store
        /// </summary>
        /// <returns>List of person objects from table </returns>
        Task<List<Person>> GetAllPersons();
        /// <summary>
        /// Returns a person object based on the given person id 
        /// </summary>
        /// <param name="personId">PersonId to search</param>
        /// <returns>A person object or null</returns>
        Task<Person?> GetPersonByPersonId(Guid personId);
        /// <summary>
        /// returns all person objects based on the given expression
        /// </summary>
        /// <param name="predicate">Linq expression to check</param>
        /// <returns>All matching persons</returns>

        Task<List<Person>> GetFilteredPersons(Expression<Func<Person, bool>> predicate);

        /// <summary>
        /// Deletes a person object based on the person id
        /// </summary>
        /// <param name="personId">PersonId to search</param>
        /// <returns>Returns true, if the deletion is successful; otherwise false</returns>
        Task<bool> DeletePersonByPersonId(Guid personId);

        /// <summary>
        /// Updates a person object based on given personId
        /// </summary>
        /// <param name="person">Person object to update</param>
        /// <returns>Returns the updated person object</returns>
        Task<Person> UpdatePerson(Person person);
    }
}
