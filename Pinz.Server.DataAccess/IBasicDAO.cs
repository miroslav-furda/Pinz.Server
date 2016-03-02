
namespace Com.Pinz.Server.DataAccess
{
    public interface IBasicDAO<T> where T :class
    {
        void Delete(T entity);

        void Update(T entity);

        T Create(T entity);
    }
}
