using EducationPortal.Data;
using EducationPortal.Data.DTOs;
using EducationPortal.Services.Helpers;
using EducationPortal.Services.Interfaces;
using System;
using System.Text.RegularExpressions;

namespace EducationPortal.Services
{
    public class UserInfoInputService : IUserInfoInputService
    {
        private readonly string passwordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^a-zA-Z0-9])\S{1,16}$";
        private readonly string emailRegex = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
        private readonly Session session;

        public UserInfoInputService(Session session)
        {
            this.session = session;
        }

        public void PrintSessionUser()
        {
            if(session.IsAuthorised)
            {
                Console.WriteLine(session.User);
            }
            else
            {
                Console.WriteLine("Not authorised");
            }
        }

        public UserDTO UserDTOInfoInput()
        {
            string email = string.Empty;
            string password = string.Empty;

            email = InfoUnit.GetInfoUnit
                (
                    email, 
                    x => !string.IsNullOrEmpty(x.ToString()),
                    "Enter your email!",
                    @"Error! Enter the correct email!"
                ).ToString();

            password = InfoUnit.GetInfoUnit
                (
                    password,
                    x => !string.IsNullOrEmpty(x.ToString()),
                    "Enter your password!",
                    @"Error! Enter the correct password!"
                ).ToString();

            return new UserDTO(email, password);
        }

        public User UserInfoInput()
        {
            string firstName = string.Empty;
            string lastName = string.Empty;
            string login = string.Empty;
            string email = string.Empty;
            string password = string.Empty;

            firstName = InfoUnit.GetInfoUnit
                (
                    firstName,
                    x => !string.IsNullOrEmpty(x.ToString()) && x.ToString().Length > 3,
                    "Enter your name!",
                    @"Error! Enter the correct name!"
                ).ToString();

            lastName = InfoUnit.GetInfoUnit
                (
                    lastName,
                    x => !string.IsNullOrEmpty(x.ToString()) && x.ToString().Length > 3,
                    "Enter your last name!",
                    @"Error! Enter the correct last name!"
                ).ToString();

            login = InfoUnit.GetInfoUnit
                (
                    login,
                    x => !string.IsNullOrEmpty(x.ToString()) && x.ToString().Length > 1,
                    "Enter your login!",
                    @"Error! Enter the correct login"
                ).ToString();

            email = InfoUnit.GetInfoUnit
                (
                    email,
                    x => Regex.IsMatch(x.ToString(), emailRegex, RegexOptions.IgnoreCase),
                    "Enter your Email!",
                    @"Error! Enter the correct email!"
                ).ToString();

            password = InfoUnit.GetInfoUnit
                (
                    password,
                    x => Regex.IsMatch(x.ToString(), passwordRegex, RegexOptions.IgnoreCase),
                    "Enter your password",
                    @"Error! Enter the correct password!"
                ).ToString();

            return new User(firstName, lastName, login, email, password);
        }
    }
}
