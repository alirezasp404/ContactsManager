using ContactsManager.Core.Domain.RepositoryContracts;
using ContactsManager.Core.DTO;
using ContactsManager.Core.ServiceContracts;
using Microsoft.Extensions.Logging;
using Serilog;


namespace ContactsManager.Core.Services
{
    public class PersonsAdderService : IPersonsAdderService
    {
        private readonly IPersonsRepository _personRepository;
        private readonly ILogger<PersonsAdderService> _logger;
        private readonly IDiagnosticContext _diagnosticContext;
        public PersonsAdderService(IPersonsRepository personsRepository, ILogger<PersonsAdderService> logger, IDiagnosticContext diagnosticContext)
        {
            _diagnosticContext = diagnosticContext;
            _logger = logger;
            _personRepository = personsRepository;
        }
        public Task<PersonResponse> AddPerson(PersonAddRequest personAddRequest)
        {
            throw new NotImplementedException();
        }
    }
}
