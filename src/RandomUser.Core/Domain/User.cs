using NodaTime;

namespace RandomUser.Core.Domain
{
    public class User
    {
        public User()
        {
        }

        public User(Name name, string email, LocalDate dateOfBirth, string phoneNumber)
        {
            Email = email;
            Name = name;
            DateOfBirth = dateOfBirth;
            PhoneNumber = phoneNumber;
        }

        /// <summary>
        /// Identifier for user
        /// </summary>
        public string Id { get; set; } = string.Empty;
        public Name Name { get; set; }
        public LocalDate DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfileImage { get; set; } = "/img/profile.png";
        public string ProfileThumbImage { get; set; } = "/img/profile-small.png";
    }
}