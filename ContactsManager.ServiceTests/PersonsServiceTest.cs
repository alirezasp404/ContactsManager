using AutoFixture;
using ContactsManager.Core.Domain.Entities;
using ContactsManager.Core.Domain.RepositoryContracts;
using ContactsManager.Core.DTO;
using ContactsManager.Core.Enums;
using ContactsManager.Core.ServiceContracts;
using ContactsManager.Core.Services;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Serilog;
using System.Linq.Expressions;


namespace ContactsManager.ServiceTests
{
    public class PersonsServiceTest
    {
        private readonly IPersonsAdderService _personsAdderService;
        private readonly IPersonsDeleterService _personsDeleterService;
        private readonly IPersonsGetterService _personsGetterService;
        private readonly IPersonsSorterService _personsSorterService;
        private readonly IPersonsUpdaterService _personsUpdaterService;
        private readonly IPersonsRepository _personsRepository;
        private readonly Mock<IPersonsRepository> _personsRepositoryMock;
        private readonly IFixture _fixture;

        public PersonsServiceTest()
        {
            var diagnosticContextMock = new Mock<IDiagnosticContext>();
            var loggerMockAdder = new Mock<ILogger<PersonsAdderService>>();
            var loggerMockDeleter = new Mock<ILogger<PersonsDeleterService>>();
            var loggerMockUpdater = new Mock<ILogger<PersonsUpdaterService>>();
            var loggerMockSorter = new Mock<ILogger<PersonsSorterService>>();
            var loggerMockGetter = new Mock<ILogger<PersonsGetterService>>();

            _personsRepositoryMock = new Mock<IPersonsRepository>();
            _personsRepository = _personsRepositoryMock.Object;

            _personsAdderService = new PersonsAdderService(_personsRepository, loggerMockAdder.Object, diagnosticContextMock.Object);
            _personsDeleterService = new PersonsDeleterService(_personsRepository, loggerMockDeleter.Object, diagnosticContextMock.Object);
            _personsGetterService = new PersonsGetterService(_personsRepository, loggerMockGetter.Object, diagnosticContextMock.Object);
            _personsSorterService = new PersonsSorterService(_personsRepository, loggerMockSorter.Object, diagnosticContextMock.Object);
            _personsUpdaterService = new PersonsUpdaterService(_personsRepository, loggerMockUpdater.Object, diagnosticContextMock.Object);

            _fixture = new Fixture();
        }

