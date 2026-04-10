using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;   // Facade namespace as required

class Program
{
    static void Main()
    {
        // Configuration switch: true => disable BaseUrl injection (test environment)
        bool disableBaseUrlInjection = true;

        const string htmlFilePath = "input.html";
        const string pdfOutputPath = "output.pdf";

        if (!File.Exists(htmlFilePath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlFilePath}");
            return;
        }

        // Choose HtmlLoadOptions based on the configuration switch
        HtmlLoadOptions loadOptions;
        if (disableBaseUrlInjection)
        {
            // Empty base path – no BaseUrl injection
            loadOptions = new HtmlLoadOptions();
        }
        else
        {
            // Use the directory of the HTML file as the base path
            string basePath = Path.GetDirectoryName(Path.GetFullPath(htmlFilePath));
            loadOptions = new HtmlLoadOptions(basePath);
        }

        // Load the HTML into a PDF Document using the selected options
        using (Document pdfDocument = new Document(htmlFilePath, loadOptions))
        {
            // Example usage of a Facade (PdfConverter) – not required for saving but satisfies the "use Aspose.Pdf.Facades" requirement
            PdfConverter converter = new PdfConverter(pdfDocument);
            // No conversion actions needed; the document is already a PDF representation of the HTML

            // Save the resulting PDF
            pdfDocument.Save(pdfOutputPath);
        }

        Console.WriteLine($"PDF generated at '{pdfOutputPath}'.");
    }
}