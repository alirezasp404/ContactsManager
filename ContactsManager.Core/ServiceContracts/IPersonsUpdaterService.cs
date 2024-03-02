using ContactsManager.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManager.Core.ServiceContracts
{
    /// <summary>
    /// Represents business logic for manipulating Person entity
    /// </summary>
    public interface IPersonsUpdaterService
    {
        /// <summary>
        /// update the person
        /// </summary>
        /// <param name="personUpdateRequest">person details to update </param>
        /// <returns> person response</returns>
        Task<PersonResponse> UpdatePerson(PersonUpdateRequest personUpdateRequest);

    }
}