        #region AddPerson
        [Fact]
        public async Task AddPerson_NullPerson_ToBeArgumentNullException()
        {
            //Arrange
            PersonAddRequest? personAddRequest = null;

            //Act
            Func<Task> action = (async () =>
            {
                await _personsAdderService.AddPerson(personAddRequest);
            });

            //Assert
            await action.Should().ThrowAsync<ArgumentNullException>();

        }
        [Fact]
        public async Task AddPerson_PersonNameIsNull_ToBeArgumentException()
        {//Arrange
            var personAddRequest = _fixture.Build<PersonAddRequest>()
                .With(temp => temp.PersonName, null as string)
                .Create();

            var person = personAddRequest.ToPerson();

            _personsRepositoryMock.Setup(temp => temp.AddPerson(It.IsAny<Person>())).ReturnsAsync(person);
            //Act
            Func<Task> action = (async () =>
            {
                await _personsAdderService.AddPerson(personAddRequest);
            });
            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }
        [Fact]
        public async Task AddPerson_FullPersonDetails_ToBeSuccessful()
        {
            //Arrange
            var personAddRequest = _fixture.Build<PersonAddRequest>()
                .With(temp => temp.Email, "a@gmail.com")
                .Create();

            var person = personAddRequest.ToPerson();
            PersonResponse personResponseExpected = person.ToPersonResponse();
            _personsRepositoryMock.Setup(temp => temp.AddPerson(It.IsAny<Person>())).ReturnsAsync(person);
            //Act
            var personResponse = await _personsAdderService.AddPerson(personAddRequest);
            personResponseExpected.PersonID = personResponse.PersonID;
            //Assert
            personResponse.PersonID.Should().NotBe(Guid.Empty);
            personResponse.Should().Be(personResponseExpected);
        }
        #endregion
        #region GetPersonByPersonId
        [Fact]
        public async Task GetPersonByPersonId_NullPersonId_ToBeNull()
        {
            //Arrange
            Guid? personId = null;
            //Act
            PersonResponse? personResponseFromGet =
                await _personsGetterService.GetPersonByPersonID(personId);
            //Assert
            personResponseFromGet.Should().BeNull();
        }
        [Fact]
        public async Task GetPersonByPersonId_ValidPersonId_ToBeSuccessful()
        {

            Person person = _fixture.Build<Person>()
                .With(temp => temp.Email, "b@gmail.com")
                .With(temp => temp.Country, null as Country)
                .Create();

            _personsRepositoryMock.Setup(temp => temp.GetPersonByPersonId(It.IsAny<Guid>())).ReturnsAsync(person);
            var personExpected = person.ToPersonResponse();

            PersonResponse personResponseFromGet = await _personsGetterService.GetPersonByPersonID(person.PersonID);
            //Assert.Equal(personResponse,personResponseFromGet);
            personResponseFromGet.Should().Be(personExpected);
        }
        #endregion
        #region GetAllPersons
        [Fact]
        public async Task GetAllPersons_EmptyList()
        {
            var persons = new List<Person>();

            _personsRepositoryMock.Setup(temp => temp.GetAllPersons()).ReturnsAsync(persons);

            var personsFromGet = await _personsGetterService.GetAllPersons();
            personsFromGet.Should().BeEmpty();
        }
        [Fact]
        public async Task GetAllPersons_AddFewPersons_ToBeSuccessFul()
        {
            var persons = new List<Person>()
            {
                _fixture.Build<Person>()
                .With(temp => temp.Email, "c@gmail.com")
                .With(temp=>temp.Country,null as Country)
                .Create(),

                 _fixture.Build<Person>()
                .With(temp => temp.Email, "d@gmail.com")
                .With(temp=>temp.Country,null as Country)
                .Create()
        };
            var personResponseListFromAdd = persons.Select(temp => temp.ToPersonResponse()).ToList();

            _personsRepositoryMock.Setup(temp => temp.GetAllPersons()).ReturnsAsync(persons);

            List<PersonResponse> personListFromGet = await _personsGetterService.GetAllPersons();

            personListFromGet.Should().BeEquivalentTo(personResponseListFromAdd);

        }
        #endregion
        #region GetFilteredPersons
        [Fact]
        public async Task GetFilteredPersons_EmptySearchText_ToBeSuccessFul()
        {


            var persons = new List<Person>()
            {
                _fixture.Build<Person>()
                .With(temp => temp.Email, "c@gmail.com")
                .With(temp=>temp.Country,null as Country)
                .Create(),

                 _fixture.Build<Person>()
                .With(temp => temp.Email, "d@gmail.com")
                .With(temp=>temp.Country,null as Country)
                .Create()
        };
            var personResponseListExpected = persons.Select(temp => temp.ToPersonResponse()).ToList();

            _personsRepositoryMock.Setup(temp => temp.GetFilteredPersons(It.IsAny<Expression<Func<Person, bool>>>())).ReturnsAsync(persons);

            List<PersonResponse> personListFromGet = await _personsGetterService.GetFilteredPersons(nameof(Person.PersonName), "");

            personListFromGet.Should().BeEquivalentTo(personResponseListExpected);
        }
        [Fact]
        public async Task GetFilteredPersons_SearchByPersonName_ToBeSuccessFul()
        {


            var persons = new List<Person>()
            {
                _fixture.Build<Person>()
                .With(temp => temp.Email, "c@gmail.com")
                .With(temp=>temp.Country,null as Country)
                .Create(),

                 _fixture.Build<Person>()
                .With(temp => temp.Email, "d@gmail.com")
                .With(temp=>temp.Country,null as Country)
                .Create()
        };
            var personResponseListExpected = persons.Select(temp => temp.ToPersonResponse()).ToList();

            _personsRepositoryMock.Setup(temp => temp.GetFilteredPersons(It.IsAny<Expression<Func<Person, bool>>>())).ReturnsAsync(persons);

            List<PersonResponse> personListFromGet = await _personsGetterService.GetFilteredPersons(nameof(Person.PersonName), "sa");

            personListFromGet.Should().BeEquivalentTo(personResponseListExpected);
        }
        #endregion
        #region GetSortedPersons
        [Fact]
        public async Task GetSortedPersons_ToBeSuccessFul()
        {
            //Arrange
            var persons = new List<Person>()
            {
                _fixture.Build<Person>()
                .With(temp => temp.Email, "c@gmail.com")
                .With(temp=>temp.Country,null as Country)
                .Create(),

                 _fixture.Build<Person>()
                .With(temp => temp.Email, "d@gmail.com")
                .With(temp=>temp.Country,null as Country)
                .Create()
        };
            var personResponseListFromAdd = persons.Select(temp => temp.ToPersonResponse()).ToList();
            _personsRepositoryMock.Setup(temp => temp.GetAllPersons()).ReturnsAsync(persons);
            var allPersons = await _personsGetterService.GetAllPersons();

            //Act
            List<PersonResponse> personListFromSort = await _personsSorterService.GetSortedPersons(allPersons, nameof(Person.PersonName), SortOrderOptions.DESC);

            //Assert
            personListFromSort.Should().BeInDescendingOrder(temp => temp.PersonName);
        }
        #endregion
        #region UpdatePerson
        [Fact]
        public async Task UpdatePerson_NullPerson_ToBeArgumentNullException()
        {
            PersonUpdateRequest? personUpdateRequest = null;

            Func<Task> action = (async () =>
            {
                await _personsUpdaterService.UpdatePerson(personUpdateRequest);
            });
            await action.Should().ThrowAsync<ArgumentNullException>();
        }
        [Fact]
        public async Task UpdatePerson_InvalidPersonId_ToBeArgumentException()
        {
            PersonUpdateRequest? personUpdateRequest = _fixture.Create<PersonUpdateRequest>();
            Func<Task> action = (async () =>
            {
                await _personsUpdaterService.UpdatePerson(personUpdateRequest);
            });

            await action.Should().ThrowAsync<ArgumentException>();
        }
        [Fact]
        public async Task UpdatePerson_PersonNameIsNull_ToBeArgumentException()
        {
            //Arrange
            Person person = _fixture.Build<Person>()
                .With(temp => temp.Country, null as Country)
                .With(temp => temp.PersonName, null as string)
                .With(temp => temp.Gender, "Male")
                .With(temp => temp.Email, "c@gmail.com").Create();

            PersonResponse personResponse = person.ToPersonResponse();
            PersonUpdateRequest personUpdateRequest = personResponse.ToPersonUpdateRequest();
            //Act
            Func<Task> action = (async () =>
            {
                await _personsUpdaterService.UpdatePerson(personUpdateRequest);
            });

            await action.Should().ThrowAsync<ArgumentException>();
        }
        [Fact]
        public async Task UpdatePerson_PersonFullDetailsUpdate_ToBeSuccessFul()
        {
            //Arrange

            Person person = _fixture.Build<Person>()
                .With(temp => temp.Country, null as Country)
                .With(temp => temp.Gender, "Male")
                .With(temp => temp.Email, "c@gmail.com").Create();

            PersonResponse personResponse = person.ToPersonResponse();
            PersonUpdateRequest personUpdateRequest = personResponse.ToPersonUpdateRequest();

            _personsRepositoryMock.Setup(temp => temp.UpdatePerson(It.IsAny<Person>())).ReturnsAsync(person);
            _personsRepositoryMock.Setup(temp => temp.GetPersonByPersonId(It.IsAny<Guid>())).ReturnsAsync(person);
            //Act
            PersonResponse personsResponseFromUpdate = await _personsUpdaterService.UpdatePerson(personUpdateRequest);

            //Assert
            personsResponseFromUpdate.Should().Be(personResponse);

        }
        #endregion
        #region DeletePerson
        [Fact]
        public async Task DeletePerson_validPersonId_ToBeSuccessFul()
        {
            //Arrange
            Person person = _fixture.Build<Person>()
                .With(temp => temp.Gender, "Male")
                .With(temp => temp.Country, null as Country)
                .With(temp => temp.Email, "c@gmail.com").Create();

            _personsRepositoryMock.Setup(temp => temp.DeletePersonByPersonId(It.IsAny<Guid>())).ReturnsAsync(true);
            _personsRepositoryMock.Setup(temp => temp.GetPersonByPersonId(It.IsAny<Guid>())).ReturnsAsync(person);
            //Act
            bool isDeleted = await _personsDeleterService.DeletePerson(person.PersonID);
            //Assert
            isDeleted.Should().BeTrue();


        }
        [Fact]
        public async Task DeletePerson_InvalidPersonId()
        {

            bool isDeleted = await _personsDeleterService.DeletePerson(Guid.NewGuid());

            isDeleted.Should().BeFalse();


        }
        #endregion
    }
}
