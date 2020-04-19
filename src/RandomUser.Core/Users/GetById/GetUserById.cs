using System;
using MediatR;
using RandomUser.Core.Domain;

namespace RandomUser.Core.Users.GetById
{
    public class GetUserById : IRequest<User>
    {
        public string Id { get; }

        public GetUserById(string id)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
        }
    }
}