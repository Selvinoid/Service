namespace Moodroon.DataLayer.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using DataAccessLayer.Models.Models;
    using DataAccessLayer.Repositories;
    using DataAccessLayer.Repositories.Identity;
    using DataAccessLayer.Repositories.Repositories.DataBaseRepositories;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.Practices.Unity;

    using Moodroon.DataLayer.Core.DTO;

    public class UserService : BaseApplicationService
    {
        private ApplicationUserManager UserManager { get; }

        //private IUserStore<User, int> UserStore { get; set; }
        //private ApplicationRoleManager RoleManager { get; }

        private readonly HotelDbContext context;

        public UserService(IUnityContainer container)
            : base(container)
        {
            this.context = HotelDbContext.Create();
            this.UserManager = new ApplicationUserManager(new UserStore<User, Role, int, UserLogin, UserRole, UserClaim>(this.context));
        }

      

        public Task<string[]> GetUserRoles(int userId)
        {
            return Task.FromResult(this.UserManager.GetRoles(userId).ToArray());
        }

        public UserDto GetUserById(int id)
        {
            try
            {
                return this.InvokeInUnitOfWorkScope(
               p =>
               {
                   var a = p.GetRepository<UserRepository>().GetSingle(x => x.Id == id);
                   var user = new UserDto(a.Id)
                   {
                       UserName = a.UserName
                   };
                   return user;
               });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<UserDto> GetUsers()
        {
            try
            {
                return this.InvokeInUnitOfWorkScope(
               p =>
               {
                   var a = p.GetRepository<UserRepository>().Get();
                   return a.Select(x => new UserDto(x.Id) { UserName = x.UserName }).ToList();
               });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ClaimsIdentity> AuthenticateApi(string email, string password)
        {
            ClaimsIdentity claim = null;
            var user = await this.UserManager.FindByEmailAsync(email);
            var result = await this.UserManager.CheckPasswordAsync(user, password);
            if (result)
            {
                claim = await this.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            }
            return claim;
        }

        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            User user = new User
            {
                UserName = userModel.UserName
            };

            var result = await this.UserManager.CreateAsync(user, userModel.Password);

            return result;
        }

        public async Task<User> FindUser(string userName, string password)
        {
            User user = await this.UserManager.FindAsync(userName, password);

            return user;
        }


        public async Task<User> FindAsync(UserLoginInfo loginInfo)
        {
            User user = await this.UserManager.FindAsync(loginInfo);

            return user;
        }

        public async Task<IdentityResult> CreateAsync(User user)
        {
            var result = await this.UserManager.CreateAsync(user);

            return result;
        }

        public async Task<IdentityResult> AddLoginAsync(int userId, UserLoginInfo login)
        {
            var result = await this.UserManager.AddLoginAsync(userId, login);

            return result;
        }

       
        public async Task<IdentityResult> Registration(RegistrationDto registrationUser)
        {
            var user = new User { Email = registrationUser.UserName, UserName = registrationUser.UserName };
            var result = await this.UserManager.CreateAsync(user, registrationUser.Password);
            if (result.Succeeded)
            {
                var iUser = await this.UserManager.FindByNameAsync(registrationUser.UserName);
                result = await this.UserManager.AddToRoleAsync(iUser.Id, "User");
            }
            return result;
        }
    }
}