using API_REST_CodeFirst.Controllers;
using API_REST_CodeFirst.Models.EntityFramework;
using API_REST_CodeFirst.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using static System.Net.Mime.MediaTypeNames;

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
                Street = "Test Street",
                Postcode = "74000",
                City = "Annecy",
                Country = "France"
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            var userId = user.UserId;

            var result = await _controller.DeleteUser(userId);

            Assert.IsType<NoContentResult>(result);

            var deletedUser = _context.Users.FirstOrDefault(u => u.UserId == userId);

            Assert.Null(deletedUser);
        }

        [Fact]
        public async Task DeleteUser_ExistingUser_ReturnsNoContent_AvecMoq()
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
                .Setup(x => x.GetById(user.UserId))
                .Returns(new ActionResult<User>(user));

            var userController =
                new UsersController(mockRepository.Object);
            var result = await userController.DeleteUser(user.UserId);
            Assert.IsType<NoContentResult>(result);
        }
    }
}
