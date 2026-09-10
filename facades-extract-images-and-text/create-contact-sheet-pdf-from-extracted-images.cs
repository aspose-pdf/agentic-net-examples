using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;          // only for creating sample images
using System.IO;
using Aspose.Pdf;                     // core PDF API
using Aspose.Pdf.Facades;             // for PdfExtractor

class ContactSheetGenerator
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";          // source PDF containing images
        const string outputPdfPath  = "contact_sheet.pdf"; // resulting contact sheet
        const int   thumbWidth      = 150;                 // thumbnail width (points)
        const int   thumbHeight     = 150;                 // thumbnail height (points)
        const int   columns         = 4;                   // thumbnails per row
        const int   margin          = 20;                  // page margin (points)
        const int   hSpacing        = 10;                  // horizontal spacing between thumbnails
        const int   vSpacing        = 10;                  // vertical spacing between thumbnails

        // -----------------------------------------------------------------
        // Ensure a source PDF exists – create a minimal one with a few images
        // -----------------------------------------------------------------
        if (!File.Exists(inputPdfPath))
        {
            // Create a simple 2‑page PDF, each page contains a generated PNG image
            using (var sampleDoc = new Document())
            {
                for (int p = 0; p < 2; p++)
                {
                    var page = sampleDoc.Pages.Add();

                    // Generate a 100x100 solid‑color bitmap in memory
                    using (var bmp = new Bitmap(100, 100))
                    {
                        using (var g = Graphics.FromImage(bmp))
                        {
                            g.Clear(p % 2 == 0 ? System.Drawing.Color.Red : System.Drawing.Color.Green);
                        }
                        using (var imgStream = new MemoryStream())
                        {
                            bmp.Save(imgStream, ImageFormat.Png);
                            imgStream.Position = 0;

                            // Add the image to the PDF page
                            var rect = new Aspose.Pdf.Rectangle(50, 500, 150, 600);
                            page.AddImage(imgStream, rect);
                        }
                    }
                }
                sampleDoc.Save(inputPdfPath);
            }
        }

        // -----------------------------------------------------------------
        // Step 1: Extract all images from the source PDF into memory streams
        // -----------------------------------------------------------------
        List<MemoryStream> imageStreams = new List<MemoryStream>();

        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdfPath);   // bind the source PDF
            extractor.ExtractImage();          // start image extraction

            while (extractor.HasNextImage())
            {
                // Store each extracted image in its original format
                MemoryStream ms = new MemoryStream();
                extractor.GetNextImage(ms);    // overload without ImageFormat avoids platform warning
                ms.Position = 0;               // reset for later reading
                imageStreams.Add(ms);
            }
        }

        // -----------------------------------------------------------------
        // Step 2: Create a new PDF document that will hold the contact sheet
        // -----------------------------------------------------------------
        using (Document contactDoc = new Document())
        {
            // Add a single page (size A4)
            Page page = contactDoc.Pages.Add();

            // Determine page dimensions (A4 default)
            double pageWidth  = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;

            // Compute layout positions
            int imageCount = imageStreams.Count;
            int rows = (int)Math.Ceiling(imageCount / (double)columns);

            for (int i = 0; i < imageCount; i++)
            {
                int col = i % columns;
                int row = i / columns; // 0‑based from top

                // X coordinate (left)
                double x = margin + col * (thumbWidth + hSpacing);
                // Y coordinate (bottom). PDF origin is bottom‑left, so we count from top.
                double y = pageHeight - margin - ((row + 1) * thumbHeight) - row * vSpacing;

                // Define the rectangle where the thumbnail will be placed
                var rect = new Aspose.Pdf.Rectangle(x, y, x + thumbWidth, y + thumbHeight);

                // Add the image to the page using the memory stream
                page.AddImage(imageStreams[i], rect);
            }

            // Save the contact sheet PDF
            contactDoc.Save(outputPdfPath);
        }

        // -----------------------------------------------------------------
        // Step 3: Clean up the in‑memory image streams
        // -----------------------------------------------------------------
        foreach (var ms in imageStreams)
        {
            ms.Dispose();
        }

        Console.WriteLine($"Contact sheet created: {outputPdfPath}");
    }
}
