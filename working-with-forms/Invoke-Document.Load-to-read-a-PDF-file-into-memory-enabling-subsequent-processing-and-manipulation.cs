using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";   // PDF to load
        const string outputPath = "loaded.pdf"; // optional save location

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF directly using the constructor that accepts a file path.
        // No LoadOptions are required for standard PDF files.
        using (Document doc = new Document(inputPath))
        {
            // Example processing: output the number of pages loaded
            Console.WriteLine($"Document loaded. Page count: {doc.Pages.Count}");

            // Optional: save the loaded document to verify the load succeeded
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document processing completed. Saved to '{outputPath}'.");
    }
}
