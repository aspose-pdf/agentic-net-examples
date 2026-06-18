using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class ContactSheetGenerator
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";               // source PDF containing images
        const string outputPdfPath = "contact_sheet.pdf";      // resulting contact sheet
        const int thumbWidth = 150;    // thumbnail width in points
        const int thumbHeight = 150;   // thumbnail height in points
        const int columns = 3;         // number of thumbnails per row
        const int margin = 20;         // page margin in points
        const int hSpacing = 10;       // horizontal spacing between thumbnails
        const int vSpacing = 10;       // vertical spacing between thumbnails

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Extract all images from the source PDF into memory streams
        // -----------------------------------------------------------------
        List<MemoryStream> imageStreams = new List<MemoryStream>();
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdfPath);
            extractor.ExtractImage();

            while (extractor.HasNextImage())
            {
                MemoryStream ms = new MemoryStream();
                extractor.GetNextImage(ms);          // overload that writes to a stream
                ms.Position = 0;                     // reset for later reading
                imageStreams.Add(ms);
            }
        }

        if (imageStreams.Count == 0)
        {
            Console.WriteLine("No images were found in the source PDF.");
            return;
        }

        // -----------------------------------------------------------------
        // 2. Create a new PDF document that will hold the contact sheet
        // -----------------------------------------------------------------
        using (Document contactDoc = new Document())
        {
            // Add a single page (A4 size by default)
            Page page = contactDoc.Pages.Add();

            // Retrieve page dimensions (points)
            double pageWidth = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;

            // -----------------------------------------------------------------
            // 3. Place each thumbnail on the page in a grid layout
            // -----------------------------------------------------------------
            for (int i = 0; i < imageStreams.Count; i++)
            {
                int col = i % columns;
                int row = i / columns;

                // Calculate lower‑left corner of the thumbnail rectangle
                double x1 = margin + col * (thumbWidth + hSpacing);
                double y1 = pageHeight - margin - (row + 1) * (thumbHeight + vSpacing);

                // Upper‑right corner
                double x2 = x1 + thumbWidth;
                double y2 = y1 + thumbHeight;

                // Ensure we stay within page bounds; if not, add a new page
                if (y1 < margin)
                {
                    page = contactDoc.Pages.Add();
                    pageHeight = page.PageInfo.Height;
                    y1 = pageHeight - margin - thumbHeight;
                    y2 = y1 + thumbHeight;
                }

                // Add the image to the page directly from the stream
                // The overload adds the image resource and draws it at the specified rectangle
                page.AddImage(imageStreams[i], new Aspose.Pdf.Rectangle(x1, y1, x2, y2));
            }

            // -----------------------------------------------------------------
            // 4. Save the contact sheet PDF
            // -----------------------------------------------------------------
            contactDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Contact sheet created: {outputPdfPath}");
    }
}