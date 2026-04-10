using System;
using System.IO;
using System.Drawing;                     // For System.Drawing.Color
using Aspose.Pdf.Facades;               // Facade classes for stamping and text addition

class BatchWatermark
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = @"C:\PdfInput";
        // Folder where watermarked PDFs will be saved
        const string outputFolder = @"C:\PdfOutput";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Define the multi‑line watermark text
        string watermarkText = "CONFIDENTIAL\nDo Not Distribute\nCompany XYZ";

        // Create a FormattedText object using Facades types and System.Drawing.Color
        var formattedWatermark = new Aspose.Pdf.Facades.FormattedText(
            watermarkText,                     // Text (lines separated by '\n')
            System.Drawing.Color.Gray,        // Text color (System.Drawing.Color)
            "Arial",                         // Font name
            Aspose.Pdf.Facades.EncodingType.Winansi, // Encoding
            false,                            // IsEmbedded (false = use system font)
            48f);                             // Font size (float)

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            // Use PdfFileMend to add the formatted text as a watermark
            using (var mend = new PdfFileMend())
            {
                mend.BindPdf(inputPath);

                // Add the watermark to all pages. Passing null for the pages array applies to every page.
                // The rectangle (llx, lly, urx, ury) defines where the text will be placed.
                mend.AddText(formattedWatermark, null, 100f, 400f, 500f, 800f);

                // Save the watermarked PDF
                mend.Save(outputPath);
            }

            Console.WriteLine($"Watermarked: {fileName} → {outputPath}");
        }

        Console.WriteLine("Batch watermarking completed.");
    }
}
