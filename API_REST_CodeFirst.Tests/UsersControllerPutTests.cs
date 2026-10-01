using API_REST_CodeFirst.Controllers;
using API_REST_CodeFirst.Models.EntityFramework;
using API_REST_CodeFirst.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace API_REST_CodeFirst.Tests
{
    public class UsersControllerPutTests : TestBase
    {
        [Fact]
        public async Task PutUser_ValidUser_UpdatesUser()
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

            var result = await _controller.PutUser(userId, user);

            Assert.IsType<NoContentResult>(result);

            var userFromDatabase = _context.Users.FirstOrDefault(u => u.UserId == userId);

            Assert.NotNull(userFromDatabase);
            Assert.Equal("UpdatedName", userFromDatabase.Name);
            Assert.Equal("UpdatedFirstName", userFromDatabase.FirstName);
            Assert.Equal(user.Mail, userFromDatabase.Mail);
            Assert.Equal("Updated Street", userFromDatabase.Street);

            _context.Users.Remove(userFromDatabase);
            _context.SaveChanges();
        }

        [Fact]
        public async Task PutUser_ExistingUser_UpdatesUser_AvecMoq()
        {
            var existingUser = new User
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

            var updatedUser = new User
            {
                UserId = 1,
                Name = "Moreau",
                FirstName = "Élise",
                Mobile = "0678123456",
                Mail = "elise.moreau@gmail.com",
                Pwd = "Test1234!",
                Street = "Rue de la République",
                Postcode = "74000",
                City = "Annecy",
                Country = "France"
            };


            var mockRepository = new Mock<IDataRepository<User>>();

            mockRepository.Setup(x => x.GetById(existingUser.UserId)).Returns(new ActionResult<User>(existingUser));

            var userController =
                new UsersController(mockRepository.Object);

            var result = await userController.PutUser(existingUser.UserId,updatedUser);

            Assert.IsType<NoContentResult>(result);

            mockRepository.Verify(x => x.UpdateAsync(existingUser, It.Is<User>(u => u.UserId == 1
            && u.Name == "Moreau" && u.FirstName == "Élise" &&
            u.Mobile == "0678123456" && u.Mail == "elise.moreau@gmail.com" && u.Pwd == "Test1234!" 
            && u.Street == "Rue de la République" && u.Postcode == "74000" && u.City == "Annecy" && u.Country == "France")), Times.Once);
        }
    }
}