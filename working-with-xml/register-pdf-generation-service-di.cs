using Microsoft.Extensions.DependencyInjection;
using Aspose.Pdf.Comparison;

namespace AsposePdfApi
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers the Aspose.Pdf comparison PDF generation service for DI.
        /// </summary>
        /// <param name="services">The IServiceCollection to add the service to.</param>
        /// <returns>The same IServiceCollection for chaining.</returns>
        public static IServiceCollection AddPdfGeneration(this IServiceCollection services)
        {
            // Register PdfOutputGenerator as a singleton so the same instance can be reused.
            services.AddSingleton<PdfOutputGenerator>();
            return services;
        }
    }

    // Dummy entry point to satisfy the compiler when the project is built as an executable.
    internal class Program
    {
        public static void Main(string[] args)
        {
            // No runtime logic required for the library; this method exists only to provide an entry point.
        }
    }
}