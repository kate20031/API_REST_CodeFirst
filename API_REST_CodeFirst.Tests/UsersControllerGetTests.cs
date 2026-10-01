using API_REST_CodeFirst.Controllers;
using API_REST_CodeFirst.Models.EntityFramework;
using API_REST_CodeFirst.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace API_REST_CodeFirst.Tests
{
    public class UsersControllerGetTests : TestBase
    {
        [Fact]
        public async Task GetUsers_ReturnsAllUsers()
        {
            var expectedUsers = _context.Users.ToList();

            var result = await _controller.GetUsers();

            var actualUsers = Assert.IsAssignableFrom<IEnumerable<User>>(result.Value);

            Assert.Equal(expectedUsers.Count, actualUsers.Count());

            foreach (var expectedUser in expectedUsers)
            {
                var actualUser = actualUsers
                    .FirstOrDefault(u => u.UserId == expectedUser.UserId);

                Assert.NotNull(actualUser);
                Assert.Equal(expectedUser.Mail, actualUser.Mail);
                Assert.Equal(expectedUser.Name, actualUser.Name);
                Assert.Equal(expectedUser.FirstName, actualUser.FirstName);
            }
        }

        [Fact]
        public async Task GetUtilisateurById_ExistingUser_ReturnsUser()
        {
            var expectedUser = _context.Users.First();

            var result = await _controller
                .GetUtilisateurById(expectedUser.UserId);

            Assert.NotNull(result.Value);
            Assert.Equal(expectedUser.UserId, result.Value.UserId);
            Assert.Equal(expectedUser.Mail, result.Value.Mail);
        }

        [Fact]
        public async Task GetUtilisateurById_NonExistingUser_ReturnsNotFound()
        {
            var existingIds = _context.Users
                .Select(u => u.UserId)
                .ToList();

            var nonExistingId = existingIds.Count == 0
                ? 999999
                : existingIds.Max() + 1;

            var result = await _controller
                .GetUtilisateurById(nonExistingId);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetUserByEmail_ExistingUser_ReturnsUser()
        {
            var expectedUser = _context.Users.First();

            var result = await _controller
                .GetUserByEmail(expectedUser.Mail);

            Assert.NotNull(result.Value);
            Assert.Equal(expectedUser.UserId, result.Value.UserId);
            Assert.Equal(expectedUser.Mail, result.Value.Mail);
        }

        [Fact]
        public async Task GetUserByEmail_NonExistingUser_ReturnsNotFound()
        {
            var email = "defcditelynot-existing-" + Guid.NewGuid()
                + "@example.com";

            var result = await _controller
                .GetUserByEmail(email);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetUtilisateurById_ExistingUser_ReturnsUser_AvecMoq()
        {

            var user = new User
            {
                UserId = 1,
                Name = "Dubois",
                FirstName = "Camille",
                Mobile = "0612345678",
                Mail = "camille.dubois@gmail.com",
                Pwd = "Test1234!",

                Street = "Impasse des bergeronneses",
                Postcode = "74200",
                City = "Allinges",
                Country = "France"
            };

            var mockRepository = new Mock<IDataRepository<User>>();

            mockRepository
                .Setup(x => x.GetById(1))
                .Returns(new ActionResult<User>(user));

            var userController =
                new UsersController(mockRepository.Object);

            var result = await userController.GetUtilisateurById(1);

            Assert.NotNull(result.Value);
            Assert.Equal(user, result.Value);
        }

        [Fact]
        public async Task GetUtilisateurById_UnknownId_ReturnsNotFound_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<User>>();

            mockRepository
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(new Microsoft.AspNetCore.Mvc.ActionResult<User>(
                    new NotFoundResult()
                ));

            var userController =
                new UsersController(mockRepository.Object);

            var result = await userController.GetUtilisateurById(0);

            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}