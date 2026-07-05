using System;
using System.IO;
using System.Drawing.Imaging;               // for ImageFormat
using Aspose.Pdf;                           // core PDF API
using Aspose.Pdf.Facades;                  // facades for extraction and editing

class Program
{
    static void Main()
    {
        const string inputPdfPath      = "input.pdf";          // source PDF
        const string watermarkImgPath  = "watermark.png";      // watermark image to overlay
        const string outputFolder      = "ExtractedImages";   // folder for watermarked images

        // Validate input files
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(watermarkImgPath))
        {
            Console.Error.WriteLine($"Watermark image not found: {watermarkImgPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // -----------------------------------------------------------------
        // Step 1: Add watermark to each page of the PDF and save a temp file
        // -----------------------------------------------------------------
        string tempWatermarkedPdf = Path.Combine(outputFolder, "temp_watermarked.pdf");

        // Determine page count using a lightweight Document instance
        int pageCount;
        using (Document srcDoc = new Document(inputPdfPath))
        {
            pageCount = srcDoc.Pages.Count;   // 1‑based page indexing
        }

        // Use PdfFileMend to place the watermark image on every page
        using (PdfFileMend mend = new PdfFileMend())
        {
            mend.BindPdf(inputPdfPath);

            // Position and size of the watermark on the page.
            // Adjust these values as needed (coordinates are in points).
            float lowerLeftX  = 50f;   // distance from left edge
            float lowerLeftY  = 50f;   // distance from bottom edge
            float upperRightX = 200f;  // width of watermark
            float upperRightY = 200f;  // height of watermark

            for (int pageNum = 1; pageNum <= pageCount; pageNum++)
            {
                // Add the same watermark image to each page.
                // The method returns a bool indicating success; ignore for brevity.
                mend.AddImage(watermarkImgPath, pageNum,
                              lowerLeftX, lowerLeftY,
                              upperRightX, upperRightY);
            }

            // Save the watermarked PDF to a temporary file.
            mend.Save(tempWatermarkedPdf);
        }

        // ---------------------------------------------------------------
        // Step 2: Extract images from the watermarked PDF using PdfExtractor
        // ---------------------------------------------------------------
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(tempWatermarkedPdf);
            extractor.ExtractImage();                     // Prepare extraction

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Build output file name (PNG format)
                string outImagePath = Path.Combine(outputFolder,
                    $"image-{imageIndex}.png");

                // Save the extracted image as PNG.
                // Overload with ImageFormat allows explicit format selection.
                extractor.GetNextImage(outImagePath, ImageFormat.Png);
                imageIndex++;
            }
        }

        // Optional: clean up the temporary watermarked PDF
        try { File.Delete(tempWatermarkedPdf); } catch { /* ignore cleanup errors */ }

        Console.WriteLine($"Extraction complete. Images saved to '{outputFolder}'.");
    }
}