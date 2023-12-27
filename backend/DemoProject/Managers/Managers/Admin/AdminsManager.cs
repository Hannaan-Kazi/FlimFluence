using DataService;
using DataService.Abstract;
using DataService.Services;
using Managers.Managers.Dto.AdminDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Managers.Managers
{
    public class AdminsManager
    {
        public readonly IAdminDataService _adminDataService;
        public readonly IConfiguration _configuration;
        public readonly IUserDataService _userDataService;

        public AdminsManager(IAdminDataService adminDataService, IConfiguration configuration, IUserDataService userDataService)
        {
            _adminDataService = adminDataService;
            _configuration = configuration;
            _userDataService=userDataService;
        }
            
        public async Task<List<AdminDto>> GetAdmins()
        {
            return MapDto(await _adminDataService.GetAdmins());
        }

        public async Task<AdminDto> GetById(int id)
        {
            return MapDto(await _adminDataService.GetById(id));
        }

        public async Task<AdminDto> GetByEmail(string email)
        {
            var a= await _adminDataService.GetByEmail(email);
            return MapDto( a);
        }

        public async Task<bool> AddAd(Admin admin)
        {
            string salt = GenerateSalt();
            createPasswordHash(admin.Password, salt, out string PasswordSalt, out string PasswordHash);

            var newAdmin = new Admin
            {
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                Email = admin.Email,
                Password = admin.Password,
                DateOfBirth = admin.DateOfBirth,
                PhoneNumber = admin.PhoneNumber,
                CreatedOn = DateTime.Now,
                Salt = PasswordSalt,
                HashedPassword = PasswordHash
            };
            return await _adminDataService.AddAdmin(newAdmin);
        }

        public async Task<bool> UpdateAdmin(int id,Admin admin)
        {
            

            var adm = await _adminDataService.GetById(id);
            if (adm == null) return false;

            string salt = GenerateSalt();
            createPasswordHash(admin.Password, salt, out string PasswordSalt, out string PasswordHash);

            adm.FirstName = admin.FirstName;
            adm.LastName = admin.LastName;
            adm.Email = admin.Email;
            adm.Password = admin.Password;
            adm.UpdatedOn = DateTime.Now;
            adm.HashedPassword = PasswordHash;
            adm.Salt = PasswordSalt;

            return await _adminDataService.UpdateAdmin(id, adm);

        }

        public async Task<ActionResult<string>> SignIn(string email, string password)
        {
            var DbUserA = await _adminDataService.GetByEmail(email);
            if (DbUserA != null)
            {

                string salt = DbUserA.Salt;
                if (!VerifyPassword(password, salt, DbUserA.HashedPassword)) return "Wrong Password.";

                string Token = CreateAToken(DbUserA);
                return Token;

                var jsonData = new { token = Token, r = "admin" };
                string jsonString = JsonConvert.SerializeObject(jsonData);
                //string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(jsonData);

                //// Parse the JSON string to a JObject
                //JObject jsonObject = JObject.Parse(jsonString);

                // Return the JSON object
                return jsonString;
                //return {token: Token, r:"user"};
            }
            var DbUser =  await _userDataService.GetUserByEmail(email);
            if (DbUser != null)
            {
                string salt = DbUser.Salt;
                //var s=VerifyPassword(password, salt, DbUser.HashedPassword)?$"welcome {DbUser.FirstName}":"Wrong password.";
                if (!VerifyPassword(password, salt, DbUser.HashedPassword)) return "Wrong Password.";

                string Token = CreateToken(DbUser);
                return Token;
                var jsonData = new { token = Token, r = "user" };
                string jsonString = JsonConvert.SerializeObject(jsonData);


                //string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(jsonData);

                // Parse the JSON string to a JObject
                JObject jsonObject = JObject.Parse(jsonString);

                // Return the JSON object

                return jsonString;
            }
            else
            {
                return "Invalid Email.";
            }

        }

        public async Task<bool> Delete(int userId, int toBeDeleted)
        {
            var dbAdmin = await _adminDataService.GetById(toBeDeleted);
            if (dbAdmin == null) { return false; }
            dbAdmin.DeletedOn= DateTime.Now;
            dbAdmin.Deletedby = userId;
            return await _adminDataService.DeleteAdmin()?true:false;
        }


        public string CreateAToken(Admin admin)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Email, admin.Email),
                new Claim(JwtRegisteredClaimNames.NameId, admin.AdminId.ToString()),
                //new Claim(ClaimTypes.NameIdentifier, admin.AdminId.ToString()),
                new Claim(ClaimTypes.Role, "admin"),
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim("Email", admin.Email),
                new Claim(JwtRegisteredClaimNames.Iat,
                DateTime.UtcNow.ToString())

             }),
                Expires = DateTime.UtcNow.AddDays(5),

                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);
            return jwt;

        }

        public string CreateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim("Email", user.Email),
                new Claim(ClaimTypes.Role, "user"),

                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString())

             }),
                Expires = DateTime.UtcNow.AddDays(5),

                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);
            return jwt;


        }

        public void createPasswordHash(string password, string salt, out string PasswordSalt, out string PasswordHash)
        {

            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] saltBytes = Convert.FromBase64String(salt);
            using (var hmac = new HMACSHA512(saltBytes))
            {
                PasswordSalt = salt;
                PasswordHash = Convert.ToBase64String(hmac.ComputeHash(passwordBytes));
            }
        }

        public string GenerateSalt()
        {
            byte[] saltBytes = new byte[64];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }

            return Convert.ToBase64String(saltBytes);
        }

        bool VerifyPassword(string userInputPassword, string salt, string storedHashedPassword)
        {
            createPasswordHash(userInputPassword, salt, out string PasswordSalt, out string PasswordHash);
            string hashedUserInputPassword = PasswordHash;
            return hashedUserInputPassword == storedHashedPassword;
        }

        public List<AdminDto> MapDto(List<Admin> admins)
        {
            List<AdminDto> ListOfDto = new List<AdminDto>();

            foreach (var item in admins)
            {
                ListOfDto.Add(
                new AdminDto
                {
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    PhoneNumber = item.PhoneNumber,
                    Email = item.Email,
                    DateOfBirth = (DateTime)item.DateOfBirth
                });
            }
            return ListOfDto;
        }

        public AdminDto MapDto(Admin admin)
        {
            return new AdminDto
            {
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                PhoneNumber = admin.PhoneNumber,
                Email = admin.Email,
                DateOfBirth = (DateTime)admin.DateOfBirth
            };

        }
    }
}
