
namespace Com.Pinz.Server.DataAccess
{
    public interface IBasicDAO<T> where T :class
    {
        void Delete(T entity);

        T Update(T entity);

        T Create(T entity);
    }
}
