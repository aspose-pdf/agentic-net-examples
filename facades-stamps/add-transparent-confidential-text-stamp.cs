using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing; // Required for System.Drawing.Color

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "confidential_output.pdf";

        // Pages on which the stamp should appear (1‑based indexing)
        int[] selectedPages = new int[] { 1, 3, 5 };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create formatted text for the stamp using the Facades overload that expects
        // System.Drawing.Color and a float font size.
        Aspose.Pdf.Facades.FormattedText ft = new Aspose.Pdf.Facades.FormattedText(
            "CONFIDENTIAL",
            System.Drawing.Color.Red,               // System.Drawing.Color required
            "Helvetica",
            Aspose.Pdf.Facades.EncodingType.Winansi,
            false,
            48f);                                   // float font size

        // Fully qualify Stamp to avoid ambiguity with other Stamp classes
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindLogo(ft);          // Bind the text to the stamp
        stamp.Opacity = 0.7f;        // 70% opacity (0 = fully transparent, 1 = opaque)
        stamp.IsBackground = true;  // Place stamp behind page content
        stamp.Pages = selectedPages; // Apply only to selected pages

        // Optional positioning and size (adjust as needed)
        stamp.SetOrigin(100, 400);   // X, Y coordinates (from bottom‑left)
        stamp.SetImageSize(300, 100); // Width, Height of the text bounding box

        // Use PdfFileStamp facade to apply the stamp
        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            fileStamp.BindPdf(inputPath); // Load source PDF
            fileStamp.AddStamp(stamp);    // Add the configured stamp
            fileStamp.Save(outputPath);   // Save the stamped PDF
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}
