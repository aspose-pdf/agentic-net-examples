using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPdfPath      = "input.pdf";          // source PDF
        const string watermarkImagePath = "watermark.png";      // watermark image
        const string outputDir         = "ExtractedImages";    // folder for images

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

        Directory.CreateDirectory(outputDir);

        // -----------------------------------------------------------------
        // Step 1: Add watermark image to every page of the PDF.
        // -----------------------------------------------------------------
        string tempWatermarkedPdf = Path.Combine(Path.GetTempPath(), $"watermarked_{Guid.NewGuid()}.pdf");

        // Determine total page count.
        int pageCount;
        using (Document doc = new Document(inputPdfPath))
        {
            pageCount = doc.Pages.Count;
        }

        // Prepare an array with all page numbers (1‑based indexing).
        int[] allPages = Enumerable.Range(1, pageCount).ToArray();

        // Add the watermark using PdfFileMend.
        using (PdfFileMend mend = new PdfFileMend())
        {
            mend.BindPdf(inputPdfPath); // load source PDF

            // Open the watermark image as a stream.
            using (FileStream wmStream = File.OpenRead(watermarkImagePath))
            {
                // Position the watermark at the lower‑left corner (adjust as needed).
                // lowerLeftX, lowerLeftY, upperRightX, upperRightY are in PDF points.
                mend.AddImage(wmStream, allPages, 10f, 10f, 110f, 110f);
            }

            // Save the watermarked PDF to a temporary file.
            mend.Save(tempWatermarkedPdf);
            mend.Close();
        }

        // -----------------------------------------------------------------
        // Step 2: Extract images from the watermarked PDF.
        // -----------------------------------------------------------------
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(tempWatermarkedPdf);
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                string outputImagePath = Path.Combine(outputDir, $"image-{imageIndex}.png");
                // Save each extracted image as PNG.
                extractor.GetNextImage(outputImagePath, ImageFormat.Png);
                imageIndex++;
            }
        }

        // Clean up the temporary watermarked PDF.
        try { File.Delete(tempWatermarkedPdf); } catch { /* ignore cleanup errors */ }

        Console.WriteLine($"Extraction complete. Images saved to '{outputDir}'.");
    }
}