using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Drawing;

class ContactSheetGenerator
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "contact_sheet.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Step 1: Extract all images from the source PDF into memory streams
        List<MemoryStream> imageStreams = new List<MemoryStream>();
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdfPath);
            extractor.ExtractImage();

            while (extractor.HasNextImage())
            {
                MemoryStream imgStream = new MemoryStream();
                extractor.GetNextImage(imgStream);
                imgStream.Position = 0; // reset for later reading
                imageStreams.Add(imgStream);
            }

            extractor.Close(); // optional, Dispose will be called by using
        }

        if (imageStreams.Count == 0)
        {
            Console.WriteLine("No images found in the PDF.");
            return;
        }

        // Step 2: Create a new PDF document that will hold the contact sheet
        using (Document contactDoc = new Document())
        {
            // Define layout parameters
            const int columns = 5;                     // thumbnails per row
            const double thumbWidth = 100;             // width of each thumbnail (points)
            const double thumbHeight = 100;            // height of each thumbnail (points)
            const double margin = 20;                  // space between thumbnails (points)

            // Add the first page
            Page page = contactDoc.Pages.Add();

            // Helper to add a new page when needed
            Action addNewPageIfNeeded = () =>
            {
                // If the next thumbnail would go below the page bottom, start a new page
                double requiredHeight = ((imageStreams.Count - 1) / columns + 1) * (thumbHeight + margin) + margin;
                if (requiredHeight > page.PageInfo.Height)
                {
                    page = contactDoc.Pages.Add();
                }
            };

            // Place each thumbnail on the grid
            for (int i = 0; i < imageStreams.Count; i++)
            {
                int col = i % columns;
                int row = i / columns;

                double x = margin + col * (thumbWidth + margin);
                // PDF origin is bottom‑left, so compute Y from top
                double y = page.PageInfo.Height - margin - (row + 1) * (thumbHeight + margin) + margin;

                // Ensure the current page can accommodate the thumbnail
                if (y < margin)
                {
                    // Not enough space on current page – start a new one
                    page = contactDoc.Pages.Add();
                    row = 0;
                    y = page.PageInfo.Height - margin - thumbHeight;
                }

                // Add the image to the page at the calculated rectangle
                MemoryStream imgStream = imageStreams[i];
                imgStream.Position = 0; // reset before each use
                page.AddImage(imgStream, new Aspose.Pdf.Rectangle(x, y, x + thumbWidth, y + thumbHeight));
            }

            // Step 3: Save the contact sheet PDF
            contactDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Contact sheet created: {outputPdfPath}");
    }
}