using System;
using System.IO;
using System.Net;                     // for NetworkCredential
using Aspose.Pdf;                     // Document, HtmlLoadOptions

class HtmlToPdfConverter
{
    static void Main()
    {
        // Paths to the source HTML file and the target PDF file.
        const string htmlPath = "input.html";
        const string pdfPath  = "output.pdf";

        // Credentials required to access external resources referenced in the HTML.
        const string userName = "myUser";
        const string password = "myPassword";

        // Verify that the HTML source file exists.
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // Configure load options: supply credentials for external resources.
        HtmlLoadOptions loadOptions = new HtmlLoadOptions();
        loadOptions.ExternalResourcesCredentials = new NetworkCredential(userName, password);

        // Load the HTML into a PDF document using the configured options.
        // The Document constructor handles creation and loading.
        using (Document pdfDocument = new Document(htmlPath, loadOptions))
        {
            // Save the resulting PDF. The Save method follows the lifecycle rule.
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"HTML successfully converted to PDF: {pdfPath}");
    }
}