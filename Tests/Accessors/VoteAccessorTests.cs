using Microsoft.EntityFrameworkCore;
using MyVotingSystem.Accessors;
using MyVotingSystem.Data;
using MyVotingSystem.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

/**
* Since votes have the most dependencies, this test class uses
* other accessors quite often. This is not in violation of SOLID
* Since Vote depends on all the other classes. It wouldn't make sense
* to have a vote without an election and candidates
*/

namespace MyVotingSystem.Tests.Accessors
{
    public class VoteAccessorTests
    {
        // since votes are so complicated, this dbcontext adds users for simplicity
        // this function sets up the "fake" database, so it's not essential that it uses votes
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
        public async void VoteAccessor_GetVoteById_Success_Test()
        {
            var user = new User("steveasmith@gmail.com", "espn4LIfe", "Steven", "ASmith");
            var candidate1 = new Candidate("Policy 1,", "Policy 2", "Description", 55, "James", "Polk", "Democrat", false);
            var candidate2 = new Candidate("Policy 1,", "Policy 2", "Description", 55, "John", "Wilkes-Booth", "Democrat", false);
            var election = new Election(candidate1, candidate2, "President", 54352, 74634);
            var vote = new Vote(1, 1, 2);

            var dbContext = await GetDbContext();
            var userAccessor = new UserAccessor(dbContext);
            var candidateAccessor = new CandidateAccessor(dbContext);
            var electionAccessor = new ElectionAccessor(dbContext);
            var voteAccessor = new VoteAccessor(dbContext);

            //Act
            userAccessor.AddUser(user);
            candidateAccessor.AddCandidate(candidate1);
            candidateAccessor.AddCandidate(candidate2);
            electionAccessor.AddElection(election);
            voteAccessor.AddVote(vote);
            var result = voteAccessor.GetVoteById(1);

            //Assert
            Assert.Equal(vote, result);
        }

        [Fact]
        public async void VoteAccessor_GetVoteById_Fail_Test()
        {
            var user = new User("stevesmith@gmail.com", "iateapie4", "Steve", "Smith");
            var candidate1 = new Candidate("Policy 1,", "Policy 2", "Description", 55, "Richard", "Nixon", "Republican", false);
            var candidate2 = new Candidate("Policy 1,", "Policy 2", "Description", 55, "Barack", "Obama", "Democrat", true);
            var election = new Election(candidate1, candidate2, "President", 12345, 23456);
            var vote = new Vote(1, 1, 1);

            var dbContext = await GetDbContext();
            var userAccessor = new UserAccessor(dbContext);
            var candidateAccessor = new CandidateAccessor(dbContext);
            var electionAccessor = new ElectionAccessor(dbContext);
            var voteAccessor = new VoteAccessor(dbContext);

            //Act
            userAccessor.AddUser(user);
            candidateAccessor.AddCandidate(candidate1);
            candidateAccessor.AddCandidate(candidate2);
            electionAccessor.AddElection(election);
            voteAccessor.AddVote(vote);
            var result = voteAccessor.GetVoteById(5);

            //Assert
            Assert.False(vote.Equals(result));
        }

        [Fact]
        public async void VoteAccessor_GetSpecificElectionVote_Success_Test()
        {
            var user = new User("jimjohn@gmail.com", "pizzaguym4n", "Jim", "John");
            var candidate1 = new Candidate("Policy 1,", "Policy 2", "Description", 55, "Richard", "Nixon", "Republican", false);
            var candidate2 = new Candidate("Policy 1,", "Policy 2", "Description", 55, "Barack", "Obama", "Democrat", true);
            var election = new Election(candidate1, candidate2, "President", 12345, 23456);
            var vote = new Vote(1, 1, 1);

            var dbContext = await GetDbContext();
            var userAccessor = new UserAccessor(dbContext);
            var candidateAccessor = new CandidateAccessor(dbContext);
            var electionAccessor = new ElectionAccessor(dbContext);
            var voteAccessor = new VoteAccessor(dbContext);

            //Act
            userAccessor.AddUser(user);
            candidateAccessor.AddCandidate(candidate1);
            candidateAccessor.AddCandidate(candidate2);
            electionAccessor.AddElection(election);
            voteAccessor.AddVote(vote);
            var result = voteAccessor.GetSpecificElectionVote(1, 1);

            //Assert
            Assert.Equal(vote, result);
        }

