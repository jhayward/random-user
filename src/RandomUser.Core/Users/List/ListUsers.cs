using MediatR;

namespace RandomUser.Core.Users.List
{
    public class ListUsers : IRequest<UserListing>
    {
        /// <summary>
        /// Limit the number of users returned
        /// </summary>
        public int Limit { get; set; } = 20;

        /// <summary>
        /// Limit results to user where the first
        /// or last names contain the given query.
        /// </summary>
        /// <remarks>Empty will match all</remarks>
        public string SearchQuery { get; set; } = "";
    }
}