using System;
using System.IO;
using System.Net;               // For NetworkCredential
using Aspose.Pdf;               // Core Aspose.Pdf namespace
using Aspose.Pdf;               // HtmlLoadOptions is also in this namespace

class Program
{
    static void Main()
    {
        // Paths to the source HTML file and the destination PDF file.
        const string htmlPath = "input.html";
        const string pdfPath  = "output.pdf";

        // Verify that the HTML source exists.
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // Configure HtmlLoadOptions to supply credentials for external resources
        // (e.g., images, CSS files) referenced by the HTML.
        HtmlLoadOptions loadOptions = new HtmlLoadOptions();

        // Example: set basic authentication credentials.
        // Replace "user" and "password" with real values as needed.
        loadOptions.ExternalResourcesCredentials = new NetworkCredential("user", "password");

        // If you need a custom loader for resources (e.g., fetching from a cloud storage),
        // assign a delegate to CustomLoaderOfExternalResources here.
        // loadOptions.CustomLoaderOfExternalResources = (uri) => { /* custom fetch logic */ return null; };

        // Load the HTML into a PDF Document using the configured options.
        using (Document pdfDocument = new Document(htmlPath, loadOptions))
        {
            // Save the resulting PDF. No SaveOptions are required because the target format is PDF.
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"HTML successfully converted to PDF: {pdfPath}");
    }
}