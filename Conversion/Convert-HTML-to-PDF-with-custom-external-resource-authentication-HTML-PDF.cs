using System;
using System.IO;
using System.Net;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for input HTML and output PDF
        const string htmlPath = "input.html";
        const string pdfPath  = "output.pdf";

        // Verify that the HTML file exists
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // Configure HtmlLoadOptions with authentication for external resources
        HtmlLoadOptions loadOptions = new HtmlLoadOptions
        {
            // Provide credentials (e.g., for HTTP basic auth) that will be used when loading images, CSS, etc.
            ExternalResourcesCredentials = new NetworkCredential("username", "password")
        };

        // Load the HTML document into a PDF Document using the configured options
        using (Document pdfDocument = new Document(htmlPath, loadOptions))
        {
            // Save the resulting PDF
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"HTML successfully converted to PDF: {pdfPath}");
    }
}