using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Desired output PPTX file path (extension determines format)
        const string outputPath = "output.pptx";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{inputPath}'.");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        // Retrieve total page count for progress reporting
        int totalPages = pdfDocument.Pages.Count;

        // Simple progress loop – iterates over pages to show status
        for (int i = 1; i <= totalPages; i++)
        {
            Console.WriteLine($"Processing page {i} of {totalPages}...");
            // No modification is performed; this loop only provides visual feedback.
        }

        // Save the document as PPTX; format is inferred from the .pptx extension
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Conversion completed. PPTX saved to '{outputPath}'.");
    }
}