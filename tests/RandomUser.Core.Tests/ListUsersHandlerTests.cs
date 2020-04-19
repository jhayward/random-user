using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using RandomUser.Core.Infrastructure;
using RandomUser.Core.Users.List;
using static RandomUser.Core.Tests.TestInfrastructure.TestUserGenerator;

namespace RandomUser.Core.Tests
{
    public class ListUsersHandlerTests
    {
        private Mock<IUserStore> _userStore;
        private ListUsersHandler _handler;

        [SetUp]
        public void BeforeEach()
        {
            _userStore = new Mock<IUserStore>();
            _handler = new ListUsersHandler(_userStore.Object);
        }

        [Test]
        public async Task DbReturnsNoUsers_ReturnsEmptyUsers()
        {
            SetupDbReturnUsers(Enumerable.Empty<Domain.User>());

            var command = new ListUsers();
            var result = await _handler.Handle(command, CancellationToken.None);

            result.Should().NotBeNull();
            result.Users.Should().BeEmpty();
            _userStore.Verify();
        }


        [Test]
        public async Task ReturnsAllUsers()
        {
            var users = CreateUsers();
            users.Should().NotBeEmpty();

            SetupDbReturnUsers(users);

            var command = new ListUsers();
            var result = await _handler.Handle(command, CancellationToken.None);

            result.Should().NotBeNull();
            result.Users.Should().BeEquivalentTo(users);
            _userStore.Verify();
        }

        [Test]
        public async Task GivenLimit_LimitsNumberOfUsersReturned()
        {
            var users = CreateUsers();
            users.Should().HaveCountGreaterThan(1);

            SetupDbReturnUsers(users);

            var command = new ListUsers
            {
                Limit = 1
            };
            var result = await _handler.Handle(command, CancellationToken.None);

            result.Should().NotBeNull();
            result.Users.Should().HaveCount(1);
            _userStore.Verify();
        }

        [Test]
        public async Task GivenSearchQuery_FiltersUsersByFirstName()
        {
            var users = CreateUsers();
            users.Should().HaveCountGreaterThan(1);
            users.Skip(1).First().Name.First = "Bruce";

            SetupDbReturnUsers(users);

            var command = new ListUsers
            {
                SearchQuery = "bru"
            };
            var result = await _handler.Handle(command, CancellationToken.None);

            result.Should().NotBeNull();
            result.Users.Should().HaveCount(1);
            result.Users.First().Name.First.Should().Be("Bruce");
            _userStore.Verify();
        }

        [Test]
        public async Task GivenSearchQuery_FiltersUsersByLastName()
        {
            var users = CreateUsers();
            users.Should().HaveCountGreaterThan(1);
            users.Skip(1).First().Name.Last = "Kent";
            
            SetupDbReturnUsers(users);

            var command = new ListUsers
            {
                SearchQuery = "kent"
            };
            var result = await _handler.Handle(command, CancellationToken.None);

            result.Should().NotBeNull();
            result.Users.Should().HaveCount(1);
            result.Users.First().Name.Last.Should().Be("Kent");
            _userStore.Verify();
        }

        private void SetupDbReturnUsers(IEnumerable<Domain.User> users)
            => _userStore
                .Setup(x => x.GetUsers())
                .Returns(Task.FromResult(users))
                .Verifiable();
    }
}
