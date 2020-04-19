using System;
using System.Collections.Generic;
using System.Linq;
using RandomUser.Core.Domain;
using static RandomUser.Core.Users.Generate.UserGenerator;

namespace RandomUser.Core.Tests.TestInfrastructure
{
    internal static class TestUserGenerator
    {
        private static readonly Random Random = new Random();
        private static readonly string[] TestTitles = { "Mr", "Dr" };
        private static readonly string[] TestFirstNames = { "Tim", "Josh", "Sam" };
        private static readonly string[] TestLastNames = { "Hayward", "Fisher", "Clark" };

        /// <summary>
        /// Creates a set of valid users for test usage.
        /// </summary>
        internal static IList<User> CreateUsers()
            => Enumerable.Range(1, 3)
                .Select(x => GenerateUser(TestTitles, TestFirstNames, TestLastNames, Random))
                .ToList();
    }
}
