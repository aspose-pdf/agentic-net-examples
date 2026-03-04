using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal.
            using (Document doc = new Document(inputPath))
            {
                // No format change is required; simply save the document as PDF.
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF successfully saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Handle any unexpected errors.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}