        [Fact]
        public async void VoteAccessor_GetSpecificVote_Fail_Test1()
        {
            var user = new User("jimjohn@gmail.com", "pizzaguym4n", "Jim", "John");
            var candidate1 = new Candidate("Policy 1,", "Policy 2", "Description", 55, "Richard", "Nixon", "Republican", false);
            var candidate2 = new Candidate("Policy 1,", "Policy 2", "Description", 55, "Barack", "Obama", "Democrat", true);
            var election = new Election(candidate1, candidate2, "President", 12345, 23456);
            var vote = new Vote(1, 1, 1);

            var dbContext = await GetDbContext();
            var userAccessor = new UserAccessor(dbContext);
            var candidateAccessor = new CandidateAccessor(dbContext);
            var electionAccessor = new ElectionAccessor(dbContext);
            var voteAccessor = new VoteAccessor(dbContext);

            //Act
            userAccessor.AddUser(user);
            candidateAccessor.AddCandidate(candidate1);
            candidateAccessor.AddCandidate(candidate2);
            electionAccessor.AddElection(election);
            voteAccessor.AddVote(vote);
            var result = voteAccessor.GetSpecificElectionVote(4, 1);

            //Assert
            Assert.False(vote.Equals(result));
        }

        [Fact]
        public async void VoteAccessor_GetSpecificVote_Fail_Test2()
        {
            var user = new User("jimjohn@gmail.com", "pizzaguym4n", "Jim", "John");
            var candidate1 = new Candidate("Policy 1,", "Policy 2", "Description", 55, "Richard", "Nixon", "Republican", false);
            var candidate2 = new Candidate("Policy 1,", "Policy 2", "Description", 55, "Barack", "Obama", "Democrat", true);
            var election = new Election(candidate1, candidate2, "President", 12345, 23456);
            var vote = new Vote(1, 1, 2);

            var dbContext = await GetDbContext();
            var userAccessor = new UserAccessor(dbContext);
            var candidateAccessor = new CandidateAccessor(dbContext);
            var electionAccessor = new ElectionAccessor(dbContext);
            var voteAccessor = new VoteAccessor(dbContext);

            //Act
            userAccessor.AddUser(user);
            candidateAccessor.AddCandidate(candidate1);
            candidateAccessor.AddCandidate(candidate2);
            electionAccessor.AddElection(election);
            voteAccessor.AddVote(vote);
            var result = voteAccessor.GetSpecificElectionVote(1, 4);

            //Assert
            Assert.False(vote.Equals(result));
        }

