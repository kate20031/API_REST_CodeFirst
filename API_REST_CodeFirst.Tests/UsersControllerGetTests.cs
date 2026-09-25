using API_REST_CodeFirst.Models.EntityFramework;
using Microsoft.AspNetCore.Mvc;

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
    }
}