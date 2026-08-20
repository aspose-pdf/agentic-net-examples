using System;
using System.IO;
using System.Drawing.Imaging; // for ImageFormat
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class ImageCatalogGenerator
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "catalog.pdf";
        const string tempFolderPath = "extracted_images";

        // Validate input
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure temporary folder exists
        Directory.CreateDirectory(tempFolderPath);

        // -----------------------------------------------------------------
        // Step 1: Extract all images from the source PDF using PdfExtractor
        // -----------------------------------------------------------------
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdfPath);
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Save each image as PNG (you can choose other formats)
                string imageFile = Path.Combine(tempFolderPath, $"image_{imageIndex}.png");
                extractor.GetNextImage(imageFile, ImageFormat.Png);
                imageIndex++;
            }
        }

        // -----------------------------------------------------------------
        // Step 2: Build a new PDF catalog with thumbnail previews
        // -----------------------------------------------------------------
        using (Document catalogDoc = new Document())
        {
            // Get list of extracted image files
            string[] imageFiles = Directory.GetFiles(tempFolderPath, "*.png");

            foreach (string imgPath in imageFiles)
            {
                // Add a new page for each thumbnail
                Page page = catalogDoc.Pages.Add();

                // Create an Aspose.Pdf.Image and point it to the extracted file
                Aspose.Pdf.Image pdfImage = new Aspose.Pdf.Image
                {
                    File = imgPath,
                    // Set a reasonable thumbnail size (e.g., 200x200 points)
                    FixWidth = 200,
                    FixHeight = 200,
                    // Center the image on the page
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Center
                };

                // Add the image to the page's content
                page.Paragraphs.Add(pdfImage);
            }

            // Save the catalog PDF
            catalogDoc.Save(outputPdfPath);
        }

        // -----------------------------------------------------------------
        // Optional: Clean up temporary extracted images
        // -----------------------------------------------------------------
        try
        {
            Directory.Delete(tempFolderPath, recursive: true);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to delete temporary folder: {ex.Message}");
        }

        Console.WriteLine($"Image catalog created at '{outputPdfPath}'.");
    }
}