using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Models;
using API.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{


    public class UserController : BaseApiController
    {
        private readonly DataContext _context;
        public IUserRepository _userRepository { get; }
        private readonly IMapper _mapper;

        public UserController(DataContext context, IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _context = context;
        }

        [HttpGet("getAllUserDetails")]
        public async Task<IActionResult> GetAllUserDetails()
        {
            var users = await _userRepository.GetAllUserDetails();
            return Ok(users);
        }

        [HttpGet("getUserDetails/{id}")]
        public async Task<IActionResult> GetUserDetail(int id)
        {
            var user = await _userRepository.GetUserDetails(id);
            return Ok(user);
        }


        [HttpPost("adduser")]
        public async Task<IActionResult> AddUser(User userdetails)
        {
            var res = await _userRepository.AddUser(userdetails);
            if (res) return Ok(res);

            return BadRequest("Failed to add user");
        }

        [HttpPut("updateUser/{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] UserDto userdetails)
        {
            var res = await _userRepository.UpdateUser(id, userdetails);

            if (res == null)
            {
                return BadRequest("Failed To Update User");
            }

            var resDto = _mapper.Map<UserDto>(res);

            return Ok(resDto);
        }




    }
}