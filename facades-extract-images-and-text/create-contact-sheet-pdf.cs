using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "contact_sheet.pdf";
        const string tempImageDir = "temp_images";

        // Thumbnail size and grid layout
        const double thumbWidth = 100;   // points
        const double thumbHeight = 100;  // points
        const int columns = 5;
        const double margin = 10;        // points between images

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure temporary folder exists
        if (Directory.Exists(tempImageDir))
            Directory.Delete(tempImageDir, true);
        Directory.CreateDirectory(tempImageDir);

        // -----------------------------------------------------------------
        // Step 1: Extract all images from the source PDF to temporary files
        // -----------------------------------------------------------------
        List<string> extractedImagePaths = new List<string>();
        PdfExtractor extractor = new PdfExtractor();
        extractor.BindPdf(inputPdfPath);
        extractor.ExtractImage();

        int imageIndex = 1;
        while (extractor.HasNextImage())
        {
            string tempImagePath = Path.Combine(tempImageDir, $"img_{imageIndex}.png");
            extractor.GetNextImage(tempImagePath);
            extractedImagePaths.Add(tempImagePath);
            imageIndex++;
        }

        // -----------------------------------------------------------------
        // Step 2: Create a new PDF that will hold the contact sheet
        // -----------------------------------------------------------------
        using (Document contactDoc = new Document())
        {
            // Add a single page (default size A4)
            Aspose.Pdf.Page contactPage = contactDoc.Pages.Add();

            // Page dimensions (points)
            double pageWidth = contactPage.PageInfo.Width;
            double pageHeight = contactPage.PageInfo.Height;

            // Calculate positions for each thumbnail
            int currentColumn = 0;
            int currentRow = 0;

            foreach (string imgPath in extractedImagePaths)
            {
                double x = margin + currentColumn * (thumbWidth + margin);
                double y = pageHeight - margin - (currentRow + 1) * (thumbHeight + margin);

                // Add the image at the calculated rectangle
                contactPage.AddImage(
                    imgPath,
                    new Aspose.Pdf.Rectangle(x, y, x + thumbWidth, y + thumbHeight));

                // Move to next cell in the grid
                currentColumn++;
                if (currentColumn >= columns)
                {
                    currentColumn = 0;
                    currentRow++;
                }

                // If the next image would exceed the page height, add a new page
                if (y - thumbHeight - margin < 0 && currentColumn == 0)
                {
                    contactPage = contactDoc.Pages.Add();
                    pageHeight = contactPage.PageInfo.Height;
                }
            }

            // Save the contact sheet PDF
            contactDoc.Save(outputPdfPath);
        }

        // -----------------------------------------------------------------
        // Cleanup temporary images
        // -----------------------------------------------------------------
        try
        {
            Directory.Delete(tempImageDir, true);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to delete temporary folder: {ex.Message}");
        }

        Console.WriteLine($"Contact sheet created: {outputPdfPath}");
    }
}