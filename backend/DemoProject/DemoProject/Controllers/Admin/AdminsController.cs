using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataService;
using Managers.Managers;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;
using Managers.Managers.Dto.AdminDTO;

namespace DemoProject.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles ="admin")]
    [ApiController]
    public class AdminsController : Controller
    {
        private readonly AdminsManager _admins;

        public AdminsController(AdminsManager adminMan)
        {
            _admins = adminMan;
        }

        // GET: api/Admins
        [AllowAnonymous]
        [HttpGet]

        public async Task<ActionResult<List<AdminDto>>> Get()
        {
            var a= await _admins.GetAdmins();
            //if (a == null) { return BadRequest(); }
            return Ok(a);
        }

        //GET: api/Admins/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Admin>> GetAdmin(int id)
        {

            var admin = await _admins.GetById(id);

            if (admin == null)
            {
                return NotFound("Invalid id");
            }

            return Ok(admin);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<string>> LogIn(Admin admin)
        {
            var a= await _admins.SignIn(admin.Email,admin.Password);

            if (a == null) { return NotFound("Invalid email or password"); }
            return a;
        }

        [HttpPut]
        public async Task<ActionResult<string>> UpdateAd(int id, Admin admin)
        {
            return await _admins.UpdateAdmin(id, admin)?$"updated":"error";
        }

        [HttpDelete, Authorize(Roles ="admin")]
        public async Task<ActionResult> DelAd(int id)
        {
            var authorizationHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
        
            var jwtToken = authorizationHeader[7..]; // Remove "Bearer " prefix

            // Decode the JWT token
            var handler = new JsonWebTokenHandler();
            var jsonToken = handler.ReadToken(jwtToken) as JsonWebToken;
            //Console.WriteLine(jsonToken);
            Console.WriteLine($"{jsonToken?.Claims.FirstOrDefault(claim=>claim.Type==JwtRegisteredClaimNames.NameId)?.Value} claimNames");

            var UserId=User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Console.WriteLine($"{UserId} NameIdentifier");

            var a = await _admins.Delete(int.Parse(UserId), id) ? $"{id} deleted" : "error";
            return Ok(a);
        }
        
        [HttpPost,  Authorize(Roles = "admin")]
        [Route("SignUp")]

        public async Task<ActionResult<Admin>> PostAdmin(Admin admin)
        {
            var check=await _admins.AddAd(admin);
            return Ok(check);
        }              

            //private bool AdminExists(int id)
            //{
            //    return (_context.Admins?.Any(e => e.AdminId == id)).GetValueOrDefault();
            //}
        }
    }
