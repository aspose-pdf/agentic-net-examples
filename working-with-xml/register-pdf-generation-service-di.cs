using Microsoft.Extensions.DependencyInjection;
using Aspose.Pdf.Comparison;

namespace AsposePdfApi
{
    // Registers the Aspose.Pdf PDF generation service for reuse throughout the application.
    // The service is registered as a singleton because PdfOutputGenerator is stateless
    // and can be safely shared across multiple requests.
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPdfGeneration(this IServiceCollection services)
        {
            // Register the concrete PdfOutputGenerator.
            services.AddSingleton<PdfOutputGenerator>();

            // Optionally expose it via its interface for more flexible consumption.
            services.AddSingleton<IFileOutputGenerator>(provider => provider.GetRequiredService<PdfOutputGenerator>());

            return services;
        }
    }

    // Minimal entry point required for a console‑style project.
    // The method does not need to perform any work for the purpose of this library.
    public static class Program
    {
        public static void Main(string[] args)
        {
            // Intentionally left blank.
        }
    }
}
