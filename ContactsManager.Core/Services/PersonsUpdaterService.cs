using ContactsManager.Core.Domain.RepositoryContracts;
using ContactsManager.Core.DTO;
using ContactsManager.Core.ServiceContracts;
using Microsoft.Extensions.Logging;
using Serilog;


namespace ContactsManager.Core.Services
{
    public class PersonsUpdaterService : IPersonsUpdaterService
    {
        private readonly IPersonsRepository _personRepository;
        private readonly ILogger<PersonsUpdaterService> _logger;
        private readonly IDiagnosticContext _diagnosticContext;
        public PersonsUpdaterService(IPersonsRepository personsRepository, ILogger<PersonsUpdaterService> logger, IDiagnosticContext diagnosticContext)
        {
            _diagnosticContext = diagnosticContext;
            _logger = logger;
            _personRepository = personsRepository;
        }
        public Task<PersonResponse> UpdatePerson(PersonUpdateRequest personUpdateRequest)
        {
            throw new NotImplementedException();
        }
    }
}
