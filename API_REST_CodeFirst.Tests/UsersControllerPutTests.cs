using API_REST_CodeFirst.Models.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace API_REST_CodeFirst.Tests
{
    public class UsersControllerPutTests : TestBase
    {
        [Fact]
        public void PutUser_ValidUser_UpdatesUser()
        {
            var originalEmail = $"put-test-{Guid.NewGuid()}@example.com";

            var user = new User
            {
                Name = "OriginalName",
                FirstName = "OriginalFirstName",
                Mail = originalEmail,
                Pwd = "password123",
                Mobile = "0612345678",
                Street = "Original Street",
                Postcode = "74000",
                City = "Annecy",
                Country = "France"
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            var userId = user.UserId;

            user.Name = "UpdatedName";
            user.FirstName = "UpdatedFirstName";
            user.Mail = $"put-updated-{Guid.NewGuid()}@example.com";
            user.Street = "Updated Street";
            var result = _controller.PutUser(userId, user).Result;

            Assert.IsType<NoContentResult>(result);

            var userFromDatabase = _context.Users
                .FirstOrDefault(u => u.UserId == userId);

            Assert.NotNull(userFromDatabase);
            Assert.Equal("UpdatedName", userFromDatabase.Name);
            Assert.Equal("UpdatedFirstName", userFromDatabase.FirstName);
            Assert.Equal(user.Mail, userFromDatabase.Mail);
            Assert.Equal("Updated Street", userFromDatabase.Street);

            _context.Users.Remove(userFromDatabase);
            _context.SaveChanges();
        }
    }
}