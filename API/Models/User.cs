using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class User
    {
        public int Id { get; set; }

        public string FullName { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string Bio { get; set; }
        public string ProfilePic { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;



    }
}