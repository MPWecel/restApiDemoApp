using Autofac;
using RestApiDemo.DomainCore.Models.Resource;
using RestApiDemo.Infrastructure.Data;
using RestApiDemo.Kernel.Interfaces;
using Module = Autofac.Module;

namespace RestApiDemo.Infrastructure
{
    public class DefaultInfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder cBuilder)
        {
            //cBuilder.RegisterType<MockRepository>().As<IRepository<Resource>>().InstancePerLifetimeScope();   //normalnie
            //cBuilder.RegisterType<MockRepository>().As<IRepository<Resource>>().SingleInstance();   //dla listy
            cBuilder.RegisterType<SqlServerRepository>().As<IRepository<Resource>>().InstancePerLifetimeScope();
            cBuilder.RegisterType<EmailSender>().As<IEmailSender>().SingleInstance();
        }
    }
}
