using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "trimmed_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Default pixel‑tolerance expressed as a fill‑threshold factor (0..1).
        const double defaultTolerance = 0.05; // 5 % of page area.

        using (Document doc = new Document(inputPath))
        {
            // Iterate backwards because removing pages shifts indices.
            for (int i = doc.Pages.Count; i >= 1; i--)
            {
                Page page = doc.Pages[i];
                double tolerance = GetToleranceForPage(i, defaultTolerance);

                // If the page is considered blank within the tolerance, delete it.
                if (page.IsBlank(tolerance))
                {
                    doc.Pages.Delete(i);
                    Console.WriteLine($"Removed blank page {i} (tolerance {tolerance}).");
                }
                else
                {
                    // Example of trimming whitespace: set TrimBox to the current CropBox.
                    // In a real scenario you would calculate the content bounds and assign
                    // a tighter rectangle here.
                    page.TrimBox = page.CropBox;
                }
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }

    // Determines the tolerance for a given page.
    // Replace with custom logic (e.g., per‑page configuration) as needed.
    static double GetToleranceForPage(int pageNumber, double defaultTolerance)
    {
        return defaultTolerance;
    }
}