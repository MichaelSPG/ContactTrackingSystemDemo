using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactTrackingSystem.Shared.Interfaces
{
    /// <summary>
    /// Interface IDataService: Emulates a generic repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDataService<T>  where T : IBdEntity, new()
    {
        Task<IEnumerable<T>> GetList(string address);
        Task<T?> Add(string address, T entity);
        Task<T?> Get(string address, string? id);
        Task<bool> Remove(string address, T entity);
        Task<bool> Edit(string address, T entity);
    }
}
