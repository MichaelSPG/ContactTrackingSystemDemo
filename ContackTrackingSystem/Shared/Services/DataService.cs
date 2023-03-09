using ContactTrackingSystem.Shared.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace ContactTrackingSystem.Shared.Interfaces
{
    /// <summary>
    /// Implementation of IDataService<T>
    /// </summary>
    /// <typeparam name="T">The generic type for a class, mist inherits from the base entity IBdEntity</typeparam>
    public class DataService<T> : IDataService<T> where T : IBdEntity, new()
    {
        public DataService(IMemoryCache memoryCache)
        {
            this.MemoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }
        IMemoryCache MemoryCache { get; set; }

        List<T> GetData(string address)
        {
            var entityList = MemoryCache.Get<List<T>>(address);
            if (entityList == null)
            {
                entityList = new List<T>();
            }
            return entityList;
        }
        List<T> SetData(string address, List<T> data)
        {
            return MemoryCache.Set(address, data);
        }
        public async Task<T?> Add(string address, T entity)
        {
            var entityList = GetData(address);
            
            if (!entity.Id.HasValue && entityList.Find(x => x.Id.Equals(entity.Id)) == null)
            {
                entity.Id = Guid.NewGuid();
                entityList.Add(entity);
            }
            SetData(address, entityList);
            return entity;
        }
        public async Task<bool> Edit(string address, T entity)
        {
            var original = await Get(address, entity.Id.ToString());
            return original?.Copy(entity) != null;
        }
        public Task<T?> Get(string address, string? id)
        {
            var entityList = MemoryCache.Get<List<T>>(address);
            if (entityList == null)
            {
                entityList = new List<T>();
            }
            return Task.FromResult(entityList.Find(x => x.Id == x.Id));
        }
        public Task<IEnumerable<T>> GetList(string address)
        {
            var entityList = MemoryCache.Get<List<T>>(address);
            if (entityList == null)
            {
                entityList = new List<T>();
            }
            return Task.FromResult<IEnumerable<T>>(entityList);
        }
        public Task<bool> Remove(string address, T entity)
        {
            var entityList = MemoryCache.Get<List<T>>(address);
            if (entityList == null)
            {
                entityList = new List<T>();
            }
            bool result = entityList.Remove(entity);
            
            return Task.FromResult(result);
        }
    }
}
