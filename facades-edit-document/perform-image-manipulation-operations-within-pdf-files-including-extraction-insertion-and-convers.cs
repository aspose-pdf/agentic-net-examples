using System;
using System.IO;
using System.Drawing.Imaging; // For ImageFormat
using Aspose.Pdf.Facades;

class PdfImageManipulation
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath = "input.pdf";
        const string imageToInsertPath = "insert.png";
        const string extractedImagesDir = "ExtractedImages";
        const string insertedPdfPath = "output_inserted.pdf";
        const string convertedImagesDir = "ConvertedPages";

        // Validate input files
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(imageToInsertPath))
        {
            Console.Error.WriteLine($"Image to insert not found: {imageToInsertPath}");
            return;
        }

        // Ensure output directories exist
        Directory.CreateDirectory(extractedImagesDir);
        Directory.CreateDirectory(convertedImagesDir);

        // -------------------------------------------------
        // 1. Extract all images embedded in the PDF file
        // -------------------------------------------------
        try
        {
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(inputPdfPath);          // Load PDF
                extractor.ExtractImage();                // Prepare image extraction

                int imgIndex = 1;
                while (extractor.HasNextImage())
                {
                    string outImagePath = Path.Combine(
                        extractedImagesDir, $"image_{imgIndex}.png");
                    extractor.GetNextImage(outImagePath); // Save image as PNG
                    Console.WriteLine($"Extracted image: {outImagePath}");
                    imgIndex++;
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during image extraction: {ex.Message}");
        }

        // -------------------------------------------------
        // 2. Insert a new image onto the first page of the PDF
        // -------------------------------------------------
        try
        {
            using (PdfFileMend mend = new PdfFileMend())
            {
                mend.BindPdf(inputPdfPath); // Load PDF for modification

                // Add the image to page 1 at coordinates (50,50) – (200,200)
                using (FileStream imgStream = File.OpenRead(imageToInsertPath))
                {
                    bool added = mend.AddImage(
                        imgStream,          // image stream
                        1,                  // target page number (1‑based)
                        50f, 50f,           // lower‑left X,Y
                        200f, 200f);        // upper‑right X,Y

                    if (!added)
                        Console.Error.WriteLine("Failed to add image to the PDF.");
                }

                // Save the modified PDF
                mend.Save(insertedPdfPath);
                Console.WriteLine($"Image inserted PDF saved to: {insertedPdfPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during image insertion: {ex.Message}");
        }

        // -------------------------------------------------
        // 3. Convert each PDF page to an image (PNG)
        // -------------------------------------------------
        try
        {
            using (PdfConverter converter = new PdfConverter())
            {
                converter.BindPdf(inputPdfPath); // Load PDF for conversion
                converter.DoConvert();           // Initialize conversion

                int pageIndex = 1;
                while (converter.HasNextImage())
                {
                    string outPagePath = Path.Combine(
                        convertedImagesDir, $"page_{pageIndex}.png");
                    // Save current page as PNG
                    converter.GetNextImage(outPagePath, ImageFormat.Png);
                    Console.WriteLine($"Converted page {pageIndex} to image: {outPagePath}");
                    pageIndex++;
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during PDF‑to‑image conversion: {ex.Message}");
        }

        Console.WriteLine("All operations completed.");
    }
}