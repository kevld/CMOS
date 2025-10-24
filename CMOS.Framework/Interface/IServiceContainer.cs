namespace CMOS.Framework.Interface
{
    public interface IServiceContainer
    {
        // Transient avec interface
        void AddTransient<TInterface, TImplementation>()
            where TImplementation : TInterface, new();

        // Transient sans interface (classe concrète)
        void AddTransient<TService>() where TService : new();

        // Transient avec factory
        void AddTransient<TInterface>(Func<TInterface> factory);

        // Singleton avec interface
        void AddSingleton<TInterface, TImplementation>()
            where TImplementation : TInterface, new();

        // Singleton sans interface (classe concrète)
        void AddSingleton<TService>() where TService : new();

        // Singleton avec instance existante
        void AddSingleton<TInterface>(TInterface instance);

        // Singleton avec factory
        void AddSingleton<TInterface>(Func<TInterface> factory);

        // Résolution générique
        TInterface GetService<TInterface>();

        // Résolution non-générique
        object GetService(Type serviceType);

        // Vérifier si un service est enregistré
        bool IsRegistered<TInterface>();

        // Obtenir le nombre de services
        int Count { get; }
    }
}
