using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RandomUser.Core.Domain;
using RandomUser.Core.Infrastructure;
using static RandomUser.Core.Users.Generate.UserGenerator;

namespace RandomUser.Core.Users.Generate
{
    internal class GenerateUsersHandler : IRequestHandler<GenerateUsers, GeneratedUserListing>
    {
        private readonly ISeedDataStore _seedDataStore;
        private readonly IUserStore _userStore;

        public GenerateUsersHandler(ISeedDataStore seedDataStore, IUserStore userStore)
        {
            _seedDataStore = seedDataStore ?? throw new ArgumentNullException(nameof(seedDataStore));
            _userStore = userStore ?? throw new ArgumentNullException(nameof(userStore));
        }

        public async Task<GeneratedUserListing> Handle(GenerateUsers request, CancellationToken cancellationToken)
        {
            var seed = request.Seed != 0
                ? request.Seed
                : DateTime.UtcNow.Millisecond;

            var randomGenerator = new Random(seed);

            var newUsers = new List<User>(); 
            for (int i = 0; i < request.Limit; i++)
            {
                var id = Guid.NewGuid().ToString();
                var user = GenerateUser(
                    _seedDataStore.Titles().ToList(),
                    _seedDataStore.FirstNames().ToList(),
                    _seedDataStore.LastNames().ToList(), randomGenerator);

                await _userStore.Insert(id, user);
                newUsers.Add(user);
            }

            return new GeneratedUserListing(seed, newUsers);
        }
    }
}