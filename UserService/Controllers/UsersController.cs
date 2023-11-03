using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using UserService.AuthService;
using UserService.Data;
using UserService.Dtos;
using UserService.Models;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUserRepo _repository;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public UsersController(IUserRepo repository, IMapper mapper, IAuthService authService)
        {
            _repository = repository;
            _mapper = mapper;
            _authService = authService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserReadDto>> GetUsers()
        {
            Console.WriteLine("--> Getting Users");
            var users = _repository.GetUsers();
            //return Ok(users);
            return Ok(_mapper.Map<IEnumerable<UserReadDto>>(users));
        }
        [HttpGet("id/{userId}")]
        public ActionResult<UserReadDto> GetUserById(int userId)
        {
            Console.WriteLine("--> Getting user by Id");
            var userItem = _repository.GetUserById(userId);
            if (userItem != null)
            {
                return Ok(_mapper.Map<UserReadDto>(userItem));
            }
            else
            {
                return NotFound();
            }

        }

        [HttpGet("name/{username}")]
        public ActionResult<UserReadDto> GetUserByName(string username)
        {
            Console.WriteLine("--> Getting user by Id");
            var userItem = _repository.GetUserByName(username);
            if (userItem != null)
            {
                return Ok(_mapper.Map<UserReadDto>(userItem));
            }
            else
            {
                return NotFound();
            }

        }

        [HttpPost("register")]
        public IActionResult CreateUser(UserCreateDto userCreateDto)
        {
            Console.WriteLine("--> Creating user");
            var userExists = _repository.GetUserByName(userCreateDto.Username);
            if (userExists != null)
                return BadRequest("User already exists");


            var userModel = _mapper.Map<User>(userCreateDto);

            var passwordHash = _authService.HashPassword(userCreateDto.PasswordHash);
            userModel.PasswordHash = passwordHash;
            Console.WriteLine("Password hash: " + userModel.PasswordHash);

            _repository.CreateUser(userModel);
            _repository.SaveChanges();
            return Ok(_mapper.Map<UserReadDto>(userModel));

        }
        [HttpPost("login")]
        public IActionResult Login(UserLoginDto userLoginDto)
        {
            var user = _repository.GetUserByName(userLoginDto.Username);
            Console.WriteLine($"--> Logging in with user: {user.Username}");

            if (user == null)
                return NotFound();

            var isPasswordValid = _authService.VerifyPassword(user.PasswordHash, userLoginDto.PasswordHash);

            if (!isPasswordValid)
                return Unauthorized();

            var token = _authService.GenerateToken(user.Username);
            Console.WriteLine("User succesfully logged in");
            return Ok(new { Token = token });
        }
        
        [HttpGet("validate")]
        public IActionResult SecureEndpoint()
        {
            string token = Request.Headers["Authorization"];
            if (string.IsNullOrWhiteSpace(token))
                return Unauthorized();

            token = token.Substring(7);
            
            if(!_authService.ValidateToken(token))
            {
                return Unauthorized();
            }
            return Ok("Successful validation");
        }
        
    }
}
