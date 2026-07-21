using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF and watermark image paths
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string watermarkImage = "watermark.png";

        // Verify files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(watermarkImage))
        {
            Console.Error.WriteLine($"Watermark image not found: {watermarkImage}");
            return;
        }

        // Initialize PdfFileStamp facade and bind the source PDF
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPdf);               // Load source PDF

        // Create a stamp that will be used as a background watermark
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

        // Bind an image to the stamp (you could also use BindLogo for text)
        stamp.BindImage(watermarkImage);

        // Make the stamp appear behind existing page content
        stamp.IsBackground = true;

        // Apply the stamp only to pages 2 through 5 (1‑based indexing)
        stamp.Pages = new int[] { 2, 3, 4, 5 };

        // Add the configured stamp to the PDF
        fileStamp.AddStamp(stamp);

        // Save the result to the output file
        fileStamp.Save(outputPdf);

        // Close the facade (releases resources)
        fileStamp.Close();

        Console.WriteLine($"Background watermark applied to pages 2‑5 and saved as '{outputPdf}'.");
    }
}