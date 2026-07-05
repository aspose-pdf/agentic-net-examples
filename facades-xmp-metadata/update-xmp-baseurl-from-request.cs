using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace PdfMetadataExample
{
    // Simple DTO that mimics the parts of HttpContext.Request we need.
    // Made properties nullable to satisfy the non‑nullable warnings when the object is instantiated without a constructor.
    public class RequestInfo
    {
        public string? Scheme { get; set; }      // "http" or "https"
        public string? Host { get; set; }        // "example.com"
        public string? PathBase { get; set; }    // optional base path, e.g. "/app"
    }

    public static class UrlHelper
    {
        public static string BuildBaseUrl(RequestInfo request)
        {
            // Guard against null values – fall back to empty strings if any part is missing.
            string scheme = request.Scheme ?? string.Empty;
            string host = request.Host ?? string.Empty;
            string pathBase = string.IsNullOrEmpty(request.PathBase) ? string.Empty :
                (request.PathBase.StartsWith("/") ? request.PathBase : "/" + request.PathBase);

            return $"{scheme}://{host}{pathBase}";
        }
    }

    public class PdfMetadataService
    {
        // Updates the XMP BaseURL property of a PDF using the supplied base URL.
        public void UpdateXmpBaseUrl(string baseUrl, string inputPdfPath, string outputPdfPath)
        {
            using (PdfXmpMetadata xmp = new PdfXmpMetadata())
            {
                xmp.BindPdf(inputPdfPath);
                xmp.Add(DefaultMetadataProperties.BaseURL, baseUrl);
                xmp.Save(outputPdfPath);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Simulate obtaining request data in a web service.
            var request = new RequestInfo
            {
                Scheme = "https",
                Host = "example.com",
                PathBase = "/app"
            };

            string baseUrl = UrlHelper.BuildBaseUrl(request);
            Console.WriteLine($"Computed BaseURL: {baseUrl}");

            // -----------------------------------------------------------------
            // Ensure we have a PDF to work with.  For a self‑contained example we
            // create a minimal PDF in memory and save it to "input.pdf".
            // -----------------------------------------------------------------
            string inputPdf = "input.pdf";
            CreateSamplePdfIfMissing(inputPdf);

            string outputPdf = "output.pdf";

            var service = new PdfMetadataService();
            service.UpdateXmpBaseUrl(baseUrl, inputPdf, outputPdf);

            Console.WriteLine("XMP BaseURL updated successfully.");
        }

        private static void CreateSamplePdfIfMissing(string path)
        {
            if (File.Exists(path))
                return;

            // Create a very simple PDF with one blank page.
            using (Document doc = new Document())
            {
                // Add a page – Aspose.Pdf creates a default size (A4) page.
                doc.Pages.Add();
                doc.Save(path);
            }
        }
    }
}
