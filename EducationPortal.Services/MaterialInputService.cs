using EducationPortal.Data;
using EducationPortal.Services.Helpers;
using EducationPortal.Services.Interfaces;
using EducationPortal.Start.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EducationPortal.Services
{
    public class MaterialInputService : IMaterialInputService
    {
        private readonly string urlRegex = @"www.([A-Za-z0-9-]{1,}).([A-Za-z0-9-]{1,3})";
        private readonly IMaterialService materialService;
        private readonly ISkillService skillService;
        private readonly ISkillInputService skillInputService;
        private readonly Session session;

        public MaterialInputService(IMaterialService materialService, 
            Session session, 
            ISkillService skillService, 
            ISkillInputService skillInputService)
        {
            this.materialService = materialService;
            this.session = session;
            this.skillService = skillService;
            this.skillInputService = skillInputService;
        }
        
        private MaterialDTO BasicInfoInput()
        {
            string name = string.Empty;
            string link = string.Empty;
            string skills = string.Empty;

            name = InfoUnit.GetInfoUnit(name, 
                x => !string.IsNullOrEmpty(x.ToString()) && x.ToString().Length > 1,
                "Enter material name", 
                @"Error! Enter the material name!").ToString();

            link = InfoUnit.GetInfoUnit(link,
                x => !string.IsNullOrEmpty(x.ToString()) && Regex.IsMatch(x.ToString(), urlRegex, RegexOptions.IgnoreCase),
                "Enter material link!",
                @"Error! Enter the correct link!").ToString();

            skillService.ShowSkills();

            List<Skill> skillList = new List<Skill>();
            while (true)
            {
                string command = string.Empty;
                command = InfoUnit.GetInfoUnit(command,
                    x => !string.IsNullOrEmpty(x.ToString()) && new List<string> { "0","1", "2" }.Contains(x.ToString()),
                    "Wanna add new skill(1) or existing one(2) PRESS 0 TO EXIT SKILL ADDING MODE?",
                    @"Error! Enter the correct command!").ToString();

                if (command == "0")
                {
                    break;
                }

                if (command == "1")
                {
                    var skillName = skillInputService.SkillInfoInput();
                    var skill = skillService.Get(skillName);
                    skillList.Add(skill);
                }
                else
                {
                    string skillName = string.Empty;
                    skillName = InfoUnit.GetInfoUnit(skillName,
                    x => !string.IsNullOrEmpty(x.ToString()) && x.ToString().Length > 1,
                    "Enter skill name",
                    @"Error! skill name invalid!").ToString();

                    skillList.Add(skillService.Get(skillName.TrimStart()));
                }
            }
            return new MaterialDTO(name, link, skillList);
        }

        public void MaterialInfoUpdate()
        {
            if (session.IsAuthorised)
            {
                materialService.ShowMaterials();

                string name = string.Empty;
                name = InfoUnit.GetInfoUnit(
                    name,
                    x => !(string.IsNullOrEmpty(x.ToString())),
                    "Enter name of the material you want to edit",
                    "Error! Name is invalid"
                    ).ToString();

                var material = materialService.Get(name);

                string field = string.Empty;
                field = InfoUnit.GetInfoUnit(
                   field,
                   x => !(string.IsNullOrEmpty(x.ToString()) &&
                   new List<string> { "1", "2", "3" }.Contains(x.ToString())),
                   "Enter name of the field you want to edit: \n 1.Name \n 2.Link \n 3.Skills",
                   "Error! Field is invalid"
                   ).ToString();

                if (field == "1")
                {
                    var newName = string.Empty;
                    newName = InfoUnit.GetInfoUnit(
                        newName,
                        x => !(string.IsNullOrEmpty(x.ToString())),
                        "Enter new name",
                        "Error! Name is invalid"
                        ).ToString();

                    materialService.Update(material.Name, newName, null);
                }
                if (field == "2")
                {
                    var newLink = string.Empty;
                    newLink = InfoUnit.GetInfoUnit(
                        newLink,
                        x => !(string.IsNullOrEmpty(x.ToString())),
                        "Enter new description",
                        "Error! Description is invalid"
                        ).ToString();

                    materialService.Update(material.Name, null, newLink);
                }
                if (field == "3")
                {
                    Console.WriteLine(material);

                    var command = string.Empty;
                    command = InfoUnit.GetInfoUnit(
                        command,
                        x => !(string.IsNullOrEmpty(x.ToString())) &&
                        new List<string> { "1", "2" }.Contains(x.ToString()),
                        "Wanna add new skills(1), add existing(2) or delete some (3)",
                        "Error! command is invalid"
                        ).ToString();
                    if (command == "1")
                    {
                        var skillName = skillInputService.SkillInfoInput();
                        var skill = skillService.Get(skillName);
                        materialService.AddSkillToMaterial(material.Name, skill);
                    }
                    if(command == "2")
                    {
                        skillService.ShowSkills();

                        var skillName = string.Empty;
                        skillName = InfoUnit.GetInfoUnit(
                            skillName,
                            x => !(string.IsNullOrEmpty(x.ToString())),
                            "Enter skill name",
                            "Error! Skill name is invalid"
                            ).ToString();

                        var skill = skillService.Get(skillName);
                        materialService.AddSkillToMaterial(material.Name, skill);
                    }
                    if (command == "3")
                    {
                        var skillName = string.Empty;
                        skillName = InfoUnit.GetInfoUnit(
                            skillName,
                            x => !(string.IsNullOrEmpty(x.ToString())),
                            "Enter material name",
                            "Error! Material name is invalid"
                            ).ToString();

                        var skill = skillService.Get(skillName);
                        //skillService.Delete(material.Id);
                    }
                }
                else
                {
                    Console.WriteLine("You need to authorize");
                }
            }
        }

        public string MaterialInfoInput()
        {
            if(session.IsAuthorised)
            {
                materialService.ShowMaterials();

                string name = string.Empty;
                string link = string.Empty;

                Console.WriteLine("Enter material type:\n 1. Video\n 2.Article\n 3.Book");

                while (true)
                {
                    var materialType = Console.ReadLine();
                    if (string.IsNullOrEmpty(materialType))
                    {
                        Console.WriteLine(@"Error! {0}", "Enter the material type!");
                    }

                    if (materialType == "1" || materialType == "Video")
                    {
                        string length = string.Empty;
                        string quality = string.Empty;

                        var materialDTO = BasicInfoInput();

                        length = InfoUnit.GetInfoUnit(length,
                            x => !string.IsNullOrEmpty(x.ToString()),
                            "Enter video length!",
                            @"Error! Enter the correct length!").ToString();

                        quality = InfoUnit.GetInfoUnit(quality,
                            x => !string.IsNullOrEmpty(x.ToString()),
                            "Enter video quality!",
                            @"Error! Enter the correct quality!").ToString();

                        materialService.CreateVideo(materialDTO.Name, materialDTO.Link, length, quality, materialDTO.Skills);
                        return materialDTO.Name;
                    }

                    if (materialType == "2" || materialType == "Article")
                    {
                        string publishDate = string.Empty;

                        var materialDTO = BasicInfoInput();

                        publishDate = InfoUnit.GetInfoUnit(publishDate,
                            x => !string.IsNullOrEmpty(x.ToString()),
                            "Enter publish date!",
                            @"Error! Enter the correct publish date!").ToString();

                        materialService.CreateArticle(materialDTO.Name, materialDTO.Link, publishDate, materialDTO.Skills);
                        return materialDTO.Name;
                    }

                    if (materialType == "3" || materialType == "Book")
                    {
                        string author = string.Empty;
                        int pageNumber = 0;
                        string format = string.Empty;
                        string year = string.Empty;

                        var materialDTO = BasicInfoInput();

                        author = InfoUnit.GetInfoUnit(author,
                            x => !string.IsNullOrEmpty(x.ToString()),
                            "Enter author name!",
                            @"Error! Enter the correct author!").ToString();

                        pageNumber = Convert.ToInt32
                            (
                                InfoUnit.GetInfoUnit
                                (
                                    pageNumber,
                                    x => !string.IsNullOrEmpty(x.ToString()),
                                    "Enter page number!",
                                    @"Error! Enter the correct page number!"
                                )
                            );

                        format = InfoUnit.GetInfoUnit(format,
                            x => !string.IsNullOrEmpty(x.ToString()),
                            "Enter format!",
                            @"Error! Enter the correct format!").ToString();

                        year = InfoUnit.GetInfoUnit(year,
                            x => !string.IsNullOrEmpty(x.ToString()),
                            "Enter year!",
                            @"Error! Enter the correct year!").ToString();

                        materialService.CreateBook(materialDTO.Name, materialDTO.Link, author, pageNumber, 
                            format, year, materialDTO.Skills);
                        return materialDTO.Name;
                    }
                }
            }
            else
            {
                Console.WriteLine("You need to authorize");
                return null;
            }
        }
    }
}
