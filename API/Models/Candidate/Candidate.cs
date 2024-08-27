using System.ComponentModel.DataAnnotations;

namespace MyVotingSystem.Models
{
    public class Candidate
    {

        public Candidate (
            string policy1,
            string policy2,
            string description,
            int age,
            string firstName,
            string lastName,
            string party,
            bool isIncumbent
        ) {
            Policy1 = policy1;
            Policy2 = policy2;
            Description = description;
            Age = age;
            FirstName = firstName;
            LastName = lastName;
            Party = party;
            IsIncumbent = isIncumbent;
        }

        [Key]
        public int CandidateId { get; set; }
        public string Policy1 {get; set;}
        public string Policy2 {get; set;}
        public string Description {get; set;}
        public int Age {get; set;}
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Party { get; set; }
        public bool IsIncumbent { get; set; }
    }
}