using System;
using System.Collections.Generic;
using System.Linq;
using NodaTime;
using NodaTime.Extensions;
using RandomUser.Core.Domain;

namespace RandomUser.Core.Users.Generate
{
    internal class UserGenerator
    {
        internal static User GenerateUser(IReadOnlyList<string> titles, IReadOnlyList<string> firstNames, IReadOnlyList<string> lastNames, Random randomGenerator)
        {
            var name = GenerateName(randomGenerator, titles, firstNames, lastNames);
            var email = GenerateEmail(name);
            var dateOfBirth = GenerateBirthDate(randomGenerator);
            var phoneNumber = GenerateMobile(randomGenerator);

            return new User(name, email, dateOfBirth, phoneNumber);
        }

        internal static Name GenerateName(Random randomGenerator, IReadOnlyList<string> titles, IReadOnlyList<string> firstNames, IReadOnlyList<string> lastNames)
        {
            var title = PickOne(titles, randomGenerator);
            var firstName = PickOne(firstNames, randomGenerator);
            var lastName = PickOne(lastNames, randomGenerator);
            return new Name(title, firstName, lastName);
        }

        internal static string GenerateMobile(Random randomGenerator)
        {
            const string prefix = "021";
            var numbers = Enumerable.Repeat(0, 7)
                .Select(x => randomGenerator.Next(0, 10).ToString());
            var phoneNumber = string.Join(string.Empty, numbers);

            return $"{prefix} {phoneNumber}";
        }

        internal static LocalDate GenerateBirthDate(Random randomGenerator)
        {
            var age = randomGenerator.Next(20, 100);
            var month = randomGenerator.Next(1, 13);
            var day = randomGenerator.Next(1, 29);
            var clock = SystemClock.Instance.InUtc();
            var year = clock.GetCurrentDate().Year - age;

            return new LocalDate(year, month, day);
        }

        internal static string GenerateEmail(Name name)
            => $"{name.First}.{name.Last}@example.com";

        internal static T PickOne<T>(IReadOnlyList<T> items, Random randomGenerator)
            => items.Skip(randomGenerator.Next(0, items.Count)).First();
    }
}