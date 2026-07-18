using System;
using System.IO;
using Aspose.Pdf.Facades; // PdfFileStamp, Stamp, FormattedText, EncodingType

class Program
{
    static void Main(string[] args)
    {
        // Input folder (first argument) and output folder (second argument) – defaults if not supplied
        string inputFolder  = args.Length > 0 ? args[0] : "InputPdfs";
        string outputFolder = args.Length > 1 ? args[1] : "WatermarkedPdfs";

        // Ensure both directories exist (create if missing)
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder '{inputFolder}' does not exist. Creating it…");
            Directory.CreateDirectory(inputFolder);
            Console.WriteLine("Place PDF files into the input folder and re‑run the program.");
            return; // nothing to process yet
        }
        Directory.CreateDirectory(outputFolder);

        // Process every PDF file in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine($"No PDF files found in '{inputFolder}'." );
            return;
        }

        foreach (string sourcePath in pdfFiles)
        {
            string fileName = Path.GetFileNameWithoutExtension(sourcePath);
            string destPath = Path.Combine(outputFolder, fileName + "_watermarked.pdf");

            AddMultiLineWatermark(sourcePath, destPath);
        }

        Console.WriteLine("Batch watermarking completed.");
    }

    // Adds the same multi‑line text watermark to all pages of a PDF file
    static void AddMultiLineWatermark(string sourcePdf, string outputPdf)
    {
        // Create multi‑line formatted text (uses System.Drawing.Color as required by FormattedText)
        FormattedText watermarkText = new FormattedText(
            "CONFIDENTIAL\nDo Not Distribute", // two lines separated by newline
            System.Drawing.Color.Red,            // text color
            "Helvetica",                         // font name
            EncodingType.Winansi,                // encoding
            false,                               // embed font flag
            36);                                 // font size

        // Configure the stamp that will carry the formatted text
        Stamp stamp = new Stamp();
        stamp.BindLogo(watermarkText);   // attach the text to the stamp
        stamp.IsBackground = true;      // render behind page content
        stamp.Opacity = 0.5f;            // semi‑transparent
        stamp.SetOrigin(100, 400);       // position of the watermark on each page
        stamp.Pages = null;              // apply to all pages (null means every page)

        // Use PdfFileStamp facade to apply the stamp
        PdfFileStamp fileStamp = new PdfFileStamp();
        try
        {
            fileStamp.BindPdf(sourcePdf);   // load source PDF
            fileStamp.AddStamp(stamp);      // add the configured stamp
            fileStamp.Save(outputPdf);      // write the watermarked PDF
        }
        finally
        {
            // Close releases resources; PdfFileStamp does not implement IDisposable
            fileStamp.Close();
        }
    }
}
