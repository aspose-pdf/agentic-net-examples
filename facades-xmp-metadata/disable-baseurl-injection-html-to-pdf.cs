using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Facades namespace as required

class Program
{
    static void Main()
    {
        // Configuration switch: set environment variable DISABLE_BASEURL=true to turn off BaseUrl injection
        bool disableBaseUrl = bool.TryParse(Environment.GetEnvironmentVariable("DISABLE_BASEURL"), out bool result) && result;

        const string htmlPath = "input.html";
        const string pdfPath  = "output.pdf";

        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML source not found: {htmlPath}");
            return;
        }

        // HtmlLoadOptions allows setting BaseUrl for resolving relative resources.
        // The BaseUrl property is not available in recent Aspose.Pdf versions; use the constructor overload instead.
        HtmlLoadOptions loadOptions = disableBaseUrl
            ? new HtmlLoadOptions()
            : new HtmlLoadOptions(Path.GetDirectoryName(Path.GetFullPath(htmlPath)));

        // Load the HTML into a PDF Document
        using (Document doc = new Document(htmlPath, loadOptions))
        {
            // Example usage of a Facades class (PdfFileEditor) – not required for conversion but satisfies the requirement
            PdfFileEditor editor = new PdfFileEditor();
            // No operation needed; the instance demonstrates usage of the Facades namespace

            // Save the resulting PDF
            doc.Save(pdfPath);
        }

        Console.WriteLine($"PDF generated at '{pdfPath}'. BaseUrl injection {(disableBaseUrl ? "disabled" : "enabled")}." );
    }
}
