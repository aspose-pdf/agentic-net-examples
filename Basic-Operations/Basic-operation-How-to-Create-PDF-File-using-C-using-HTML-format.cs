using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Input HTML file path (adjust as needed)
        string htmlPath = "input.html";
        // Output PDF file path
        string pdfPath = "output.pdf";

        // Verify that the HTML source file exists
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"Error: HTML file not found at '{htmlPath}'.");
            return;
        }

        try
        {
            // Load the HTML file into a PDF document using HtmlLoadOptions
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();
            Document pdfDocument = new Document(htmlPath, loadOptions);

            // Save the document as PDF
            // Using the provided lifecycle rule for saving
            pdfDocument.Save(pdfPath);

            Console.WriteLine($"PDF successfully created at '{pdfPath}'.");
        }
        catch (Exception ex)
        {
            // Generic error handling
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}