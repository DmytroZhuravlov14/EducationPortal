using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Data
{
    public class Video : Material
    {
        public string Length { get; set; }

        public string Quality { get; set; }

        public override string ToString()
        {
            return base.ToString() + $"Length: {Length}, Quality: {Quality}";
        }
    }
}
  