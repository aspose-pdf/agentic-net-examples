using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

namespace PdfGenerationExample
{
    // Service contract for PDF generation
    public interface IPdfGenerationService
    {
        Aspose.Pdf.Document CreateEmptyDocument();
        void AddText(Aspose.Pdf.Document document, string text, int pageNumber);
        void Encrypt(Aspose.Pdf.Document document, string userPassword, string ownerPassword, Aspose.Pdf.Permissions permissions);
        Aspose.Pdf.Document Decrypt(string encryptedPath, string userPassword);
        void Save(Aspose.Pdf.Document document, string outputPath);
    }

    // Concrete implementation using core Aspose.Pdf APIs only
    public class AsposePdfGenerationService : IPdfGenerationService
    {
        // Creates a new PDF with a single blank page
        public Aspose.Pdf.Document CreateEmptyDocument()
        {
            var doc = new Aspose.Pdf.Document();
            doc.Pages.Add(); // add first page
            return doc;
        }

        // Adds a simple text fragment to the specified page
        public void AddText(Aspose.Pdf.Document document, string text, int pageNumber)
        {
            if (document == null) throw new ArgumentNullException(nameof(document));
            if (pageNumber < 1 || pageNumber > document.Pages.Count)
                throw new ArgumentOutOfRangeException(nameof(pageNumber));

            Aspose.Pdf.Page page = document.Pages[pageNumber];
            var textFragment = new Aspose.Pdf.Text.TextFragment(text)
            {
                TextState =
                {
                    // Use a concrete font from the repository instead of the abstract Font constructor
                    Font = FontRepository.FindFont("Arial"),
                    FontSize = 12,
                    ForegroundColor = Aspose.Pdf.Color.Black
                }
            };
            page.Paragraphs.Add(textFragment);
        }

        // Encrypts the document with AES‑256
        public void Encrypt(Aspose.Pdf.Document document, string userPassword, string ownerPassword, Aspose.Pdf.Permissions permissions)
        {
            if (document == null) throw new ArgumentNullException(nameof(document));
            document.Encrypt(userPassword, ownerPassword, permissions, Aspose.Pdf.CryptoAlgorithm.AESx256);
        }

        // Opens an encrypted PDF and removes protection
        public Aspose.Pdf.Document Decrypt(string encryptedPath, string userPassword)
        {
            if (!File.Exists(encryptedPath))
                throw new FileNotFoundException($"File not found: {encryptedPath}");

            var encryptedDoc = new Aspose.Pdf.Document(encryptedPath, userPassword);
            encryptedDoc.Decrypt(); // no parameters
            return encryptedDoc;
        }

        // Saves the document to the given file path
        public void Save(Aspose.Pdf.Document document, string outputPath)
        {
            if (document == null) throw new ArgumentNullException(nameof(document));

            string directory = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            document.Save(outputPath);
        }
    }

    // Minimal DI helper to avoid missing BuildServiceProvider extension in the current project setup
    public static class ServiceCollectionExtensions
    {
        public static IServiceProvider BuildServiceProvider(this IServiceCollection services)
        {
            return new SimpleServiceProvider(services);
        }
    }

    // Very small ServiceProvider implementation that supports only singleton registrations used in this example
    internal class SimpleServiceProvider : IServiceProvider
    {
        private readonly Dictionary<Type, object> _singletons = new();

        public SimpleServiceProvider(IServiceCollection services)
        {
            foreach (var descriptor in services)
            {
                if (descriptor.Lifetime == ServiceLifetime.Singleton)
                {
                    var implementationType = descriptor.ImplementationType ?? descriptor.ServiceType;
                    // Assume a parameter‑less constructor for the implementation type
                    var instance = Activator.CreateInstance(implementationType);
                    _singletons[descriptor.ServiceType] = instance;
                }
            }
        }

        public object GetService(Type serviceType)
        {
            _singletons.TryGetValue(serviceType, out var instance);
            return instance;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Register the PDF generation service in the DI container
            var services = new ServiceCollection();
            services.AddSingleton<IPdfGenerationService, AsposePdfGenerationService>();
            var serviceProvider = services.BuildServiceProvider();

            // Resolve the service
            var pdfService = (IPdfGenerationService)serviceProvider.GetService(typeof(IPdfGenerationService));

            // Create a new PDF, add text, encrypt, save, then decrypt and save again
            var doc = pdfService.CreateEmptyDocument();
            pdfService.AddText(doc, "Hello, Aspose.Pdf!", 1);

            // Define permissions (example: allow printing and content extraction)
            Aspose.Pdf.Permissions perms = Aspose.Pdf.Permissions.PrintDocument | Aspose.Pdf.Permissions.ExtractContent;
            pdfService.Encrypt(doc, "user123", "owner123", perms);
            string encryptedPath = "encrypted.pdf";
            pdfService.Save(doc, encryptedPath);

            // Decrypt and save the clear PDF
            var decryptedDoc = pdfService.Decrypt(encryptedPath, "user123");
            string decryptedPath = "decrypted.pdf";
            pdfService.Save(decryptedDoc, decryptedPath);

            Console.WriteLine("PDF creation, encryption, and decryption completed successfully.");
        }
    }
}
