namespace MyVotingSystem.Models
{
    public class PackedCandidate
    {
        public PackedCandidate(int id, string name, string party, int votes, bool isWinninger, string description, List<string> policies, int age){
            Id = id;
            Name = name;
            Party = party;
            Votes = votes;
            IsWinner = isWinninger;
            Description = description;
            PriorityPolicies = policies;
            Age = age;
        }
        public int Id {get; set;}
        public string Name {get; set;}
        public int Age {get; set;}
        public string Party {get; set;}
        public int Votes {get; set;}
        public bool IsWinner {get; set;}
        public string Description {get; set;}
        public List<string> PriorityPolicies {get; set;}
    }
}