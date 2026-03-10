using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the resulting PDF
        const string inputPath  = "source.pdf";
        const string outputPath = "source_pages_deleted.pdf";

        // Pages to delete – note that page numbers are 1‑based
        // Example: delete page 2 and page 4
        int[] pagesToDelete = new int[] { 2, 4 };

        // Verify the source file exists
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
                // Ensure the requested pages exist before attempting deletion
                int maxPage = doc.Pages.Count;
                foreach (int p in pagesToDelete)
                {
                    if (p < 1 || p > maxPage)
                    {
                        Console.Error.WriteLine($"Page number {p} is out of range (1‑{maxPage}). Skipping deletion.");
                        return;
                    }
                }

                // Delete the specified pages in a single call
                doc.Pages.Delete(pagesToDelete);

                // Save the modified document (PDF format, no SaveOptions needed)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Pages deleted successfully. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}