using System;
using System.IO;
using System.Net;
using Aspose.Pdf;

class HtmlToPdfConverter
{
    static void Main()
    {
        // Input HTML file path
        const string htmlPath = "input.html";
        // Output PDF file path
        const string pdfPath = "output.pdf";

        // Credentials for external resources referenced in the HTML (e.g., images, CSS)
        var externalCredentials = new NetworkCredential("username", "password");

        // Verify that the input file exists
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // Configure load options for HTML → PDF conversion
        HtmlLoadOptions loadOptions = new HtmlLoadOptions
        {
            // Assign the credentials so that Aspose.Pdf can authenticate when fetching external resources
            ExternalResourcesCredentials = externalCredentials
        };

        // Load the HTML document and convert it to PDF
        using (Document pdfDocument = new Document(htmlPath, loadOptions))
        {
            // Save the resulting PDF
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"HTML successfully converted to PDF: {pdfPath}");
    }
}