using ContactsManager.Core.Domain.RepositoryContracts;
using ContactsManager.Core.DTO;
using ContactsManager.Core.Enums;
using ContactsManager.Core.ServiceContracts;
using Microsoft.Extensions.Logging;
using Serilog;


namespace ContactsManager.Core.Services
{
    public class PersonsSorterService : IPersonsSorterService
    {
        private readonly IPersonsRepository _personRepository;
        private readonly ILogger<PersonsSorterService> _logger;
        private readonly IDiagnosticContext _diagnosticContext;
        public PersonsSorterService(IPersonsRepository personsRepository, ILogger<PersonsSorterService> logger, IDiagnosticContext diagnosticContext)
        {
            _diagnosticContext = diagnosticContext;
            _logger = logger;
            _personRepository = personsRepository;
        }
        public Task<List<PersonResponse>> GetSortedPersons(List<PersonResponse> allPersons, string sortBy, SortOrderOptions sortOrder)
        {
            throw new NotImplementedException();
        }
    }
}
