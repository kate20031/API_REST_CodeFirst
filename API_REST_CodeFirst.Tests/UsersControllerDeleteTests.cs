using API_REST_CodeFirst.Models.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace API_REST_CodeFirst.Tests
{
    public class UsersControllerDeleteTests : TestBase
    {
        [Fact]
        public async Task DeleteUser_ExistingUser_DeletesUser()
        {
            var user = new User
            {
                Name = "DeleteTest",
                FirstName = "User5",
                Mail = $"delete-test-{Guid.NewGuid()}@example.com",
                Pwd = "paffzef",
                Mobile = "0612345678",
                Street = "Test  Street",
                Postcode = "74000",
                City = "Annecy",
                Country = "France"
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            var userId = user.UserId;

            var result = await _controller.DeleteUser(userId);

            Assert.IsType<NoContentResult>(result);

            var deletedUser = _context.Users
                .FirstOrDefault(u => u.UserId == userId);

            Assert.Null(deletedUser);
        }
    }
}