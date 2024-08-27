using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using MyVotingSystem.Accessors;
using MyVotingSystem.Data;
using MyVotingSystem.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MyVotingSystem.Tests.Accessors
{
    public class ElectionAccessorTests
    {
        private async Task<ApplicationDbContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new ApplicationDbContext(options);
            databaseContext.Database.EnsureCreated();
            if (await databaseContext.Elections.CountAsync() < 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    var candidate1 = new Candidate("Policy 1,", "Policy 2", "Description", 55, "Richard", "Nixon", "Republican", false);
                    var candidate2 = new Candidate("Policy 1,", "Policy 2", "Description", 55, "Barack", "Obama", "Democrat", true);
                    databaseContext.Elections.Add(
                        new Election(candidate1, candidate2, "President", 12345, 23456)
                    );
                    await databaseContext.SaveChangesAsync();
                }
            }
            return databaseContext;
        }

        [Fact]
        public async void ElectionAccessor_GetElectionById_Success_Test()
        {
            var candidate1 = new Candidate("Policy 1,", "Policy 2", "Description", 55, "Dave", "Smith", "Democrat", true);
            var candidate2 = new Candidate("Policy 1,", "Policy 2", "Description", 55, "Charles", "Churchill", "Republican", false);

            var election = new Election(candidate1, candidate2, "Mayor", 754348, 249823);
            var dbContext = await GetDbContext();
            var ElectionAccessor = new ElectionAccessor(dbContext);

            //Act
            ElectionAccessor.AddElection(election);
            var result = ElectionAccessor.GetElectionById(1);

            //Assert
            Assert.Equal(election, result);
        }

        [Fact]
        public async void ElectionAccessor_GetElectionById_Fail_Test()
        {
            var candidate1 = new Candidate("Policy 1,", "Policy 2", "Description", 55, "Thomas", "David", "Democrat", true);
            var candidate2 = new Candidate("Policy 1,", "Policy 2", "Description", 55, "David", "Thomas", "Republican", false);

            var election = new Election(candidate1, candidate2, "Secretary of War", 546293, 254353);
            var dbContext = await GetDbContext();
            var ElectionAccessor = new ElectionAccessor(dbContext);

            //Act
            ElectionAccessor.AddElection(election);
            var result = ElectionAccessor.GetElectionById(3);

            //Assert
            Assert.False(election.Equals(result));
        }

        [Fact]
        public async void ElectionAccessor_GetAllElections_Success_Test()
        {
            var candidate1 = new Candidate("Policy 1,", "Policy 2", "Description", 55, "Thomas", "David", "Democrat", true);
            var candidate2 = new Candidate("Policy 1,", "Policy 2", "Description", 55, "David", "Thomas", "Republican", false);
            var candidate3 = new Candidate("Policy 1,", "Policy 2", "Description", 55, "Dave", "Smith", "Democrat", true);
            var candidate4 = new Candidate("Policy 1,", "Policy 2", "Description", 55, "Charles", "Churchill", "Republican", false);


            var election1 = new Election(candidate1, candidate2, "Secretary of War", 546293, 254353);

            var election2 = new Election(candidate3, candidate4, "Mayor", 754348, 249823);
            var dbContext = await GetDbContext();
            var ElectionAccessor = new ElectionAccessor(dbContext);

            //Act
            ElectionAccessor.AddElection(election1);
            ElectionAccessor.AddElection(election2);

            List<Election> electionList = [election1, election2];

            var result = ElectionAccessor.GetAllElections();

            //Assert
            Assert.Equal(electionList, result);
        }

        [Fact]
        public async void ElectionAccessor_Add_Success_Test1()
        {
            var election = new Election(
                new Candidate("Policy 1,", "Policy 2", "Description", 55, "Thomas", "David", "Democrat", true), new Candidate("Policy 1,", "Policy 2", "Description", 55, "David", "Thomas", "Republican", false),
                "Secretary of War", 546293, 254353
            );
            var dbContext = await GetDbContext();
            var ElectionAccessor = new ElectionAccessor(dbContext);

            //Act
            var result = ElectionAccessor.AddElection(election);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async void ElectionAccessor_Add_Success_Test2()
        {
            var election = new Election(
                new Candidate("Policy 1,", "Policy 2", "Description", 55, "Dave", "Smith", "Democrat", true), new Candidate("Policy 1,", "Policy 2", "Description", 55, "Charles", "Churchill", "Republican", false),
                "Mayor", 754348, 249823
            );
            var dbContext = await GetDbContext();
            var ElectionAccessor = new ElectionAccessor(dbContext);

            //Act
            var result = ElectionAccessor.AddElection(election);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async void ElectionAccessor_Update_Success_Test1()
        {
            var election = new Election(
                new Candidate("Policy 1,", "Policy 2", "Description", 55, "Thomas", "David", "Democrat", true), new Candidate("Policy 1,", "Policy 2", "Description", 55, "David", "Thomas", "Republican", false),
                "Secretary of War", 546293, 254353
            );
            var dbContext = await GetDbContext();
            var ElectionAccessor = new ElectionAccessor(dbContext);

            //Act
            ElectionAccessor.AddElection(election);
            election.Candidate1VoteCount = 547295;
            var result = ElectionAccessor.UpdateElection(election);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async void ElectionAccessor_Update_Success_Test2()
        {
            var election = new Election(
                new Candidate("Policy 1,", "Policy 2", "Description", 55, "Dave", "Smith", "Democrat", true), new Candidate("Policy 1,", "Policy 2", "Description", 55, "Charles", "Churchill", "Republican", false),
                "Mayor", 754348, 249823
            );
            var dbContext = await GetDbContext();
            var ElectionAccessor = new ElectionAccessor(dbContext);

            //Act
            ElectionAccessor.AddElection(election);
            election.Position = "President";
            var result = ElectionAccessor.UpdateElection(election);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async void ElectionAccessor_Update_Success_Test3()
        {
            var election = new Election(
                new Candidate("Policy 1,", "Policy 2", "Description", 55, "Dave", "Smith", "Democrat", true), new Candidate("Policy 1,", "Policy 2", "Description", 55, "Charles", "Churchill", "Republican", false),
                "Mayor", 754348, 249823
            );
            var dbContext = await GetDbContext();
            var ElectionAccessor = new ElectionAccessor(dbContext);

            //Act
            ElectionAccessor.AddElection(election);
            election.Candidate1 = new Candidate("Policy 1,", "Policy 2", "Description", 55, "Winston", "Churchill", "Monarchist", false);
            var result = ElectionAccessor.UpdateElection(election);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async void ElectionAccessor_Update_Success_Test4()
        {
            var election = new Election(
                new Candidate("Policy 1,", "Policy 2", "Description", 55, "Dwight", "Schrute", "Democrat", false), new Candidate("Policy 1,", "Policy 2", "Description", 55, "Jim", "Halpert", "Republican", false),
                "Secretary of Defenese", 12, 24
            );
            var dbContext = await GetDbContext();
            var ElectionAccessor = new ElectionAccessor(dbContext);

            //Act
            ElectionAccessor.AddElection(election);
            election.Candidate2VoteCount = 4;
            var result = ElectionAccessor.UpdateElection(election);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async void ElectionAccessor_Delete_Success_Test1()
        {
            var election = new Election(
                new Candidate("Policy 1,", "Policy 2", "Description", 55, "Dwight", "Schrute", "Democrat", false), new Candidate("Policy 1,", "Policy 2", "Description", 55, "Jim", "Halpert", "Republican", false),
                "Secretary of Defenese", 12, 24
            );
            var dbContext = await GetDbContext();
            var ElectionAccessor = new ElectionAccessor(dbContext);

            //Act
            ElectionAccessor.AddElection(election);
            var result = ElectionAccessor.DeleteElection(1);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async void ElectionAccessor_Delete_Success_Test2()
        {
            var election = new Election(
                new Candidate("Policy 1,", "Policy 2", "Description", 55, "Dave", "Smith", "Democrat", true), new Candidate("Policy 1,", "Policy 2", "Description", 55, "Charles", "Churchill", "Republican", false),
                "Mayor", 754348, 249823
            );
            var dbContext = await GetDbContext();
            var ElectionAccessor = new ElectionAccessor(dbContext);

            //Act
            ElectionAccessor.AddElection(election);
            var result = ElectionAccessor.DeleteElection(1);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async void ElectionAccessor_Delete_Fail_Test()
        {
            var election = new Election(
                new Candidate("Policy 1,", "Policy 2", "Description", 55, "Dave", "Smith", "Democrat", true), new Candidate("Policy 1,", "Policy 2", "Description", 55, "Charles", "Churchill", "Republican", false),
                "Mayor", 754348, 249823
            );
            var dbContext = await GetDbContext();
            var ElectionAccessor = new ElectionAccessor(dbContext);

            //Act
            ElectionAccessor.AddElection(election);
            var result = ElectionAccessor.DeleteElection(3);

            //Assert
            Assert.False(result);
        }
    }
}