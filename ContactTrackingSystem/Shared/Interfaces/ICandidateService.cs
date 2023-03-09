using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactTrackingSystem.Shared.Model;

namespace ContactTrackingSystem.Shared.Interfaces
{
    /// <summary>
    /// Some services for a Candidate
    /// </summary>
    public interface ICandidateService
    {
        Task<List<Candidate>> GetAllCandidates();
        Task<Candidate?> AddCandidate(Candidate Candidate);
        Task<Candidate?> GetCandidate(string? id);
        Task<bool> RemoveCandidate(Candidate Candidate);
        Task<bool> EditCandidate(Candidate Candidate);
    }
}
