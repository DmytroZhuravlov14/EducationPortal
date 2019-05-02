using EducationPortal.Data;
using EducationPortal.Repository.Interfaces;
using EducationPortal.Repository.SQLRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Repository.Repository
{
    public class UserRepository : BaseRepository<User>
    {
        private readonly EducationPortalContext context;

        public UserRepository(EducationPortalContext context) : base(context)
        {
            this.context = context;
        }
    }
}