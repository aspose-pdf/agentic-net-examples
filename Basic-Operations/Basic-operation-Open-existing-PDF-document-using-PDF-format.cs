using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the existing PDF file
        const string inputPath = "input.pdf";

        // Optional: path to save a copy (can be the same as inputPath)
        const string outputPath = "output.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        // Load the existing PDF document (PDF format is inferred from the file extension)
        Document pdfDocument = new Document(inputPath);

        // Example operation: display the number of pages
        Console.WriteLine($"PDF loaded successfully. Page count: {pdfDocument.Pages.Count}");

        // Save the document (using the provided document-save rule)
        pdfDocument.Save(outputPath);
    }
}