using EducationPortal.Data;
using EducationPortal.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Start
{
    public class InteractionCommands
    {
        public void Show(IRepository<User> UserRepository)
        {
            var users = UserRepository.Get();
            if(users.Any())
            {
                foreach (var user in users)
                {
                    Console.WriteLine(user);
                }
            }
            else
            {
                Console.WriteLine("No registered users");
            }
            
        }

        public void Delete(IRepository<User> jsonUserRepository)
        {
            var temp = Console.ReadLine();
            jsonUserRepository.Delete(temp);
            jsonUserRepository.Save();
            Console.WriteLine("User deleted!");
        }
    }
}
