using Microsoft.EntityFrameworkCore;
using MyVotingSystem.Accessors;
using MyVotingSystem.Data;
using MyVotingSystem.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MyVotingSystem.Tests.Accessors
{
    public class CandidateAccessorTests
    {
        private async Task<ApplicationDbContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new ApplicationDbContext(options);
            databaseContext.Database.EnsureCreated();
            if (await databaseContext.Candidates.CountAsync() < 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    databaseContext.Candidates.Add(
                        new Candidate("Policy 1,", "Policy 2", "Description", 55, "Jim", "Pillen", "Republican", false)
                    );
                    await databaseContext.SaveChangesAsync();
                }
            }
            return databaseContext;
        }

        [Fact]
        public async void CandidateAccessor_GetCandidateById_Success_Test()
        {
            var candidate = new Candidate("Policy 1,", "Policy 2", "Description", 55, "David", "Cross", "Democrat", true);
            var dbContext = await GetDbContext();
            var candidateAccessor = new CandidateAccessor(dbContext);

            //Act
            candidateAccessor.AddCandidate(candidate);
            var result = candidateAccessor.GetCandidateById(1);

            //Assert
            Assert.Equal(candidate, result);
        }

        [Fact]
        public async void CandidateAccessor_GetCandidateById_Fail_Test()
        {
            var candidate = new Candidate("Policy 1,", "Policy 2", "Description", 55, "Test", "Candidate", "Rebuplican", false);
            var dbContext = await GetDbContext();
            var candidateAccessor = new CandidateAccessor(dbContext);

            //Act
            candidateAccessor.AddCandidate(candidate);
            var result = candidateAccessor.GetCandidateById(3);

            //Assert
            Assert.False(candidate.Equals(result));
        }

        [Fact]
        public async void CandidateAccessor_GetAllCandidates_Success_Test()
        {
            var candidate1 = new Candidate("Policy 1,", "Policy 2", "Description", 55, "Bob", "Johnson", "Republican", true);
            var candidate2 = new Candidate("Policy 1,", "Policy 2", "Description", 55, "Parker", "Morris", "Democrat", false);
            var dbContext = await GetDbContext();
            var candidateAccessor = new CandidateAccessor(dbContext);

            //Act
            candidateAccessor.AddCandidate(candidate1);
            candidateAccessor.AddCandidate(candidate2);

            List<Candidate> candidateList = [candidate1, candidate2];

            var result = candidateAccessor.GetAllCandidates();

            //Assert
            Assert.Equal(candidateList, result);
        }

        [Fact]
        public async void CandidateAccessor_Add_Success_Test1()
        {
            var candidate = new Candidate("Policy 1,", "Policy 2", "Description", 55, "Bob", "Johnson", "Republican", true);
            var dbContext = await GetDbContext();
            var candidateAccessor = new CandidateAccessor(dbContext);

            //Act
            var result = candidateAccessor.AddCandidate(candidate);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async void CandidateAccessor_Add_Success_Test2()
        {
            var candidate = new Candidate("Policy 1,", "Policy 2", "Description", 55, "Parker", "Morris", "Democrat", false);
            var dbContext = await GetDbContext();
            var candidateAccessor = new CandidateAccessor(dbContext);

            //Act
            var result = candidateAccessor.AddCandidate(candidate);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async void CandidateAccessor_Update_Success_Test1()
        {
            var candidate = new Candidate("Policy 1,", "Policy 2", "Description", 55, "Bob", "Johnson", "Republican", true);
            var dbContext = await GetDbContext();
            var candidateAccessor = new CandidateAccessor(dbContext);

            //Act
            candidateAccessor.AddCandidate(candidate);
            candidate.FirstName = "Andrew";
            var result = candidateAccessor.UpdateCandidate(candidate);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async void CandidateAccessor_Update_Success_Test2()
        {
            var candidate = new Candidate("Policy 1,", "Policy 2", "Description", 55, "Parker", "Morris", "Democrat", false);
            var dbContext = await GetDbContext();
            var candidateAccessor = new CandidateAccessor(dbContext);

            //Act
            candidateAccessor.AddCandidate(candidate);
            candidate.Party = "Republican";
            var result = candidateAccessor.UpdateCandidate(candidate);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async void CandidateAccessor_Update_Success_Test3()
        {
            var candidate = new Candidate("Policy 1,", "Policy 2", "Description", 55, "Bob", "Barker", "Democrat", false);
            var dbContext = await GetDbContext();
            var candidateAccessor = new CandidateAccessor(dbContext);

            //Act
            candidateAccessor.AddCandidate(candidate);
            candidate.IsIncumbent = true;
            var result = candidateAccessor.UpdateCandidate(candidate);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async void CandidateAccessor_Delete_Success_Test1()
        {
            var candidate = new Candidate("Policy 1,", "Policy 2", "Description", 55, "Parker", "Morris", "Democrat", false);
            var dbContext = await GetDbContext();
            var candidateAccessor = new CandidateAccessor(dbContext);

            //Act
            candidateAccessor.AddCandidate(candidate);
            var result = candidateAccessor.DeleteCandidate(1);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async void CandidateAccessor_Delete_Success_Test2()
        {
            var candidate = new Candidate("Policy 1,", "Policy 2", "Description", 55, "Bob", "Barker", "Democrat", false);
            var dbContext = await GetDbContext();
            var candidateAccessor = new CandidateAccessor(dbContext);

            //Act
            candidateAccessor.AddCandidate(candidate);
            var result = candidateAccessor.DeleteCandidate(1);

            //Assert
            Assert.True(result);
        }
    }
}