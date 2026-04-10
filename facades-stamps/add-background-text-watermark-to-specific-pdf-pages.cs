using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string watermarkText = "CONFIDENTIAL";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the PdfFileStamp facade and bind the source PDF.
        Aspose.Pdf.Facades.PdfFileStamp fileStamp = new Aspose.Pdf.Facades.PdfFileStamp();
        fileStamp.BindPdf(inputPath);

        // Create formatted text for the watermark.
        Aspose.Pdf.Facades.FormattedText formattedText = new Aspose.Pdf.Facades.FormattedText(
            watermarkText,
            System.Drawing.Color.FromArgb(128, 200, 200, 200), // semi‑transparent gray
            "Helvetica",
            Aspose.Pdf.Facades.EncodingType.Winansi,
            false,
            48); // font size

        // Configure the stamp.
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindLogo(formattedText);          // use text as stamp content
        stamp.IsBackground = true;              // place behind existing page content
        stamp.Pages = new int[] { 2, 3, 4, 5 };  // apply only to pages 2‑5
        stamp.SetOrigin(100f, 400f);            // position (X, Y) from bottom‑left
        stamp.SetImageSize(200f, 100f);         // size of the stamp area
        stamp.Opacity = 0.5f;                   // make the stamp semi‑transparent

        // Add the stamp to the document and save.
        fileStamp.AddStamp(stamp);
        fileStamp.Save(outputPath);
        fileStamp.Close();

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}