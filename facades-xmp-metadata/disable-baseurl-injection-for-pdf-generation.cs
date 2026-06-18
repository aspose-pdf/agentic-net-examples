using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;   // Facade namespace as required

class Program
{
    static void Main()
    {
        // Input HTML file and output PDF file paths
        const string htmlPath = "input.html";
        const string pdfPath  = "output.pdf";

        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // Configuration switch: set an environment variable (e.g., ASPose_Pdf_Test=true)
        // to indicate a test environment where BaseUrl injection must be disabled.
        bool disableBaseUrl = string.Equals(
            Environment.GetEnvironmentVariable("ASPOSE_PDF_TEST"),
            "true",
            StringComparison.OrdinalIgnoreCase);

        // When BaseUrl injection is disabled we use the parameterless constructor,
        // which creates HtmlLoadOptions with an empty BasePath.
        // Otherwise we can provide a specific base path if needed.
        HtmlLoadOptions loadOptions = disableBaseUrl
            ? new HtmlLoadOptions()                     // empty base path
            : new HtmlLoadOptions(Path.GetDirectoryName(Path.GetFullPath(htmlPath)));

        // Load the HTML into a PDF document using the chosen load options.
        using (Document pdfDocument = new Document(htmlPath, loadOptions))
        {
            // Save the resulting PDF.
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"PDF generated at '{pdfPath}'. BaseUrl injection disabled: {disableBaseUrl}");
    }
}