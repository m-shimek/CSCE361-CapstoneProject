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
    public class UserControllerTests
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
        public async void UserController_SignIn_NotNull_Test()
        {
            //Arrange
            var user = new User("stevesmith@gmail.com", "iateapie4", "Steve", "Smith");
            var dbContext = await GetDbContext();
            var userAccessor = new UserAccessor(dbContext);
            var userController = new UserController(userAccessor);

            //Act
            userAccessor.AddUser(user);

            var result = userController.signin("stevesmith@gmail.com", "iateapie4");

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async void UserController_SignIn_Check_Type_Test()
        {
            //Arrange
            var user = new User("stevesmith@gmail.com", "iateapie4", "Steve", "Smith");
            var dbContext = await GetDbContext();
            var userAccessor = new UserAccessor(dbContext);
            var userController = new UserController(userAccessor);

            //Act
            userAccessor.AddUser(user);

            var result = userController.signin("stevesmith@gmail.com", "iateapie4");
            Assert.NotNull(result);

            //Assert
            Assert.IsType<JsonHttpResult<User>>(result);
        }

        [Fact]
        public async void UserController_SignIn_Check_Status_Code_Test()
        {
            //Arrange
            var user = new User("stevesmith@gmail.com", "iateapie4", "Steve", "Smith");
            var dbContext = await GetDbContext();
            var userAccessor = new UserAccessor(dbContext);
            var userController = new UserController(userAccessor);

            //Act
            userAccessor.AddUser(user);

            var result = userController.signin("stevesmith@gmail.com", "iateapie4");

            Assert.NotNull(result);
            var jsonResult = Assert.IsType<JsonHttpResult<User>>(result);

            //Assert
            Assert.Equal(200, jsonResult.StatusCode);
        }

        [Fact]
        public async void UserController_SignIn_Check_Value_Success_Test()
        {
            //Arrange
            var user = new User("stevesmith@gmail.com", "iateapie4", "Steve", "Smith");
            var dbContext = await GetDbContext();
            var userAccessor = new UserAccessor(dbContext);
            var userController = new UserController(userAccessor);

            //Act
            userAccessor.AddUser(user);
            var result = userController.signin("stevesmith@gmail.com", "iateapie4");

            Assert.NotNull(result);
            var jsonResult = Assert.IsType<JsonHttpResult<User>>(result);
            Assert.Equal(200, jsonResult.StatusCode);

            //Assert
            var resultUser = jsonResult.Value;
            Assert.Equal(user, resultUser);
        }

        [Fact]
        public async void UserController_SignIn_Check_Type_Fail_Test()
        {
            //Arrange
            var user = new User("stevesmith@gmail.com", "iateapie4", "Steve", "Smith");
            var dbContext = await GetDbContext();
            var userAccessor = new UserAccessor(dbContext);
            var userController = new UserController(userAccessor);

            //Act
            userAccessor.AddUser(user);
            var result = userController.signin("lucyjane@gmail.com", "iateapie4");

            Assert.NotNull(result);

            //Assert
            Assert.IsNotType<JsonHttpResult<User>>(result);
        }

        [Fact]
        public async void UserController_SignUp_NotNull_Test()
        {
            //Arrange
            DateTime specific = new DateTime(2024, 5, 10, 12, 30, 0);
            var dbContext = await GetDbContext();
            var userAccessor = new UserAccessor(dbContext);
            var userController = new UserController(userAccessor);

            //Act
            var result = userController.signup("stevesmith@gmail.com", "iateapie4", "Steve", "Smith", specific, "Democratic");

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async void UserController_SignUp_Check_Type_Test()
        {
            //Arrange
            DateTime specific = new DateTime(2024, 5, 10, 12, 30, 0);
            var dbContext = await GetDbContext();
            var userAccessor = new UserAccessor(dbContext);
            var userController = new UserController(userAccessor);

            //Act
            var result = userController.signup("stevesmith@gmail.com", "iateapie4", "Steve", "Smith", specific, "Democratic");
            Assert.NotNull(result);

            //Assert
            Assert.IsType<JsonHttpResult<User>>(result);
        }

        [Fact]
        public async void UserController_SignUp_Check_Status_Code_Test()
        {
            //Arrange
            DateTime specific = new DateTime(2024, 5, 10, 12, 30, 0);
            var dbContext = await GetDbContext();
            var userAccessor = new UserAccessor(dbContext);
            var userController = new UserController(userAccessor);

            //Act
            var result = userController.signup("stevesmith@gmail.com", "iateapie4", "Steve", "Smith", specific, "Democratic");
            Assert.NotNull(result);

            var jsonResult = Assert.IsType<JsonHttpResult<User>>(result);

            //Assert
            Assert.Equal(200, jsonResult.StatusCode);
        }

        [Fact]
        public async void UserController_SignUp_Check_Value_Success_Test()
        {
            //Arrange
            DateTime specific = new DateTime(2024, 5, 10, 12, 30, 0);
            var dbContext = await GetDbContext();
            var userAccessor = new UserAccessor(dbContext);
            var userController = new UserController(userAccessor);

            //Act
            var result = userController.signup("stevesmith@gmail.com", "iateapie4", "Steve", "Smith", specific, "Democratic");
            Assert.NotNull(result);

            var jsonResult = Assert.IsType<JsonHttpResult<User>>(result);
            Assert.Equal(200, jsonResult.StatusCode);

            //Assert
            var resultUser = jsonResult.Value;
            Assert.Equal(userAccessor.GetUserByEmail("stevesmith@gmail.com"), resultUser);
        }

        [Fact]
        public async void UserController_SignUp_Check_Type_Fail_Test()
        {
            //Arrange
            DateTime specific = new DateTime(2024, 5, 10, 12, 30, 0);
            var dbContext = await GetDbContext();
            var userAccessor = new UserAccessor(dbContext);
            var userController = new UserController(userAccessor);

            //Act
            var signedUpAlready = userController.signup("stevesmith@gmail.com", "iateapie4", "Steve", "Smith", specific, "Democratic");
            Assert.NotNull(signedUpAlready);

            var result = userController.signup("stevesmith@gmail.com", "iateapie4", "Steve", "Smith", specific, "Democratic");

            //Assert
            Assert.IsNotType<JsonHttpResult<User>>(result);
        }
    }
}