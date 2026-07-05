using System;
using System.IO;
using System.Drawing;                     // Required for FormattedText color
using Aspose.Pdf.Facades;                // Facade classes for stamping

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
        string watermarkText = "Confidential\nDo Not Distribute";

        // Create a FormattedText object (uses System.Drawing.Color)
        FormattedText formatted = new FormattedText(
            watermarkText,                     // Text (newlines create multiple lines)
            Color.Red,                         // Text color
            "Helvetica",                       // Font name
            EncodingType.Winansi,              // Encoding
            false,                             // Do not embed the font
            36);                               // Font size

        // Iterate over all PDF files in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string outputPath = Path.Combine(outputFolder, Path.GetFileName(inputPath));

            // Use PdfFileStamp facade to add the watermark
            using (PdfFileStamp fileStamp = new PdfFileStamp())
            {
                // Bind the source PDF
                fileStamp.BindPdf(inputPath);

                // Create a stamp and bind the formatted text
                Stamp stamp = new Stamp();
                stamp.BindLogo(formatted);          // Attach the multi‑line text
                stamp.SetOrigin(100, 400);          // Position of the watermark
                stamp.Opacity = 0.5f;               // Semi‑transparent
                stamp.IsBackground = true;          // Render behind page content

                // Add the stamp to the document (applies to all pages by default)
                fileStamp.AddStamp(stamp);

                // Save the watermarked PDF
                fileStamp.Save(outputPath);
                fileStamp.Close();                  // Explicit close (optional, handled by using)
            }

            Console.WriteLine($"Watermarked: {outputPath}");
        }
    }
}