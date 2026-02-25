using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for input and output PDF files
        string inputPath = "input.pdf";
        string outputPath = "output.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Save the document as PDF (same format) to the output path
                doc.Save(outputPath);
                Console.WriteLine($"Saved → '{outputPath}'");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during loading or saving
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}