using EducationPortal.Data;
using EducationPortal.Repository.Interfaces;
using EducationPortal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EducationPortal.Services
{
    public class ConsoleRegistrationService : IRegistrationService
    {
        private IUserInfoInputService userInfoInputService;
        private IRepository<User> repos;

        public ConsoleRegistrationService(IUserInfoInputService userInfoInputService, IRepository<User> repos)
        {
            this.userInfoInputService = userInfoInputService;
            this.repos = repos;
        }

        public void Register()
        {
            var user = userInfoInputService.UserInfoInput();
            var password = user.Password.GetHashCode().ToString();
            user.Password = password;
            repos.Add(user);
            repos.Save();
        }
    }
}
