using Autofac;
using Employee.Management.System.Data;
using Employee.Management.System.Repositories;
using Employee.Management.System.Services;

namespace Employee.Management.System
{
    public class AutoFacModule : Autofac.Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            builder.RegisterType<StoreContext>().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(typeof(IEmployeeService).Assembly).AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }

}
