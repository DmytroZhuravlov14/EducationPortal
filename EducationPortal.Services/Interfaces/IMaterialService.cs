using EducationPortal.Data;
using EducationPortal.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Services.Interfaces
{
    public interface IMaterialService
    {
        void ShowMaterials();

        List<Material> Get();

        Material Get(string name);

        void CreateArticle(string name, string link, string publishDate, List<Skill> skills);

        void CreateBook(string name, string link, string author, int pageNumber, string format, string year, List<Skill> skills);

        void CreateVideo(string name, string link, string length, string quality, List<Skill> skills);

        void Update(string materialName, string name, string link);

        void AddSkillToMaterial(string materialName, Skill skill);
    }
}
