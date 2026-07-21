using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath      = "input.pdf";          // source PDF
        const string watermarkImagePath = "watermark.png";      // image to use as watermark
        const string outputFolder      = "ExtractedImages";    // folder for extracted images

        // Validate inputs
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(watermarkImagePath))
        {
            Console.Error.WriteLine($"Watermark image not found: {watermarkImagePath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // -----------------------------------------------------------------
        // Step 1: Add the watermark image to each page of the PDF.
        // -----------------------------------------------------------------
        // PdfFileStamp does NOT implement IDisposable, so we do not wrap it in a using block.
        PdfFileStamp pdfStamp = new PdfFileStamp();
        pdfStamp.BindPdf(inputPdfPath);                     // load source PDF

        // Add the watermark image as a header (you can also use AddFooter or AddStamp for different positioning).
        // The second parameter is the Y‑coordinate; 0 places it at the top of the page.
        pdfStamp.AddHeader(watermarkImagePath, 0);

        // Save the watermarked PDF to a temporary file.
        string tempWatermarkedPdf = Path.Combine(Path.GetTempPath(), $"watermarked_{Guid.NewGuid()}.pdf");
        pdfStamp.Save(tempWatermarkedPdf);
        pdfStamp.Close(); // release internal resources

        // -----------------------------------------------------------------
        // Step 2: Extract images from the watermarked PDF.
        // -----------------------------------------------------------------
        PdfExtractor extractor = new PdfExtractor();
        extractor.BindPdf(tempWatermarkedPdf);   // work on the watermarked PDF
        extractor.ExtractImage();                // prepare image extraction

        int imageIndex = 1;
        while (extractor.HasNextImage())
        {
            // Build output file name (default format is JPEG).
            string outputImagePath = Path.Combine(outputFolder, $"image-{imageIndex}.jpg");

            // Save the next extracted image.
            extractor.GetNextImage(outputImagePath);

            Console.WriteLine($"Extracted image saved to: {outputImagePath}");
            imageIndex++;
        }

        // Clean up temporary watermarked PDF.
        try { File.Delete(tempWatermarkedPdf); } catch { /* ignore cleanup errors */ }

        Console.WriteLine("Image extraction completed.");
    }
}