using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactTrackingSystem.Shared.Filtering
{
    /// <summary>
    /// Supported operators
    /// </summary>
    public enum ConnectorType
    {
        None,
        And,
        Or,
    }
    /// <summary>
    /// Mapping of supported operators to their string representation
    /// </summary>
    public static class ConnectionTypeMapping
    {
        static Dictionary<ConnectorType, string> keyValuePairs = new Dictionary<ConnectorType, string>
            {
                { ConnectorType.And, "&&" },
                { ConnectorType.Or, "||" },
            };
        /// <summary>
        /// Gets the string for a connector type
        /// </summary>
        /// <param name="connectorType">The connectorType enumeration value</param>
        /// <returns> String representation of ConnectorType</returns>
        public static string GetString(ConnectorType connectorType)
        {
            return keyValuePairs[connectorType];
        }
        
    }
}
