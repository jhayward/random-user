using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RandomUser.Core.Domain;
using RandomUser.Core.Infrastructure;

namespace RandomUser.Core.Users.List
{
    internal class ListUsersHandler : IRequestHandler<ListUsers, UserListing>
    {
        private readonly IUserStore _userStore;

        public ListUsersHandler(IUserStore userStore)
        {
            _userStore = userStore ?? throw new ArgumentNullException(nameof(userStore));
        }

        public async Task<UserListing> Handle(ListUsers request, CancellationToken cancellationToken)
        {
            var users = await _userStore.GetUsers();

            var filteredUsers = string.IsNullOrEmpty(request.SearchQuery)
                ? users
                : NameMatches(users, request.SearchQuery);

            var limitedUsers = filteredUsers
                .OrderBy(x => x.Name.First)
                .Take(request.Limit);

            return new UserListing
            {
                Users = limitedUsers
            };
        }


        private static IEnumerable<User> NameMatches(IEnumerable<User> users, string query)
            => users.Where(x =>
                    x.Name.First.Contains(query, StringComparison.OrdinalIgnoreCase)
                    || x.Name.Last.Contains(query, StringComparison.OrdinalIgnoreCase)
               );
    }
}