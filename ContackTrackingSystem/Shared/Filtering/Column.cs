using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactTrackingSystem.Shared.Filtering
{
    /// <summary>
    /// A column to use filtering in
    /// </summary>
    public class Column
    {
        public string? Name { get; set; }
        public string? PropertyName { get; set; }
        public bool Focused{ get; set; }
        public bool OrderDesc { get; set; }
    }
}
