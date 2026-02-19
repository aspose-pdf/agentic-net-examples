using System;
using System.IO;
using Aspose.Pdf;          // Core PDF classes
using Aspose.Pdf;          // HtmlLoadOptions resides in this namespace

class Program
{
    static void Main()
    {
        // Paths for the source HTML file and the resulting PDF file
        const string htmlPath = "input.html";
        const string pdfPath  = "output.pdf";

        // Verify that the HTML file exists before attempting to load it
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // Load the HTML content into a new PDF document
        // HtmlLoadOptions can be customized if needed; here we use the default options
        HtmlLoadOptions loadOptions = new HtmlLoadOptions();
        Document pdfDocument = new Document(htmlPath, loadOptions);

        // Save the generated PDF document to the specified output path
        // This follows the standard document-save rule
        pdfDocument.Save(pdfPath);

        Console.WriteLine($"PDF successfully created at: {pdfPath}");
    }
}