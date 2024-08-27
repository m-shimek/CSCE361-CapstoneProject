using MyVotingSystem.Models;

namespace MyVotingSystem.Accessors
{

    public interface ICandidateAccessor
    {
        Candidate? GetCandidateById(int candidateId);
        List<Candidate> GetAllCandidates();
        bool AddCandidate(Candidate candidate);
        bool UpdateCandidate(Candidate candidate);
        bool DeleteCandidate(int candidateId);
        bool Save();
    }
}