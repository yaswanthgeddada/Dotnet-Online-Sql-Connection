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
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetAllUserDetails()
        {
            var users = await _context.Users.ToListAsync();
            var userDtos = _mapper.Map<List<UserDto>>(users);
            return userDtos;
        }

        public async Task<UserDto> GetUserDetails(int id)
        {
            var user = await _context.Users.FindAsync(id);
            var userDto = _mapper.Map<UserDto>(user);

            return userDto;
        }

        public async Task<bool> AddUser(User userDetails)
        {
            var result = await _context.Users.AddAsync(userDetails);

            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }

            return false;
        }

        public async Task<User> UpdateUser(int id, UserDto userDetails)
        {
            var result = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (result == null)
            {
                return null;
            }

            result.Bio = userDetails.Bio != null ? userDetails.Bio : result.Bio;
            result.FullName = userDetails.FullName != null ? userDetails.FullName : result.FullName;
            result.EmailAddress = userDetails.EmailAddress != null ? userDetails.EmailAddress : result.EmailAddress;
            result.ProfilePic = userDetails.ProfilePic != null ? userDetails.ProfilePic : result.ProfilePic;
            result.UserName = userDetails.UserName != null ? userDetails.UserName : result.UserName;
            result.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return result;
        }

    }
}