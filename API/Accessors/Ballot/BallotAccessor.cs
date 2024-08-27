using Microsoft.EntityFrameworkCore;
using MyVotingSystem.Data;
using MyVotingSystem.Models;

namespace MyVotingSystem.Accessors
{

    public class BallotAccessor : IBallotAccessor
    {

        private readonly ApplicationDbContext _context;

        public BallotAccessor(ApplicationDbContext context)
        {
            _context = context;
        }

        public Ballot? GetCurrentBallot()
        {
            return _context.Ballots
            .Where(e => e.EndDate >= DateTime.Now && e.StartDate <= DateTime.Now)
                    .Include(b => b.Elections)
                        .ThenInclude(e => e.Candidate1)
                    .Include(b => b.Elections)
                        .ThenInclude(e => e.Candidate2)
                .FirstOrDefault();
        }

        public List<Ballot> GetPastBallots()
        {
            return _context.Ballots
            .Where(e => e.EndDate <= DateTime.Now)
                .Include(b => b.Elections)
                    .ThenInclude(e => e.Candidate1)
                .Include(b => b.Elections)
                    .ThenInclude(e => e.Candidate2).ToList();
        }

        public List<Ballot> GetUpcomingBallots()
        {
            return _context.Ballots
            .Where(e => e.StartDate >= DateTime.Now)
                .Include(b => b.Elections)
                    .ThenInclude(e => e.Candidate1)
                .Include(b => b.Elections)
                    .ThenInclude(e => e.Candidate2).ToList();
        }

        public Ballot? GetBallotById(int electionId)
        {
            return _context.Ballots
            .Where(c => c.BallotId == electionId)
                .Include(b => b.Elections)
                    .ThenInclude(e => e.Candidate1)
                .Include(b => b.Elections)
                    .ThenInclude(e => e.Candidate2)
            .FirstOrDefault();
        }

        public List<Ballot> GetAllBallots()
        {
            return _context.Ballots.Include(c => c.Elections).ToList();
        }

        public bool AddBallot(Ballot election)
        {
            _context.Ballots.Add(election);
            return Save();
        }

        public bool UpdateBallot(Ballot election)
        {
            _context.Entry(election).State = EntityState.Modified;
            return Save();
        }

        public bool DeleteBallot(int electionId)
        {
            var election = _context.Ballots.Find(electionId);
            if (election != null)
            {
                _context.Ballots.Remove(election);
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