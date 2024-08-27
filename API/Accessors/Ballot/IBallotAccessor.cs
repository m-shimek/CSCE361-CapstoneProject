using MyVotingSystem.Models;

namespace MyVotingSystem.Accessors
{
    public interface IBallotAccessor
    {
        Ballot? GetCurrentBallot();
        List<Ballot> GetPastBallots();
        List<Ballot> GetUpcomingBallots();
        Ballot? GetBallotById(int electionId);
        List<Ballot> GetAllBallots();
        bool AddBallot(Ballot election);
        bool UpdateBallot(Ballot election);
        bool DeleteBallot(int electionId);
        bool Save();
    }

}