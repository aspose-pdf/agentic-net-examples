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

        // -----------------------------------------------------------------
        // Step 1: Extract all images from the source PDF into memory streams
        // -----------------------------------------------------------------
        List<MemoryStream> extractedImages = new List<MemoryStream>();
        PdfExtractor extractor = new PdfExtractor();
        extractor.BindPdf(inputPdfPath);
        extractor.ExtractImage();

        while (extractor.HasNextImage())
        {
            MemoryStream imgStream = new MemoryStream();
            extractor.GetNextImage(imgStream);
            imgStream.Position = 0; // reset for later reading
            extractedImages.Add(imgStream);
        }

        // -----------------------------------------------------------------
        // Step 2: Build a contact sheet PDF with thumbnails arranged in a grid
        // -----------------------------------------------------------------
        // Layout parameters
        const int columns = 3;                 // thumbnails per row
        const int thumbWidth = 150;            // thumbnail width (points)
        const int thumbHeight = 150;           // thumbnail height (points)
        const int margin = 20;                 // margin between thumbnails and page edges
        const int rowsPerPage = 4;             // rows per page (adjust as needed)

        // Create the output PDF document
        using (Document contactDoc = new Document())
        {
            int imagesPerPage = columns * rowsPerPage;
            for (int i = 0; i < extractedImages.Count; i++)
            {
                // Start a new page when needed
                if (i % imagesPerPage == 0)
                {
                    contactDoc.Pages.Add();
                }

                // Current page reference
                Page page = contactDoc.Pages[contactDoc.Pages.Count];

                // Compute row and column within the current page
                int indexInPage = i % imagesPerPage;
                int row = indexInPage / columns;
                int col = indexInPage % columns;

                // Calculate thumbnail position (lower‑left and upper‑right points)
                double llx = margin + col * (thumbWidth + margin);
                double lly = page.PageInfo.Height - margin - (row + 1) * (thumbHeight + margin);
                double urx = llx + thumbWidth;
                double ury = lly + thumbHeight;

                // Retrieve the image stream for this thumbnail
                MemoryStream imgStream = extractedImages[i];
                imgStream.Position = 0; // ensure stream is at the beginning

                // Add the image to the page at the calculated rectangle
                page.AddImage(imgStream, new Aspose.Pdf.Rectangle(llx, lly, urx, ury));
            }

            // Save the contact sheet PDF
            contactDoc.Save(outputPdfPath);
        }

        // Cleanup extracted image streams
        foreach (var ms in extractedImages)
        {
            ms.Dispose();
        }

        Console.WriteLine($"Contact sheet created: {outputPdfPath}");
    }
}