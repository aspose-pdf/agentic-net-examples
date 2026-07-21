using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // ---------------------------------------------------------------------
        // Ensure a source PDF exists. The sandbox is empty, so we create a minimal
        // PDF with at least three pages, save it, and then work on it.
        // ---------------------------------------------------------------------
        if (!File.Exists(inputPath))
        {
            using (Document seed = new Document())
            {
                // Add three blank pages (1‑based indexing).
                seed.Pages.Add();
                seed.Pages.Add();
                seed.Pages.Add();
                seed.Save(inputPath);
            }
        }

        // Load the PDF (you could also load from a byte array via MemoryStream).
        using (Document doc = new Document(inputPath))
        {
            // Verify that page 3 exists before rotating.
            if (doc.Pages.Count >= 3)
            {
                // Rotate page 3 by 180 degrees.
                doc.Pages[3].Rotate = Rotation.on180;
            }

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}
