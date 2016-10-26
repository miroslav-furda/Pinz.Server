using System.Threading.Tasks;
using AutoMapper;
using Com.Pinz.Server.DataAccess.Model;
using Com.Pinz.Server.TaskService.FastSpring;
using Ninject.Activation;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Com.Pinz.Server.TaskService.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Com.Pinz.Server.TaskService.App_Start.NinjectWebCommon), "Stop")]

namespace Com.Pinz.Server.TaskService.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using DataAccess;
    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Load(new DataAccessNinjectModule());
            kernel.Bind<IMapper>().ToMethod(StartAutoMapper);
        }

        private static IMapper StartAutoMapper(IContext arg)
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Task, Task>();
                cfg.CreateMap<Subscription, SubscriptionDO>();
                cfg.CreateMap<SubscriptionDO, Subscription>();
            });

            return config.CreateMapper();
        }

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }
    }
}
