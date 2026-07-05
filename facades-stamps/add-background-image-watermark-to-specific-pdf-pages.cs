using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string watermarkImagePath = "watermark.png";

        // Verify required files exist
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(watermarkImagePath))
        {
            Console.Error.WriteLine($"Watermark image not found: {watermarkImagePath}");
            return;
        }

        // Initialize the PdfFileStamp facade and specify source/target files
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.InputFile = inputPath;
        fileStamp.OutputFile = outputPath;

        // Create a stamp object
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

        // Use an image as the watermark content
        stamp.BindImage(watermarkImagePath);

        // Ensure the stamp is placed behind existing page content
        stamp.IsBackground = true;

        // Apply the stamp only to pages 2 through 5 (1‑based indexing)
        stamp.Pages = new int[] { 2, 3, 4, 5 };

        // Add the configured stamp to the document
        fileStamp.AddStamp(stamp);

        // Persist changes and release resources
        fileStamp.Close();

        Console.WriteLine($"Background watermark applied to pages 2‑5 and saved as '{outputPath}'.");
    }
}