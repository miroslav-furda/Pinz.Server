using Com.Pinz.Server.DataAccess.DAO;
using Com.Pinz.Server.DataAccess.Db;
using Ninject.Modules;

namespace Com.Pinz.Server.DataAccess
{
    public class DataAccessNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<PinzDbContext>().ToSelf().InThreadScope().WithConstructorArgument("connectionString", "name=pinzDBConnectionString"); 
            Kernel.Bind<ITaskDAO>().To<TaskDAO>();
            Kernel.Bind<ICategoryDAO>().To<CategoryDAO>();
            Kernel.Bind<ICompanyDAO>().To<CompanyDAO>();
            Kernel.Bind<IProjectDAO>().To<ProjectDAO>();
            Kernel.Bind<IProjectStaffDAO>().To<ProjectStaffDAO>();
            Kernel.Bind<IUserDAO>().To<UserDAO>();
        }
    }
}
