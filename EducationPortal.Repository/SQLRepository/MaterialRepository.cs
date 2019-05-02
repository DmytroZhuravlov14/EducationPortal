using EducationPortal.Data;
using EducationPortal.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Repository.SQLRepository
{
    public class MaterialRepository : BaseRepository<Material>
    {
        private readonly EducationPortalContext context;

        public MaterialRepository(EducationPortalContext context) : base(context)
        {
            this.context = context;
        }

    }
}
