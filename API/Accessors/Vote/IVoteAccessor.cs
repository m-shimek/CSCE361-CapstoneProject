using MyVotingSystem.Models;

namespace MyVotingSystem.Accessors
{

    public interface IVoteAccessor
    {
        Vote? GetSpecificElectionVote(int userId, int electionId);
        Vote? GetVoteById(int voteId);
        List<Vote> GetAllVotes();
        bool AddVote(Vote vote);
        bool UpdateVote(Vote vote);
        bool DeleteVote(int voteId);
        bool Save();
        int GetVoteStatus(int userId, int candidateElectionId);
        int GetVoteStatusId(int userId, int electionId);
    }

}