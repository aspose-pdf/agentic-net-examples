using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;   // Facade namespace as requested

class Program
{
    // Configuration switch – set to true in test environments to disable BaseUrl injection
    private const bool DisableBaseUrlInTests = true;

    static void Main()
    {
        const string htmlPath = "input.html";
        const string pdfPath  = "output.pdf";

        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML source not found: {htmlPath}");
            return;
        }

        // Choose HtmlLoadOptions based on the configuration switch
        HtmlLoadOptions loadOptions = DisableBaseUrlInTests
            ? new HtmlLoadOptions()                     // empty base path – no BaseUrl injection
            : new HtmlLoadOptions(Path.GetDirectoryName(Path.GetFullPath(htmlPath)) ?? string.Empty);

        // Load the HTML file into a PDF document using the selected options
        using (Document pdfDocument = new Document(htmlPath, loadOptions))
        {
            // Save the resulting PDF
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"PDF generated at '{pdfPath}'. BaseUrl injection {(DisableBaseUrlInTests ? "disabled" : "enabled")}.");
    }
}