using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Aspose.Pdf.Comparison;

namespace MyApp.DependencyInjection
{
    // Extension methods to register Aspose.Pdf services
    public static class ServiceCollectionExtensions
    {
        // Registers PdfOutputGenerator for reuse throughout the application.
        // Singleton is used because PdfOutputGenerator is stateless and thread‑safe.
        public static IServiceCollection AddPdfGeneration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            // Register the generator as a singleton.
            services.AddSingleton<PdfOutputGenerator>(sp => new PdfOutputGenerator());

            return services;
        }

        // Minimal BuildServiceProvider implementation to avoid external package dependency.
        // This mimics the behaviour of Microsoft.Extensions.DependencyInjection's default provider
        // for the limited scenario used in this sample (singleton services only).
        public static IServiceProvider BuildServiceProvider(this IServiceCollection services)
        {
            return new SimpleServiceProvider(services);
        }
    }

    // Very small ServiceProvider that resolves singleton registrations from the collection.
    // It is sufficient for the demonstration purpose and removes the need for the official
    // Microsoft.Extensions.DependencyInjection package.
    internal class SimpleServiceProvider : IServiceProvider
    {
        private readonly Dictionary<Type, object> _singletons = new();

        public SimpleServiceProvider(IEnumerable<ServiceDescriptor> descriptors)
        {
            foreach (var descriptor in descriptors)
            {
                // Only handle Singleton lifetimes for this simple implementation.
                if (descriptor.Lifetime != ServiceLifetime.Singleton)
                    continue;

                object implementation = null;

                if (descriptor.ImplementationInstance != null)
                {
                    implementation = descriptor.ImplementationInstance;
                }
                else if (descriptor.ImplementationFactory != null)
                {
                    implementation = descriptor.ImplementationFactory(this);
                }
                else if (descriptor.ImplementationType != null)
                {
                    // Use Activator to create the instance. No constructor injection is needed
                    // for the current use‑case (PdfOutputGenerator has a parameter‑less ctor).
                    implementation = Activator.CreateInstance(descriptor.ImplementationType);
                }

                if (implementation != null)
                {
                    _singletons[descriptor.ServiceType] = implementation;
                }
            }
        }

        public object GetService(Type serviceType)
        {
            _singletons.TryGetValue(serviceType, out var instance);
            return instance;
        }
    }

    // Minimal entry point required for a console‑style project.
    public class Program
    {
        public static void Main(string[] args)
        {
            // Configure DI container.
            var services = new ServiceCollection();
            services.AddPdfGeneration();
            var provider = services.BuildServiceProvider();

            // Resolve the generator to prove registration works.
            var generator = provider.GetRequiredService<PdfOutputGenerator>();
            Console.WriteLine("PdfOutputGenerator registered successfully.");
        }
    }
}
