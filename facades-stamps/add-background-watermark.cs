using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the PdfFileStamp facade and bind the source PDF
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPath);

        // Create a text stamp (watermark) using FormattedText (Facades version)
        // Fully qualify ambiguous Facades types to avoid CS0104.
        Aspose.Pdf.Facades.FormattedText formatted = new Aspose.Pdf.Facades.FormattedText(
            "CONFIDENTIAL",
            System.Drawing.Color.Gray,
            "Helvetica",
            Aspose.Pdf.Facades.EncodingType.Winansi,
            false,
            48f);

        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindLogo(formatted);
        stamp.IsBackground = true; // place behind existing content
        stamp.Pages = new int[] { 2, 3, 4, 5 }; // apply to pages 2‑5

        // Add the stamp to the document
        fileStamp.AddStamp(stamp);

        // Save the result and close the facade
        fileStamp.Save(outputPath);
        fileStamp.Close();

        Console.WriteLine($"Background watermark applied to pages 2‑5 and saved as '{outputPath}'.");
    }
}
