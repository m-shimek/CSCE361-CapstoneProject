namespace MyVotingSystem.Models
{
    public class PackedElection
    {
        public PackedElection(int id, string position, PackedCandidate candidate1, PackedCandidate candidate2)
        {
            Id = id;
            Position = position;
            Candidates = [candidate1, candidate2];
        }
        public int Id { get; set; }
        public string Position { get; set; }
        public List<PackedCandidate> Candidates { get; set; }
    }
}