        [Fact]
        public async void VoteAccessor_GetAllVotes_Success_Test()
        {
            var user1 = new User("stevesmith@gmail.com", "iateapie4", "Steve", "Smith");
            var user2 = new User("johndoe@gmail.com", "my1name2is3john4doe", "John", "Doe");
            var user3 = new User("mrnewyork@gmail.com", "novaboys2016", "Jalen", "Brunson");
            var candidate1 = new Candidate("Policy 1,", "Policy 2", "Description", 55, "Ronald", "Reagan", "Republican", true);
            var candidate2 = new Candidate("Policy 1,", "Policy 2", "Description", 55, "Walter", "Mondale", "Democrat", false);
            var election = new Election(candidate1, candidate2, "President", 2345235, 1);
            var vote1 = new Vote(1, 1, 1);
            var vote2 = new Vote(2, 1, 2);
            var vote3 = new Vote(3, 1, 1);

            var dbContext = await GetDbContext();
            var userAccessor = new UserAccessor(dbContext);
            var candidateAccessor = new CandidateAccessor(dbContext);
            var electionAccessor = new ElectionAccessor(dbContext);
            var voteAccessor = new VoteAccessor(dbContext);

            //Act
            userAccessor.AddUser(user1);
            userAccessor.AddUser(user2);
            userAccessor.AddUser(user3);
            candidateAccessor.AddCandidate(candidate1);
            candidateAccessor.AddCandidate(candidate2);
            electionAccessor.AddElection(election);
            voteAccessor.AddVote(vote1);
            voteAccessor.AddVote(vote2);
            voteAccessor.AddVote(vote3);
            List<Vote> voteList = [vote1, vote2, vote3];

            var result = voteAccessor.GetAllVotes();

            //Assert
            Assert.Equal(voteList, result);
        }

