using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OpaAuth.Contracts;
using OpaAuth.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
            _logger.LogInformation("Get All Users");
            var users = _userRepository.GetAll();
            return Ok(users);
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            _userRepository.Add(user);
            _logger.LogInformation($"Create New User : {user.Name}");
            return Ok("Succesfully");
        }
    }
}
