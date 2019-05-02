using EducationPortal.Data;
using EducationPortal.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Repository.SQLRepository
{
    public class SkillRepository : BaseRepository<Skill>
    {
        private readonly EducationPortalContext context;

        public SkillRepository(EducationPortalContext context):base(context)
        {
            this.context = context;
        }
    }
}
