using System;

namespace CodeBase.Infrastructure.Services
{
    public class AllServices
    {
        private static AllServices _instance;
        
        public static AllServices Container => _instance ?? new AllServices();

        public void Register<TService>(TService service) 
            where TService : IService
        {
            ServiceImplementation<TService>.Service = service;
        }

        public TService Get<TService>() 
            where TService : IService
        {
            var service = ServiceImplementation<TService>.Service;
            if (service == null)
                throw new NullReferenceException($"Not registered service: {service}");
            
            return service;
        }

        private class ServiceImplementation<TService>
            where TService : IService
        {
            public static TService Service;
        }
    }
}