using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF. Document preserves the entire structure, resources and metadata.
            using (Document pdfDocument = new Document(inputPath))
            {
                // Save to a new file, keeping all original content intact.
                pdfDocument.Save(outputPath);
            }

            Console.WriteLine($"PDF successfully transformed and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during PDF transformation: {ex.Message}");
        }
    }
}
