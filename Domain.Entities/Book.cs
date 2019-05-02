using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Data
{
    public class Book : Material
    {
        public string Author { get; set; }

        public int PageNumber { get; set; }

        public string Format { get; set; }

        public string Year { get; set; }

        public override string ToString()
        {
            return base.ToString() + $"Author: {Author}, PageNumber: {PageNumber}, Format: {Format}, Year: {Year}";
        }
    }
}
