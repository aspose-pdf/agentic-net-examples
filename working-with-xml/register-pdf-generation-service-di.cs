using Microsoft.Extensions.DependencyInjection;
using Aspose.Pdf.Comparison;

namespace MyApp.DependencyInjection
{
    /// <summary>
    /// Extension methods for registering Aspose.Pdf services.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers the PDF generation service (<see cref="PdfOutputGenerator"/>) for reuse across the application.
        /// The generator implements <see cref="IFileOutputGenerator"/> and can be injected wherever needed.
        /// </summary>
        /// <param name="services">The DI container's service collection.</param>
        /// <returns>The same <see cref="IServiceCollection"/> instance for chaining.</returns>
        public static IServiceCollection AddPdfGeneration(this IServiceCollection services)
        {
            // Register the concrete generator as a singleton.
            // Singleton is appropriate because the generator holds no per‑request state
            // and can be reused safely across the application lifetime.
            services.AddSingleton<PdfOutputGenerator>();

            // Also expose the generator via its interface for abstraction.
            services.AddSingleton<IFileOutputGenerator>(provider =>
                provider.GetRequiredService<PdfOutputGenerator>());

            return services;
        }
    }
}

// ---------------------------------------------------------------------------
// Added a minimal entry point so the project can be built as an executable.
// This satisfies the compiler error CS5001 (missing static Main method).
// ---------------------------------------------------------------------------
namespace MyApp
{
    public static class Program
    {
        /// <summary>
        /// Application entry point – currently a no‑op placeholder.
        /// </summary>
        /// <param name="args">Command‑line arguments.</param>
        public static void Main(string[] args)
        {
            // No startup logic required for the library‑style registration demo.
            // The method exists solely to satisfy the C# compiler.
        }
    }
}
