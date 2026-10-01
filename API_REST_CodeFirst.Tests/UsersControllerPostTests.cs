using API_REST_CodeFirst.Controllers;
using API_REST_CodeFirst.Models.EntityFramework;
using API_REST_CodeFirst.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace API_REST_CodeFirst.Tests
{
    public class UsersControllerPostTests : TestBase
    {
        [Fact]
        public async Task PostUser_ValidUser_CreatesUser()
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

            var result = await _controller.PostUser(userToTest);

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

        [Fact]
        public async Task PostUser_InvalidModel_ReturnsBadRequest()
        {
            var userToTest = new User
            {
                Name = "Test1",
                FirstName = "User",
                Mail = "invalfid-email",
                Pwd = "password12355"
            };

            _controller.ModelState.AddModelError("Mail", "Invalid email address");

            var result = await _controller.PostUser(userToTest);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task PostUser_ValidUser_CreatesUser_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<User>>();

            var userController = new UsersController(mockRepository.Object);

            var user = new User
            {
                Name = "KATYA",                 
                FirstName = "Test",             
                Mobile = "0612345678",       
                Mail = "katya@test.com",       
                Pwd = "Test1234!",         
                Street = "1 Test Street",      
                Postcode = "74000",            
                City = "Annecy",                
                Country = "France"             
            };

            var actionResult = await userController.PostUser(user);

            var createdResult = Assert.IsType<CreatedAtActionResult>(
                actionResult.Result
            );

            var createdUser = Assert.IsType<User>(
                createdResult.Value
            );

            user.UserId = createdUser.UserId;


            Assert.Equal(user, createdUser);
        }
    }
}