using ContactsManager.Core.Domain.Entities;
using ContactsManager.Core.Domain.RepositoryContracts;
using ContactsManager.Core.DTO;
using ContactsManager.Core.ServiceContracts;
using CsvHelper.Configuration;
using CsvHelper;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using Serilog;
using System.Globalization;
using SerilogTimings;


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
        public async Task<List<PersonResponse>> GetAllPersons()
        {
            _logger.LogInformation("GetAllPersons of PersonsService");
            var persons = await _personRepository.GetAllPersons();
            return persons.Select(temp => temp.ToPersonResponse()).ToList();
       

        }

        public async Task<PersonResponse> GetPersonByPersonID(Guid? personID)
        {
            if (personID == null)
                return null;
            Person? person = await _personRepository.GetPersonByPersonId(personID.Value);
            if (person == null)
                return null;

            return person.ToPersonResponse();
        }

        public async Task<List<PersonResponse>> GetFilteredPersons(string searchBy, string? searchString)
        {
            _logger.LogInformation("GetFileteredPersons of PersonsService");
            List<Person> persons;
            using (Operation.Time("Time for Filtered Persons from Database"))
            {
                persons = searchBy switch
                {
                    nameof(PersonResponse.PersonName) =>
                       await _personRepository.GetFilteredPersons(
                            temp => (temp.PersonName.Contains(searchString))),

                    nameof(PersonResponse.Email) =>
                       await _personRepository.GetFilteredPersons(
                            temp => (temp.Email.Contains(searchString))),

                    nameof(PersonResponse.DateOfBirth) =>
                       await _personRepository.GetFilteredPersons(
                            temp => (temp.DateOfBirth.Value.ToString("dd MMMM yyyy").Contains(searchString))),

                    nameof(PersonResponse.Gender) =>
                       await _personRepository.GetFilteredPersons(
                            temp => (temp.Gender.Contains(searchString))),

                    nameof(PersonResponse.CountryID) =>
                       await _personRepository.GetFilteredPersons(
                            temp => (temp.Country.CountryName.Contains(searchString))),

                    nameof(PersonResponse.Address) =>
                       await _personRepository.GetFilteredPersons(
                            temp => (temp.Address.Contains(searchString))),
                    _ => await _personRepository.GetAllPersons()
                };
            }

            _diagnosticContext.Set("Persons", persons);
            return persons.Select(temp => temp.ToPersonResponse()).ToList();
        }




        public async Task<MemoryStream> GetPersonCSV()
        {
            MemoryStream memoryStream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(memoryStream);

            CsvConfiguration csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture);
            CsvWriter csvWriter = new CsvWriter(streamWriter, csvConfiguration);
            csvWriter.WriteHeader<PersonResponse>();
            csvWriter.NextRecord();

            var persons = await GetAllPersons();
            await csvWriter.WriteRecordsAsync(persons);

            memoryStream.Position = 0;
            return memoryStream;

        }

        public async Task<MemoryStream> GetPersonsExcel()
        {
            MemoryStream memoryStream = new MemoryStream();
            using (ExcelPackage excelPackage = new ExcelPackage(memoryStream))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("personsSheet");
                worksheet.Cells["A1"].Value = "Person Name";
                worksheet.Cells["B1"].Value = "Email";
                worksheet.Cells["C1"].Value = "Date Of Birth";
                worksheet.Cells["D1"].Value = "Age";
                worksheet.Cells["E1"].Value = "Gender";
                worksheet.Cells["F1"].Value = "Country";
                worksheet.Cells["G1"].Value = "Address";
                worksheet.Cells["H1"].Value = "Receive News Letters";


                using (ExcelRange headerCells = worksheet.Cells["A1:H1"])
                {
                    headerCells.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    headerCells.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                    headerCells.Style.Font.Bold = true;
                }

                int row = 2;
                var persons = await GetAllPersons();
                foreach (PersonResponse person in persons)
                {
                    worksheet.Cells[row, 1].Value = person.PersonName;
                    worksheet.Cells[row, 2].Value = person.Email;
                    if (person.DateOfBirth.HasValue)
                    {
                        worksheet.Cells[row, 3].Value = person.DateOfBirth.Value.ToString("yyyy-MM-dd");
                    }
                    worksheet.Cells[row, 4].Value = person.Age;
                    worksheet.Cells[row, 5].Value = person.Gender;
                    worksheet.Cells[row, 6].Value = person.Country;
                    worksheet.Cells[row, 7].Value = person.Address;
                    worksheet.Cells[row, 8].Value = person.ReceiveNewsLetters;
                    row++;
                }
                worksheet.Cells[$"A1:H{row}"].AutoFitColumns();
                await excelPackage.SaveAsync();
            }
            memoryStream.Position = 0;
            return memoryStream;

        }
    }
}
