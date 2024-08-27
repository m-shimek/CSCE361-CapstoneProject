using MyVotingSystem.Models;

namespace MyVotingSystem.Accessors
{

    public interface IElectionAccessor
    {
        Election? GetElectionById(int electionId);
        List<Election> GetAllElections();
        bool AddElection(Election election);
        bool UpdateElection(Election election);
        bool DeleteElection(int electionId);
        bool Save();
    }

}