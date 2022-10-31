using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Models;
using API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{

    public class AuthController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

        public AuthController(DataContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }


        [HttpPost("register")]
        public async Task<IActionResult> registerUser(RegisterDto registerDto)
        {
            var isUserPresent = await isUserValid(registerDto.UserName);
            if (isUserPresent) return BadRequest("username not available");
            if (!registerDto.Password.Equals(registerDto.ConfirmPassword)) return BadRequest("Passowrd doesn't matching");
            if (await isEmailValid(registerDto.Email)) return BadRequest("Email already used");


            using var hmac = new HMACSHA512();

            var user = new User();

            user.UserName = registerDto.UserName;
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password));
            user.PasswordSalt = hmac.Key;
            user.EmailAddress = registerDto.Email;

            await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();

            return Ok(new UserRegisterResponseDto
            {
                token = _tokenService.CreateToken(user),
                username = user.UserName
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> loginUser(LoginDto loginDto)
        {
            if (String.IsNullOrEmpty(loginDto.Email) && String.IsNullOrEmpty(loginDto.username)) return BadRequest("Enter username or email address");
            if (!String.IsNullOrEmpty(loginDto.Email) && !await isEmailValid(loginDto.Email)) return BadRequest("Email Not Found");
            if (!String.IsNullOrEmpty(loginDto.username) && !await isUserValid(loginDto.username)) return BadRequest("Username Not Found");

            var user = new User();

            if (!String.IsNullOrEmpty(loginDto.username))
            {
                user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == loginDto.username);
            }
            else
            {

                user = await _context.Users.FirstOrDefaultAsync(u => u.EmailAddress == loginDto.Email);
            }

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var ComputeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.password));

            var isPasswordValid = ComputeHash.SequenceEqual(user.PasswordHash);

            if (!isPasswordValid) return BadRequest("Wrong Password");

            return Ok(new UserRegisterResponseDto
            {
                token = _tokenService.CreateToken(user),
                username = user.UserName
            });
        }



        private async Task<bool> isUserValid(string userName)
        {
            return await _context.Users.AnyAsync(u => u.UserName == userName);
        }

        private async Task<bool> isEmailValid(string emailAddress)
        {
            return await _context.Users.AnyAsync(u => u.EmailAddress == emailAddress);
        }


    }
}