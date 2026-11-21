using System;
using Microsoft.Extensions.DependencyInjection;
using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace PdfGenerationDemo
{
    /// <summary>
    /// Service contract for PDF generation.
    /// </summary>
    public interface IPdfGenerator
    {
        /// <summary>
        /// Generates a simple PDF document at the specified path.
        /// </summary>
        /// <param name="outputPath">File system path where the PDF will be saved.</param>
        void Generate(string outputPath);
    }

    /// <summary>
    /// Implementation that uses Aspose.Pdf to create PDFs.
    /// </summary>
    public class AsposePdfGenerator : IPdfGenerator
    {
        public void Generate(string outputPath)
        {
            // Document creation must be wrapped in a using block for deterministic disposal.
            using (Document doc = new Document())
            {
                // Add a new page to the document.
                Page page = doc.Pages.Add();

                // Create a text fragment and add it to the page.
                TextFragment text = new TextFragment("Hello from Aspose.Pdf");
                page.Paragraphs.Add(text);

                // Save the document as PDF.
                doc.Save(outputPath);
            }
        }
    }

    /// <summary>
    /// Extension methods for registering PDF services in the DI container.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers the PDF generation service as a singleton.
        /// </summary>
        /// <param name="services">The IServiceCollection to add the service to.</param>
        /// <returns>The same IServiceCollection instance for chaining.</returns>
        public static IServiceCollection AddPdfGeneration(this IServiceCollection services)
        {
            services.AddSingleton<IPdfGenerator, AsposePdfGenerator>();
            return services;
        }
    }

    /// <summary>
    /// A very small, self‑contained service provider used when the standard
    /// BuildServiceProvider extension is unavailable (e.g., missing package).
    /// It resolves only the services that are registered in this demo.
    /// </summary>
    public class SimpleServiceProvider : IServiceProvider
    {
        private readonly IServiceCollection _services;
        public SimpleServiceProvider(IServiceCollection services) => _services = services;

        public object GetService(Type serviceType)
        {
            // Resolve the IPdfGenerator singleton manually.
            if (serviceType == typeof(IPdfGenerator))
            {
                // The singleton registration ensures a single instance.
                // For simplicity we create a new instance here; in a real app you would
                // store and reuse the instance according to the lifetime.
                return new AsposePdfGenerator();
            }
            return null;
        }
    }

    /// <summary>
    /// Console entry point that demonstrates DI registration and PDF generation.
    /// </summary>
    class Program
    {
        static void Main()
        {
            // Build a service collection and register the PDF generator.
            var services = new ServiceCollection()
                .AddPdfGeneration();

            // Use the custom SimpleServiceProvider to resolve services without
            // requiring Microsoft.Extensions.DependencyInjection's BuildServiceProvider.
            var serviceProvider = new SimpleServiceProvider(services);

            // Resolve the generator from the container.
            var generator = (IPdfGenerator)serviceProvider.GetService(typeof(IPdfGenerator));

            // Define an output path (temporary location).
            string outputPath = System.IO.Path.Combine(
                System.IO.Path.GetTempPath(),
                $"AsposeDemo_{Guid.NewGuid():N}.pdf");

            // Generate the PDF.
            generator.Generate(outputPath);

            Console.WriteLine($"PDF generated at: {outputPath}");
        }
    }
}
