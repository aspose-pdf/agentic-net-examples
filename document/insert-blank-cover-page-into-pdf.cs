using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // Existing PDF
        const string outputPath = "output_with_cover.pdf"; // Resulting PDF

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Insert a blank page at the very beginning (position 1, 1‑based indexing)
            // The Insert method creates an empty page with the most common size in the document
            Page coverPage = doc.Pages.Insert(1);

            // Optional: set a background color or other properties on the cover page
            // coverPage.Background = Aspose.Pdf.Color.LightGray;

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Blank cover page inserted. Saved to '{outputPath}'.");
    }
}