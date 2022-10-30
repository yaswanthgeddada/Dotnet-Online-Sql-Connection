using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Models;

namespace API.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserDto>> GetAllUserDetails();

        Task<UserDto> GetUserDetails(int id);

        Task<bool> AddUser(User userdetails);

        Task<User> UpdateUser(int id, UserDto userDetails);

    }
}