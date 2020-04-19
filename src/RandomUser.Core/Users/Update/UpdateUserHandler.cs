using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RandomUser.Core.Infrastructure;
using RandomUser.Core.Users.Delete;

namespace RandomUser.Core.Users.Update
{
    internal class UpdateUserHandler : IRequestHandler<UpdateUser, DeleteUserResult>
    {
        private readonly IUserStore _userStore;

        public UpdateUserHandler(IUserStore userStore)
        {
            _userStore = userStore ?? throw new ArgumentNullException(nameof(userStore));
        }

        public async Task<DeleteUserResult> Handle(UpdateUser request, CancellationToken cancellationToken)
        {
            var updated = await _userStore.Update(request.Id, request.User);
            return new DeleteUserResult
            {
                Success = updated
            };
        }
    }
}
