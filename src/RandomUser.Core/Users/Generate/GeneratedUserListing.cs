using System;
using System.Collections.Generic;
using RandomUser.Core.Domain;

namespace RandomUser.Core.Users.Generate
{
    public class GeneratedUserListing
    {
        /// <summary>
        /// Seed used to generate users.
        /// </summary>
        public int Seed { get; }

        /// <summary>
        /// List of generated users.
        /// </summary>
        public IEnumerable<User> Users { get; }

        public GeneratedUserListing(int seed, IEnumerable<User> users)
        {
            Seed = seed;
            Users = users ?? throw new ArgumentNullException(nameof(users));
        }
    }
}