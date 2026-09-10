using System;
using System.IO;
using Aspose.Pdf; // Core API namespace

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Document disposal must be handled with a using block (document-disposal-with-using rule)
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based (page-indexing-one-based rule)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Set rotation based on page number parity
                // Odd pages → 90° clockwise, Even pages → no rotation
                page.Rotate = (i % 2 == 1) ? Rotation.on90 : Rotation.None;
            }

            // Save the modified PDF (standard Save for PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}
