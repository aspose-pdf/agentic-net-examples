using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing; // needed for System.Drawing.Color used by FormattedText

class BatchWatermark
{
    static void Main(string[] args)
    {
        // Input folder containing PDFs – can be passed as first argument or set here
        string inputFolder = args.Length > 0 ? args[0] : @"C:\PdfInput";
        // Output folder for watermarked PDFs
        string outputFolder = Path.Combine(inputFolder, "Watermarked");
        Directory.CreateDirectory(outputFolder);

        // Multi‑line watermark text
        string watermarkText = "Confidential\nDo Not Distribute";

        // Iterate over all PDF files in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string outputPath = Path.Combine(
                outputFolder,
                Path.GetFileNameWithoutExtension(inputPath) + "_watermarked.pdf");

            // PdfFileStamp does NOT implement IDisposable – do NOT use a using block
            PdfFileStamp fileStamp = new PdfFileStamp();
            try
            {
                // Bind the source PDF
                fileStamp.BindPdf(inputPath);

                // Create a stamp that contains the multi‑line text
                Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

                // FormattedText constructor: (text, System.Drawing.Color, fontName, encoding, embedFont, fontSize)
                Aspose.Pdf.Facades.FormattedText ft = new Aspose.Pdf.Facades.FormattedText(
                    watermarkText,
                    System.Drawing.Color.Red,          // text color (System.Drawing.Color)
                    "Helvetica",                     // font name
                    Aspose.Pdf.Facades.EncodingType.Winansi,
                    false,                             // embed font
                    36f);                              // font size (float)

                // Bind the formatted text to the stamp
                stamp.BindLogo(ft);

                // Make the stamp appear as a background watermark
                stamp.IsBackground = true;
                stamp.Opacity = 0.5f; // 50 % opacity

                // Position the watermark (optional – here centered roughly)
                stamp.SetOrigin(100, 400);

                // Apply the stamp to all pages (Pages = null by default)
                fileStamp.AddStamp(stamp);

                // Save the watermarked PDF
                fileStamp.Save(outputPath);
            }
            finally
            {
                // Ensure resources are released
                fileStamp.Close();
            }

            Console.WriteLine($"Watermarked: {outputPath}");
        }
    }
}
