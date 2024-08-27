using System.ComponentModel.DataAnnotations;

namespace MyVotingSystem.Models
{
    public class Election
    {

        public Election()
        { }
        
        public Election(
            Candidate candidate1,
            Candidate candidate2,
            string position,
            int candidate1VoteCount,
            int candidate2VoteCount
        )
        {
            Candidate1 = candidate1;
            Candidate2 = candidate2;
            Position = position;
            Candidate1VoteCount = candidate1VoteCount;
            Candidate2VoteCount = candidate2VoteCount;
        }

        public PackedElection convertToFrontEndData()
        {
            string name1 = Candidate1.FirstName + " " + Candidate1.LastName;
            List<string> policies1 = [Candidate1.Policy1, Candidate2.Policy2];
            Candidate c1 = Candidate1;
            bool candidate1Winner = Candidate1VoteCount > Candidate2VoteCount;
            PackedCandidate packedCandidate1 = new PackedCandidate(c1.CandidateId, name1, c1.Party, Candidate1VoteCount, candidate1Winner, c1.Description, policies1, c1.Age);

            string name2 = Candidate2.FirstName + " " + Candidate2.LastName;
            List<string> policies2 = [Candidate2.Policy2, Candidate2.Policy2];
            Candidate c2 = Candidate2;
            bool candidate2Winner = Candidate2VoteCount > Candidate1VoteCount;
            PackedCandidate packedCandidate2 = new PackedCandidate(c2.CandidateId, name2, c2.Party, Candidate2VoteCount, candidate2Winner, c2.Description, policies2, c1.Age);

            return new PackedElection(ElectionId, Position, packedCandidate1, packedCandidate2);

        }

        [Key]
        public int ElectionId { get; set; }
        public int BallotId { get; set; }
        public int Candidate1Id { get; set; }
        public Candidate Candidate1 { get; set; }
        public int Candidate2Id { get; set; }
        public Candidate Candidate2 { get; set; }
        public string Position { get; set; }
        public int Candidate1VoteCount { get; set; }
        public int Candidate2VoteCount { get; set; }
    }
}