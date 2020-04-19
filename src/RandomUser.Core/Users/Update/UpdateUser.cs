using System;
using MediatR;
using RandomUser.Core.Domain;
using RandomUser.Core.Users.Delete;

namespace RandomUser.Core.Users.Update
{
    public class UpdateUser : IRequest<DeleteUserResult>
    {
        public string Id { get; }
        public User User { get; }

        public UpdateUser(string id, User user)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            User = user ?? throw new ArgumentNullException(nameof(user));
        }
    }
}
