using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "trimmed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Custom pixel tolerance expressed as a fill‑threshold factor (0..1).
        // Smaller values make the detection stricter (less whitespace tolerated).
        double tolerance = 0.02; // e.g., 2 % of the page area must be filled to be considered non‑blank.

        try
        {
            // Load the PDF document.
            using (Document doc = new Document(inputPath))
            {
                // Iterate pages in reverse order so that deletions do not affect the index of remaining pages.
                for (int i = doc.Pages.Count; i >= 1; i--)
                {
                    Page page = doc.Pages[i];

                    // If the page is considered blank according to the tolerance, remove it.
                    if (page.IsBlank(tolerance))
                    {
                        doc.Pages.Delete(i);
                        Console.WriteLine($"Removed blank page {i} (tolerance={tolerance}).");
                    }
                }

                // Save the resulting PDF.
                doc.Save(outputPath);
                Console.WriteLine($"Trimmed PDF saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}