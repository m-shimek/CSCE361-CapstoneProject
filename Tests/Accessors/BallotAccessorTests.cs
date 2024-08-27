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
    public class BallotAccessorTests
    {
        private async Task<ApplicationDbContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new ApplicationDbContext(options);
            databaseContext.Database.EnsureCreated();
            if (await databaseContext.Ballots.CountAsync() < 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    databaseContext.Ballots.Add(
                        new Ballot(new DateTime(2024, 1, 1), new DateTime(2024, 2, 2))
                    );
                    await databaseContext.SaveChangesAsync();
                }
            }
            return databaseContext;
        }

        [Fact]
        public async void BallotAccessor_GetCurrentBallot_Success_Test()
        {
            var ballot = new Ballot(new DateTime(2024, 1, 1), new DateTime(2024, 12, 12));
            var election = new Election(
                new Candidate("Taxes", "Gun Control", "Description", 65, "Thomas", "David", "Democrat", true), new Candidate("Policy 1,", "Policy 2", "Description", 55, "David", "Thomas", "Republican", false),
                "Secretary of War", 546293, 254353
            );
            ballot.Elections = [election];
            var dbContext = await GetDbContext();
            var ballotAccessor = new BallotAccessor(dbContext);

            //Act
            ballotAccessor.AddBallot(ballot);
            var result = ballotAccessor.GetCurrentBallot();

            //Assert
            Assert.Equal(ballot, result);
        }

        [Fact]
        public async void BallotAccessor_GetCurrentBallot_Fail_Test()
        {
            var ballot1 = new Ballot(new DateTime(2020, 1, 6), new DateTime(2020, 4, 4));
            var ballot2 = new Ballot(new DateTime(2016, 3, 3), new DateTime(2016, 12, 12));
            var election1 = new Election(
                new Candidate("Policy 1,", "Policy 2", "Description", 55, "Thomas", "David", "Democrat", true), new Candidate("Policy 1,", "Policy 2", "Description", 55, "David", "Thomas", "Republican", false),
                "Secretary of War", 546293, 254353
            );
            var election2 = new Election(
                new Candidate("Policy 1,", "Policy 2", "Description", 55, "Dave", "Smith", "Democrat", true), new Candidate("Policy 1,", "Policy 2", "Description", 55, "Charles", "Churchill", "Republican", false),
                "Mayor", 754348, 249823
            );
            ballot1.Elections = [election1];
            ballot2.Elections = [election2];
            var dbContext = await GetDbContext();
            var ballotAccessor = new BallotAccessor(dbContext);

            //Act
            ballotAccessor.AddBallot(ballot1);
            ballotAccessor.AddBallot(ballot2);
            var result = ballotAccessor.GetCurrentBallot();

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void BallotAccessor_GetBallotById_Success_Test()
        {
            var ballot = new Ballot(new DateTime(2024, 1, 1), new DateTime(2024, 12, 12));
            var dbContext = await GetDbContext();
            var ballotAccessor = new BallotAccessor(dbContext);

            //Act
            ballotAccessor.AddBallot(ballot);
            var result = ballotAccessor.GetBallotById(1);

            //Assert
            Assert.Equal(ballot, result);
        }

        [Fact]
        public async void BallotAccessor_GetBallotById_Fail_Test()
        {
            var ballot = new Ballot(new DateTime(2020, 11, 5), new DateTime(2020, 11, 14));
            var dbContext = await GetDbContext();
            var ballotAccessor = new BallotAccessor(dbContext);

            //Act
            ballotAccessor.AddBallot(ballot);
            var result = ballotAccessor.GetBallotById(3);

            //Assert
            Assert.False(ballot.Equals(result));
        }

        [Fact]
        public async void BallotAccessor_GetAllBallots_Success_Test()
        {
            var ballot1 = new Ballot(new DateTime(2020, 1, 6), new DateTime(2020, 4, 4));
            var ballot2 = new Ballot(new DateTime(2016, 3, 3), new DateTime(2016, 12, 12));
            var dbContext = await GetDbContext();
            var ballotAccessor = new BallotAccessor(dbContext);

            //Act
            ballotAccessor.AddBallot(ballot1);
            ballotAccessor.AddBallot(ballot2);

            List<Ballot> ballotList = [ballot1, ballot2];

            var result = ballotAccessor.GetAllBallots();

            //Assert
            Assert.Equal(ballotList, result);
        }

        [Fact]
        public async void BallotAccessor_GetPastBallots_Success_Test()
        {
            var ballot1 = new Ballot(new DateTime(2020, 1, 6), new DateTime(2020, 4, 4));
            var ballot2 = new Ballot(new DateTime(2016, 3, 3), new DateTime(2016, 12, 12));
            var ballot3 = new Ballot(new DateTime(2024, 1, 1), new DateTime(2024, 12, 12));
            var dbContext = await GetDbContext();
            var ballotAccessor = new BallotAccessor(dbContext);

            //Act
            ballotAccessor.AddBallot(ballot1);
            ballotAccessor.AddBallot(ballot2);
            ballotAccessor.AddBallot(ballot3);

            List<Ballot> ballotList = [ballot1, ballot2];

            var result = ballotAccessor.GetPastBallots();

            //Assert
            Assert.Equal(ballotList, result);
        }

        [Fact]
        public async void BallotAccessor_GetUpcomingBallots_Success_Test()
        {
            var ballot1 = new Ballot(new DateTime(2025, 1, 6), new DateTime(2025, 4, 4));
            var ballot2 = new Ballot(new DateTime(2026, 3, 3), new DateTime(2026, 12, 12));
            var ballot3 = new Ballot(new DateTime(2024, 1, 1), new DateTime(2024, 12, 12));
            var dbContext = await GetDbContext();
            var ballotAccessor = new BallotAccessor(dbContext);

            //Act
            ballotAccessor.AddBallot(ballot1);
            ballotAccessor.AddBallot(ballot2);
            ballotAccessor.AddBallot(ballot3);

            List<Ballot> ballotList = [ballot1, ballot2];

            var result = ballotAccessor.GetUpcomingBallots();

            //Assert
            Assert.Equal(ballotList, result);
        }

        [Fact]
        public async void BallotAccessor_Add_Success_Test1()
        {
            var ballot = new Ballot(new DateTime(2020, 1, 1), new DateTime(2020, 2, 2));
            var dbContext = await GetDbContext();
            var ballotAccessor = new BallotAccessor(dbContext);

            //Act
            var result = ballotAccessor.AddBallot(ballot);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async void BallotAccessor_Add_Success_Test2()
        {
            var ballot = new Ballot(new DateTime(2024, 1, 1), DateTime.Now);
            var dbContext = await GetDbContext();
            var ballotAccessor = new BallotAccessor(dbContext);

            //Act
            var result = ballotAccessor.AddBallot(ballot);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async void BallotAccessor_Update_Success_Test1()
        {
            var ballot = new Ballot(new DateTime(2020, 1, 1), new DateTime(2020, 2, 2));
            var dbContext = await GetDbContext();
            var ballotAccessor = new BallotAccessor(dbContext);

            //Act
            ballotAccessor.AddBallot(ballot);
            ballot.EndDate = new DateTime(2024, 3, 3);
            var result = ballotAccessor.UpdateBallot(ballot);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async void BallotAccessor_Update_Success_Test2()
        {
            var ballot = new Ballot(new DateTime(2024, 1, 1), DateTime.Now);
            var dbContext = await GetDbContext();
            var ballotAccessor = new BallotAccessor(dbContext);

            //Act
            ballotAccessor.AddBallot(ballot);
            DateTime newDate = new DateTime(2023, 12, 31);
            ballot.StartDate = newDate;
            var result = ballotAccessor.UpdateBallot(ballot);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async void BallotAccessor_Update_Success_Test3()
        {
            var ballot = new Ballot(new DateTime(1960, 11, 5), new DateTime(1960, 11, 19));
            var dbContext = await GetDbContext();
            var ballotAccessor = new BallotAccessor(dbContext);

            //Act
            ballotAccessor.AddBallot(ballot);
            ballot.StartDate = new DateTime(1960, 11, 3);
            ballot.EndDate = new DateTime(1960, 11, 17);
            var result = ballotAccessor.UpdateBallot(ballot);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async void BallotAccessor_Delete_Success_Test1()
        {
            var ballot = new Ballot(new DateTime(2024, 1, 1), new DateTime(2024, 12, 12));
            var dbContext = await GetDbContext();
            var ballotAccessor = new BallotAccessor(dbContext);

            //Act
            ballotAccessor.AddBallot(ballot);
            var result = ballotAccessor.DeleteBallot(1);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async void BallotAccessor_Delete_Success_Test2()
        {
            var ballot = new Ballot(new DateTime(2024, 1, 1), DateTime.Now);
            var dbContext = await GetDbContext();
            var ballotAccessor = new BallotAccessor(dbContext);

            //Act
            ballotAccessor.AddBallot(ballot);
            var result = ballotAccessor.DeleteBallot(1);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async void BallotAccessor_Delete_Fail_Test()
        {
            var ballot = new Ballot(DateTime.Now, new DateTime(2024, 6, 6));
            var dbContext = await GetDbContext();
            var ballotAccessor = new BallotAccessor(dbContext);

            //Act
            ballotAccessor.AddBallot(ballot);
            var result = ballotAccessor.DeleteBallot(3);

            //Assert
            Assert.False(result);
        }
    }
}