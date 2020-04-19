using System;
using MediatR;

namespace RandomUser.Core.Users.Delete
{
    public class DeleteUser : IRequest<DeleteUserResult>
    {
        public string Id { get; }

        public DeleteUser(string id)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
        }
    }
}
