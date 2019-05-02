using EducationPortal.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Services.Interfaces
{
    public interface IUserService
    {
        User Get(string email);

        void Add(string firstName, string LastName, string login, string email, string password);
    }
}
