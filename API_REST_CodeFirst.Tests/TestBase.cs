using API_REST_CodeFirst.Controllers;
using API_REST_CodeFirst.Models.DataManager;
using API_REST_CodeFirst.Models.EntityFramework;
using API_REST_CodeFirst.Models.Repository;
using Microsoft.EntityFrameworkCore;

namespace API_REST_CodeFirst.Tests
{
    public class TestBase
    {
        protected readonly CinemaContext _context;
        protected readonly IDataRepository<User> _dataRepository;
        protected readonly UsersController _controller;

        public TestBase()
        {
            var options = new DbContextOptionsBuilder<CinemaContext>()
                .UseNpgsql(
                    "Host=localhost;Port=5432;Database=cinema;Username=postgres;Password=postgres")
                .Options;

            _context = new CinemaContext(options);

            _dataRepository = new UserManager(_context);
            _controller = new UsersController(_dataRepository);
        }
    }
}