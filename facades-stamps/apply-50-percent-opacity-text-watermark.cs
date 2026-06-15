using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf;
using System.Drawing; // needed for System.Drawing.Color

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "watermarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the PdfFileStamp facade and bind the source PDF
        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            fileStamp.BindPdf(inputPath);

            // Create a text stamp (watermark)
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

            // Define the watermark text and style
            Aspose.Pdf.Facades.FormattedText ft = new Aspose.Pdf.Facades.FormattedText(
                "CONFIDENTIAL",                     // text
                System.Drawing.Color.LightGray,     // text color
                "Helvetica",                        // font name
                Aspose.Pdf.Facades.EncodingType.Winansi,
                false,                              // embed font?
                48);                                // font size

            // Bind the formatted text to the stamp
            stamp.BindLogo(ft);

            // Position the stamp (example coordinates)
            stamp.SetOrigin(100, 400);

            // Set opacity to 50% (0.5)
            stamp.Opacity = 0.5f;

            // Make the stamp appear behind page content (optional)
            stamp.IsBackground = true;

            // Add the stamp to all pages (Pages = null by default)
            fileStamp.AddStamp(stamp);

            // Save the watermarked PDF
            fileStamp.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}