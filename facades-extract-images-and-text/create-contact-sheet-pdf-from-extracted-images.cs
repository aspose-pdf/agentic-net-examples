using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "source.pdf";
        const string outputPdfPath = "contact_sheet.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Load the source PDF and collect all images from all pages.
        // -----------------------------------------------------------------
        List<XImage> extractedImages = new List<XImage>();
        using (Document srcDoc = new Document(inputPdfPath))
        {
            // Pages are 1‑based.
            for (int p = 1; p <= srcDoc.Pages.Count; p++)
            {
                Page page = srcDoc.Pages[p];
                // XImageCollection is enumerable – use direct foreach.
                foreach (XImage img in page.Resources.Images)
                {
                    extractedImages.Add(img);
                }
            }
        }

        if (extractedImages.Count == 0)
        {
            Console.WriteLine("No images found in the source PDF.");
            return;
        }

        // -----------------------------------------------------------------
        // 2. Create a new PDF that will hold the contact sheet.
        // -----------------------------------------------------------------
        using (Document contactDoc = new Document())
        {
            // Define page size (A4) and margins.
            const double pageWidth = 595;   // A4 width in points
            const double pageHeight = 842;  // A4 height in points
            const double margin = 20;       // points
            const int columns = 3;          // thumbnails per row
            const double thumbWidth = 150;  // desired thumbnail width
            const double thumbHeight = 200; // desired thumbnail height

            // Calculate how many rows fit on a page.
            int rowsPerPage = (int)((pageHeight - 2 * margin) / (thumbHeight + margin));

            // Helper to add a new blank page.
            Page AddNewPage()
            {
                Page newPage = contactDoc.Pages.Add();
                newPage.PageInfo.Width = pageWidth;
                newPage.PageInfo.Height = pageHeight;
                return newPage;
            }

            Page currentPage = AddNewPage();
            int currentRow = 0;
            int currentCol = 0;

            foreach (XImage srcImg in extractedImages)
            {
                // If the current row exceeds the page capacity, start a new page.
                if (currentRow >= rowsPerPage)
                {
                    currentPage = AddNewPage();
                    currentRow = 0;
                    currentCol = 0;
                }

                // -----------------------------------------------------------------
                // 3. Create a thumbnail stamp for the image.
                // -----------------------------------------------------------------
                // Save the XImage into a memory stream so ImageStamp can consume it.
                using (MemoryStream imgStream = new MemoryStream())
                {
                    srcImg.Save(imgStream);
                    imgStream.Position = 0;

                    // ImageStamp works with a stream or file path.
                    Aspose.Pdf.ImageStamp stamp = new Aspose.Pdf.ImageStamp(imgStream)
                    {
                        // Set explicit size for the thumbnail.
                        Width = thumbWidth,
                        Height = thumbHeight,
                        // Position the stamp using XIndent/YIndent.
                        XIndent = margin + currentCol * (thumbWidth + margin),
                        YIndent = margin + currentRow * (thumbHeight + margin)
                    };

                    // Add the stamp to the current page.
                    currentPage.AddStamp(stamp);
                }

                // Move to next column; wrap to next row when needed.
                currentCol++;
                if (currentCol >= columns)
                {
                    currentCol = 0;
                    currentRow++;
                }
            }

            // -----------------------------------------------------------------
            // 4. Save the contact sheet PDF.
            // -----------------------------------------------------------------
            contactDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Contact sheet created: {outputPdfPath}");
    }
}