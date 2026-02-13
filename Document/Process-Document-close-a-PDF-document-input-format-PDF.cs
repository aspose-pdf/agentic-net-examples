using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Path to the PDF file that will be opened.
        const string inputPath = "input.pdf";

        // Verify that the file exists before attempting to load it.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{inputPath}'.");
            return;
        }

        // Load the PDF document inside a using block.
        // The using statement ensures that the Document is disposed (closed) automatically.
        using (Document pdfDocument = new Document(inputPath))
        {
            // No modifications are required; we simply demonstrate loading and closing.
            // Optionally, you can save the document back to the same file or another location.
            // This follows the provided document-save rule.
            pdfDocument.Save(inputPath); // Save back to the original file (or specify a different output path).

            Console.WriteLine("PDF document loaded and closed successfully.");
        } // Document is disposed here.
    }
}