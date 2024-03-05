using ContactsManager.Core.ServiceContracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManager.Core.Services
{
    public class CountriesUploaderService : ICountriesUploaderService
    {
        public Task<int> UploadCountriesFromExcelFile(IFormFile formFile)
        {
            throw new NotImplementedException();
        }
    }
}
