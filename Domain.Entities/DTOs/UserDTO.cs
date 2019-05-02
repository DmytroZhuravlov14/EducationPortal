using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Data.DTOs
{
    public class UserDTO
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public UserDTO(string email, string password)
        {
            this.Email = email;
            this.Password = password;
        }
    }
}
