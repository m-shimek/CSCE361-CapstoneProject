using Microsoft.EntityFrameworkCore;
using MyVotingSystem.Data;
using MyVotingSystem.Models;

namespace MyVotingSystem.Accessors
{

    public class ElectionAccessor : IElectionAccessor
    {

        private readonly ApplicationDbContext _context;

        public ElectionAccessor(ApplicationDbContext context)
        {
            _context = context;
        }

        public Election? GetElectionById(int electionId)
        {
            return _context.Elections.Find(electionId);
        }

        public List<Election> GetAllElections()
        {
            return _context.Elections.ToList();
        }

        public bool AddElection(Election election)
        {
            _context.Elections.Add(election);
            return Save();
        }

        public bool UpdateElection(Election election)
        {
            _context.Entry(election).State = EntityState.Modified;
            return Save();
        }

        public bool DeleteElection(int electionId)
        {
            var election = _context.Elections.Find(electionId);
            if (election != null)
            {
                _context.Elections.Remove(election);
                return Save();
            }
            return false;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}