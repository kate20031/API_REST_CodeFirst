using API_REST_CodeFirst.Models.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace API_REST_CodeFirst.Tests
{
    public class UsersControllerPostTests : TestBase
    {
        [Fact]
        public void PostUser_ValidUser_CreatesUser()
        {
            var uniqueEmail = $"test-{Guid.NewGuid()}@example.com";

            var userToTest = new User
            {
                Name = "Test",
                FirstName = "User",
                Mail = uniqueEmail,
                Pwd = "password123",
                Mobile = "0612345678",
                Street = "1 Test Street",
                Postcode = "74000",
                City = "Annecy",
                Country = "France"
            };

            var result = _controller.PostUser(userToTest).Result;

            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var createdUser = Assert.IsType<User>(createdResult.Value);

            var userFromDatabase = _context.Users
                .FirstOrDefault(u => u.Mail == uniqueEmail);

            Assert.NotNull(userFromDatabase);
            Assert.Equal(createdUser.UserId, userFromDatabase.UserId);
            Assert.Equal(userToTest.Mail, userFromDatabase.Mail);
            Assert.Equal(userToTest.Name, userFromDatabase.Name);
            Assert.Equal(userToTest.FirstName, userFromDatabase.FirstName);
        }
    }
}