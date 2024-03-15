using AutoFixture;
using ContactsManager.Core.DTO;
using ContactsManager.Core.Enums;
using ContactsManager.Core.ServiceContracts;
using ContactsManager.UI.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManager.ControllerTests
{
    public class PersonsControllerTest
    {
        private readonly IPersonsAdderService _personsAdderService;
        private readonly IPersonsDeleterService _personsDeleterService;
        private readonly IPersonsGetterService _personsGetterService;
        private readonly IPersonsSorterService _personsSorterService;
        private readonly IPersonsUpdaterService _personsUpdaterService;
        private readonly ICountriesGetterService _countriesGetterService;
        private readonly Mock<IPersonsAdderService> _personsAdderServiceMock;
        private readonly Mock<IPersonsDeleterService> _personsDeleterServiceMock;
        private readonly Mock<IPersonsGetterService> _personsGetterServiceMock;
        private readonly Mock<IPersonsSorterService> _personsSorterServiceMock;
        private readonly Mock<IPersonsUpdaterService> _personsUpdaterServiceMock;
        private readonly Mock<ICountriesGetterService> _countriesGetterServiceMock;
        private readonly ILogger<PersonsController> _logger;
        private readonly IFixture _fixture;
        public PersonsControllerTest()
        {
            _fixture = new Fixture();
            var loggerMock = new Mock<ILogger<PersonsController>>();
            _logger = loggerMock.Object;
            _countriesGetterServiceMock = new Mock<ICountriesGetterService>();
            _personsAdderServiceMock = new Mock<IPersonsAdderService>();
            _personsDeleterServiceMock = new Mock<IPersonsDeleterService>();
            _personsSorterServiceMock = new Mock<IPersonsSorterService>();
            _personsUpdaterServiceMock = new Mock<IPersonsUpdaterService>();
            _personsGetterServiceMock = new Mock<IPersonsGetterService>();
            _countriesGetterService = _countriesGetterServiceMock.Object;
            _personsAdderService = _personsAdderServiceMock.Object;
            _personsDeleterService = _personsDeleterServiceMock.Object;
            _personsGetterService = _personsGetterServiceMock.Object;
            _personsSorterService = _personsSorterServiceMock.Object;
            _personsUpdaterService = _personsUpdaterServiceMock.Object;
        }

        #region Index
        [Fact]
        public async Task Index_ShouldReturnIndexViewWithPersonsList()
        {
            //Arrange
            List<PersonResponse> personResponseList = _fixture.Create<List<PersonResponse>>();
            PersonsController personsController = new PersonsController(_personsAdderService, _personsDeleterService, _personsGetterService, _personsSorterService, _personsUpdaterService, _countriesGetterService, _logger);

            _personsGetterServiceMock.Setup(temp => temp.GetFilteredPersons(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(personResponseList);
            _personsSorterServiceMock.Setup(temp => temp.GetSortedPersons(It.IsAny<List<PersonResponse>>(), It.IsAny<string>(), It.IsAny<SortOrderOptions>())).ReturnsAsync(personResponseList);
            //Act
            IActionResult result = await personsController.Index(_fixture.Create<string>(),
                             _fixture.Create<string>(), _fixture.Create<string>(),
                             _fixture.Create<SortOrderOptions>());
            //Assert
            ViewResult viewResult = Assert.IsType<ViewResult>(result);
            viewResult.ViewData.Model.Should().BeAssignableTo<IEnumerable<PersonResponse>>();
            viewResult.ViewData.Model.Should().Be(personResponseList);
        }


        #endregion
        #region Create
        [Fact]
        public async Task Create_IfNoModelErrors_ToReturnRedirectToIndex()
        {
            //Arrange
            PersonAddRequest personAddRequest = _fixture.Create<PersonAddRequest>();
            PersonResponse personResponse = _fixture.Create<PersonResponse>();
            List<CountryResponse> countryResponseList = _fixture.Create<List<CountryResponse>>();


            _countriesGetterServiceMock.Setup(temp => temp.GetAllCountries()).ReturnsAsync(countryResponseList);
            _personsAdderServiceMock.Setup(temp => temp.AddPerson(It.IsAny<PersonAddRequest>())).ReturnsAsync(personResponse);
            PersonsController personsController = new PersonsController(_personsAdderService, _personsDeleterService, _personsGetterService, _personsSorterService, _personsUpdaterService, _countriesGetterService, _logger);



            //Act

            IActionResult result = await personsController.Create(personAddRequest);
            //Assert
            RedirectToActionResult redirectResult = Assert.IsType<RedirectToActionResult>(result);
            redirectResult.ActionName.Should().Be("Index");

        }

        #endregion

    }
}
