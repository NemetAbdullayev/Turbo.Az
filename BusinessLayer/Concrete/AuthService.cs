using AutoMapper;
using BusinessLayer.Abstract;
using DataAccessLayer;
using EntityLayer.DTOs.UserDtos;
using EntityLayer.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BusinessLayer.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;
        public IMapper _mapper;
        public AuthService(ApplicationDbContext dbContext, Microsoft.Extensions.Configuration.IConfiguration configuration, IMapper mapper)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _mapper = mapper;   
        }

        public async Task<UserList> Login(string userName, string password)
        {
            User? user = await _dbContext.Users.FirstOrDefaultAsync(x=>x.UserName== userName);
            UserList userList=new UserList();
            _mapper.Map(user, userList);
            if (user == null || BCrypt.Net.BCrypt.Verify(password, user.Password) == false)
            {
                return null; 
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JWT:SecretKey"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.GivenName, user.Name),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                IssuedAt = DateTime.UtcNow,
                Issuer = _configuration["JWT:Issuer"],
                Audience = _configuration["JWT:Audience"],
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            userList.Token ="Bearer " +tokenHandler.WriteToken(token);
            userList.IsActive = true;

            return userList;
        }

        public async Task<User> Register(User user)
        {
            try
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                _dbContext.Users.Add(user);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception (ex.InnerException.Message)   ;
            }
           
            
            return  user;
        }


    }
}
