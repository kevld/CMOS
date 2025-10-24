using CMOS.Framework.Interface;

namespace CMOS.Framework.Implementation
{
    // DI container
    internal class ServiceDescriptor
    {
        public string TypeName { get; set; }
        public object Instance { get; set; }
        public Func<object> Factory { get; set; }
        public bool IsTransient { get; set; }
    }

    public class ServiceContainer : IServiceContainer
    {
        private readonly List<ServiceDescriptor> _services;

        public ServiceContainer()
        {
            _services = new List<ServiceDescriptor>();
        }

        public void AddTransient<TInterface, TImplementation>()
            where TImplementation : TInterface, new()
        {
            var typeName = typeof(TInterface).Name;

            RemoveService(typeName);

            _services.Add(new ServiceDescriptor
            {
                TypeName = typeName,
                Factory = () => new TImplementation(),
                IsTransient = true
            });
        }

        public void AddTransient<TService>() where TService : new()
        {
            var typeName = typeof(TService).Name;

            RemoveService(typeName);

            _services.Add(new ServiceDescriptor
            {
                TypeName = typeName,
                Factory = () => new TService(),
                IsTransient = true
            });
        }

        public void AddSingleton<TInterface, TImplementation>()
            where TImplementation : TInterface, new()
        {
            var typeName = typeof(TInterface).Name;
            var instance = new TImplementation();

            RemoveService(typeName);

            _services.Add(new ServiceDescriptor
            {
                TypeName = typeName,
                Instance = instance,
                IsTransient = false
            });
        }

        public void AddSingleton<TService>() where TService : new()
        {
            var typeName = typeof(TService).Name;
            var instance = new TService();

            RemoveService(typeName);

            _services.Add(new ServiceDescriptor
            {
                TypeName = typeName,
                Instance = instance,
                IsTransient = false
            });
        }

        public void AddSingleton<TInterface>(TInterface instance)
        {
            if (instance == null)
                throw new Exception("L'instance ne peut pas être null");

            var typeName = typeof(TInterface).Name;

            RemoveService(typeName);

            _services.Add(new ServiceDescriptor
            {
                TypeName = typeName,
                Instance = instance,
                IsTransient = false
            });
        }

        public void AddSingleton<TInterface>(Func<TInterface> factory)
        {
            if (factory == null)
                throw new Exception("La factory ne peut pas être null");

            var typeName = typeof(TInterface).Name;
            var instance = factory();

            RemoveService(typeName);

            _services.Add(new ServiceDescriptor
            {
                TypeName = typeName,
                Instance = instance,
                IsTransient = false
            });
        }

        public void AddTransient<TInterface>(Func<TInterface> factory)
        {
            if (factory == null)
                throw new Exception("La factory ne peut pas être null");

            var typeName = typeof(TInterface).Name;

            RemoveService(typeName);

            _services.Add(new ServiceDescriptor
            {
                TypeName = typeName,
                Factory = () => factory(),
                IsTransient = true
            });
        }

        public TInterface GetService<TInterface>()
        {
            var typeName = typeof(TInterface).Name;

            foreach (var service in _services)
            {
                if (service.TypeName == typeName)
                {
                    object result;
                    if (service.IsTransient)
                    {
                        result = service.Factory();
                    }
                    else
                    {
                        result = service.Instance;
                    }

                    return (TInterface)(object)result;
                }
            }

            throw new Exception($"Service de type {typeName} non enregistré");
        }

        public object GetService(Type serviceType)
        {
            var typeName = serviceType.Name;

            foreach (var service in _services)
            {
                if (service.TypeName == typeName)
                {
                    if (service.IsTransient)
                    {
                        return service.Factory();
                    }
                    else
                    {
                        return service.Instance;
                    }
                }
            }

            throw new Exception($"Service de type {typeName} non enregistré");
        }

        private void RemoveService(string typeName)
        {
            for (int i = _services.Count - 1; i >= 0; i--)
            {
                if (_services[i].TypeName == typeName)
                {
                    _services.RemoveAt(i);
                }
            }
        }

        public bool IsRegistered<TInterface>()
        {
            var typeName = typeof(TInterface).Name;

            foreach (var service in _services)
            {
                if (service.TypeName == typeName)
                    return true;
            }

            return false;
        }

        public int Count => _services.Count;
    }
}