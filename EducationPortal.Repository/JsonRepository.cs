using EducationPortal.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using EducationPortal.Data;
using System.Linq.Expressions;

namespace EducationPortal.Repository
{
    public class JsonRepository : IRepository<User>
    {
        private List<User> Users;

        private readonly string fileName;

        public JsonRepository(string FileName)
        {
            fileName = FileName;
            this.Users = new List<User>();
            this.Users = Get().ToList();
        }

        public void Add(User user)
        {
            try
            {
                if (Users.Exists(e => e.Email == user.Email) || Users.Exists(l => l.Login == user.Login))
                {
                    throw new ArgumentException("invalid smth");
                }
                else
                {
                    Users.Add(user);
                }
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void Delete(string id)
        {
            var user = Users.Find(u => u.Id == id);
            if(user != null)
            {
                Users.Remove(user);
            }
            else
            {
                throw new ArgumentException("User is not found");
            }
        }

        public IEnumerable<User> Get()
        {
            if (!File.Exists(fileName)) return Users;
            TextReader reader = null;
            try
            {
                reader = new StreamReader(fileName);
                var fileContents = reader.ReadToEnd();
                Users = JsonConvert.DeserializeObject<List<User>>(fileContents);
            }
            finally
            {
                reader?.Close();
            }

            return Users;

        }

        public User Get(string email)
        {
            var user = Users.Find(u => u.Email == email);
            if (Users.Contains(user))
            {
                return Users.First(u => u.Email == email);
            }
            else
            {
                throw new ArgumentException("Email is not found");
            }
        }

        public void Save()
        {
            TextWriter writer = null;
            try
            {
                var contentsToWriteToFile = JsonConvert.SerializeObject(Users);
                writer = new StreamWriter(fileName, false);
                writer.Write(contentsToWriteToFile);
            }
            finally
            {
                writer?.Close();
            }
        }

        public void Add(Material material)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Material> GetMaterials()
        {
            throw new NotImplementedException();
        }

        public Material FindMaterial(string name)
        {
            throw new NotImplementedException();
        }

        public void AddCourse(Course course)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Course> GetCourses()
        {
            throw new NotImplementedException();
        }

        public void Update(Course targetCourse, string name, string description)
        {
            throw new NotImplementedException();
        }

        public void Update(User target, params string[] parameters)
        {
            throw new NotImplementedException();
        }

        public void AddMaterial(Course course, Material material)
        {
            throw new NotImplementedException();
        }

        public void Start(string name)
        {
            throw new NotImplementedException();
        }

        public void AddSkill(Material material, Skill skill)
        {
            throw new NotImplementedException();
        }

        public void Start(string courseName, string email)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> Get(Expression<Func<User, bool>> predicate, Expression<Func<User, User>> select)
        {
            throw new NotImplementedException();
        }
    }
}
