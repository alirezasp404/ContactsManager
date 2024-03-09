using ContactsManager.Core.DTO;
using ContactsManager.Core.Enums;
using ContactsManager.UI.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ContactsManager.UI.Filters.ActionFilters
{
    public class PersonsListActionFilter : IActionFilter
    {
        private ILogger<PersonsListActionFilter> _logger;
        public PersonsListActionFilter(ILogger<PersonsListActionFilter> logger)
        {
            _logger = logger;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("{FilterName}.{ActionName} method", nameof(PersonsListActionFilter), nameof(OnActionExecuted));
            PersonsController personsController = (PersonsController)context.Controller;
            var parameters = (IDictionary<string, object?>?)context.HttpContext.Items["arguments"];
            if (parameters != null && parameters.ContainsKey("searchBy"))
            {
                personsController.ViewData["CurrentSearchBy"] = parameters["searchBy"];
            }
            if (parameters != null && parameters.ContainsKey("searchString"))
            {
                personsController.ViewData["CurrentSearchString"] = parameters["searchString"];
            }

            if (parameters != null && parameters.ContainsKey("sortBy"))
            {
                personsController.ViewData["CurrentSortBy"] = parameters["sortBy"];
            }
            else
            {
                personsController.ViewData["CurrentSortBy"] = nameof(PersonResponse.PersonName);

            }
            if (parameters != null && parameters.ContainsKey("sortOrder"))
            {
                personsController.ViewData["CurrentSortOrder"] = parameters["sortOrder"]?.ToString();
            }
            else
            {
                personsController.ViewData["CurrentSortOrder"] = nameof(SortOrderOptions.ASC);
            }
            personsController.ViewBag.SearchFields = new Dictionary<string, string>()
            {
                { nameof(PersonResponse.PersonName),"Person Name" },
                { nameof(PersonResponse.Email),"Email" },
                { nameof(PersonResponse.DateOfBirth),"Date Of Birth" },
                { nameof(PersonResponse.Gender),"Gender" },
                { nameof(PersonResponse.Country),"Country" },
                { nameof(PersonResponse.Address),"Address" },
            };

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("{FilterName}.{ActionName} method", nameof(PersonsListActionFilter), nameof(OnActionExecuting));
            context.HttpContext.Items["arguments"] = context.ActionArguments;

            if (context.ActionArguments.ContainsKey("searchBy"))
            {
                var searchBy = Convert.ToString(context.ActionArguments["searchBy"]);
                if (!string.IsNullOrEmpty(searchBy))
                {
                    var searchByOptions = new List<string>()
                    {
                        nameof(PersonResponse.PersonName),
                        nameof(PersonResponse.Email),
                        nameof(PersonResponse.DateOfBirth),
                        nameof(PersonResponse.Gender),
                        nameof(PersonResponse.CountryID),
                        nameof(PersonResponse.Address),
                    };
                    if (searchByOptions.Any(temp => temp == searchBy) == false)
                    {
                        _logger.LogInformation("searchBy actual value {searchBy}", searchBy);
                        context.ActionArguments["searchBy"] = nameof(PersonResponse.PersonName);
                        _logger.LogInformation("searchBy updated value {searchBy}", context.ActionArguments["searchBy"]);
                    }

                }
            }
        }
    }
}
