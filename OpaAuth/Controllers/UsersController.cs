using Microsoft.AspNetCore.Mvc;
using OpaAuth.Constants;
using OpaAuth.Contracts;
using OpaAuth.Models;

namespace OpaAuth.Controllers
{
    [ApiController]
    [Route("/api/users")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserRepository _userRepository;

        public UsersController(ILogger<UsersController> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var users = _userRepository.GetAll();
            _logger.LogInformation(LogMessages.GetAllUsersRequestSuccessful);
            return Ok(new
            {
                IsSuccess = true,
                Message = ApiMessages.UsersRetrieved,
                Data = users
            });
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            _userRepository.Add(user);
            _logger.LogInformation(LogMessages.UserCreationSuccessful, user.Name);
            return Ok(new
            {
                IsSuccess = true,
                Message = ApiMessages.UserCreated,
            });
        }
    }
}
