using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class ImageCatalogGenerator
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "catalog.pdf";
        const string tempFolder = "extracted_images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure temporary folder exists
        Directory.CreateDirectory(tempFolder);

        // -----------------------------------------------------------------
        // Step 1: Extract all images from the source PDF using PdfExtractor
        // -----------------------------------------------------------------
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                string imagePath = Path.Combine(tempFolder, $"image_{imageIndex}.jpg");
                extractor.GetNextImage(imagePath);
                imageIndex++;
            }
        }

        // -----------------------------------------------------------------
        // Step 2: Create a new PDF document that will serve as the catalog
        // -----------------------------------------------------------------
        using (Document catalog = new Document())
        {
            // Get list of extracted image files
            string[] imageFiles = Directory.GetFiles(tempFolder);
            foreach (string imgFile in imageFiles)
            {
                // Add a new page for each thumbnail
                Page page = catalog.Pages.Add();

                // Create an Image object and point it to the extracted file
                Aspose.Pdf.Image pdfImage = new Aspose.Pdf.Image
                {
                    File = imgFile
                };

                // Scale the image to a thumbnail size (e.g., 150x150 points)
                // Preserve aspect ratio by setting only one dimension if desired
                pdfImage.FixWidth = 150;
                pdfImage.FixHeight = 150;

                // Center the thumbnail on the page
                pdfImage.HorizontalAlignment = HorizontalAlignment.Center;
                pdfImage.VerticalAlignment   = VerticalAlignment.Center;

                // Add the image to the page's paragraph collection
                page.Paragraphs.Add(pdfImage);
            }

            // Save the catalog PDF
            catalog.Save(outputPdf);
        }

        // Optional: clean up extracted images
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore cleanup errors
        }

        Console.WriteLine($"Image catalog created: {outputPdf}");
    }
}