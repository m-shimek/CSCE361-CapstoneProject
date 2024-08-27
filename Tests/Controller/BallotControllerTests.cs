using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyVotingSystem.Accessors;
using MyVotingSystem.Controllers;
using MyVotingSystem.Data;
using MyVotingSystem.Models;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace MyVotingSystem.Tests.Controller
{
    public class BallotControllerTests
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
        public async void BallotController_GetCurrentBallot_NotNull_Test()
        {
            //Arrange
            var ballot = new Ballot(new DateTime(2024, 1, 1), new DateTime(2024, 12, 12));
            var election = new Election(
                new Candidate("Policy 1,", "Policy 2", "Description", 55, "Thomas", "David", "Democrat", true), new Candidate("Policy 1,", "Policy 2", "Description", 55, "David", "Thomas", "Republican", false),
                "Secretary of War", 546293, 254353
            );
            ballot.Elections = [election];
            var dbContext = await GetDbContext();
            var ballotAccessor = new BallotAccessor(dbContext);
            var ballotController = new BallotController(ballotAccessor);

            //Act
            ballotAccessor.AddBallot(ballot);
            var result = ballotController.getCurrentBallot();

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async void BallotController_GetCurrentBallot_Check_Type_Test()
        {
            //Arrange
            var ballot = new Ballot(new DateTime(2024, 1, 1), new DateTime(2024, 12, 12));
            var election = new Election(
                new Candidate("Policy 1,", "Policy 2", "Description", 55, "Thomas", "David", "Democrat", true), new Candidate("Policy 1,", "Policy 2", "Description", 55, "David", "Thomas", "Republican", false),
                "Secretary of War", 546293, 254353
            );
            ballot.Elections = [election];
            var dbContext = await GetDbContext();
            var ballotAccessor = new BallotAccessor(dbContext);
            var ballotController = new BallotController(ballotAccessor);

            //Act
            ballotAccessor.AddBallot(ballot);
            var result = ballotController.getCurrentBallot();

            Assert.NotNull(result);

            //Assert
            Assert.IsType<JsonHttpResult<PackedBallot>>(result);
        }

        [Fact]
        public async void BallotController_GetCurrentBallot_Check_Status_Code_Test()
        {
            //Arrange
            var ballot = new Ballot(new DateTime(2024, 1, 1), new DateTime(2024, 12, 12));
            var election = new Election(
                new Candidate("Policy 1,", "Policy 2", "Description", 55, "Thomas", "David", "Democrat", true), new Candidate("Policy 1,", "Policy 2", "Description", 55, "David", "Thomas", "Republican", false),
                "Secretary of War", 546293, 254353
            );
            ballot.Elections = [election];
            var dbContext = await GetDbContext();
            var ballotAccessor = new BallotAccessor(dbContext);
            var ballotController = new BallotController(ballotAccessor);

            //Act
            ballotAccessor.AddBallot(ballot);
            var result = ballotController.getCurrentBallot();

            Assert.NotNull(result);

            var jsonResult = Assert.IsType<JsonHttpResult<PackedBallot>>(result);

            //Assert
            Assert.Equal(200, jsonResult.StatusCode);
        }

        [Fact]
        public async void BallotController_GetCurrentBallot_Check_Value_Success_Test()
        {
            //Arrange
            var ballot = new Ballot(new DateTime(2024, 1, 1), new DateTime(2024, 12, 12));
            var election = new Election(
                new Candidate("Policy 1,", "Policy 2", "Description", 55, "Thomas", "David", "Democrat", true), new Candidate("Policy 1,", "Policy 2", "Description", 55, "David", "Thomas", "Republican", false),
                "Secretary of War", 546293, 254353
            );
            ballot.Elections = [election];
            var dbContext = await GetDbContext();
            var ballotAccessor = new BallotAccessor(dbContext);
            var ballotController = new BallotController(ballotAccessor);

            //Act
            ballotAccessor.AddBallot(ballot);
            var result = ballotController.getCurrentBallot();

            Assert.NotNull(result);
            var jsonResult = Assert.IsType<JsonHttpResult<PackedBallot>>(result);
            Assert.Equal(200, jsonResult.StatusCode);

            //Assert
            var resultBallot = jsonResult.Value;
            Assert.Equal(ballot.BallotId, resultBallot.Id);
        }

        [Fact]
        public async void BallotController_GetCurrentBallot_Check_Type_Fail_Test()
        {
            //Arrange        
            var dbContext = await GetDbContext();
            var ballotAccessor = new BallotAccessor(dbContext);
            var ballotController = new BallotController(ballotAccessor);

            //Act
            var result = ballotController.getCurrentBallot();

            Assert.NotNull(result);

            //Assert
            Assert.IsNotType<JsonHttpResult<PackedBallot>>(result);
        }

        [Fact]
        public async void BallotController_GetUpcomingBallots_NotNull_Test()
        {
            var ballot1 = new Ballot(new DateTime(2025, 1, 6), new DateTime(2025, 4, 4));
            var ballot2 = new Ballot(new DateTime(2026, 3, 3), new DateTime(2026, 12, 12));
            var ballot3 = new Ballot(new DateTime(2024, 1, 1), new DateTime(2024, 12, 12));
            var dbContext = await GetDbContext();
            var ballotAccessor = new BallotAccessor(dbContext);
            var ballotController = new BallotController(ballotAccessor);

            //Act
            ballotAccessor.AddBallot(ballot1);
            ballotAccessor.AddBallot(ballot2);
            ballotAccessor.AddBallot(ballot3);

            var result = ballotController.getUpcomingBallots();

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async void BallotController_GetUpcomingBallots_Check_Type_Test()
        {
            var ballot1 = new Ballot(new DateTime(2025, 1, 6), new DateTime(2025, 4, 4));
            var ballot2 = new Ballot(new DateTime(2026, 3, 3), new DateTime(2026, 12, 12));
            var ballot3 = new Ballot(new DateTime(2024, 1, 1), new DateTime(2024, 12, 12));
            var dbContext = await GetDbContext();
            var ballotAccessor = new BallotAccessor(dbContext);
            var ballotController = new BallotController(ballotAccessor);

            //Act
            ballotAccessor.AddBallot(ballot1);
            ballotAccessor.AddBallot(ballot2);
            ballotAccessor.AddBallot(ballot3);

            var result = ballotController.getUpcomingBallots();
            Assert.NotNull(result);

            //Assert
            Assert.IsType<JsonHttpResult<List<PackedBallot>>>(result);
        }

        [Fact]
        public async void BallotController_GetUpcomingBallots_Check_Status_Code_Test()
        {
            var ballot1 = new Ballot(new DateTime(2025, 1, 6), new DateTime(2025, 4, 4));
            var ballot2 = new Ballot(new DateTime(2026, 3, 3), new DateTime(2026, 12, 12));
            var ballot3 = new Ballot(new DateTime(2024, 1, 1), new DateTime(2024, 12, 12));
            var dbContext = await GetDbContext();
            var ballotAccessor = new BallotAccessor(dbContext);
            var ballotController = new BallotController(ballotAccessor);

            //Act
            ballotAccessor.AddBallot(ballot1);
            ballotAccessor.AddBallot(ballot2);
            ballotAccessor.AddBallot(ballot3);

            var result = ballotController.getUpcomingBallots();
            Assert.NotNull(result);

            var jsonResult = Assert.IsType<JsonHttpResult<List<PackedBallot>>>(result);

            //Assert
            Assert.Equal(200, jsonResult.StatusCode);
        }

        [Fact]
        public async void BallotController_GetUpcomingBallots_Check_Value_Success_Test1()
        {
            var ballot1 = new Ballot(new DateTime(2025, 1, 6), new DateTime(2025, 4, 4));
            var ballot2 = new Ballot(new DateTime(2026, 3, 3), new DateTime(2026, 12, 12));
            var ballot3 = new Ballot(new DateTime(2024, 1, 1), new DateTime(2024, 12, 12));
            var dbContext = await GetDbContext();
            var ballotAccessor = new BallotAccessor(dbContext);
            var ballotController = new BallotController(ballotAccessor);

            //Act
            ballotAccessor.AddBallot(ballot1);
            ballotAccessor.AddBallot(ballot2);
            ballotAccessor.AddBallot(ballot3);

            List<Ballot> ballotList = [ballot1, ballot2];

            var result = ballotController.getUpcomingBallots();
            Assert.NotNull(result);

            var jsonResult = Assert.IsType<JsonHttpResult<List<PackedBallot>>>(result);
            Assert.Equal(200, jsonResult.StatusCode);

            //Assert
            var resultUser = jsonResult.Value;
            Assert.Equal(ballotList[0].BallotId, resultUser[0].Id);
            Assert.Equal(ballotList[1].BallotId, resultUser[1].Id);
        }

        [Fact]
        public async void BallotController_GetUpcomingBallots_Check_Value_Success_Test2()
        {
            var ballot1 = new Ballot(new DateTime(2020, 1, 6), new DateTime(2020, 4, 4));
            var ballot2 = new Ballot(new DateTime(2024, 1, 1), new DateTime(2024, 12, 12));
            var dbContext = await GetDbContext();
            var ballotAccessor = new BallotAccessor(dbContext);
            var ballotController = new BallotController(ballotAccessor);

            //Act
            ballotAccessor.AddBallot(ballot1);
            ballotAccessor.AddBallot(ballot2);

            var result = ballotController.getUpcomingBallots();
            Assert.NotNull(result);

            var jsonResult = Assert.IsType<JsonHttpResult<List<PackedBallot>>>(result);
            Assert.Equal(200, jsonResult.StatusCode);

            //Assert
            var resultUser = jsonResult.Value;
            Assert.Equal([], resultUser);
        }

        [Fact]
        public async void BallotController_GetPastBallots_NotNull_Test()
        {
            var ballot1 = new Ballot(new DateTime(2020, 1, 6), new DateTime(2020, 4, 4));
            var ballot2 = new Ballot(new DateTime(2016, 3, 3), new DateTime(2016, 12, 12));
            var ballot3 = new Ballot(new DateTime(2024, 1, 1), new DateTime(2024, 12, 12));
            var dbContext = await GetDbContext();
            var ballotAccessor = new BallotAccessor(dbContext);
            var ballotController = new BallotController(ballotAccessor);

            //Act
            ballotAccessor.AddBallot(ballot1);
            ballotAccessor.AddBallot(ballot2);
            ballotAccessor.AddBallot(ballot3);

            var result = ballotController.getPastBallots();

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async void BallotController_GetPastBallots_Check_Type_Test()
        {
            var ballot1 = new Ballot(new DateTime(2020, 1, 6), new DateTime(2020, 4, 4));
            var ballot2 = new Ballot(new DateTime(2016, 3, 3), new DateTime(2016, 12, 12));
            var ballot3 = new Ballot(new DateTime(2024, 1, 1), new DateTime(2024, 12, 12));
            var dbContext = await GetDbContext();
            var ballotAccessor = new BallotAccessor(dbContext);
            var ballotController = new BallotController(ballotAccessor);

            //Act
            ballotAccessor.AddBallot(ballot1);
            ballotAccessor.AddBallot(ballot2);
            ballotAccessor.AddBallot(ballot3);

            var result = ballotController.getPastBallots();
            Assert.NotNull(result);

            //Assert
            Assert.IsType<JsonHttpResult<List<PackedBallot>>>(result);
        }

        [Fact]
        public async void BallotController_GetPastBallots_Check_Status_Code_Test()
        {
            var ballot1 = new Ballot(new DateTime(2020, 1, 6), new DateTime(2020, 4, 4));
            var ballot2 = new Ballot(new DateTime(2016, 3, 3), new DateTime(2016, 12, 12));
            var ballot3 = new Ballot(new DateTime(2024, 1, 1), new DateTime(2024, 12, 12));
            var dbContext = await GetDbContext();
            var ballotAccessor = new BallotAccessor(dbContext);
            var ballotController = new BallotController(ballotAccessor);

            //Act
            ballotAccessor.AddBallot(ballot1);
            ballotAccessor.AddBallot(ballot2);
            ballotAccessor.AddBallot(ballot3);

            var result = ballotController.getPastBallots();
            Assert.NotNull(result);

            var jsonResult = Assert.IsType<JsonHttpResult<List<PackedBallot>>>(result);

            //Assert
            Assert.Equal(200, jsonResult.StatusCode);
        }

        [Fact]
        public async void BallotController_GetPastBallots_Check_Value_Success_Test1()
        {
            var ballot1 = new Ballot(new DateTime(2020, 1, 6), new DateTime(2020, 4, 4));
            var ballot2 = new Ballot(new DateTime(2016, 3, 3), new DateTime(2016, 12, 12));
            var ballot3 = new Ballot(new DateTime(2024, 1, 1), new DateTime(2024, 12, 12));
            var dbContext = await GetDbContext();
            var ballotAccessor = new BallotAccessor(dbContext);
            var ballotController = new BallotController(ballotAccessor);

            //Act
            ballotAccessor.AddBallot(ballot1);
            ballotAccessor.AddBallot(ballot2);
            ballotAccessor.AddBallot(ballot3);

            List<Ballot> ballotList = [ballot1, ballot2];

            var result = ballotController.getPastBallots();
            Assert.NotNull(result);

            var jsonResult = Assert.IsType<JsonHttpResult<List<PackedBallot>>>(result);
            Assert.Equal(200, jsonResult.StatusCode);

            //Assert
            var resultUser = jsonResult.Value;
            Assert.Equal(ballotList[0].BallotId, resultUser[0].Id);
            Assert.Equal(ballotList[1].BallotId, resultUser[1].Id);
        }

        [Fact]
        public async void BallotController_GetPastBallots_Check_Value_Success_Test2()
        {
            var ballot1 = new Ballot(new DateTime(2025, 1, 6), new DateTime(2025, 4, 4));
            var ballot2 = new Ballot(new DateTime(2024, 1, 1), new DateTime(2024, 12, 12));
            var dbContext = await GetDbContext();
            var ballotAccessor = new BallotAccessor(dbContext);
            var ballotController = new BallotController(ballotAccessor);

            //Act
            ballotAccessor.AddBallot(ballot1);
            ballotAccessor.AddBallot(ballot2);

            var result = ballotController.getPastBallots();
            Assert.NotNull(result);

            var jsonResult = Assert.IsType<JsonHttpResult<List<PackedBallot>>>(result);
            Assert.Equal(200, jsonResult.StatusCode);

            //Assert
            var resultUser = jsonResult.Value;
            Assert.Equal([], resultUser);
        }
    }
}