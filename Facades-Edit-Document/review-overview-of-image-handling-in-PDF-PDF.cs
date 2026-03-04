using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    // Entry point
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath   = "input.pdf";
        const string imagePath      = "picture.jpg";
        const string outputPdfPath  = "output_with_image.pdf";

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // ------------------------------------------------------------
        // 1. Add an image to an existing PDF using PdfFileMend (Facades API)
        // ------------------------------------------------------------
        // PdfFileMend does not implement IDisposable, so we manage its lifecycle manually.
        // BindPdf initializes the facade with the source document.
        // AddImage places the image on the specified page and rectangle.
        // Save writes the modified document to a new file.
        // Close releases internal resources.
        // ------------------------------------------------------------
        PdfFileMend mender = new PdfFileMend();
        try
        {
            // Bind the source PDF
            mender.BindPdf(inputPdfPath);

            // Define the rectangle where the image will be placed.
            // Coordinates are in points (1/72 inch). Example places a 100x100 pt image
            // at (50, 700) lower‑left corner.
            float lowerLeftX  = 50f;
            float lowerLeftY  = 700f;
            float upperRightX = 150f; // lowerLeftX + width
            float upperRightY = 800f; // lowerLeftY + height

            // Add the image to page 1
            bool added = mender.AddImage(imagePath, 1, lowerLeftX, lowerLeftY, upperRightX, upperRightY);
            if (!added)
            {
                Console.Error.WriteLine("Failed to add image to the PDF.");
                return;
            }

            // Save the modified PDF
            mender.Save(outputPdfPath);
            Console.WriteLine($"Image added successfully. Output saved to '{outputPdfPath}'.");
        }
        finally
        {
            // Ensure resources are released even if an exception occurs
            mender.Close();
        }

        // ------------------------------------------------------------
        // 2. Extract images from a PDF using PdfExtractor (Facades API)
        // ------------------------------------------------------------
        // This demonstrates the counterpart operation – retrieving images.
        // ------------------------------------------------------------
        const string extractionOutputDir = "ExtractedImages";
        Directory.CreateDirectory(extractionOutputDir);

        PdfExtractor extractor = new PdfExtractor();
        try
        {
            // Bind the PDF (same input or any other PDF)
            extractor.BindPdf(inputPdfPath);

            // Configure extraction (optional – set resolution, etc.)
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                string imageFile = Path.Combine(extractionOutputDir, $"image_{imageIndex}.jpg");
                extractor.GetNextImage(imageFile);
                Console.WriteLine($"Extracted image saved to: {imageFile}");
                imageIndex++;
            }

            if (imageIndex == 1)
                Console.WriteLine("No images were found in the PDF.");
        }
        finally
        {
            extractor.Close();
        }
    }
}