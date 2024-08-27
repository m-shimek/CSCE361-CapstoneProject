using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.Json;
using MyVotingSystem.Accessors;
using MyVotingSystem.Data;
using MyVotingSystem.Models;


namespace MyVotingSystem.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class VoteController : ControllerBase
    {
        private readonly IVoteAccessor _voteAccessor;
        private readonly IElectionAccessor _electionAccessor;

        public VoteController(IVoteAccessor voteAccessor, IElectionAccessor electionAccessor)
        {
            _voteAccessor = voteAccessor;
            _electionAccessor = electionAccessor;
        }

        [HttpGet]
        public int getVoteStatusInt(int userId, int electionId)
        {
            return _voteAccessor.GetVoteStatus(userId, electionId);
        }

        [HttpGet]
        public IResult getVoteStatusId(int userId, int electionId)
        {
            int candidateId = _voteAccessor.GetVoteStatusId(userId, electionId);
            if (candidateId != -1)
            {
                return Results.Json(candidateId);
            }
            else
            {
                return Results.BadRequest("No such user or election.");
            }
        }

        [HttpPost]
        public IResult submitVoteById(Vote v)
        {
            int userId = v.UserId;
            int candidateId = v.CandidateId;
            int electionId = v.ElectionId;

            Election? election = _electionAccessor.GetElectionById(electionId);
            if (election == null)
            {
                return Results.BadRequest("No such election.");
            }
            if (_voteAccessor.GetSpecificElectionVote(userId, electionId) != null)
            {
                return Results.BadRequest("User already voted.");
            }
            Vote vote = new Vote(userId, electionId, candidateId);
            _voteAccessor.AddVote(vote);
            if (candidateId == election.Candidate1Id)
            {
                election.Candidate1VoteCount++;
            }
            else if (candidateId == election.Candidate2Id)
            {
                election.Candidate2VoteCount++;
            }
            _electionAccessor.UpdateElection(election);
            return Results.Ok();
        }

        [HttpPost]
        public IResult submitVoteByInt(int electionId, int userId, int candidate)
        {
            int candidateId;
            Election election = _electionAccessor.GetElectionById(electionId);
            if (_voteAccessor.GetSpecificElectionVote(userId, electionId) != null)
            {
                return Results.BadRequest("User already voted brah");
            }
            if (candidate == 1)
            {
                candidateId = election.Candidate1Id;
                election.Candidate1VoteCount += 1;
            }
            else if (candidate == 2)
            {
                candidateId = election.Candidate2Id;
                election.Candidate2VoteCount += 1;
            }
            else
            {
                return Results.BadRequest("Candidate has to be 1 or 2 brah");
            }

            Vote vote = new Vote(userId, electionId, candidateId);
            _voteAccessor.AddVote(vote);
            _electionAccessor.UpdateElection(election);
            return Results.Ok();
        }
    }
}