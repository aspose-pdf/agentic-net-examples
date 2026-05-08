using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            int pageCount = doc.Pages.Count;

            // Ensure there are at least three pages to delete.
            if (pageCount >= 3)
            {
                // Delete the last three pages. Page numbers are 1‑based, so start from the highest index.
                for (int i = pageCount; i > pageCount - 3; i--)
                {
                    doc.Pages.Delete(i);
                }
            }
            else
            {
                Console.WriteLine("Document has fewer than three pages; no pages were deleted.");
            }

            // Save the modified document (PDF format).
            doc.Save(outputPath);
        }

        Console.WriteLine($"Last three pages removed. Saved to '{outputPath}'.");
    }
}