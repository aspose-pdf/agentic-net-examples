using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

// This program converts an HTML file to PDF.
// It demonstrates a custom resource loader by specifying a base path
// where the HTML engine will resolve relative resources (images, CSS, etc.).
class HtmlToPdfConverter
{
    static void Main(string[] args)
    {
        // Input HTML file path (first argument) and output PDF path (second argument)
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: HtmlToPdfConverter <inputHtml> <outputPdf>");
            return;
        }

        string htmlPath = args[0];
        string pdfPath = args[1];

        // Verify that the HTML source file exists
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"Error: HTML file not found: {htmlPath}");
            return;
        }

        // Determine the directory that will serve as the base for relative resources.
        // This mimics a custom resource loader: any relative URL in the HTML will be
        // resolved against this folder.
        string resourceBasePath = Path.GetDirectoryName(Path.GetFullPath(htmlPath)) ?? Directory.GetCurrentDirectory();

        // Configure HTML load options.
        // - BasePath tells the loader where to look for images, CSS, fonts, etc.
        // - IsEmbedFonts ensures that fonts referenced in the HTML are embedded into the PDF.
        // - DisableFontLicenseVerifications can be set to true if you need to embed fonts
        //   that have restrictive licenses (optional).
        var loadOptions = new HtmlLoadOptions(resourceBasePath)
        {
            IsEmbedFonts = true,
            DisableFontLicenseVerifications = false
        };

        try
        {
            // Load the HTML document with the custom options.
            // The Document constructor accepts the source file path and the load options.
            Document pdfDocument = new Document(htmlPath, loadOptions);

            // Save the resulting PDF.
            pdfDocument.Save(pdfPath);

            Console.WriteLine($"HTML successfully converted to PDF: {pdfPath}");
        }
        catch (Exception ex)
        {
            // Generic error handling – report any issues that occur during conversion.
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}