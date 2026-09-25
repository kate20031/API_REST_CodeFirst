using API_REST_CodeFirst.Controllers;
using API_REST_CodeFirst.Models.EntityFramework;
using API_REST_CodeFirst.Repositories;
using Microsoft.EntityFrameworkCore;

namespace API_REST_CodeFirst.Tests
{
    public class TestBase
    {
        protected readonly CinemaContext _context;
        protected readonly IUserRepository _repository;
        protected readonly UsersController _controller;

        public TestBase()
        {
            var options = new DbContextOptionsBuilder<CinemaContext>()
                .UseNpgsql(
                    "Host=localhost;Port=5432;Database=cinema;Username=postgres;Password=postgres")
                .Options;

            _context = new CinemaContext(options);

            _repository = new UserRepository(_context);
            _controller = new UsersController(_repository);
        }
    }
}