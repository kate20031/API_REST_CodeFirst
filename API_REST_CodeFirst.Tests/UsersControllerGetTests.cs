using API_REST_CodeFirst.Models.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace API_REST_CodeFirst.Tests
{
    public class UsersControllerGetTests : TestBase
    {
        [Fact]
        public void GetUsers_ReturnsAllUsers()
        {
            var expectedUsers = _context.Users.ToList();

            var result = _controller.GetUsers().Result;
            var actualUsers = result.Value;

            Assert.NotNull(actualUsers);
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
        public void GetUtilisateurById_ExistingUser_ReturnsUser()
        {
            var expectedUser = _context.Users.First();

            var result = _controller
                .GetUtilisateurById(expectedUser.UserId)
                .Result;

            Assert.NotNull(result.Value);
            Assert.Equal(expectedUser.UserId, result.Value.UserId);
            Assert.Equal(expectedUser.Mail, result.Value.Mail);
        }

        [Fact]
        public void GetUtilisateurById_NonExistingUser_ReturnsNotFound()
        {
            var existingIds = _context.Users
                .Select(u => u.UserId)
                .ToList();

            var nonExistingId = existingIds.Count == 0
                ? 999999
                : existingIds.Max() + 1;

            var result = _controller
                .GetUtilisateurById(nonExistingId)
                .Result;

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetUserByEmail_ExistingUser_ReturnsUser()
        {
            var expectedUser = _context.Users.First();

            var result = _controller
                .GetUserByEmail(expectedUser.Mail)
                .Result;

            Assert.NotNull(result.Value);
            Assert.Equal(expectedUser.UserId, result.Value.UserId);
            Assert.Equal(expectedUser.Mail, result.Value.Mail);
        }

        [Fact]
        public void GetUserByEmail_NonExistingUser_ReturnsNotFound()
        {
            var email = "definitely-not-existing-" + Guid.NewGuid()
                + "@example.com";

            var result = _controller
                .GetUserByEmail(email)
                .Result;

            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}