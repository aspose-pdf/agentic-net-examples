using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // Added namespace for TextFragment

class ImageCatalogGenerator
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";          // source PDF containing images
        const string outputPdfPath  = "catalog.pdf";        // PDF catalog to be created

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Extract all images from the source PDF into a temporary folder
        // -----------------------------------------------------------------
        string tempImageFolder = Path.Combine(Path.GetTempPath(),
                                               "PdfImageExtract_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempImageFolder);

        int imageCounter = 1;
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdfPath);
            extractor.ExtractImage();

            while (extractor.HasNextImage())
            {
                string imageFile = Path.Combine(tempImageFolder,
                                                $"image_{imageCounter}.jpg"); // default JPEG format
                extractor.GetNextImage(imageFile); // saves next image to file
                imageCounter++;
            }
        }

        if (imageCounter == 1)
        {
            Console.WriteLine("No images were found in the source PDF.");
            Directory.Delete(tempImageFolder, true);
            return;
        }

        // ---------------------------------------------------------------
        // 2. Build a new PDF that shows each extracted image as a thumbnail
        // ---------------------------------------------------------------
        using (Document catalogDoc = new Document())
        {
            // Optional: set a title for the catalog PDF
            catalogDoc.Info.Title = "Image Catalog";

            string[] imageFiles = Directory.GetFiles(tempImageFolder, "*.jpg");
            Array.Sort(imageFiles); // ensure deterministic order

            foreach (string imgPath in imageFiles)
            {
                // Add a new page for each thumbnail
                Page page = catalogDoc.Pages.Add();

                // Add the image to the page
                Aspose.Pdf.Image img = new Aspose.Pdf.Image
                {
                    File = imgPath,
                    // Scale the image to a thumbnail size (e.g., 200x200 points)
                    FixWidth = 200,
                    FixHeight = 200
                };
                page.Paragraphs.Add(img);

                // Add a caption below the thumbnail (optional)
                TextFragment caption = new TextFragment(Path.GetFileName(imgPath))
                {
                    // Center the caption under the image
                    TextState = { FontSize = 12, ForegroundColor = Aspose.Pdf.Color.Black }
                };
                // Position the caption by adding a margin
                caption.Margin = new MarginInfo { Top = 210 };
                page.Paragraphs.Add(caption);
            }

            // Save the catalog PDF
            catalogDoc.Save(outputPdfPath);
        }

        // ---------------------------------------------------------------
        // 3. Clean up temporary images
        // ---------------------------------------------------------------
        try
        {
            Directory.Delete(tempImageFolder, true);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to delete temporary folder: {ex.Message}");
        }

        Console.WriteLine($"Image catalog created successfully at '{outputPdfPath}'.");
    }
}
