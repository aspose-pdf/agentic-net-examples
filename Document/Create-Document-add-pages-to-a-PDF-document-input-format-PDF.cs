using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Input and output PDF file paths
        string inputPdfPath = "input.pdf";
        string outputPdfPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input PDF not found at '{inputPdfPath}'.");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDocument = new Document(inputPdfPath);

            // Add three blank pages to the document
            pdfDocument.Pages.Add(); // 1st blank page
            pdfDocument.Pages.Add(); // 2nd blank page
            pdfDocument.Pages.Add(); // 3rd blank page

            // Save the modified PDF (using the provided document-save rule)
            pdfDocument.Save(outputPdfPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}