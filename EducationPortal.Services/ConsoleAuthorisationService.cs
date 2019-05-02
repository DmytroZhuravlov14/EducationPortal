using EducationPortal.Data;
using EducationPortal.Repository.Interfaces;
using EducationPortal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Services
{
    public class ConsoleAuthorisationService : IAuthorisationService
    {
        private Session session;
        private IUserInfoInputService userInfoInputService;

        public ConsoleAuthorisationService(IUserInfoInputService userInfoInputService, Session session)
        {
            this.userInfoInputService = userInfoInputService;
            this.session = session;
        }

        public bool SignIn(IRepository<User> repos)
        {
            var inputtedUser = userInfoInputService.UserDTOInfoInput();

            var reposUser = repos.Get(x => x.Email == inputtedUser.Email, x => x).FirstOrDefault();
            if (reposUser.Email == inputtedUser.Email && reposUser.Password == inputtedUser.Password.GetHashCode().ToString())
            {
                Console.WriteLine("Logged in");

                session.IsAuthorised = true;
                session.LoginTime = DateTime.Now;
                session.User = reposUser;

                return true;
            }
            Console.WriteLine("Incorrect login or password!");
            return false;
        }

        public void SignOut()
        {
            Console.WriteLine("You signed out!");
            session.IsAuthorised = false;
        }
    }
}
