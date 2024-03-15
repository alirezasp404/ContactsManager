using Fizzler.Systems.HtmlAgilityPack;
using FluentAssertions;
using HtmlAgilityPack;


namespace ContactsManager.IntegrationTests
{
    public class PersonsControllerIntegrationTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        public PersonsControllerIntegrationTest(CustomWebApplicationFactory factory)
        {

            _client = factory.CreateClient();
        }
        #region Index
        [Fact]
        public async void Index_ToReturnView()
        {

            //Arrange

            //Act
            HttpResponseMessage responseMessage = await _client.GetAsync("/Persons/Index");

            //Assert
            responseMessage.Should().BeSuccessful();
            string responseBody = await responseMessage.Content.ReadAsStringAsync();
            HtmlDocument html = new HtmlDocument();
            html.LoadHtml(responseBody);
            var document = html.DocumentNode;
            document.QuerySelectorAll("table.persons").Should().NotBeNull();
        }
        #endregion
    }
}
