using ContactsManager.Core.Domain.Entities;
using ContactsManager.Core.Enums;


namespace ContactsManager.Core.DTO
{
    /// <summary>
    /// Represents DTO class that is used as return type of most methods of Persons Service
    /// </summary>
    public class PersonResponse
    {
        public Guid PersonID { get; set; }
        public string? PersonName { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public Guid? CountryID { get; set; }
        public string? Country { get; set; }
        public string? Address { get; set; }
        public bool ReceiveNewsLetters { get; set; }

        public double? Age { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(PersonResponse)) return false;

            var person = (PersonResponse)obj;
            return PersonID == person.PersonID &&
                PersonName == person.PersonName &&
                Email == person.Email &&
                DateOfBirth == person.DateOfBirth &&
                Gender == person.Gender &&
                CountryID == person.CountryID &&
                Address == person.Address &&
                ReceiveNewsLetters == person.ReceiveNewsLetters &&
                Age == person.Age;
        }
        public PersonUpdateRequest ToPersonUpdateRequest()
        {
            return new PersonUpdateRequest()
            {
                PersonID = PersonID,
                PersonName = PersonName,
                Email = Email,
                Gender = (GenderOptions)Enum.Parse(typeof(GenderOptions), Gender, true),
                DateOfBirth = DateOfBirth,
                Address = Address,
                CountryID = CountryID,
                ReceiveNewsLetters = ReceiveNewsLetters
            };
        }

        public override int GetHashCode()
        {
           return base.GetHashCode();
        }
    }
    public static class PersonExtensions
    {
        public static PersonResponse ToPersonResponse(this Person person)
        {
            return new PersonResponse
            {
                PersonID = person.PersonID,
                PersonName = person.PersonName,
                Email = person.Email,
                DateOfBirth = person.DateOfBirth,
                Gender = person.Gender,
                Address = person.Address,
                ReceiveNewsLetters = person.ReceiveNewsLetters,
                Age = (person.DateOfBirth != null) ? Math.Round((DateTime.Now - person.DateOfBirth.Value).TotalDays / 365.25) : null,
                Country = person.Country?.CountryName
            };
        }
    }
}
