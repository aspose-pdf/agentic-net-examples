using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the existing PDF file
        const string inputPath = "input.pdf";

        // Optional: path to save a copy of the opened document
        const string outputPath = "copy.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document inside a using block to ensure proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Example operation: display the number of pages
            Console.WriteLine($"Pages in document: {doc.Pages.Count}");

            // Example operation: display the document title (if any)
            Console.WriteLine($"Title: {doc.Info.Title}");

            // Save a copy of the document (demonstrates the Save method)
            doc.Save(outputPath);
            Console.WriteLine($"Document saved to '{outputPath}'.");
        }
    }
}