using ContactsManager.Core.Domain.Entities;


namespace ContactsManager.Core.DTO
{
    /// <summary>
    /// DTO Class that is used as return type for most of CountriesService methods
    /// </summary>
    public class CountryResponse
    {
        public Guid CountryID { get; set; }
        public string? CountryName { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null && obj.GetType() == typeof(CountryResponse))
            {
                return false;
            }
            var countryCompare = (CountryResponse)obj;

            return (this.CountryID == countryCompare.CountryID && this.CountryName == countryCompare.CountryName);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    public static class CountryExtensions
    {
        public static CountryResponse ToCountryResponse(this Country country)
        {
            return new CountryResponse { CountryID = country.CountryID, CountryName = country.CountryName };
        }
    }
}
