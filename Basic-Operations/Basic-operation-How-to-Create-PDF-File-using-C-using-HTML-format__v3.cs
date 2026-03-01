using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Facades namespace is available if needed for other operations

class Program
{
    static void Main()
    {
        // Directory that contains the source HTML file and where the PDF will be written
        string dataDir = @"YOUR_DATA_DIRECTORY";

        // Input HTML file path
        string htmlPath = Path.Combine(dataDir, "input.html");

        // Output PDF file path
        string pdfPath = Path.Combine(dataDir, "output.pdf");

        // Verify that the HTML source exists before attempting conversion
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"Error: HTML source file not found at '{htmlPath}'.");
            return;
        }

        // Load the HTML file using HtmlLoadOptions (no base path needed for this example)
        HtmlLoadOptions loadOptions = new HtmlLoadOptions();

        // Create a Document from the HTML content and save it as PDF
        using (Document pdfDocument = new Document(htmlPath, loadOptions))
        {
            pdfDocument.Save(pdfPath); // Save the generated PDF
        }

        Console.WriteLine($"Successfully created PDF at '{pdfPath}'.");
    }
}