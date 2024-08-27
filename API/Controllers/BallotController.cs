using Microsoft.AspNetCore.Mvc;
using MyVotingSystem.Accessors;
using MyVotingSystem.Data;
using MyVotingSystem.Models;

namespace MyVotingSystem.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class BallotController : ControllerBase
    {
        private readonly IBallotAccessor _ballotAccessor;

        public BallotController(IBallotAccessor ballotAccessor)
        {
            _ballotAccessor = ballotAccessor;
        }

        [HttpGet]
        public IResult getCurrentBallot()
        {
            Ballot? currentBallot = _ballotAccessor.GetCurrentBallot();
            if (currentBallot == null)
            {
                return Results.Json(new { }, statusCode: 200);
            }
            else
            {
                PackedBallot x = currentBallot.convertToFrontEndData();
                return Results.Json(x, statusCode: 200);
            }
        }

        [HttpGet]
        public IResult getPastBallots()
        {
            List<Ballot> pastBallots = _ballotAccessor.GetPastBallots();
            if (pastBallots == null)
            {
                return Results.Json(new { }, statusCode: 200);
            }
            else
            {
                List<PackedBallot> packedPastBallots = new List<PackedBallot>();
                for (int i = 0; i < pastBallots.Count; i++)
                {
                    packedPastBallots.Add(pastBallots[i].convertToFrontEndData());
                }
                return Results.Json(packedPastBallots, statusCode: 200);
            }
        }

        [HttpGet]
        public IResult getUpcomingBallots()
        {
            List<Ballot> pastBallots = _ballotAccessor.GetUpcomingBallots();
            if (pastBallots == null)
            {
                return Results.Json(new { }, statusCode: 200);
            }
            else
            {
                List<PackedBallot> packedPastBallots = new List<PackedBallot>();
                for (int i = 0; i < pastBallots.Count; i++)
                {
                    packedPastBallots.Add(pastBallots[i].convertToFrontEndData());
                }
                return Results.Json(packedPastBallots, statusCode: 200);
            }
        }
    }
}