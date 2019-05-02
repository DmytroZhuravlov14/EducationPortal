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
    public class UserService : IUserService
    {
        private readonly IRepository<User> repository;
        private readonly Session session;

        public UserService(IRepository<User> repository, Session session)
        {
            this.repository = repository;
            this.session = session;
        }

        public void Add(string firstName, string LastName, string login, string email, string password)
        {
            repository.Add(new User
            {
                FirstName = firstName,
                LastName = LastName, 
                Login = login,
                Email = email, 
                Password = password.GetHashCode().ToString()
            });
            repository.Save();
        }

        public User Get(string email)
        {
            var user = repository.Get(x => x.Email == email, x => x).FirstOrDefault();
            if (user != null)
            {
                return user;
            }
            else
            {
                throw new ArgumentException("user not found");
            }
        }
    }
}
