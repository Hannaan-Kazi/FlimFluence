using DataService;
using DataService.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Managers.Managers
{
    public class UsersManager
    {
        private readonly IUserDataService _userDataService;
        private readonly IConfiguration _configuration;
        private readonly JwtSettingscs _jwtSettingscs;

        public UsersManager(IUserDataService userDataService, IConfiguration configuration, IOptions<JwtSettingscs> options)
        {
            _userDataService = userDataService;
            _configuration = configuration;
            _jwtSettingscs = options.Value;
        }

        public async Task<List<GetUserDTO>> AllUsers()
        {
            var AllUser = await _userDataService.GetUsers();
            return MapDto(AllUser);
            //return AllUser.ToList();
        }


        public async Task<GetUserDTO> GetById(int id)
        {
            var found = await _userDataService.GetUser(id);
            return MapDtoSingle( found);
        }

        public async Task<GetUserDTO> GetByEmail(string email)
        {
            var found = await _userDataService.GetUserByEmail(email);
            return MapDtoSingle(found);
        }


        public async Task<List<GetUserDTO>> CurrentUsers()
        {
            var current = await _userDataService.GetCurrentUsers();
            return MapDto(current);
        }


        public async Task<GetUserDTO> Add(User user)
        {
            string salt = GenerateSalt();
            createPasswordHash(user.Password, salt, out string PasswordSalt, out string PasswordHash);

            var newUser = new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                DateOfBirth = user.DateOfBirth,
                PhoneNumber = user.PhoneNumber,
                PictureUrl = user.PictureUrl,
                CreatedOn = DateTime.Now,
                Salt = PasswordSalt,
                HashedPassword = PasswordHash

            };

            //var added = 
               return await _userDataService.AddUser(newUser)? MapDtoSingle(newUser):null;
           // return added;

        }

        public async Task<bool> UpdateUser(int id, User user)
        {

            var DbUser = await _userDataService.GetUser(id);

            if (DbUser == null) { return false; }
            DbUser.FirstName = user.FirstName;
            DbUser.LastName = user.LastName;
            DbUser.Email = user.Email;
            DbUser.PhoneNumber = user.PhoneNumber;
            DbUser.Password = user.Password;
            DbUser.DateOfBirth = user.DateOfBirth;
            DbUser.PictureUrl = user.PictureUrl;
            DbUser.UpdatedOn = DateTime.Now;

            await _userDataService.UpdateUser(id, DbUser);

            return true;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var DbUser = await _userDataService.GetUser(id);
            if (DbUser == null) { return false; }
            DbUser.DeletedOn = DateTime.Now;
            DbUser.Deletedby = 1;
            await _userDataService.DeleteUser(id);
            return true;
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



        public async Task<ActionResult<string>> SignIn(string email, string password)
        {
            var DbUser = await _userDataService.GetUserByEmail(email);
            if (DbUser == null) { return "Invalid Email."; }
            string salt = DbUser.Salt;
            //var s=VerifyPassword(password, salt, DbUser.HashedPassword)?$"welcome {DbUser.FirstName}":"Wrong password.";
            if (!VerifyPassword(password, salt, DbUser.HashedPassword)) return "Wrong Password.";

            string Token = CreateToken(DbUser);
            return Token;

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
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString())

             }),
                //Expires = DateTime.UtcNow.AddHours(5),

                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);
            return jwt;


        }

        public List<GetUserDTO>MapDto(List<User> user)
        {
            List<GetUserDTO> ListOfDto = new List<GetUserDTO>();

            foreach (var item in user)
            {
                ListOfDto.Add(
                new GetUserDTO
                {
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    PhoneNumber = item.PhoneNumber,
                    Email = item.Email,
                    DateOfBirth = (DateTime)item.DateOfBirth,

                });

            }
            return ListOfDto;

        }

        public GetUserDTO MapDtoSingle(User user)
        {
            return new GetUserDTO
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email,
                    DateOfBirth = (DateTime)user.DateOfBirth,

                };
        }
    }
}
