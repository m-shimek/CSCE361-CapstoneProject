namespace MyVotingSystem.Models
{
    public class PackedBallot
    {
        public PackedBallot(int ballotId, string year, List<PackedElection> eles)
        {
            Id = ballotId;
            Year = year;
            Elections = eles;
        }
        
        public int Id { get; set; }
        public string Year { get; set; }
        public List<PackedElection> Elections { get; set; }
    }
}