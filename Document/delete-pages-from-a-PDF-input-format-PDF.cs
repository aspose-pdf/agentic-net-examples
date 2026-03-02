using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Output PDF file path after pages are removed
        const string outputPath = "output.pdf";

        // Pages to delete (1‑based indexing as required by Aspose.Pdf)
        // Example: delete pages 2 and 4
        int[] pagesToDelete = new int[] { 2, 4 };

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Ensure the requested pages are within the document range
                int pageCount = doc.Pages.Count;
                foreach (int p in pagesToDelete)
                {
                    if (p < 1 || p > pageCount)
                    {
                        Console.Error.WriteLine($"Page number {p} is out of range (1‑{pageCount}).");
                        return;
                    }
                }

                // Delete the specified pages
                doc.Pages.Delete(pagesToDelete);

                // Save the modified document
                doc.Save(outputPath);
            }

            Console.WriteLine($"Pages deleted successfully. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}