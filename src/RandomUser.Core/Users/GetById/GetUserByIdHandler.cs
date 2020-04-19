using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RandomUser.Core.Domain;
using RandomUser.Core.Infrastructure;

namespace RandomUser.Core.Users.GetById
{
    internal class GetUserByIdHandler : IRequestHandler<GetUserById, User>
    {
        private readonly IUserStore _userStore;

        public GetUserByIdHandler(IUserStore userStore)
        {
            _userStore = userStore ?? throw new ArgumentNullException(nameof(userStore));
        }
        
        public Task<User> Handle(GetUserById request, CancellationToken cancellationToken)
        {
            var user = _userStore.GetUser(request.Id);

            return user;
        }
    }
}