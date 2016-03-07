using Com.Pinz.Server.DataAccess.Db;
using System.Data.Entity;

namespace Com.Pinz.Server.DataAccess.DAO
{
    abstract class BasicDAO<T> :IBasicDAO<T> where T : class
    {
        protected PinzDbContext context;

        public BasicDAO(PinzDbContext context)
        {
            this.context = context;
        }

        public T Create(T entity)
        {
            T savedTask = GetDbSet().Add(entity);
            context.SaveChanges();
            return savedTask;
        }

        public void Delete(T entity)
        {
            context.Entry(entity).State = EntityState.Deleted;
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        protected abstract DbSet<T> GetDbSet();
    }
}
