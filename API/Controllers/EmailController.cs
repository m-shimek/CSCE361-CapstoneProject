using Microsoft.AspNetCore.Mvc;
using MyVotingSystem.Accessors;
using MyVotingSystem.Models;
using MyVotingSystem.Engines;

namespace MyVotingSystem.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class EmailController: Controller
    {
		EmailSender _emailSender;
        private readonly IUserAccessor _userAccessor;

        public EmailController(IUserAccessor userAccessor)
        {
            _userAccessor = userAccessor;
			_emailSender = new EmailSender();
        }

		[HttpGet]
		public IResult confirmAccount(string email){
			string code = AuthenticationCodeEngine.create(email);
			_emailSender.Send(email,"Pacopolis Online Voting Account Confirmation", code);
			return Results.Ok();
		}

		[HttpGet]
		public IResult verifyConfirmationCode(string email, string confirmationCode){
			if (AuthenticationCodeEngine.verify(email, confirmationCode)) {
				User user = _userAccessor.GetUserByEmail(email);
				user.IsVerified = true;
				_userAccessor.UpdateUser(user);
				return Results.Ok();
			} else {
				return Results.BadRequest();
			}
		}

	}
}