        [Fact]
        public async void VoteAccessor_Add_Success_Test1()
        {
            var user = new User("jimjohn@gmail.com", "pizzaguym4n", "Jim", "John");
            var candidate1 = new Candidate("Policy 1,", "Policy 2", "Description", 55, "Richard", "Nixon", "Republican", false);
            var candidate2 = new Candidate("Policy 1,", "Policy 2", "Description", 55, "Barack", "Obama", "Democrat", true);
            var election = new Election(candidate1, candidate2, "President", 12345, 23456);
            var vote = new Vote(1, 1, 1);

            var dbContext = await GetDbContext();
            var userAccessor = new UserAccessor(dbContext);
            var candidateAccessor = new CandidateAccessor(dbContext);
            var electionAccessor = new ElectionAccessor(dbContext);
            var voteAccessor = new VoteAccessor(dbContext);

            //Act
            userAccessor.AddUser(user);
            candidateAccessor.AddCandidate(candidate1);
            candidateAccessor.AddCandidate(candidate2);
            electionAccessor.AddElection(election);
            var result = voteAccessor.AddVote(vote);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async void VoteAccessor_Add_Success_Test2()
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

            //Act
            userAccessor.AddUser(user);
            candidateAccessor.AddCandidate(candidate1);
            candidateAccessor.AddCandidate(candidate2);
            electionAccessor.AddElection(election);
            var result = voteAccessor.AddVote(vote);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async void VoteAccessor_Update_Success_Test1()
        {
            var user1 = new User("jimjohn@gmail.com", "pizzaguym4n", "Jim", "John");
            var user2 = new User("jameothyhames@gmail.com", "crazypassword9", "Gerald", "Wallace");
            var candidate1 = new Candidate("Policy 1,", "Policy 2", "Description", 55, "Richard", "Nixon", "Republican", false);
            var candidate2 = new Candidate("Policy 1,", "Policy 2", "Description", 55, "Barack", "Obama", "Democrat", true);
            var election = new Election(candidate1, candidate2, "President", 12345, 23456);
            var vote = new Vote(1, 1, 1);

            var dbContext = await GetDbContext();
            var userAccessor = new UserAccessor(dbContext);
            var candidateAccessor = new CandidateAccessor(dbContext);
            var electionAccessor = new ElectionAccessor(dbContext);
            var voteAccessor = new VoteAccessor(dbContext);

            //Act
            userAccessor.AddUser(user1);
            userAccessor.AddUser(user2);
            candidateAccessor.AddCandidate(candidate1);
            candidateAccessor.AddCandidate(candidate2);
            electionAccessor.AddElection(election);

            voteAccessor.AddVote(vote);
            vote.UserId = 2;
            var result = voteAccessor.UpdateVote(vote);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async void VoteAccessor_Update_Success_Test2()
        {
            var user = new User("jimjohn@gmail.com", "pizzaguym4n", "Jim", "John");
            var candidate1 = new Candidate("Policy 1,", "Policy 2", "Description", 55, "Richard", "Nixon", "Republican", false);
            var candidate2 = new Candidate("Policy 1,", "Policy 2", "Description", 55, "Barack", "Obama", "Democrat", true);
            var candidate3 = new Candidate("Policy 1,", "Policy 2", "Description", 55, "Mitt", "Romney", "Republican", false);
            var election1 = new Election(candidate1, candidate2, "President", 12345, 23456);
            var election2 = new Election(candidate2, candidate3, "President", 123123, 12342);
            var vote = new Vote(1, 1, 1);

            var dbContext = await GetDbContext();
            var userAccessor = new UserAccessor(dbContext);
            var candidateAccessor = new CandidateAccessor(dbContext);
            var electionAccessor = new ElectionAccessor(dbContext);
            var voteAccessor = new VoteAccessor(dbContext);

            //Act
            userAccessor.AddUser(user);
            candidateAccessor.AddCandidate(candidate1);
            candidateAccessor.AddCandidate(candidate2);
            candidateAccessor.AddCandidate(candidate3);
            electionAccessor.AddElection(election1);
            electionAccessor.AddElection(election2);

            voteAccessor.AddVote(vote);
            vote.ElectionId = 2;
            var result = voteAccessor.UpdateVote(vote);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async void VoteAccessor_Update_Success_Test3()
        {
            var user = new User("jimjohn@gmail.com", "pizzaguym4n", "Jim", "John");
            var candidate1 = new Candidate("Policy 1,", "Policy 2", "Description", 55, "Richard", "Nixon", "Republican", false);
            var candidate2 = new Candidate("Policy 1,", "Policy 2", "Description", 55, "Barack", "Obama", "Democrat", true);
            var election = new Election(candidate1, candidate2, "President", 12345, 23456);
            var vote = new Vote(1, 1, 1);

            var dbContext = await GetDbContext();
            var userAccessor = new UserAccessor(dbContext);
            var candidateAccessor = new CandidateAccessor(dbContext);
            var electionAccessor = new ElectionAccessor(dbContext);
            var voteAccessor = new VoteAccessor(dbContext);

            //Act
            userAccessor.AddUser(user);
            candidateAccessor.AddCandidate(candidate1);
            candidateAccessor.AddCandidate(candidate2);
            electionAccessor.AddElection(election);

            voteAccessor.AddVote(vote);
            vote.CandidateId = 2;
            var result = voteAccessor.UpdateVote(vote);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async void VoteAccessor_Delete_Success_Test1()
        {
            var user = new User("jimjohn@gmail.com", "pizzaguym4n", "Jim", "John");
            var candidate1 = new Candidate("Policy 1,", "Policy 2", "Description", 55, "Richard", "Nixon", "Republican", false);
            var candidate2 = new Candidate("Policy 1,", "Policy 2", "Description", 55, "Barack", "Obama", "Democrat", true);
            var election = new Election(candidate1, candidate2, "President", 12345, 23456);
            var vote = new Vote(1, 1, 1);

            var dbContext = await GetDbContext();
            var userAccessor = new UserAccessor(dbContext);
            var candidateAccessor = new CandidateAccessor(dbContext);
            var electionAccessor = new ElectionAccessor(dbContext);
            var voteAccessor = new VoteAccessor(dbContext);

            //Act
            userAccessor.AddUser(user);
            candidateAccessor.AddCandidate(candidate1);
            candidateAccessor.AddCandidate(candidate2);
            electionAccessor.AddElection(election);

            voteAccessor.AddVote(vote);
            var result = voteAccessor.DeleteVote(1);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async void VoteAccessor_Delete_Success_Test2()
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

            //Act
            userAccessor.AddUser(user);
            candidateAccessor.AddCandidate(candidate1);
            candidateAccessor.AddCandidate(candidate2);
            electionAccessor.AddElection(election);
            voteAccessor.AddVote(vote);
            var result = voteAccessor.DeleteVote(1);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async void VoteAccessor_GetVoteStatus_Fail_Test1()
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

            //Act
            userAccessor.AddUser(user);
            candidateAccessor.AddCandidate(candidate1);
            candidateAccessor.AddCandidate(candidate2);
            electionAccessor.AddElection(election);

            int result = voteAccessor.GetVoteStatus(3, 1);
            Assert.Equal(-1, result);
        }


        [Fact]
        public async void VoteAccessor_GetVoteStatus_Fail_Test2()
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

            //Act
            userAccessor.AddUser(user);
            candidateAccessor.AddCandidate(candidate1);
            candidateAccessor.AddCandidate(candidate2);
            electionAccessor.AddElection(election);

            int result = voteAccessor.GetVoteStatus(1, 3);
            Assert.Equal(-1, result);
        }

        [Fact]
        public async void VoteAccessor_GetVoteStatus_Fail_Test3()
        {
            var user1 = new User("coolguy@gmail.com", "justac00lguy", "Keanu", "Reeves");
            var user2 = new User("codedude@gmail.com", "cod3rdude", "Mark", "Zuckerberg");
            var candidate1 = new Candidate("Policy 1,", "Policy 2", "Description", 55, "Scrooge", "McDuck", "Republican", false);
            var candidate2 = new Candidate("Policy 1,", "Policy 2", "Description", 55, "Mickey", "Mouse", "Democrat", false);
            var election = new Election(candidate1, candidate2, "Secretary of War", 2345242, 234423);
            var vote = new Vote(1, 1, 1);

            var dbContext = await GetDbContext();
            var userAccessor = new UserAccessor(dbContext);
            var candidateAccessor = new CandidateAccessor(dbContext);
            var electionAccessor = new ElectionAccessor(dbContext);
            var voteAccessor = new VoteAccessor(dbContext);

            //Act
            userAccessor.AddUser(user1);
            userAccessor.AddUser(user2);
            candidateAccessor.AddCandidate(candidate1);
            candidateAccessor.AddCandidate(candidate2);
            electionAccessor.AddElection(election);

            int result = voteAccessor.GetVoteStatus(2, 1);
            Assert.Equal(0, result);
        }

        [Fact]
        public async void VoteAccessor_GetVoteStatus_Success_Test1()
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

            //Act
            userAccessor.AddUser(user);
            candidateAccessor.AddCandidate(candidate1);
            candidateAccessor.AddCandidate(candidate2);
            electionAccessor.AddElection(election);
            voteAccessor.AddVote(vote);

            int result = voteAccessor.GetVoteStatus(1, 1);
            Assert.Equal(1, result);
        }

        [Fact]
        public async void VoteAccessor_GetVoteStatus_Success_Test2()
        {
            var user = new User("coolguy@gmail.com", "justac00lguy", "Keanu", "Reeves");
            var candidate1 = new Candidate("Policy 1,", "Policy 2", "Description", 55, "Scrooge", "McDuck", "Republican", false);
            var candidate2 = new Candidate("Policy 1,", "Policy 2", "Description", 55, "Mickey", "Mouse", "Democrat", false);
            var election = new Election(candidate1, candidate2, "Secretary of War", 2345242, 234423);
            var vote = new Vote(1, 1, 2);

            var dbContext = await GetDbContext();
            var userAccessor = new UserAccessor(dbContext);
            var candidateAccessor = new CandidateAccessor(dbContext);
            var electionAccessor = new ElectionAccessor(dbContext);
            var voteAccessor = new VoteAccessor(dbContext);

            //Act
            userAccessor.AddUser(user);
            candidateAccessor.AddCandidate(candidate1);
            candidateAccessor.AddCandidate(candidate2);
            electionAccessor.AddElection(election);
            voteAccessor.AddVote(vote);

            int result = voteAccessor.GetVoteStatus(1, 1);
            Assert.Equal(2, result);
        }
    }
}