using AutoMapper;
using EducationPortal.Data;
using EducationPortal.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EducationPortal.UI.Helpers
{
    public static class MaterialMapper
    {
        public static List<MaterialViewModel> Map(List<Material> materials)
        {
            List<MaterialViewModel> mvms = new List<MaterialViewModel>();
            foreach (var material in materials)
            {
                if (material is Article)
                {
                    mvms.Add(Mapper.Map<ArticleViewModel>(material));
                }
                if (material is Video)
                {
                    mvms.Add(Mapper.Map<VideoViewModel>(material));
                }
                if (material is Book)
                {
                    mvms.Add(Mapper.Map<BookViewModel>(material));
                }
            }
            return mvms;
        }
    }
}