using FakeItEasy;
using System.Linq.Expressions;
using UpSchool.Domain.Data;
using UpSchool.Domain.Entities;
using UpSchool.Domain.Services;

namespace UpSchool.Domain.Test.Services
{
    public class UserServiceTests
    {
        [Fact]
        public async void GetUser_ShouldGetUserWithCorrectId()
        {
            var userRepositoryMock = A.Fake<IUserRepository>();

            Guid userId = new Guid("8f319b0a-2428-4e9f-b7c6-ecf78acf00f9");

            var cancellationSource = new CancellationTokenSource();

            var expectedUser = new User()
            {
                Id = userId
            };

            A.CallTo(() => userRepositoryMock.GetByIdAsync(userId, cancellationSource.Token))
                .Returns(Task.FromResult(expectedUser));

            IUserService userService = new UserManager(userRepositoryMock);

            var user = await userService.GetByIdAsync(userId, cancellationSource.Token);

            Assert.Equal(expectedUser, user);
        }


        [Fact]
        public async void AddAsync_ShouldThrowException_WhenEmailIsEmptyOrNull()
        {
            var userRepositoryMock = A.Fake<IUserRepository>();

            var cancellationSource = new CancellationTokenSource();

            Guid userId = new Guid("8f319b0a-2428-4e9f-b7c6-ecf78acf00f9");


            var expectedUser = new User()
            {
                Id = userId,
                FirstName = "Rana",
                LastName = "Kaya",
                Age = 24,
                Email = string.Empty,
            };

            IUserService userService = new UserManager(userRepositoryMock);

            var user = await userService.AddAsync(expectedUser.FirstName,expectedUser.LastName, expectedUser.Age,expectedUser.Email, cancellationSource.Token);

            Assert.Throws<ArgumentException>(() => user);

        }


        public async Task DeleteAsync_ShouldReturnTrue_WhenUserExists()
        {
            // Arrange
            var userRepositoryMock = A.Fake<IUserRepository>();

            var cancelletionSource = new CancellationTokenSource();

            Guid userId = Guid.NewGuid();

            A.CallTo(() => userRepositoryMock.DeleteAsync(A<Expression<Func<User, bool>>>.Ignored, cancelletionSource.Token))
                .Returns(Task.FromResult(1));

            IUserService userService = new UserManager(userRepositoryMock);

            // Act
            bool result = await userService.DeleteAsync(userId, cancelletionSource.Token);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrowException_WhenUserDoesntExists()
        {
            var userRepositoryMock = A.Fake<IUserRepository>();

            var cancellationSource = new CancellationTokenSource();

            IUserService userService = new UserManager(userRepositoryMock);

            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await userService.DeleteAsync(Guid.Empty, cancellationSource.Token);
            });
        }

        [Fact]
        public async void UpdateAsync_ShouldThrowException_WhenUserIdIsEmpty()
        {
            var userRepositoryMock = A.Fake<IUserRepository>();
            var cancellationSource = new CancellationTokenSource();

            var expectedUser = new User()
            {
                Id = Guid.Empty,
                FirstName = "Rana",
                LastName = "Kaya",
                Age = 24,
                Email = "wertyukl"
            };

            IUserService userService = new UserManager(userRepositoryMock);

            var user = await userService.UpdateAsync(expectedUser, cancellationSource.Token);

            Assert.Throws<ArgumentException>(() => user);
        }

        [Fact]
        public async void UpdateAsync_ShouldThrowException_WhenUserEmailEmptyOrNull()
        {
            var userRepositoryMock = A.Fake<IUserRepository>();
            var cancellationSource = new CancellationTokenSource();

            Guid userId = new Guid("8f319b0a-2428-4e9f-b7c6-ecf78acf00f9");

            var expectedUser = new User()
            {
                Id = userId,
                FirstName = "Aleyna",
                LastName = "Meydan",
                Age = 23,
                Email = String.Empty
            };

            IUserService userService = new UserManager(userRepositoryMock);

            var user = await userService.UpdateAsync(expectedUser, cancellationSource.Token);

            Assert.Throws<ArgumentException>(() => user);

        }

        [Fact]
        public async void GetAllAsync_ShouldReturn_UserListWithAtLeastTwoRecords()
        {
            var userRepositoryMock = A.Fake<IUserRepository>();

            var cancellationSource = new CancellationTokenSource();

            List<User> userList = new List<User>()
            {
                new User(){ Id = Guid.NewGuid() },
                new User() { Id = Guid.NewGuid() }
            };

            A.CallTo(() => userRepositoryMock.GetAllAsync(cancellationSource.Token))
                .Returns(Task.FromResult(userList));

            IUserService userService = new UserManager(userRepositoryMock);

            var expectedUserList = await userService.GetAllAsync(cancellationSource.Token);

            Assert.True(expectedUserList.Count >= 2);
        }
    }
}
