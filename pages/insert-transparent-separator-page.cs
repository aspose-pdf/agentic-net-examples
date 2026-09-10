using System;
using System.IO;
using Aspose.Pdf;               // Core API
using Aspose.Pdf.Annotations;   // Not needed here but kept for completeness

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_separator.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block (ensures deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Determine where to insert the separator.
            // Example: insert after the first page (position = 2 because indexing is 1‑based).
            int insertPosition = 2;

            // Insert an empty page at the desired position.
            Page separatorPage = doc.Pages.Insert(insertPosition);

            // Set the page background to transparent.
            separatorPage.Background = Aspose.Pdf.Color.Transparent;

            // Save the modified document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Separator page added. Saved to '{outputPath}'.");
    }
}