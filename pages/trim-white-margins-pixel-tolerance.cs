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

        // Default pixel‑tolerance expressed as a fill‑threshold factor (0..1).
        // Adjust this value per page if needed.
        const double defaultTolerance = 0.05;

        using (Document doc = new Document(inputPath))
        {
            // Iterate backwards because deleting pages shifts indices.
            for (int i = doc.Pages.Count; i >= 1; i--)
            {
                Page page = doc.Pages[i];

                // Use a custom tolerance; replace with per‑page logic if required.
                double tolerance = defaultTolerance;

                // Remove page if it is considered blank according to the tolerance.
                if (page.IsBlank(tolerance))
                {
                    doc.Pages.Delete(i);
                    continue;
                }

                // Trim white margins by setting the TrimBox.
                // Here we align TrimBox with the existing CropBox, which typically
                // excludes the outermost white space. For finer control you could
                // compute a tighter bounding box based on page contents.
                page.TrimBox = page.CropBox;
            }

            // Save the resulting PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}