using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models.Models;
using Microsoft.AspNet.Identity;

namespace Moodroon.DataLayer.Core.DTO
{
    public class UserDto : IUser<int>
    {
        public UserDto(int id)
        {
            this.Id = id;
        }

        public int Id { get; }

        public string UserName { get; set; }
        
        public string Role { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User, int> manager, User user)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
