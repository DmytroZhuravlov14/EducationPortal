using EducationPortal.Data;
using EducationPortal.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Services.Interfaces
{   
    public interface IAuthorisationService
    {
        void SignOut();

        bool SignIn(IRepository<User> repos);
    }
}
