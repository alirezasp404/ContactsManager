using AutoFixture;
using ContactsManager.Core.Domain.Entities;
using ContactsManager.Core.Domain.RepositoryContracts;
using ContactsManager.Core.DTO;
using ContactsManager.Core.ServiceContracts;
using ContactsManager.Core.Services;
using FluentAssertions;
using Moq;


namespace ContactsManager.ServiceTests
{
    public class CountriesServiceTest
    {
        private readonly ICountriesRepository _countriesRepository;
        private readonly Mock<ICountriesRepository> _countriesRepositoryMock;
        private readonly ICountriesGetterService _countriesGetterService;
        private readonly ICountriesAdderService _countriesAdderService;
        private readonly IFixture _fixture;
        public CountriesServiceTest()
        {
            _fixture = new Fixture();
            _countriesRepositoryMock = new Mock<ICountriesRepository>();
            _countriesRepository = _countriesRepositoryMock.Object;
            _countriesGetterService = new CountriesGetterService(_countriesRepository);
            _countriesAdderService = new CountriesAdderService(_countriesRepository);
        }
        #region AddCountry
        [Fact]
        public async Task AddCountry_NullCountry_ToBeArgumentNullException()
        {
            //Arrange
            CountryAddRequest? request = null;
            //Act
            Func<Task> action = (async () =>
            {
                await _countriesAdderService.AddCountry(request);
            });
            //Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task AddCountry_CountryNameIsNull_ToBeArgumentException()
        {
            //Arrange
            CountryAddRequest? request = _fixture.Build<CountryAddRequest>()
                .With(temp => temp.CountryName, null as string)
                .Create();
            //Act
            Func<Task> action = (async () =>
            {
                await _countriesAdderService.AddCountry(request);
            });
            //Assert
            await action.Should().ThrowAsync<ArgumentException>();

        }

        [Fact]
        public async Task AddCountry_DuplicateCountryName_ToBeArgumentException()
        {
            //Arrange
            CountryAddRequest? request1 = _fixture.Create<CountryAddRequest>();
            CountryAddRequest? request2 = _fixture.Build<CountryAddRequest>()
                .With(temp => temp.CountryName, request1.CountryName)
                .Create();
            Country country1 = request1.ToCountry();
            Country country2 = request2.ToCountry();
            _countriesRepositoryMock.Setup(temp => temp.AddCountry(It.IsAny<Country>())).ReturnsAsync(country1);
            _countriesRepositoryMock.Setup(temp => temp.GetCountryByCountryName(It.IsAny<string>())).ReturnsAsync(null as Country);
            await _countriesAdderService.AddCountry(request1);

            //Act
            Func<Task> action = (async () =>
            {
                _countriesRepositoryMock.Setup(temp => temp.AddCountry(It.IsAny<Country>())).ReturnsAsync(country2);
                _countriesRepositoryMock.Setup(temp => temp.GetCountryByCountryName(It.IsAny<string>())).ReturnsAsync(country1);
                await _countriesAdderService.AddCountry(request2);
            });
            //Assert
            await action.Should().ThrowAsync<ArgumentException>();

        }

        [Fact]
        public async Task AddCountry_ProperCountryDetails_ToBeSuccessful()
        {
            //Arrange
            CountryAddRequest? countryAddRequest = _fixture.Create<CountryAddRequest>();
            Country country = countryAddRequest.ToCountry();
            country.CountryID = Guid.NewGuid();
            var responseExpected = country.ToCountryResponse();
            _countriesRepositoryMock.Setup(temp => temp.AddCountry(It.IsAny<Country>())).ReturnsAsync(country);
            //Act

            CountryResponse response = await _countriesAdderService.AddCountry(countryAddRequest);

            //Assert
            response.CountryID.Should().NotBe(Guid.Empty);
            response.Should().Be(responseExpected);
        }
        #endregion

        #region GetAllCountries
        [Fact]
        public async Task GetAllCountries_EmptyList_ToBeSuccessFul()
        {
            //Arrange
            _countriesRepositoryMock.Setup(temp => temp.GetAllCountries()).ReturnsAsync(new List<Country>());
            //Act
            List<CountryResponse> actual_country_response_list = await _countriesGetterService.GetAllCountries();

            //Assert
            actual_country_response_list.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAllCountries_AddFewCountries_ToBeSuccessFul()
        {
            //arrange
            var countries = new List<Country> {
            _fixture.Build<Country>()
            .With(temp=>temp.Persons,null as List<Person>)
            .Create(),
            _fixture.Build<Country>()
            .With(temp=>temp.Persons,null as List<Person>)
            .Create()
            };
            var countriesResponse = countries.Select(temp => temp.ToCountryResponse());
            _countriesRepositoryMock.Setup(temp => temp.GetAllCountries()).ReturnsAsync(countries);
            //act
            List<CountryResponse> actual_country_response = await _countriesGetterService.GetAllCountries();

            actual_country_response.Should().BeEquivalentTo(countriesResponse);
        }
        #endregion

        #region GetCountryByCountryId

        [Fact]
        public async Task GetCountryByCountryId_NullCountryId_ToBeSuccessFul()
        {
            //Arrange
            Guid? countryId = null;
            //Act
            CountryResponse? country_response_from_get_method = await _countriesGetterService.GetCountryByID(countryId);
            //Assert
            country_response_from_get_method.Should().BeNull();
        }
        [Fact]
        public async Task GetCountryByCountryId_ValidCountryId()
        {
            //arrange
            Country? country = _fixture.Build<Country>()
            .With(temp => temp.Persons, null as List<Person>)
            .Create();
            _countriesRepositoryMock.Setup(temp => temp.GetCountryByCountryId(It.IsAny<Guid>())).ReturnsAsync(country);
            var expectedCountryResponse = country.ToCountryResponse();
            //act
            CountryResponse? country_response_from_get = await _countriesGetterService.GetCountryByID(country.CountryID);


            //Assert
            country_response_from_get.Should().Be(expectedCountryResponse);


        }

        #endregion
    }
}
