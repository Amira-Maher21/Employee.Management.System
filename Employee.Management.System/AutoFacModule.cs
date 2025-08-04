using Autofac;
using Employee.Management.System.Data;
using Employee.Management.System.mediator;
using Employee.Management.System.Repositories;
using Employee.Management.System.Services.DepartmentServ;
using Employee.Management.System.Services.EmployeeServ;
using Employee.Management.System.Services.LogHistoryServ;
using MediatR;
using System.Reflection;

namespace Employee.Management.System
{
    public class AutoFacModule : Autofac.Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            builder.RegisterType<StoreContext>().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(typeof(IEmployeeService).Assembly).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(typeof(IDepartmentService).Assembly).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(typeof(ILogHistoryService).Assembly).AsImplementedInterfaces().InstancePerLifetimeScope();
            // ✅ Add MediatR registrations
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                   .AsImplementedInterfaces();

            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            builder.RegisterAssemblyTypes(ThisAssembly)
                   .AsClosedTypesOf(typeof(IRequestHandler<,>))
                   .AsImplementedInterfaces();

            // Optional: Register other handler types
            builder.RegisterAssemblyTypes(ThisAssembly)
                   .AsClosedTypesOf(typeof(INotificationHandler<>))
                   .AsImplementedInterfaces();
        }
    }

}
