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
    public class UserAccessorTests
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
        public async void UserAccessor_SignIn_Success_Test()
        {
            var user = new User("stevesmith@gmail.com", "iateapie4", "Steve", "Smith");
            var dbContext = await GetDbContext();
            var userAccessor = new UserAccessor(dbContext);

            //Act
            userAccessor.AddUser(user);
            var result = userAccessor.SignIn("stevesmith@gmail.com", "iateapie4");

            //Assert
            Assert.Equal(user, result);
        }

        [Fact]
        public async void UserAccessor_SignIn_Fail_Test()
        {
            var user = new User("stevesmith@gmail.com", "iateapie4", "Steve", "Smith");
            var dbContext = await GetDbContext();
            var userAccessor = new UserAccessor(dbContext);

            //Act
            userAccessor.AddUser(user);
            var result = userAccessor.SignIn("stevesmith@gmail", "iateapie4");

            //Assert
            Assert.False(user.Equals(result));
        }

        [Fact]
        public async void UserAccessor_GetUserById_Success_Test()
        {
            var user = new User("stevesmith@gmail.com", "iateapie4", "Steve", "Smith");
            var dbContext = await GetDbContext();
            var userAccessor = new UserAccessor(dbContext);

            //Act
            userAccessor.AddUser(user);
            var result = userAccessor.GetUserById(1);

            //Assert
            Assert.Equal(user, result);
        }

        [Fact]
        public async void UserAccessor_GetUserById_Fail_Test()
        {
            var user = new User("stevesmith@gmail.com", "iateapie4", "Steve", "Smith");
            var dbContext = await GetDbContext();
            var userAccessor = new UserAccessor(dbContext);

            //Act
            userAccessor.AddUser(user);
            var result = userAccessor.GetUserById(3);

            //Assert
            Assert.False(user.Equals(result));
        }

        [Fact]
        public async void UserAccessor_GetUserByEmail_Success_Test()
        {
            var user = new User("stevesmith@gmail.com", "iateapie4", "Steve", "Smith");
            var dbContext = await GetDbContext();
            var userAccessor = new UserAccessor(dbContext);

            //Act
            userAccessor.AddUser(user);
            var result = userAccessor.GetUserByEmail("stevesmith@gmail.com");

            //Assert
            Assert.Equal(user, result);
        }

        [Fact]
        public async void UserAccessor_GetUserByEmail_Fail_Test()
        {
            var user = new User("stevesmith@gmail.com", "iateapie4", "Steve", "Smith");
            var dbContext = await GetDbContext();
            var userAccessor = new UserAccessor(dbContext);

            //Act
            userAccessor.AddUser(user);
            var result = userAccessor.GetUserByEmail("Stevesmith@gmail.com");

            //Assert
            Assert.False(user.Equals(result));
        }

        [Fact]
        public async void UserAccessor_GetAllUsers_Success_Test()
        {
            var user = new User("stevesmith@gmail.com", "iateapie4", "Steve", "Smith");
            var user2 = new User("johndoe@gmail.com", "my1name2is3john4doe", "John", "Doe", DateTime.Now, "D");
            var dbContext = await GetDbContext();
            var userAccessor = new UserAccessor(dbContext);

            //Act
            userAccessor.AddUser(user);
            userAccessor.AddUser(user2);

            List<User> userList = [user, user2];

            var result = userAccessor.GetAllUsers();

            //Assert
            Assert.Equal(userList, result);
        }

        [Fact]
        public async void UserAccessor_Add_Success_Test1()
        {
            var user = new User("stevesmith@gmail.com", "iateapie4", "Steve", "Smith");
            var dbContext = await GetDbContext();
            var userAccessor = new UserAccessor(dbContext);

            //Act
            var result = userAccessor.AddUser(user);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async void UserAccessor_Add_Success_Test2()
        {
            var user = new User("johndoe@gmail.com", "my1name2is3john4doe", "John", "Doe", DateTime.Now, "D");
            var dbContext = await GetDbContext();
            var userAccessor = new UserAccessor(dbContext);

            //Act
            var result = userAccessor.AddUser(user);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async void UserAccessor_Update_Success_Test1()
        {
            var user = new User("stevesmith@gmail.com", "iateapie4", "Steve", "Smith");
            var dbContext = await GetDbContext();
            var userAccessor = new UserAccessor(dbContext);

            //Act
            userAccessor.AddUser(user);
            user.FirstName = "Steven";
            var result = userAccessor.UpdateUser(user);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async void UserAccessor_Update_Success_Test2()
        {
            var user = new User("stevesmith@gmail.com", "iateapie4", "Steve", "Smith");
            var dbContext = await GetDbContext();
            var userAccessor = new UserAccessor(dbContext);

            //Act
            userAccessor.AddUser(user);
            user.Affiliation = "D";
            var result = userAccessor.UpdateUser(user);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async void UserAccessor_Update_Success_Test3()
        {
            var user = new User("johndoe@gmail.com", "my1name2is3john4doe", "John", "Doe", DateTime.Now, "D");
            var dbContext = await GetDbContext();
            var userAccessor = new UserAccessor(dbContext);

            //Act
            userAccessor.AddUser(user);
            user.Affiliation = "R";
            var result = userAccessor.UpdateUser(user);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async void UserAccessor_Delete_Success_Test1()
        {
            var user = new User("stevesmith@gmail.com", "iateapie4", "Steve", "Smith");
            var dbContext = await GetDbContext();
            var userAccessor = new UserAccessor(dbContext);

            //Act
            userAccessor.AddUser(user);
            var result = userAccessor.DeleteUser(1);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async void UserAccessor_Delete_Success_Test2()
        {
            var user = new User("johndoe@gmail.com", "my1name2is3john4doe", "John", "Doe", DateTime.Now, "D");
            var dbContext = await GetDbContext();
            var userAccessor = new UserAccessor(dbContext);

            //Act
            userAccessor.AddUser(user);
            var result = userAccessor.DeleteUser(1);

            //Assert
            Assert.True(result);
        }
    }
}