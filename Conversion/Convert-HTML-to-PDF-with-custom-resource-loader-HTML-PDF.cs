using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;   // Included as requested

class HtmlToPdfConverter
{
    static void Main()
    {
        // Input HTML file and output PDF file paths
        const string htmlPath  = "input.html";
        const string pdfPath   = "output.pdf";

        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // Create HtmlLoadOptions and assign a custom loader for external resources
        HtmlLoadOptions loadOptions = new HtmlLoadOptions();

        // The delegate signature is inferred from the field type.
        // Returning null lets the internal loader handle the resource as usual.
        loadOptions.CustomLoaderOfExternalResources = (string resourceUri) =>
        {
            // Example: you could fetch the resource from a database, cloud storage, etc.
            // For this demo we simply fall back to the default loader.
            return null;
        };

        // Optional: render the whole HTML on a single PDF page
        // loadOptions.IsRenderToSinglePage = true;

        // Load the HTML document using the custom options and convert it to PDF
        using (Document pdfDocument = new Document(htmlPath, loadOptions))
        {
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"HTML successfully converted to PDF: {pdfPath}");
    }
}