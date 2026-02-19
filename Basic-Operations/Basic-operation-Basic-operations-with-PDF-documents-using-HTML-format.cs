using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace
using Aspose.Pdf;               // HtmlLoadOptions resides here

class Program
{
    static void Main(string[] args)
    {
        // Input HTML file and output PDF file paths.
        // Adjust these paths as needed.
        string htmlPath = "input.html";
        string pdfPath  = "output.pdf";

        // Verify that the HTML source file exists.
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"Error: HTML file not found at '{htmlPath}'.");
            return;
        }

        try
        {
            // Load the HTML document using Aspose.Pdf's HtmlLoadOptions.
            var loadOptions = new HtmlLoadOptions();
            Document pdfDocument = new Document(htmlPath, loadOptions);

            // Save the loaded content as a PDF file.
            pdfDocument.Save(pdfPath);

            Console.WriteLine($"HTML file successfully converted to PDF and saved as '{pdfPath}'.");
        }
        catch (Exception ex)
        {
            // Report any unexpected errors.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}