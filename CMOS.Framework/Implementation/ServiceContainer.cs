using CMOS.Framework.Interface;

namespace CMOS.Framework.Implementation
{
    // Classe interne pour stocker les descripteurs de services
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

        // Enregistrer un service transient (nouvelle instance à chaque fois)
        public void AddTransient<TInterface, TImplementation>()
            where TImplementation : TInterface, new()
        {
            var typeName = typeof(TInterface).Name;

            // Retirer l'existant si présent
            RemoveService(typeName);

            _services.Add(new ServiceDescriptor
            {
                TypeName = typeName,
                Factory = () => new TImplementation(),
                IsTransient = true
            });
        }

        // Enregistrer un transient sans interface (classe concrète)
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

        // Enregistrer un singleton (instance unique)
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

        // Enregistrer un singleton sans interface (classe concrète)
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

        // Enregistrer une instance existante
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

        // Enregistrer un singleton avec factory
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

        // Enregistrer un transient avec factory
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

        // Résoudre une dépendance (générique)
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
                        // Créer une nouvelle instance pour transient
                        result = service.Factory();
                    }
                    else
                    {
                        // Retourner l'instance singleton
                        result = service.Instance;
                    }

                    // Cast via variable intermédiaire pour éviter kernel panic
                    return (TInterface)(object)result;
                }
            }

            throw new Exception($"Service de type {typeName} non enregistré");
        }

        // Résoudre une dépendance (non-générique)
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

        // Méthode utilitaire pour retirer un service existant
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

        // Vérifier si un service est enregistré
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

        // Obtenir le nombre de services enregistrés
        public int Count => _services.Count;
    }
}