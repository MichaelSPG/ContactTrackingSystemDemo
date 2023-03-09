using ContactTrackingSystem.Shared.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ContactTrackingSystem.Shared.Model
{
    /// <summary>
    /// The Candidate model class
    /// </summary>
    public class Candidate : BdEntity
    {
        public Candidate() { }
        public Candidate(Candidate? other, bool copyId = false)
        {
            Copy(other);
            Id = copyId ? Id : other?.Id;
        }
        /// <summary>
        /// Copies other Cantidate to this and returns a self-reference
        /// </summary>
        /// <param name="otherAbs"></param>
        /// <returns></returns>
        public override IBdEntity? Copy(IBdEntity? otherAbs)
        {
            var other = (Candidate?)otherAbs;
            if (other == null) return otherAbs;

            PhoneNumber = other.PhoneNumber;
            LastName = other.LastName;
            FirstName = other.FirstName;
            Email = other.Email;
            ZipCode = other.ZipCode;
            return this;
        }

        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? ZipCode { get; set; }

    }
}
