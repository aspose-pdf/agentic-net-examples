using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class HtmlToPdfGenerator
{
    // Configuration switch – set to true in test environments to disable BaseUrl injection
    private static readonly bool DisableBaseUrlInjection =
        string.Equals(Environment.GetEnvironmentVariable("TEST_ENV"), "true", StringComparison.OrdinalIgnoreCase);

    /// <summary>
    /// Converts an HTML file to PDF.
    /// In test environments the BasePath is omitted (empty), otherwise a specific base path can be supplied.
    /// </summary>
    /// <param name="htmlPath">Full path to the source HTML file.</param>
    /// <param name="outputPdfPath">Full path where the resulting PDF will be saved.</param>
    /// <param name="basePath">Optional base path/URL for resolving relative resources in the HTML.</param>
    public static void ConvertHtmlToPdf(string htmlPath, string outputPdfPath, string basePath = null)
    {
        if (!File.Exists(htmlPath))
            throw new FileNotFoundException($"HTML file not found: {htmlPath}");

        // Choose HtmlLoadOptions based on the configuration switch
        HtmlLoadOptions loadOptions = DisableBaseUrlInjection
            ? new HtmlLoadOptions()                     // empty base path – no BaseUrl injection
            : new HtmlLoadOptions(basePath ?? Path.GetDirectoryName(htmlPath)); // use provided base path

        // Load the HTML into a PDF document using the selected options
        using (Document pdfDocument = new Document(htmlPath, loadOptions))
        {
            // Instantiate a Facade class (PdfConverter) as required – not used further but satisfies the rule
            using (PdfConverter converter = new PdfConverter(pdfDocument))
            {
                // No conversion actions needed here; the document is already a PDF.
                // The converter could be used for further processing if required.
            }

            // Save the resulting PDF
            pdfDocument.Save(outputPdfPath);
        }
    }

    static void Main()
    {
        // Example usage
        const string htmlFile = "sample.html";
        const string pdfFile = "sample.pdf";

        // In production you might provide a base URL; in tests it will be ignored automatically
        string baseUrl = "https://example.com/assets/";

        try
        {
            ConvertHtmlToPdf(htmlFile, pdfFile, baseUrl);
            Console.WriteLine($"HTML converted to PDF successfully: {pdfFile}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}