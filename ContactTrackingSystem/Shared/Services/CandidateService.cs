using ContactTrackingSystem.Shared.Data;
using ContactTrackingSystem.Shared.Interfaces;
using ContactTrackingSystem.Shared.Model;

namespace ContactTrackingSystem.Shared.Services
{
    /// <summary>
    /// Implementation of the Candidate Serices, here we have a mix of simulations, one is simulating a conection to a repository and the other for demo purposes is using a local in-memory data
    /// </summary>
    public sealed class CandidateService : ICandidateService
    {
        private readonly string Address = "Candidates";
        public CandidateService(IDataService<Candidate> dataService)
        {
            DataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
        }

        public IDataService<Candidate> DataService { get; }
        /// <summary>
        /// Adds a new candidate
        /// </summary>
        /// <param name="Candidate"></param>
        /// <returns>The reference of the new candidate</returns>
        /// <exception cref="ArgumentNullException">Triggered if the input candidate is null</exception>
        public async Task<Candidate?> AddCandidate(Candidate Candidate)
        {
            if (Candidate == null)
                throw new ArgumentNullException(nameof(Candidate));
            await Task.Delay(1);

            return await DataService.Add(Address, Candidate);
        }
        /// <summary>
        /// Edits a candidate
        /// </summary>
        /// <param name="Candidate">The candidate to edit</param>
        /// <returns>Returns the result of the editing</returns>
        /// <exception cref="ArgumentNullException">Triggered if the input candidate is null</exception>
        public async Task<bool> EditCandidate(Candidate Candidate)
        {
            if (Candidate == null)
                throw new ArgumentNullException(nameof(Candidate));
            var inMemoryResult = ApiUtils.DefaultCandidates.Find(x => x.Id == Candidate.Id);
            if (inMemoryResult != null)
            {
                inMemoryResult.Copy(Candidate);
                return inMemoryResult != null;
            }

            return await DataService.Edit(Address, Candidate);
        }
        /// <summary>
        /// Gets all the services
        /// </summary>
        /// <returns>The list of candidates</returns>
        public async  Task<List<Candidate>> GetAllCandidates()
        {
            var result = (await DataService.GetList(Address)).ToList();
            result.AddRange(ApiUtils.DefaultCandidates);
            return result;
        }
        /// <summary>
        /// Gets a candiadte by id
        /// </summary>
        /// <param name="Id">The id of a candidate to look up</param>
        /// <returns></returns>
        public async Task<Candidate?> GetCandidate(string? Id)
        {
            var inMemoryResult = ApiUtils.DefaultCandidates.Find(x => x.Id.ToString() == Id);
            return inMemoryResult ?? await DataService.Get(Address, Id);
        }
        /// <summary>
        /// Removes a candidate
        /// </summary>
        /// <param name="Candidate">The canditate</param>
        /// <returns>The removing result</returns>
        /// <exception cref="ArgumentNullException">Triggered if the input candidate is null</exception>
        public async Task<bool> RemoveCandidate(Candidate Candidate)
        {
            await Task.Delay(1);
            if (Candidate == null)
                throw new ArgumentNullException(nameof(Candidate));
            var inMemoryResult = ApiUtils.DefaultCandidates.Find(x => x.Id == Candidate.Id);
            if (inMemoryResult != null)
            {
                return ApiUtils.DefaultCandidates.Remove(inMemoryResult);
            }
            return await DataService.Remove(Address, Candidate);
        }
    }
}
