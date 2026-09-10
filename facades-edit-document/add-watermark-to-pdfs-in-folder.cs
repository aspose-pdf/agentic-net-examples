using System;
using System.IO;
using Aspose.Pdf.Facades;          // Facade classes: PdfFileStamp, Stamp
using Aspose.Pdf.Text;            // FormattedText, EncodingType
using System.Drawing;             // System.Drawing.Color for FormattedText

class Program
{
    static void Main()
    {
        // Configuration settings (could be read from a config file)
        string inputFolder   = @"C:\PdfFolder\Input";
        string outputFolder  = @"C:\PdfFolder\Output";
        string watermarkText = "CONFIDENTIAL";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName   = Path.GetFileName(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            // Initialize the PdfFileStamp facade and bind the source PDF
            PdfFileStamp fileStamp = new PdfFileStamp();
            fileStamp.BindPdf(inputPath);

            // Create a Stamp object that will act as the watermark
            Stamp stamp = new Stamp();

            // Configure the stamp appearance
            stamp.IsBackground = true;          // Place behind page content
            stamp.Opacity      = 0.5f;          // Semi‑transparent

            // Create formatted text for the watermark
            FormattedText ft = new FormattedText(
                watermarkText,                  // Text to display
                Color.Gray,                     // Text color (System.Drawing.Color)
                "Arial",                        // Font family
                EncodingType.Winansi,           // Encoding
                false,                          // Not embedded
                48);                            // Font size

            // Bind the formatted text to the stamp
            stamp.BindLogo(ft);

            // Add the stamp to the PDF
            fileStamp.AddStamp(stamp);

            // Save the watermarked PDF to the output path
            fileStamp.Save(outputPath);

            // Close the facade (PdfFileStamp does not implement IDisposable)
            fileStamp.Close();
        }

        Console.WriteLine("Watermarking completed.");
    }
}