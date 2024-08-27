using Microsoft.EntityFrameworkCore;
using MyVotingSystem.Data;
using MyVotingSystem.Models;

namespace MyVotingSystem.Accessors
{
    public class VoteAccessor : IVoteAccessor
    {
        private readonly ApplicationDbContext _context;

        public VoteAccessor(ApplicationDbContext context)
        {
            _context = context;
        }

        public Vote? GetSpecificElectionVote(int userId, int electionId)
        {
            return _context.Votes.Where(v => v.UserId == userId).Where(v => v.ElectionId == electionId).FirstOrDefault();
        }

        public Vote? GetVoteById(int voteId)
        {
            return _context.Votes.Find(voteId);
        }

        public List<Vote> GetAllVotes()
        {
            return _context.Votes.ToList();
        }

        public bool AddVote(Vote vote)
        {
            _context.Votes.Add(vote);
            return Save();
        }

        public bool UpdateVote(Vote vote)
        {
            _context.Entry(vote).State = EntityState.Modified;
            return Save();
        }

        public bool DeleteVote(int voteId)
        {
            var vote = _context.Votes.Find(voteId);
            if (vote != null)
            {
                _context.Votes.Remove(vote);
                return Save();
            }
            return false;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        /// <summary>
        /// See whether a user voted for a certain candidate election, and if so, who they voted for
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="voteId"></param>
        /// <returns> 0 if they didn't vote, 1 if they voted for candidate1, 2 if they voted for candidate2</returns>
        public int GetVoteStatus(int userId, int electionId)
        {
            UserAccessor findUser = new UserAccessor(_context);
            User? user = findUser.GetUserById(userId);
            if (user == null)
            {
                return -1;
            }
            ElectionAccessor findCandidateElection = new ElectionAccessor(_context);
            Election? election = findCandidateElection.GetElectionById(electionId);
            if (election == null)
            {
                return -1;
            }
            Vote? vote = GetSpecificElectionVote(userId, electionId);
            if (vote == null)
            {
                return 0;
            }
            if (vote.CandidateId == election.Candidate1Id)
            {
                return 1;
            }
            if (vote.CandidateId == election.Candidate2Id)
            {
                return 2;
            }
            return 0;
        }

        /// <summary>
        /// See whether a user voted for a certain candidate election, and if so, who they voted for
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="electionId"></param>
        ///  Returns candidateId that they voted for, 0 if the user didn't vote yet, or -1 if there isn't such a user or election
        public int GetVoteStatusId(int userId, int electionId)
        {
            UserAccessor findUser = new UserAccessor(_context);
            User? user = findUser.GetUserById(userId);
            if (user == null)
            {
                return -1;
            }
            ElectionAccessor findCandidateElection = new ElectionAccessor(_context);
            Election? election = findCandidateElection.GetElectionById(electionId);
            if (election == null)
            {
                return -1;
            }
            Vote? vote = GetSpecificElectionVote(userId, electionId);
            if (vote == null)
            {
                return 0;
            }
            else
            {
                return vote.CandidateId;
            }
        }

    }

}

