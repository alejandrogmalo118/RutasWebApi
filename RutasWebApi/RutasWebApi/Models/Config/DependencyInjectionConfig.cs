using Autofac;
using Autofac.Integration.Mvc;
using RutasWebApi.Models.Repositorio;

namespace RutasWebApi.Models.Config
{
    public class DependencyInjectionConfig
    {
        public static IContainer RegistrarDependencias()
        {
            var Builder = new ContainerBuilder();
            //Builder.RegisterInstance(new CiudadRepositorio(new DBRutasEntities())).As<ICiudadRepositorio>();
            Builder.RegisterGeneric(typeof(RepositorioGenerico<>)).As(typeof(IRepositorioGenerico<>));

            // Registrar todos los controladores del ensamblado
            Builder.RegisterControllers(typeof(MvcApplication).Assembly).InstancePerRequest();
            return Builder.Build();
        }
    }
}