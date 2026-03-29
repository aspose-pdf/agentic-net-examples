using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "catalog.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create a temporary directory for extracted images
        string tempDir = Path.Combine(Path.GetTempPath(), "pdf_images_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Extract images from the source PDF
        PdfExtractor extractor = new PdfExtractor();
        extractor.BindPdf(inputPath);
        extractor.ExtractImage();
        int imageIndex = 1;
        while (extractor.HasNextImage())
        {
            string imageFile = Path.Combine(tempDir, "image-" + imageIndex + ".png");
            extractor.GetNextImage(imageFile);
            imageIndex++;
        }

        // Build a catalog PDF with thumbnail previews
        using (Document catalogDoc = new Document())
        {
            string[] imageFiles = Directory.GetFiles(tempDir);
            foreach (string imgPath in imageFiles)
            {
                // Add a new page for each image
                Page catalogPage = catalogDoc.Pages.Add();

                // Create an Image object and set a fixed thumbnail width
                Aspose.Pdf.Image pdfImage = new Aspose.Pdf.Image();
                pdfImage.File = imgPath;
                pdfImage.FixWidth = 200f; // thumbnail width in points

                // Place the image on the page
                catalogPage.Paragraphs.Add(pdfImage);
            }

            // Save the catalog PDF
            catalogDoc.Save(outputPath);
        }

        // Clean up temporary files
        try
        {
            Directory.Delete(tempDir, true);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to delete temporary directory: {ex.Message}");
        }

        Console.WriteLine($"Image catalog saved to '{outputPath}'.");
    }
}