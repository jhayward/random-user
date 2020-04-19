using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RandomUser.Core.Infrastructure;

namespace RandomUser.Core.Users.Delete
{
    internal class DeleteUserHandler : IRequestHandler<DeleteUser, DeleteUserResult>
    {
        private readonly IUserStore _userStore;

        public DeleteUserHandler(IUserStore userStore)
        {
            _userStore = userStore ?? throw new ArgumentNullException(nameof(userStore));
        }

        public async Task<DeleteUserResult> Handle(DeleteUser request, CancellationToken cancellationToken)
        {
            await _userStore.DeleteUser(request.Id);
            
            return new DeleteUserResult
            {
                Success = true
            };
        }
    }
}
