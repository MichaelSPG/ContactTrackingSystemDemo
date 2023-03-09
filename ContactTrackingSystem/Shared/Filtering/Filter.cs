using ContactTrackingSystem.Shared.Filtering;
using System.ComponentModel.DataAnnotations;

namespace ContactTrackingSystem.Shared.Filtering
{
    /// <summary>
    /// Filter class, to perform filtering with some basic variables
    /// </summary>
    public class Filter
    {
        [Required]
        public Guid Id { get; set; }
        public Filter()
        {
            Source = "";
            Value = "";
            Id = Guid.NewGuid();
            MatchCase = true;
        }
        public Filter(Filter other)
        {
            CopyFrom(other);
        }
        public Filter CopyFrom(Filter other)
        {
            this.FilterType = other.FilterType;
            this.Value = other.Value;
            this.Source = other.Source;
            this.ConnectorType = other.ConnectorType;
            this.MatchCase= other.MatchCase;
            return this;
        }
        /// <summary>
        /// Source to compare
        /// </summary>
        [Required]
        public string? Source { get; set; }
        /// <summary>
        /// The variable value to compare with the source
        /// </summary>
        [Required]
        public string? Value { get; set; }
        /// <summary>
        /// Describes if we are going to care about character casing
        /// </summary>
        [Required]
        public bool MatchCase{ get; set; }
        /// <summary>
        /// The type of a filter
        /// </summary>
        [Required]
        public FilterType? FilterType { get; set; }
        /// <summary>
        /// The connector type of a filter to conecto to next filter
        /// </summary>
        public ConnectorType? ConnectorType { get; set; }
    }
}
