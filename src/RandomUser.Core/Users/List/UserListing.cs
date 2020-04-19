using System.Collections.Generic;
using RandomUser.Core.Domain;

namespace RandomUser.Core.Users.List
{
    public class UserListing
    {
        public IEnumerable<User> Users { get; set; } = new List<User>();
    }
}