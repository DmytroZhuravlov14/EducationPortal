using EducationPortal.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Services
{
    public class Session
    {
        public User User { get; set; }

        public DateTime LoginTime { get; set; }

        public bool IsAuthorised { get; set; }
    }
}
