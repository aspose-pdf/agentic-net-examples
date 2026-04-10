using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public class PdfMetadataService
{
    /// <summary>
    /// Updates the XMP BaseURL of a PDF using the supplied base URL (e.g., from a web request).
    /// If the input PDF does not exist a minimal PDF is created on‑the‑fly so the example can run
    /// without external files.
    /// </summary>
    public void UpdatePdfBaseUrl(string baseUrl, string inputPdfPath, string outputPdfPath)
    {
        if (string.IsNullOrWhiteSpace(baseUrl))
            throw new ArgumentException("Base URL is required.", nameof(baseUrl));
        if (string.IsNullOrWhiteSpace(inputPdfPath))
            throw new ArgumentException("Input PDF path is required.", nameof(inputPdfPath));
        if (string.IsNullOrWhiteSpace(outputPdfPath))
            throw new ArgumentException("Output PDF path is required.", nameof(outputPdfPath));

        // ------------------------------------------------------------
        // Ensure the source PDF exists – create a tiny placeholder PDF
        // if it is missing. This removes the FileNotFoundException that
        // the original sample produced.
        // ------------------------------------------------------------
        if (!File.Exists(inputPdfPath))
        {
            using (var placeholder = new Document())
            {
                placeholder.Pages.Add(); // add a blank page
                placeholder.Save(inputPdfPath);
            }
        }

        // Load the PDF and its XMP metadata.
        var xmp = new PdfXmpMetadata();
        // BindPdf has an overload that works with a file path – we keep it
        // because the file now definitely exists.
        xmp.BindPdf(inputPdfPath);

        // Set the BaseURL property in the XMP metadata.
        xmp.Add(DefaultMetadataProperties.BaseURL, baseUrl);

        // Save the PDF with the updated XMP metadata.
        xmp.Save(outputPdfPath);
    }
}

class Program
{
    static void Main()
    {
        // ------------------------------------------------------------
        // Simulate obtaining the request host URL in a web service.
        // In a real ASP.NET Core controller you would build it from
        // HttpContext.Request.Scheme, HttpContext.Request.Host, etc.
        // ------------------------------------------------------------
        string requestScheme = "https";
        string requestHost = "example.com";
        string requestPathBase = "/app"; // optional base path of the application
        string baseUrl = $"{requestScheme}://{requestHost}{requestPathBase}";

        string inputPdf = "input.pdf";   // path to the source PDF (created if missing)
        string outputPdf = "output.pdf"; // path where the updated PDF will be saved

        var service = new PdfMetadataService();
        service.UpdatePdfBaseUrl(baseUrl, inputPdf, outputPdf);

        Console.WriteLine($"XMP BaseURL updated to '{baseUrl}' and saved to '{outputPdf}'.");
    }
}
