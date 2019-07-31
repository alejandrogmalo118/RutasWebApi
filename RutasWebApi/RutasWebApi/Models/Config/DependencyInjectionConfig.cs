using Autofac;
using Autofac.Integration.Mvc;
using RutasWebApi.Models.Repositorio;

namespace RutasWebApi.Models.Config
{
    public class DependencyInjectionConfig
    {
        public static IContainer RegistrarDependencias()
        {
            var builder = new ContainerBuilder();
            //Builder.RegisterInstance(new CiudadRepositorio(new DBRutasEntities())).As<ICiudadRepositorio>();
            builder.RegisterGeneric(typeof(RepositorioGenerico<>)).As(typeof(IRepositorioGenerico<>));

            // Registrar todos los controladores del ensamblado
            builder.RegisterControllers(typeof(MvcApplication).Assembly).InstancePerRequest();
            return builder.Build();
        }
    }
}