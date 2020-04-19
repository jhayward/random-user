using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RandomUser.Core.Domain;
using RandomUser.Core.Users.Delete;
using RandomUser.Core.Users.Generate;
using RandomUser.Core.Users.GetById;
using RandomUser.Core.Users.List;
using RandomUser.Core.Users.Update;

namespace RandomUser.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Get stored users.
        /// </summary>
        [HttpGet]
        [Produces(typeof(UserListing))]
        public async Task<UserListing> Get([FromQuery]ListUsers command)
        {
            var listing = await _mediator.Send(command);
            return listing;
        }

        /// <summary>
        /// Get a specific user.
        /// </summary>
        /// <param name="id">Id of the user to retrieve</param>
        [HttpGet("{id}")]
        [Produces(typeof(User))]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _mediator.Send(new GetUserById(id));
            
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        /// <summary>
        /// Update a users details.
        /// </summary>
        /// <param name="id">Id of user to update</param>
        /// <param name="user">New user data</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, User user)
        {
            var result = await _mediator.Send(new UpdateUser(id, user));
            
            if (!result.Success)
            {
                return NotFound();
            }

            return Ok();
        }

        /// <summary>
        /// Remove a user from storage.
        /// </summary>
        /// <param name="id">Id of user to remove</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            await _mediator.Send(new DeleteUser(id));
            return Ok();
        }
        
        /// <summary>
        /// Generate random users.
        /// </summary>
        [HttpPost]
        public async Task<GeneratedUserListing> GenerateUsers([FromQuery]GenerateUsers command)
        {
            return await _mediator.Send(command);
        }
    }
}
