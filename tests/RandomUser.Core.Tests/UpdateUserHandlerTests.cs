using System;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using RandomUser.Core.Domain;
using RandomUser.Core.Infrastructure;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RandomUser.Core.Users.Update;
using static RandomUser.Core.Tests.TestInfrastructure.TestUserGenerator;

namespace RandomUser.Core.Tests
{
    public class UpdateUserHandlerTests
    {
        private Mock<IUserStore> _userStore;
        private UpdateUserHandler _handler;

        [SetUp]
        public void BeforeEach()
        {
            _userStore = new Mock<IUserStore>();
            _handler = new UpdateUserHandler(_userStore.Object);
        }

        [Test]
        public async Task UserNotFound_ReturnsError()
        {
            var id = Guid.NewGuid().ToString();
            var user = CreateUsers().First();
            SetupDbReturnUsers(id, user, false);


            var command = new UpdateUser(id, user);
            var result = await _handler.Handle(command, CancellationToken.None);

            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            _userStore.Verify();
        }


        [Test]
        public async Task ReturnsAllUsers()
        {
            var id = Guid.NewGuid().ToString();
            var user = CreateUsers().First();
            SetupDbReturnUsers(id, user, true);

            var command = new UpdateUser(id, user);
            var result = await _handler.Handle(command, CancellationToken.None);

            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            _userStore.Verify();
        }

        private void SetupDbReturnUsers(string id, User user, bool result)
            => _userStore
                .Setup(x => x.Update(id, user))
                .ReturnsAsync(result)
                .Verifiable();
    }
}
