using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging; // for ImageFormat (fully qualified when needed)

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "catalog.pdf";
        const string tempImageDir   = "extracted_images";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure temporary folder exists
        Directory.CreateDirectory(tempImageDir);

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
                string imageFile = Path.Combine(tempImageDir, $"image_{imageIndex}.png");
                // Save each extracted image as PNG (fully qualified ImageFormat)
                extractor.GetNextImage(imageFile, System.Drawing.Imaging.ImageFormat.Png);
                imageIndex++;
            }
        }

        // -----------------------------------------------------------------
        // Step 2: Build a new PDF that contains thumbnail previews
        // -----------------------------------------------------------------
        using (Document catalogDoc = new Document())
        {
            // Get list of extracted image files
            string[] imageFiles = Directory.GetFiles(tempImageDir, "*.png");
            foreach (string imgPath in imageFiles)
            {
                // Add a new page for each thumbnail
                Page page = catalogDoc.Pages.Add();

                // Create an Aspose.Pdf.Image and point it to the extracted file
                Aspose.Pdf.Image pdfImg = new Aspose.Pdf.Image();
                pdfImg.File = imgPath;

                // Set fixed dimensions for the thumbnail (e.g., 150x150 points)
                pdfImg.FixWidth  = 150;
                pdfImg.FixHeight = 150;

                // Center the image on the page
                pdfImg.HorizontalAlignment = HorizontalAlignment.Center;
                pdfImg.VerticalAlignment   = VerticalAlignment.Center;

                // Add the image to the page
                page.Paragraphs.Add(pdfImg);
            }

            // Save the catalog PDF
            catalogDoc.Save(outputPdfPath);
        }

        // Optional: clean up temporary images
        try
        {
            Directory.Delete(tempImageDir, true);
        }
        catch
        {
            // If cleanup fails, ignore – images are in a temp folder.
        }

        Console.WriteLine($"Image catalog created at '{outputPdfPath}'.");
    }
}