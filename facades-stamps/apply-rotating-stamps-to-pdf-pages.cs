using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // for EncodingType

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

        // Initialize the facade and bind the source PDF.
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPath);

        // Use a Document only to obtain the page count (required for the loop).
        int pageCount;
        using (Document doc = new Document(inputPath))
        {
            pageCount = doc.Pages.Count; // 1‑based indexing
        }

        // Apply a stamp to each page with a rotation based on the page index.
        for (int i = 1; i <= pageCount; i++)
        {
            // Create a stamp instance (fully qualified to avoid ambiguity).
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

            // Simple text content for the stamp.
            FormattedText ft = new FormattedText(
                $"Page {i}",
                System.Drawing.Color.Red,   // FormattedText expects System.Drawing.Color
                "Helvetica",
                EncodingType.Winansi,
                false,
                24);

            stamp.BindLogo(ft);               // Attach the text to the stamp.
            stamp.Rotation = (i * 30) % 360; // Example: rotate 30° per page.
            stamp.IsBackground = false;      // Stamp appears on top of page content.
            stamp.Pages = new int[] { i };   // Apply only to the current page.

            fileStamp.AddStamp(stamp);
        }

        // Save the result and release resources.
        fileStamp.Save(outputPath);
        fileStamp.Close();

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}