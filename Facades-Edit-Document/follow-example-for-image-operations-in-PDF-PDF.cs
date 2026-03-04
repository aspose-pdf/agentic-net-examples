using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class ImageOperationsExample
{
    static void Main()
    {
        // Paths for the source PDF, the image to add, and the resulting PDF.
        const string sourcePdfPath   = "source.pdf";
        const string imagePath       = "picture.jpg";
        const string outputPdfPath   = "output_with_image.pdf";

        // Verify that the required files exist.
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // ------------------------------------------------------------
        // Add an image to the first page of the PDF using PdfFileMend.
        // ------------------------------------------------------------
        // PdfFileMend does not implement IDisposable, so we do not wrap it
        // in a using block. We must call Close() after finishing.
        PdfFileMend mender = new PdfFileMend();

        // Bind the source PDF file.
        mender.BindPdf(sourcePdfPath);

        // Open the image as a stream and add it to page 1.
        // Coordinates are in points (1/72 inch). Adjust as needed.
        using (FileStream imgStream = File.OpenRead(imagePath))
        {
            // lowerLeftX, lowerLeftY, upperRightX, upperRightY
            bool added = mender.AddImage(imgStream, 1, 50f, 50f, 200f, 200f);
            if (!added)
            {
                Console.Error.WriteLine("Failed to add the image to the PDF.");
                mender.Close();
                return;
            }
        }

        // Save the modified PDF to the output path.
        mender.Save(outputPdfPath);
        mender.Close();

        Console.WriteLine($"Image added successfully. Output saved to '{outputPdfPath}'.");

        // ------------------------------------------------------------
        // Extract all images from the newly created PDF using PdfExtractor.
        // ------------------------------------------------------------
        const string extractionFolder = "ExtractedImages";

        // Ensure the extraction folder exists.
        Directory.CreateDirectory(extractionFolder);

        PdfExtractor extractor = new PdfExtractor();
        extractor.BindPdf(outputPdfPath);
        extractor.ExtractImage();

        int imageIndex = 1;
        while (extractor.HasNextImage())
        {
            string extractedImagePath = Path.Combine(extractionFolder, $"image_{imageIndex}.png");
            extractor.GetNextImage(extractedImagePath);
            Console.WriteLine($"Extracted image saved to '{extractedImagePath}'.");
            imageIndex++;
        }

        extractor.Close();
    }
}