using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "trimmed_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Custom tolerance (pixel‑based) for each page.
        // Values are in the range [0..1). Smaller values = stricter blank‑page detection.
        // Here we use the same tolerance for all pages; adjust per page as needed.
        double tolerancePerPage = 0.02; // 2 % fill threshold

        using (Document doc = new Document(inputPath))
        {
            // Iterate backwards so that removing a page does not affect the loop index.
            for (int i = doc.Pages.Count; i >= 1; i--)
            {
                Page page = doc.Pages[i];

                // Determine if the page is considered blank according to the tolerance.
                if (page.IsBlank(tolerancePerPage))
                {
                    // Remove the blank page.
                    doc.Pages.Delete(i);
                    Console.WriteLine($"Removed blank page {i} (tolerance={tolerancePerPage}).");
                }
                else
                {
                    // Optionally, adjust the TrimBox to remove surrounding white space.
                    // Here we simply set it to the page’s CropBox (as an example).
                    // More sophisticated calculations can be performed if needed.
                    page.TrimBox = page.CropBox;
                }
            }

            // Save the resulting PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processing complete. Output saved to '{outputPath}'.");
    }
}