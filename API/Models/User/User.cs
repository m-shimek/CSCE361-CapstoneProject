using System.ComponentModel.DataAnnotations;

namespace MyVotingSystem.Models
{
    public class User
    {
        public User(
            string email,
            string password,
            string firstName,
            string lastName
            )
        {
            Email = email;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
        }
        public User(
            string email,
            string password,
            string firstName,
            string lastName,
            DateTime? bday,
            string? partyAffiliation
            )
        {
            Email = email;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            if (bday != null)
            {
                DateOfBirth = bday;
            }
            if (partyAffiliation != null)
            {
                Affiliation = partyAffiliation;
            }
        }
        [Key]
        public int? UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool IsVerified { get; set; }
        public string? Affiliation { get; set; }
        public ICollection<Vote>? Votes { get; set; }
    }
}