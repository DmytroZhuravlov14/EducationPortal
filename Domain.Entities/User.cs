using EducationPortal.Start;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EducationPortal.Data
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public ICollection<Material> CompletedMaterials { get; set; }

        public ICollection<UserCourse> UserCourse { get; set; }

        public ICollection<UserSkill> UserSkill { get; set; }

        public User()
        {
            this.CompletedMaterials = new List<Material>();
            this.UserCourse = new List<UserCourse>();
            this.UserSkill = new List<UserSkill>();
        }

        public User(string FirstName, string LastName, string Login, string Email, string Password)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Login = Login;
            this.Email = Email;
            this.Password = Password;

            this.CompletedMaterials = new List<Material>();
            this.UserCourse = new List<UserCourse>();
            this.UserSkill = new List<UserSkill>();
        }

        public override string ToString()
        {
            var output = ($"First name: {FirstName}, Last Name: {LastName}, Login: {Login}, Email: {Email}\n ");
            //if (UserSkill.Any())
            //{
            //    foreach (var userSkill in UserSkill)
            //    {
            //        output+=($"Skill name: {userSkill.Skill.Name}, Points: {userSkill.Points}\n ");
            //    }
            //}
            //else
            //{
            //    output+=("this user have no skills\n");
            //}
            return output;
        }
    }
}
