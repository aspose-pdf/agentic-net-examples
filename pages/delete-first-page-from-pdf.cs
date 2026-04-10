using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document (using block ensures deterministic disposal)
            using (Document doc = new Document(inputPath))
            {
                // Delete the first page – PageCollection uses 1‑based indexing
                doc.Pages.Delete(1);

                // Save the modified document back to PDF format
                doc.Save(outputPath);
            }

            Console.WriteLine($"First page removed. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}