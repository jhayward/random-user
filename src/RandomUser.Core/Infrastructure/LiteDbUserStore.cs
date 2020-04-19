using LiteDB;
using RandomUser.Core.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RandomUser.Core.Infrastructure
{
    public class LiteDbUserStore : IUserStore
    {
        private readonly ILiteCollection<User> _usersCollection;

        public LiteDbUserStore(LiteDatabase database)
        {
            var db = database ?? throw new ArgumentNullException(nameof(database));
            _usersCollection = db.GetCollection<User>("users");
        }

        public Task Insert(string id, User user)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(id));

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            _usersCollection.Insert(id, user);

            return Task.CompletedTask;
        }

        public Task<bool> Update(string id, User user)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(id));

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var updated = _usersCollection.Update(id, user);

            return Task.FromResult(updated);
        }

        public Task DeleteUser(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(id));

            _usersCollection.Delete(id);
            return Task.CompletedTask;
        }

        public Task<User> GetUser(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(id));

            var user = _usersCollection.FindById(id);

            return Task.FromResult(user);
        }

        public Task<IEnumerable<User>> GetUsers()
        {
            var users = _usersCollection.Find(Query.All());
            return Task.FromResult(users);
        }
    }
}
