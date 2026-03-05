using System;
using System.IO;
using System.Net;
using System.Text;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source HTML file and the destination PDF file
        const string htmlPath = "input.html";   // ensure this file exists in the working directory
        const string pdfPath  = "output.pdf";

        // Credentials for external resources referenced in the HTML (e.g., images, CSS)
        ICredentials externalCreds = new NetworkCredential("username", "password");

        // Configure HtmlLoadOptions with the credentials
        HtmlLoadOptions loadOptions = new HtmlLoadOptions
        {
            ExternalResourcesCredentials = externalCreds
        };

        // Verify that the HTML source file exists before attempting conversion
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"Error: Cannot find source file '{htmlPath}'. Please ensure the file exists.");
            return;
        }

        // Read the HTML content into a memory stream – this avoids path‑related issues and works with Aspose correctly
        string htmlContent = File.ReadAllText(htmlPath, Encoding.UTF8);
        using (var htmlStream = new MemoryStream(Encoding.UTF8.GetBytes(htmlContent)))
        {
            // Load the HTML from the stream using the configured options and convert it to PDF
            using (Document pdfDocument = new Document(htmlStream, loadOptions))
            {
                // Save the resulting PDF
                pdfDocument.Save(pdfPath);
            }
        }

        Console.WriteLine($"HTML successfully converted to PDF: {pdfPath}");
    }
}
