namespace EducationPortal.Repository.Migrations
{
    using System;
    using EducationPortal.Data;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<EducationPortal.Repository.EducationPortalContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EducationPortal.Repository.EducationPortalContext context)
        {
            var admin = new User() { Id = Guid.NewGuid().ToString(), FirstName = "Admin", Email = "admin@gmail.com", Password = "1234567".GetHashCode().ToString() };
            var testUser = new User() { Id = Guid.NewGuid().ToString(), FirstName = "12345", Email = "123@gmail.com", Password = "Password123!".GetHashCode().ToString() };

            if (!context.Users.Any())
            {
                context.Users.Add(admin);
                context.Users.Add(testUser);
            }

            var skill1 = new Skill() { Id = Guid.NewGuid().ToString(), Name = "C#", Points = 2 };
            var skill2 = new Skill() { Id = Guid.NewGuid().ToString(), Name = ".NET", Points = 3 };
            var skill3 = new Skill() { Id = Guid.NewGuid().ToString(), Name = "ООП", Points = 2 };
            var skill4 = new Skill() { Id = Guid.NewGuid().ToString(), Name = "ООД", Points = 4 };
            var skill5 = new Skill() { Id = Guid.NewGuid().ToString(), Name = "JS", Points = 6 };
            var skill6 = new Skill() { Id = Guid.NewGuid().ToString(), Name = "HTML", Points = 1 };
            var skill7 = new Skill() { Id = Guid.NewGuid().ToString(), Name = "CSS", Points = 6 };
            var skill8 = new Skill() { Id = Guid.NewGuid().ToString(), Name = "ASP .NET", Points = 5 };

            if (!context.Skills.Any())
            {
                foreach (var skill in new List<Skill>() { skill1, skill2, skill3, skill4, skill5, skill6, skill7, skill8 })
                {
                    context.Skills.Add(skill);
                } 
            }

            var mat1 = new Article()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Полное руководство по C# 7",
                Link = "https://metanit.com/sharp/tutorial/",
                PublishDate = new DateTime(2019, 2, 4).ToString(),
                Skills = new List<Skill> { skill1, skill2, skill3 }
            };

            var mat2 = new Article()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Руководство по ASP.NET MVC 5",
                Link = "https://metanit.com/sharp/mvc5/",
                PublishDate = new DateTime(2017, 2, 3).ToString(),
                Skills = new List<Skill> { skill1, skill2, skill8, skill5, skill6, skill7 }
            };
            var mat3 = new Article()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Паттерны проектирования в C# и .NET",
                Link = "https://metanit.com/sharp/patterns/",
                PublishDate = new DateTime(2017, 10, 19).ToString(),
                Skills = new List<Skill> { skill1, skill3, skill4 }
            };
            var mat4 = new Book()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "CLR VIA C#",
                Link = "https://metanit.com/sharp/patterns/",
                Author = "Jeffrey Richter",
                PageNumber = 800,
                Skills = new List<Skill> { skill1, skill3, skill4 }
            };

            if (!context.Materials.Any())
            {
                foreach (var material in new List<Material>() { mat1, mat2, mat3, mat4 })
                {
                    context.Materials.Add(material);
                }
            }

            var course1 = new Course
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Базовый C#",
                Description = "Вы получите базовое представление о языке C# и платформе .net",
                Owner = admin,
                Materials = new List<Material> { mat1 }
            };

            var course2 = new Course
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Продвинутый C#",
                Description = "Вы получите продвинутое представление о языке C# и платформе .net",
                Owner = admin,
                Materials = new List<Material> { mat4 }
            };

            var course3 = new Course
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Основы Web программирования на платформе .NET",
                Description = "Вы получите базовое представление о ASP .net",
                Owner = admin,
                Materials = new List<Material> { mat2 }
            };


            var course4 = new Course
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Курс по паттернам программирования",
                Description = "Вы изучите базовый паттерны программирования",
                Owner = admin,
                Materials = new List<Material> { mat3 }
            };

            if (!context.Courses.Any())
            {
                foreach (var course in new List<Course>() { course1, course2, course3, course4 })
                {
                    context.Courses.Add(course);
                }
            }

            base.Seed(context);
        }
    }
}
