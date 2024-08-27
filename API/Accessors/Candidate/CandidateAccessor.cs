using Microsoft.EntityFrameworkCore;
using MyVotingSystem.Data;
using MyVotingSystem.Models;

namespace MyVotingSystem.Accessors
{

    public class CandidateAccessor : ICandidateAccessor
    {

        private readonly ApplicationDbContext _context;

        public CandidateAccessor(ApplicationDbContext context)
        {
            _context = context;
        }

        public Candidate? GetCandidateById(int candidateId)
        {
            return _context.Candidates.Find(candidateId);
        }

        public List<Candidate> GetAllCandidates()
        {
            return _context.Candidates.ToList();
        }

        public bool AddCandidate(Candidate candidate)
        {
            _context.Candidates.Add(candidate);
            return Save();
        }

        public bool UpdateCandidate(Candidate candidate)
        {
            _context.Entry(candidate).State = EntityState.Modified;
            return Save();
        }

        public bool DeleteCandidate(int candidateId)
        {
            var candidate = _context.Candidates.Find(candidateId);
            if (candidate != null)
            {
                _context.Candidates.Remove(candidate);
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