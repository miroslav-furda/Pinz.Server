using Com.Pinz.Server.DataAccess.DAO;
using Ninject.Modules;

namespace Com.Pinz.Server.DataAccess
{
    public class DataAccessNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<PinzDbContext>().ToSelf().WithConstructorArgument("connectionString", "name=pinzDBConnectionString"); 
            Kernel.Bind<ITaskDAO>().To<TaskDAO>();
            Kernel.Bind<ICategoryDAO>().To<CategoryDAO>();
        }
    }
}
