using System;
using System.IO;
using System.Net;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string htmlPath = "input.html";
        const string pdfPath  = "output.pdf";

        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // Configure load options with credentials for external resources (images, CSS, etc.)
        var loadOptions = new HtmlLoadOptions
        {
            ExternalResourcesCredentials = new NetworkCredential("username", "password")
        };

        // Load the HTML file into a PDF document using the configured options
        using (Document pdfDoc = new Document(htmlPath, loadOptions))
        {
            // Save the document as PDF
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"Conversion completed: {pdfPath}");
    }
}