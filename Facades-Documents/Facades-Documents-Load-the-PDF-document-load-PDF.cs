using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF path (change as needed)
        const string inputPath = "input.pdf";
        // Output PDF path (the loaded document will be saved here)
        const string outputPath = "output.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{inputPath}'.");
            return;
        }

        try
        {
            // Create a Form facade instance (no parameters)
            Form pdfForm = new Form();

            // Load the PDF document into the facade
            pdfForm.BindPdf(inputPath);

            // Access the underlying Document object
            var document = pdfForm.Document;

            // Example operation: display the number of pages
            Console.WriteLine($"PDF loaded successfully. Page count: {document.Pages.Count}");

            // Save the loaded document to a new file
            // Using the provided document-save rule: {DocumentVar}.Save({OutputPath});
            document.Save(outputPath);

            Console.WriteLine($"Document saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}