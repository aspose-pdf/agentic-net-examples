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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine the number of pages in the source PDF.
        int pageCount;
        using (Document srcDoc = new Document(inputPath))
        {
            pageCount = srcDoc.Pages.Count; // 1‑based page count
        }

        // Initialize the facade for stamping.
        PdfFileStamp fileStamp = new PdfFileStamp(inputPath, outputPath);

        // Apply a stamp to each page with a rotation based on its index.
        for (int pageIndex = 1; pageIndex <= pageCount; pageIndex++)
        {
            // Create a new stamp instance.
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

            // Use simple text as the stamp content.
            stamp.BindLogo(new FormattedText($"Page {pageIndex}"));

            // Example rotation: 30° per page, wrapped within 0‑360°.
            stamp.Rotation = (pageIndex * 30) % 360;

            // Restrict the stamp to the current page only.
            stamp.Pages = new int[] { pageIndex };

            // Add the configured stamp to the document.
            fileStamp.AddStamp(stamp);
        }

        // Finalize and save the stamped PDF.
        fileStamp.Close();

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}