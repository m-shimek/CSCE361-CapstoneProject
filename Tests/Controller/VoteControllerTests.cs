using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MyVotingSystem.Accessors;
using MyVotingSystem.Controllers;
using MyVotingSystem.Data;
using MyVotingSystem.Models;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyVotingSystem.Tests.Controller
{
    public class VoteControllerTests
    {
        private async Task<ApplicationDbContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new ApplicationDbContext(options);
            databaseContext.Database.EnsureCreated();
            if (await databaseContext.Users.CountAsync() < 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    databaseContext.Users.Add(
                        new User("johndowe@gmail.com", "test1234", "John", "Dowe")
                    );
                    await databaseContext.SaveChangesAsync();
                }
            }
            return databaseContext;
        }

        [Fact]
        public async void VoteController_GetVoteStatusInt_Success_Test()
        {
            var user = new User("coolguy@gmail.com", "justac00lguy", "Keanu", "Reeves");
            var candidate1 = new Candidate("Policy 1,", "Policy 2", "Description", 55, "Scrooge", "McDuck", "Republican", false);
            var candidate2 = new Candidate("Policy 1,", "Policy 2", "Description", 55, "Mickey", "Mouse", "Democrat", false);
            var election = new Election(candidate1, candidate2, "Secretary of War", 2345242, 234423);
            var vote = new Vote(1, 1, 1);

            var dbContext = await GetDbContext();
            var userAccessor = new UserAccessor(dbContext);
            var candidateAccessor = new CandidateAccessor(dbContext);
            var electionAccessor = new ElectionAccessor(dbContext);
            var voteAccessor = new VoteAccessor(dbContext);
            var voteController = new VoteController(voteAccessor, electionAccessor);

            //Act
            userAccessor.AddUser(user);
            candidateAccessor.AddCandidate(candidate1);
            candidateAccessor.AddCandidate(candidate2);
            electionAccessor.AddElection(election);
            voteAccessor.AddVote(vote);

            var result = voteController.getVoteStatusInt(1, 1);

            //Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public async void VoteController_GetVoteStatusInt_Fail_Test1()
        {
            var user = new User("coolguy@gmail.com", "justac00lguy", "Keanu", "Reeves");
            var candidate1 = new Candidate("Policy 1,", "Policy 2", "Description", 55, "Scrooge", "McDuck", "Republican", false);
            var candidate2 = new Candidate("Policy 1,", "Policy 2", "Description", 55, "Mickey", "Mouse", "Democrat", false);
            var election = new Election(candidate1, candidate2, "Secretary of War", 2345242, 234423);

            var dbContext = await GetDbContext();
            var userAccessor = new UserAccessor(dbContext);
            var candidateAccessor = new CandidateAccessor(dbContext);
            var electionAccessor = new ElectionAccessor(dbContext);
            var voteAccessor = new VoteAccessor(dbContext);
            var voteController = new VoteController(voteAccessor, electionAccessor);

            //Act
            userAccessor.AddUser(user);
            candidateAccessor.AddCandidate(candidate1);
            candidateAccessor.AddCandidate(candidate2);
            electionAccessor.AddElection(election);

            var result = voteController.getVoteStatusInt(3, 1);

            //Assert
            Assert.Equal(-1, result);
        }


        [Fact]
        public async void VoteController_GetVoteStatus_Fail_Test2()
        {
            var user = new User("coolguy@gmail.com", "justac00lguy", "Keanu", "Reeves");
            var candidate1 = new Candidate("Policy 1,", "Policy 2", "Description", 55, "Scrooge", "McDuck", "Republican", false);
            var candidate2 = new Candidate("Policy 1,", "Policy 2", "Description", 55, "Mickey", "Mouse", "Democrat", false);
            var election = new Election(candidate1, candidate2, "Secretary of War", 2345242, 234423);

            var dbContext = await GetDbContext();
            var userAccessor = new UserAccessor(dbContext);
            var candidateAccessor = new CandidateAccessor(dbContext);
            var electionAccessor = new ElectionAccessor(dbContext);
            var voteAccessor = new VoteAccessor(dbContext);
            var voteController = new VoteController(voteAccessor, electionAccessor);

            //Act
            userAccessor.AddUser(user);
            candidateAccessor.AddCandidate(candidate1);
            candidateAccessor.AddCandidate(candidate2);
            electionAccessor.AddElection(election);

            var result = voteController.getVoteStatusInt(1, 3);

            //Assert
            Assert.Equal(-1, result);
        }

        [Fact]
        public async void VoteController_GetVoteStatusInt_Fail_Test3()
        {
            var user1 = new User("coolguy@gmail.com", "justac00lguy", "Keanu", "Reeves");
            var user2 = new User("codedude@gmail.com", "cod3rdude", "Mark", "Zuckerberg");
            var candidate1 = new Candidate("Policy 1,", "Policy 2", "Description", 55, "Scrooge", "McDuck", "Republican", false);
            var candidate2 = new Candidate("Policy 1,", "Policy 2", "Description", 55, "Mickey", "Mouse", "Democrat", false);
            var election = new Election(candidate1, candidate2, "Secretary of War", 2345242, 234423);

            var dbContext = await GetDbContext();
            var userAccessor = new UserAccessor(dbContext);
            var candidateAccessor = new CandidateAccessor(dbContext);
            var electionAccessor = new ElectionAccessor(dbContext);
            var voteAccessor = new VoteAccessor(dbContext);
            var voteController = new VoteController(voteAccessor, electionAccessor);

            //Act
            userAccessor.AddUser(user1);
            userAccessor.AddUser(user2);
            candidateAccessor.AddCandidate(candidate1);
            candidateAccessor.AddCandidate(candidate2);
            electionAccessor.AddElection(election);

            var result = voteController.getVoteStatusInt(2, 1);

            //Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public async void VoteController_SubmitVoteByInt_Success_Test()
        {
            var user = new User("coolguy@gmail.com", "justac00lguy", "Keanu", "Reeves");
            var candidate1 = new Candidate("Policy 1,", "Policy 2", "Description", 55, "Scrooge", "McDuck", "Republican", false);
            var candidate2 = new Candidate("Policy 1,", "Policy 2", "Description", 55, "Mickey", "Mouse", "Democrat", false);
            var election = new Election(candidate1, candidate2, "Secretary of War", 2345242, 234423);

            var dbContext = await GetDbContext();
            var userAccessor = new UserAccessor(dbContext);
            var candidateAccessor = new CandidateAccessor(dbContext);
            var electionAccessor = new ElectionAccessor(dbContext);
            var voteAccessor = new VoteAccessor(dbContext);
            var voteController = new VoteController(voteAccessor, electionAccessor);

            //Act
            userAccessor.AddUser(user);
            candidateAccessor.AddCandidate(candidate1);
            candidateAccessor.AddCandidate(candidate2);
            electionAccessor.AddElection(election);

            var result = voteController.submitVoteByInt(1, 1, 1);

            //Assert
            Assert.Equal(Results.Ok(), result);
        }

        [Fact]
        public async void VoteController_SubmitVoteByInt_Fail_Test()
        {
            var user = new User("coolguy@gmail.com", "justac00lguy", "Keanu", "Reeves");
            var candidate1 = new Candidate("Policy 1,", "Policy 2", "Description", 55, "Scrooge", "McDuck", "Republican", false);
            var candidate2 = new Candidate("Policy 1,", "Policy 2", "Description", 55, "Mickey", "Mouse", "Democrat", false);
            var election = new Election(candidate1, candidate2, "Secretary of War", 2345242, 234423);

            var dbContext = await GetDbContext();
            var userAccessor = new UserAccessor(dbContext);
            var candidateAccessor = new CandidateAccessor(dbContext);
            var electionAccessor = new ElectionAccessor(dbContext);
            var voteAccessor = new VoteAccessor(dbContext);
            var voteController = new VoteController(voteAccessor, electionAccessor);

            //Act
            userAccessor.AddUser(user);
            candidateAccessor.AddCandidate(candidate1);
            candidateAccessor.AddCandidate(candidate2);
            electionAccessor.AddElection(election);

            var result = voteController.submitVoteByInt(1, 1, 4);

            //Assert
            Assert.NotEqual(Results.Ok(), result);
        }
    }
}