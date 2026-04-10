using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // Added for TextFragment and MarginInfo

class ImageCatalogGenerator
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputCatalogPath = "catalog.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // -----------------------------------------------------------------
        // Step 1: Extract all images from the source PDF into memory streams
        // -----------------------------------------------------------------
        List<MemoryStream> extractedImages = new List<MemoryStream>();
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdfPath);
            extractor.ExtractImage();

            while (extractor.HasNextImage())
            {
                MemoryStream imgStream = new MemoryStream();
                extractor.GetNextImage(imgStream);
                imgStream.Position = 0; // reset for later reading
                extractedImages.Add(imgStream);
            }
        }

        if (extractedImages.Count == 0)
        {
            Console.WriteLine("No images found in the PDF.");
            return;
        }

        // -----------------------------------------------------------------
        // Step 2: Create a new PDF document that will serve as the catalog
        // -----------------------------------------------------------------
        using (Document catalog = new Document())
        {
            int pageNumber = 1;
            foreach (MemoryStream imgStream in extractedImages)
            {
                // Add a new blank page for each thumbnail
                Page page = catalog.Pages.Add();

                // Create a temporary file to hold the image (PdfExtractor outputs JPEG by default)
                string tempImagePath = Path.Combine(Path.GetTempPath(), $"img_{Guid.NewGuid()}.jpg");
                using (FileStream file = new FileStream(tempImagePath, FileMode.Create, FileAccess.Write))
                {
                    imgStream.CopyTo(file);
                }

                // Add the image to the page and scale it to a thumbnail size (e.g., 200x200)
                Aspose.Pdf.Image pdfImage = new Aspose.Pdf.Image
                {
                    File = tempImagePath,
                    // FixWidth/FixHeight preserve aspect ratio when only one is set;
                    // setting both forces the thumbnail dimensions.
                    FixWidth = 200,
                    FixHeight = 200
                };
                page.Paragraphs.Add(pdfImage);

                // Optional: add a caption below the thumbnail
                TextFragment caption = new TextFragment($"Image {pageNumber}");
                // Use MarginInfo instead of the non‑existent Margin class
                caption.Margin = new MarginInfo { Top = 210 }; // position below the thumbnail
                page.Paragraphs.Add(caption);

                // Clean up the temporary image file
                try { File.Delete(tempImagePath); } catch { /* ignore cleanup errors */ }

                pageNumber++;
            }

            // Save the catalog PDF
            catalog.Save(outputCatalogPath);
        }

        Console.WriteLine($"Image catalog created at '{outputCatalogPath}'.");
    }
}
