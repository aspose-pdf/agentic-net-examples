using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string templatePath      = "template.pdf";   // PDF form template
        const string filledPath        = "filled.pdf";     // PDF after AutoFiller processing
        const string watermarkedPath   = "watermarked.pdf";// Final PDF with watermark
        const string watermarkImagePath = "watermark.png"; // Image used as watermark

        // Verify required files exist
        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template not found: {templatePath}");
            return;
        }
        if (!File.Exists(watermarkImagePath))
        {
            Console.Error.WriteLine($"Watermark image not found: {watermarkImagePath}");
            return;
        }

        // ------------------------------------------------------------
        // 1. Fill the PDF form using AutoFiller
        // ------------------------------------------------------------
        Aspose.Pdf.Facades.AutoFiller autoFiller = new Aspose.Pdf.Facades.AutoFiller();
        autoFiller.InputFileName = templatePath;   // bind the template
        // No data import in this minimal example; simply save the (unchanged) PDF
        autoFiller.Save(filledPath);               // creates filled.pdf

        // ------------------------------------------------------------
        // 2. Add a watermark to every page of the filled PDF
        // ------------------------------------------------------------
        using (Aspose.Pdf.Facades.PdfFileStamp fileStamp = new Aspose.Pdf.Facades.PdfFileStamp())
        {
            // Bind the PDF that will receive the watermark
            fileStamp.BindPdf(filledPath);

            // Create a stamp that uses an image as the watermark
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
            stamp.BindImage(watermarkImagePath);   // image to be used as watermark
            stamp.IsBackground = true;            // place behind page content
            stamp.Opacity = 0.5f;                  // semi‑transparent

            // Apply the stamp to all pages (default behavior)
            fileStamp.AddStamp(stamp);

            // Save the result
            fileStamp.Save(watermarkedPath);
            fileStamp.Close();
        }

        Console.WriteLine($"Watermarked PDF saved to '{watermarkedPath}'.");
    }
}