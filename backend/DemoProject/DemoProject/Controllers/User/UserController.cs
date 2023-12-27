using DataService;
using DataService.Abstract;
using Managers.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Drawing.Text;
using System.Security.Claims;
using System.Security.Cryptography;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DemoProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly UsersManager _users;
        public UsersController(UsersManager userMan)
        {
            _users = userMan;
        }


        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<GetUserDTO>>> Get()
        {
            return await _users.AllUsers();
        }

        

        [HttpGet("{id}")]
        public async Task<ActionResult<List<GetUserDTO>>> GetById(int id)
        {
            var f = await _users.GetById(id);
            if (f == null) { return NotFound(); }
            return Ok(f);
        }
        

        [HttpPost]
        [Route("email")]
        public async Task<ActionResult<GetUserDTO>> GetByEmail(User user)
        {   

            var f = await _users.GetByEmail(user.Email);
            if (f == null) { return NotFound(); }
            return Ok(f);
        }


        [HttpGet("Current")]
        public async Task<ActionResult<List<GetUserDTO>>> CurrentUsr()
        {
            return await _users.CurrentUsers();
        }


        [HttpPost]
        public async Task<ActionResult<bool>> AddUser(User user)
        {
            var added = await _users.Add(user);
            if(added == null) { return NotFound(); }
            return Ok(added);
        }

        [AllowAnonymous]
        [HttpPost("hash")]
        public ActionResult<login> Hashing([FromBody] User user)
        {
            string salt = _users.GenerateSalt();
            _users.createPasswordHash(user.Password, salt, out string PasswordSalt, out string PasswordHash);
            Console.WriteLine(salt);
            var test = new login
            {
                Email = user.Email,
                PasswordHash = PasswordHash,
                PasswordSalt = PasswordSalt
            };
            //bool passwordMatch = VerifyPassword(userInputPassword, storedSalt, storedHashedPassword);
            return Ok(test);
        }
        

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Sign(User user)
        {
            Console.WriteLine($"{user.Email} {user.Password} Signing");
            return await _users.SignIn(user.Email, user.Password);
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult> PutUser( User user)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var updated = await _users.UpdateUser(int.Parse(userId), user);
            return Ok(updated);
        }


        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteUser(int id)
        {
            var dletedStatus = await _users.DeleteUser(id);
            //if (dletedStatus==false) { return BadRequest(id); }
            return Ok(dletedStatus);
        }
    }
}
