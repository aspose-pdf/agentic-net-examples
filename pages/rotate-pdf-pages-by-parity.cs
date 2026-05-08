using System;
using System.IO;
using Aspose.Pdf; // Document, Page, Rotation

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using rule for loading)
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based (rule: page-indexing-one-based)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Set rotation based on page number parity
                if (i % 2 == 0) // even pages
                {
                    page.Rotate = Rotation.None;            // no rotation
                }
                else // odd pages
                {
                    page.Rotate = Rotation.on90;            // rotate 90°
                }
            }

            // Save the modified PDF (using rule for saving)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}