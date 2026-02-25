using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Save the document back to the file system as PDF.
                // No SaveOptions are required because the target format is PDF.
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF successfully saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during load or save
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}