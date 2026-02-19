using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF file path (change as needed)
        string inputPath = "input.pdf";

        // Output PDF file path (the document will be saved unchanged)
        string outputPath = "output.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document doc = new Document(inputPath);

            // Example operation: display the number of pages
            Console.WriteLine($"PDF loaded successfully. Page count: {doc.Pages.Count}");

            // Save the document (using the provided lifecycle rule)
            doc.Save(outputPath);

            Console.WriteLine($"Document saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}