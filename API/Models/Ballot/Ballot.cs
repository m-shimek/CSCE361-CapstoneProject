using System.ComponentModel.DataAnnotations;

namespace MyVotingSystem.Models
{
    public class Ballot
    {

        public Ballot(
            DateTime startDate,
            DateTime endDate
        )
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        public PackedBallot convertToFrontEndData()
        {
            List<PackedElection> packedElections = new List<PackedElection>();
            for (int i = 0; i < Elections.Count; i++)
            {
                PackedElection pe = Elections[i].convertToFrontEndData();
                packedElections.Add(pe);
            }
            PackedBallot pb = new PackedBallot(BallotId, EndDate.Year.ToString(), packedElections);
            return pb;
        }

        [Key]
        public int BallotId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Election> Elections { get; set; }
    }
}