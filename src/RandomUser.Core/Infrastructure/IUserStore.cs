using System.Collections.Generic;
using System.Threading.Tasks;
using RandomUser.Core.Domain;

namespace RandomUser.Core.Infrastructure
{
    public interface IUserStore
    {
        /// <summary>
        /// Persist a user.
        /// </summary>
        /// <param name="id">Id to save against</param>
        /// <param name="user">user to save</param>
        Task Insert(string id, User user);

        /// <summary>
        /// Update existing user.
        /// </summary>
        /// <param name="id">Id of existing user</param>
        /// <param name="user">Updated user data</param>
        /// <returns>If update was successful</returns>
        Task<bool> Update(string id, User user);

        /// <summary>
        /// Remove a user from storage.
        /// </summary>
        /// <param name="id">User Id to delete</param>
        Task DeleteUser(string id);

        /// <summary>
        /// Retrieve a user from storage.
        /// </summary>
        /// <param name="id">Id of user to get</param>
        Task<User> GetUser(string id);

        /// <summary>
        /// Get all users in storage.
        /// </summary>
        /// <returns>All users</returns>
        Task<IEnumerable<User>> GetUsers();
    }
}