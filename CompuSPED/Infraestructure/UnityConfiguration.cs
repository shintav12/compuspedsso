using CompuSPED.Service;
using Unity;

namespace CompuSPED.Infraestructure
{
    public sealed class UnityConfiguration
    {

        private static IUnityContainer container;
        private static readonly object Lock = new object();

        public static IUnityContainer GetUnityContainer()
        {
            lock (Lock)
            {
                if (container != null) return container;
                container = GetDefaultContainer();
            }

            return container;
        }

        public static UnityContainer GetDefaultContainer()
        {
            var defaultContainer = new UnityContainer();

            RegisterServices(defaultContainer);

            return defaultContainer;
        }

        private static void RegisterServices(UnityContainer defaultContainer)
        {
            //defaultContainer.RegisterType<ClientService, ClientService>();
        }
    }
}