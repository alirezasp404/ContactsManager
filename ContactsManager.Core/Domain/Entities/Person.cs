using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ContactsManager.Core.Domain.Entities
{
    /// <summary>
    /// Person domain model class
    /// </summary>
    public class Person
    {
        [Key]
        public Guid PersonID { get; set; }

        [StringLength(40)]
        public string? PersonName { get; set; }
        [StringLength(40)]

        public string? Email { get; set; }

        public DateTime? DateOfBirth { get; set; }
        [StringLength(10)]

        public string? Gender { get; set; }

        public Guid? CountryID { get; set; }
        [StringLength(200)]

        public string? Address { get; set; }
        public bool ReceiveNewsLetters { get; set; }

        public string? TIN { get; set; }

        [ForeignKey(nameof(CountryID))]
        public virtual Country? Country { get; set; }
        public override string ToString()
        {
            return $"Person ID :{PersonID},Person Name : {PersonName}," +
                $"Email : {Email},Date Of Birth : {DateOfBirth?.ToString("MM/dd/yyyy")}," +
                $"Gender : {Gender},Country ID : {CountryID},Country : {Country?.CountryName}," +
                $"Address : {Address},Receive News Letters : {ReceiveNewsLetters}";
        }
    }

}
