using MediatR;

namespace RandomUser.Core.Users.Generate
{
    public class GenerateUsers : IRequest<GeneratedUserListing>
    {
        /// <summary>
        /// Number of users to generate.
        /// </summary>
        public int Limit { get; set; } = 20;

        /// <summary>
        /// Seed for generation.
        /// </summary>
        public int Seed { get; set; } = 0;
    }
}