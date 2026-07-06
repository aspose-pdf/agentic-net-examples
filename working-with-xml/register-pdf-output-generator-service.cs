using Microsoft.Extensions.DependencyInjection;
using Aspose.Pdf.Comparison;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPdfGeneration(this IServiceCollection services)
    {
        // Register PdfOutputGenerator for reuse across the application.
        // Using the default constructor; can be changed to other overloads if needed.
        services.AddSingleton<PdfOutputGenerator>(provider => new PdfOutputGenerator());
        return services;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // Entry point required for executable projects.
        // No runtime logic needed for this library registration example.
    }
}