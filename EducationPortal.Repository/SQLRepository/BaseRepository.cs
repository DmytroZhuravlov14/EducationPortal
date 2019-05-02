using EducationPortal.Data;
using EducationPortal.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Repository.SQLRepository
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        private readonly EducationPortalContext context;

        public BaseRepository(EducationPortalContext context)
        {
            this.context = context;
        }

        public void Add(T t)
        {
            context.Set<T>().Add(t);
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> select)
        {
            return context.Set<T>().Where(predicate).Select(select);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
