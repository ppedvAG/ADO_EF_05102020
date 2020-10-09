using Autofac;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace ppedv.MessApp.UI.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public IContainer Container = null;

        protected override void OnStartup(StartupEventArgs e)
        {
            var ass = Assembly.LoadFrom("ppedv.MessApp.Data.EF.dll");

            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(ass).Where(x => x.Name.EndsWith("UnitOfWork")).AsImplementedInterfaces();
            Container = builder.Build();
            
            Container.BeginLifetimeScope();
            

            base.OnStartup(e);
        }
    }
}
