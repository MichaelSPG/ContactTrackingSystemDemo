using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactTrackingSystem.Shared.Filtering
{
    /// <summary>
    /// Supported comparissions
    /// </summary>
    public enum FilterType
    {
        None,
        Contains,
        Equals,
        NotContains,
        NotEquals,
        StartsWith,
        EndsWith,
    }
    /// <summary>
    /// The mapping of Supported comparissions to string representations
    /// </summary>
    public static class FilterTypeMapping
    {
        static Dictionary<FilterType, string> keyValuePairs = new Dictionary<FilterType, string>
            {
                { FilterType.Equals, "Equals" },
                { FilterType.NotEquals, "Not Equals" },
                { FilterType.Contains, "Contains" },
                { FilterType.NotContains, "Not Contains" },
                { FilterType.StartsWith, "Starts With" },
                { FilterType.EndsWith, "Ends With" }
            };
        public static Dictionary<FilterType, string> GetMapping()
        {
            return keyValuePairs;
        }
        public static string GetString(FilterType filterType)
        {
            return keyValuePairs[filterType];
        }
    }
}
