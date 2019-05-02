using EducationPortal.Data;
using EducationPortal.Repository.Interfaces;
using EducationPortal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly IRepository<Material> repository;
        private readonly Session session;

        public MaterialService(IRepository<Material> repository, Session session)
        {
            this.repository = repository;
            this.session = session;
        }

        public void CreateArticle(string name, string link, string publishDate, List<Skill> skills)
        {
            var material = Get(name);
            if(material == null)
            {
                var article = new Article
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = name,
                    Link = link,
                    PublishDate = publishDate,
                    Skills = skills
                };
                repository.Add(article);
                repository.Save();
            }
            else
            {
                throw new ArgumentException("Material already exists");
            }
        }

        public void CreateBook(string name, string link, string author, int pageNumber, 
            string format, string year, List<Skill> skills)
        {
            var material = Get(name);
            if (material == null)
            {
                var book = new Book
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = name,
                    Link = link,
                    Author = author,
                    PageNumber = pageNumber,
                    Format = format,
                    Year = year,
                    Skills = skills
                };
                repository.Add(book);
                repository.Save();
            }
            else
            {
                throw new ArgumentException("Material already exists");
            }
        }

        public void CreateVideo(string name, string link, string length, string quality, List<Skill> skills)
        {
            var material = Get(name);
            if (material == null)
            {
                var video = new Video
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = name,
                    Link = link,
                    Length = length,
                    Quality = quality,
                    Skills = skills
                };
                repository.Add(video);
                repository.Save();
            }
            else
            {
                throw new ArgumentException("Material already exists");
            }
        }

        public void AddSkillToMaterial(string materialName, Skill skill)
        {
            repository.Get(m => m.Name == materialName, x => x).FirstOrDefault().Skills.Add(skill);
            repository.Save();
        }

        public void Update(string materialName, string name, string link)
        {
            var material = repository.Get(x => x.Name == materialName, x => x).FirstOrDefault();
            if (name != null)
            {
                material.Name = name;
            }
            if (link != null)
            {
                material.Link = link;
            }
            repository.Save();
        }

        public Material Get(string name)
        {
            var material = repository.Get(x => x.Name == name, x => x).FirstOrDefault();
            return material;
        }

        public List<Material> Get()
        {
            return repository.Get(x => true, x => x).ToList();
        }

        public void ShowMaterials()
        {
            var materials = repository.Get(x => true, x => x);
            if(!materials.Any())
            {
                Console.WriteLine("no materials found");
            }
            else
            {
                foreach (var material in materials)
                {
                    Console.WriteLine(material+ "\n");
                }
            }
        }
    }
}
