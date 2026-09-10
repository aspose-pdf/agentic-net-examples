using System;
using System.IO;
using Aspose.Pdf;

class RotatePdfPage
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // ------------------------------------------------------------
        // Ensure the input PDF exists – create a minimal placeholder if it does not.
        // ------------------------------------------------------------
        if (!File.Exists(inputPath))
        {
            using (var placeholder = new Document())
            {
                // Add at least three pages so the demo can rotate page 3.
                placeholder.Pages.Add();
                placeholder.Pages.Add();
                placeholder.Pages.Add();
                placeholder.Save(inputPath);
            }
        }

        // Load the PDF from the file (could also be loaded from a byte array).
        using (var doc = new Document(inputPath))
        {
            // Verify that the document has at least three pages.
            if (doc.Pages.Count >= 3)
            {
                // Pages are 1‑based; get page 3.
                Page page3 = doc.Pages[3];

                // Rotate page 3 by 180 degrees.
                page3.Rotate = Rotation.on180;
            }
            else
            {
                Console.Error.WriteLine("The document contains fewer than three pages.");
                return;
            }

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page 3 rotated 180° and saved to '{outputPath}'.");
    }
}
