using Microsoft.AspNetCore.Mvc;
using MyVotingSystem.Accessors;
using MyVotingSystem.Data;
using MyVotingSystem.Models;

namespace MyVotingSystem.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserAccessor _userAccessor;

        public UserController(IUserAccessor userAccessor)
        {
            _userAccessor = userAccessor;
        }

        [HttpGet]
        public IResult signin(string email, string password)
        {
            User? user = _userAccessor.SignIn(email, password);
            if (user == null)
            {
                return Results.Json(new { }, statusCode: 401);
            }
            return Results.Json(user, statusCode: 200);
        }

        [HttpGet]
        public IResult signup(string email, string password, string firstName, string lastName, DateTime? dateOfBirth, string? partyAffiliation)
        {
            User newUser = new User(email, password, firstName, lastName, dateOfBirth, partyAffiliation);

            User? userWithEmail = _userAccessor.GetUserByEmail(email);
            if (userWithEmail != null)
            {
                return Results.Conflict($"User with email {email} exists");
            }
            else
            {
                _userAccessor.AddUser(newUser);
                return Results.Json(newUser, statusCode: 200);
            }

        }

    }
}