using ContactsManager.Core.Domain.RepositoryContracts;
using ContactsManager.Core.ServiceContracts;
using Microsoft.Extensions.Logging;
using Serilog;


namespace ContactsManager.Core.Services
{
    public class PersonsDeleterService : IPersonsDeleterService
    {
        private readonly IPersonsRepository _personRepository;
        private readonly ILogger<PersonsDeleterService> _logger;
        private readonly IDiagnosticContext _diagnosticContext;
        public PersonsDeleterService(IPersonsRepository personsRepository, ILogger<PersonsDeleterService> logger, IDiagnosticContext diagnosticContext)
        {
            _diagnosticContext = diagnosticContext;
            _logger = logger;
            _personRepository = personsRepository;
        }
        public Task<bool> DeletePerson(Guid? personId)
        {
            throw new NotImplementedException();
        }
    }
}
