using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the directory that contains the HTML source file.
        // Replace "YOUR_DATA_DIRECTORY" with the actual folder path.
        string dataDir = @"YOUR_DATA_DIRECTORY";

        // Input HTML file.
        string htmlPath = Path.Combine(dataDir, "HTML-to-PDF.html");
        // Output PDF file.
        string pdfPath = Path.Combine(dataDir, "HTML-to-PDF.pdf");

        // Verify that the HTML source exists before attempting conversion.
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"Error: HTML source file not found at '{htmlPath}'.");
            return;
        }

        // Load the HTML file using default HtmlLoadOptions.
        HtmlLoadOptions loadOptions = new HtmlLoadOptions();

        // Create a PDF document from the HTML content.
        using (Document pdfDocument = new Document(htmlPath, loadOptions))
        {
            // Save the resulting PDF.
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"PDF successfully created at '{pdfPath}'.");
    }
}