using FluentAssertions;
using Moq;
using NUnit.Framework;
using RandomUser.Core.Domain;
using RandomUser.Core.Infrastructure;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RandomUser.Core.Users.GetById;
using static RandomUser.Core.Tests.TestInfrastructure.TestUserGenerator;

namespace RandomUser.Core.Tests
{
    public class GetUserByIdHandlerTests
    {
        private Mock<IUserStore> _userStore;
        private GetUserByIdHandler _handler;

        [SetUp]
        public void BeforeEach()
        {
            _userStore = new Mock<IUserStore>();
            _handler = new GetUserByIdHandler(_userStore.Object);
        }

        [Test]
        public async Task DbReturnsNoUsers_ReturnsEmptyUsers()
        {
            const string id = "99ax";
            SetupDbReturnUser(id, null);

            var command = new GetUserById(id);
            var result = await _handler.Handle(command, CancellationToken.None);

            result.Should().BeNull();
            _userStore.Verify();
        }


        [Test]
        public async Task ReturnsUserForId()
        {
            var id = Guid.NewGuid().ToString();
            var user = CreateUsers().First();
            SetupDbReturnUser(id, user);

            var command = new GetUserById(id);
            var result = await _handler.Handle(command, CancellationToken.None);

            result.Should().BeEquivalentTo(user);
            _userStore.Verify();
        }

        private void SetupDbReturnUser(string id, User user)
            => _userStore
                .Setup(x => x.GetUser(id))
                .Returns(Task.FromResult(user))
                .Verifiable();
    }
}
