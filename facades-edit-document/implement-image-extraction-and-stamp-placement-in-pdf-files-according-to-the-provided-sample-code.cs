using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "stamped_output.pdf";
        const string stampImagePath = "stamp.png";
        const string extractedImagesFolder = "ExtractedImages";

        // Ensure the input files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Extract all images from the PDF and save them to a folder
        // -----------------------------------------------------------------
        Directory.CreateDirectory(extractedImagesFolder);

        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF
            extractor.BindPdf(inputPdfPath);

            // Extract images from the document
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Save each image as a separate file (PNG format is used by default)
                string imagePath = Path.Combine(extractedImagesFolder, $"image_{imageIndex}.png");
                extractor.GetNextImage(imagePath);
                Console.WriteLine($"Extracted image saved to: {imagePath}");
                imageIndex++;
            }
        }

        // -----------------------------------------------------------------
        // 2. Add an image stamp to each page of the PDF
        // -----------------------------------------------------------------
        // Load the PDF document first (PdfFileStamp constructor expects a Document instance)
        Document pdfDoc = new Document(inputPdfPath);

        // Use PdfFileStamp bound to the loaded Document
        using (PdfFileStamp fileStamp = new PdfFileStamp(pdfDoc))
        {
            // Create the stamp (Aspose.Pdf.Facades.Stamp) and configure it
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
            stamp.BindImage(stampImagePath);          // Set the image to be used as stamp
            stamp.SetOrigin(100, 500);                // Position (lower‑left corner) of the stamp
            stamp.SetImageSize(100, 100);             // Width and height of the stamp
            stamp.Opacity = 0.5f;                     // Semi‑transparent
            stamp.IsBackground = false;               // Place stamp on top of page content

            // Apply the stamp to all pages
            fileStamp.AddStamp(stamp);

            // Save the stamped document to the desired output path
            fileStamp.Save(outputPdfPath);
        }

        Console.WriteLine($"Stamped PDF saved to: {outputPdfPath}");
    }
}
