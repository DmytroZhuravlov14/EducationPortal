using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Data
{
    public class Article : Material
    {
        public string PublishDate { get; set; }

        public override string ToString()
        {
            return base.ToString() + $"Publish date: {PublishDate}";
        }
    }
}
