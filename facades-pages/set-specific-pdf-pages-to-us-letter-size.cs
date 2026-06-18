using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // US Letter size in points (1 point = 1/72 inch)
        const double letterWidth = 612;   // 8.5 inches * 72
        const double letterHeight = 792;  // 11 inches * 72

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has enough pages before resizing
            int pageCount = doc.Pages.Count; // 1‑based indexing

            // Resize pages 3 through 6 (or up to the last page if fewer exist)
            for (int i = 3; i <= 6 && i <= pageCount; i++)
            {
                // Set each page size to the standard US Letter dimensions
                doc.Pages[i].SetPageSize(letterWidth, letterHeight);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Pages 3‑6 have been set to Letter size and saved to '{outputPath}'.");
    }
}
