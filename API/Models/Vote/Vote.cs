namespace MyVotingSystem.Models
{
    public class Vote
    {
        public Vote(int userId, int electionId, int candidateId)
        {
            UserId = userId;
            ElectionId = electionId;
            CandidateId = candidateId;
        }
        public int? VoteId { get; set; }
        public int ElectionId { get; set; }
        public int CandidateId { get; set; }
        public int UserId { get; set; }
    }
}