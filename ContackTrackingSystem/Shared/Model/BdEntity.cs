using ContactTrackingSystem.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactTrackingSystem.Shared.Model
{
    /// <summary>
    /// Abstract class for BdEntity
    /// </summary>
    public abstract class BdEntity : IBdEntity
    {
        [Key]
        public Guid? Id { get; set; }
        public abstract IBdEntity? Copy(IBdEntity? other);
    }
}
