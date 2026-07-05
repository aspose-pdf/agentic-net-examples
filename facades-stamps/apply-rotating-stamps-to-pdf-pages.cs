using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "stamped_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfFileStamp can be initialized with input and output file paths.
        using (PdfFileStamp fileStamp = new PdfFileStamp(inputPath, outputPath))
        {
            // Load the document only to obtain the page count.
            using (Document doc = new Document(inputPath))
            {
                int pageCount = doc.Pages.Count;

                for (int i = 1; i <= pageCount; i++)
                {
                    // Example rotation: 30° per page index, wrapped to 0‑360.
                    float rotation = ((i - 1) * 30) % 360;

                    // Create a simple text stamp.
                    Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
                    stamp.BindLogo(new FormattedText($"Page {i}"));
                    stamp.Rotation = rotation;          // Set arbitrary rotation.
                    stamp.Pages = new int[] { i };      // Apply only to the current page.

                    fileStamp.AddStamp(stamp);
                }
            }

            // Close() writes the output file (no separate Save call needed).
            fileStamp.Close();
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}