using ContactsManager.Core.Domain.RepositoryContracts;
using ContactsManager.Core.DTO;
using ContactsManager.Core.ServiceContracts;
using Microsoft.Extensions.Logging;
using Serilog;


namespace ContactsManager.Core.Services
{
    public class PersonsGetterService : IPersonsGetterService
    {

        private readonly IPersonsRepository _personRepository;
        private readonly ILogger<PersonsGetterService> _logger;
        private readonly IDiagnosticContext _diagnosticContext;
        public PersonsGetterService(IPersonsRepository personsRepository, ILogger<PersonsGetterService> logger, IDiagnosticContext diagnosticContext)
        {
            _diagnosticContext = diagnosticContext;
            _logger = logger;
            _personRepository = personsRepository;
        }
        public Task<List<PersonResponse>> GetAllPersons()
        {
            throw new NotImplementedException();
        }

        public Task<List<PersonResponse>> GetFilteredPersons(string searchBy, string searchString)
        {
            throw new NotImplementedException();
        }

        public Task<PersonResponse> GetPersonByPersonID(Guid? personId)
        {
            throw new NotImplementedException();
        }

        public Task<MemoryStream> GetPersonCSV()
        {
            throw new NotImplementedException();
        }

        public Task<MemoryStream> GetPersonsExcel()
        {
            throw new NotImplementedException();
        }
    }
}
