using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Tables
{
    public class User
    {
        [Key]
        public int Id { get; set; }
      
        public string UserName { get; set; } 
        public string Name { get; set; }
        public string? Role { get; set; } 
        public bool? IsActive { get; set; } = false;      
        public string Password { get; set; } 

        public User(string userName, string name, string password, string role)
        {
            UserName = userName;
            Name = name;
            Password = password;
            Role = role;
        }
    }
 
}