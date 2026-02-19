using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source HTML file (can be any stream source)
        const string htmlPath = "input.html";
        // Path where the resulting PDF will be saved
        const string pdfPath = "output.pdf";

        // Verify that the HTML source exists
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"Error: HTML file not found at '{htmlPath}'.");
            return;
        }

        // Open the HTML file as a read‑only stream
        using (FileStream htmlStream = File.OpenRead(htmlPath))
        {
            // Load options for converting HTML to PDF
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();

            // Create a Document from the HTML stream using the load options
            Document pdfDocument = new Document(htmlStream, loadOptions);

            // Save the resulting PDF document
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"PDF successfully created at '{pdfPath}'.");
    }
}