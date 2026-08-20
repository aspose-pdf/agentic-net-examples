using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Aspose.Pdf.Comparison;

namespace MyApp.DependencyInjection
{
    // Extension methods to keep DI registration tidy
    public static class ServiceCollectionExtensions
    {
        // Registers the Aspose.Pdf Comparison PdfOutputGenerator for reuse.
        // Adjust the lifetime (Singleton, Scoped, Transient) as needed.
        public static IServiceCollection AddPdfGeneration(this IServiceCollection services)
        {
            // Register PdfOutputGenerator as a singleton instance.
            // The default constructor creates a generator ready for use.
            services.AddSingleton<PdfOutputGenerator>(provider => new PdfOutputGenerator());

            // If you need a generator with custom settings, you can register a factory:
            // services.AddSingleton<PdfOutputGenerator>(provider =>
            // {
            //     OutputTextStyle textStyle = new OutputTextStyle(); // configure as needed
            //     PageInfo pageInfo = new PageInfo();        // configure as needed
            //     return new PdfOutputGenerator(textStyle, pageInfo);
            // });

            return services;
        }
    }

    // Simple DI helper implementations for projects that do not reference the full Microsoft.Extensions.DependencyInjection package.
    internal static class SimpleServiceProviderExtensions
    {
        public static IServiceProvider BuildServiceProvider(this IServiceCollection services)
        {
            return new SimpleServiceProvider(services);
        }

        private class SimpleServiceProvider : IServiceProvider
        {
            private readonly Dictionary<Type, object> _singletons = new();

            public SimpleServiceProvider(IServiceCollection services)
            {
                foreach (var descriptor in services)
                {
                    if (descriptor.Lifetime != ServiceLifetime.Singleton)
                        continue; // For this example we only support singletons.

                    object instance;
                    if (descriptor.ImplementationInstance != null)
                    {
                        instance = descriptor.ImplementationInstance;
                    }
                    else if (descriptor.ImplementationFactory != null)
                    {
                        instance = descriptor.ImplementationFactory(this);
                    }
                    else
                    {
                        // Assume a public parameter‑less constructor exists.
                        // The null‑forgiving operator is used because Activator.CreateInstance may return null for value types,
                        // but in our scenario we only register reference types.
                        instance = Activator.CreateInstance(descriptor.ImplementationType)!;
                    }

                    _singletons[descriptor.ServiceType] = instance;
                }
            }

            public object GetService(Type serviceType)
            {
                _singletons.TryGetValue(serviceType, out var instance);
                return instance;
            }
        }
    }

    internal static class ServiceProviderRequiredExtensions
    {
        public static T GetRequiredService<T>(this IServiceProvider provider)
        {
            var service = provider.GetService(typeof(T));
            if (service == null)
                throw new InvalidOperationException($"Service of type {typeof(T)} is not registered.");
            return (T)service;
        }
    }

    // Minimal entry point required for a console‑style project.
    // This simply builds a service provider to prove that the registration works.
    public class Program
    {
        public static void Main(string[] args)
        {
            // Create a new service collection and register the PDF generation service.
            var services = new ServiceCollection();
            services.AddPdfGeneration();

            // Build the provider – in a real application you would keep this around via DI.
            var serviceProvider = services.BuildServiceProvider();

            // Resolve the generator to demonstrate that registration succeeded.
            var generator = serviceProvider.GetRequiredService<PdfOutputGenerator>();
            Console.WriteLine($"PdfOutputGenerator resolved: {generator?.GetType().FullName ?? "null"}");
        }
    }
}
