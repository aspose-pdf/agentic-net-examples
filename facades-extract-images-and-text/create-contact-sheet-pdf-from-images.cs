using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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
                MemoryStream ms = new MemoryStream();
                extractor.GetNextImage(ms);
                ms.Position = 0; // reset for later reading
                imageStreams.Add(ms);
            }
        }

        if (imageStreams.Count == 0)
        {
            Console.WriteLine("No images found in the PDF.");
            return;
        }

        // Step 2: Define thumbnail and grid layout parameters
        const double thumbWidth = 100;   // points
        const double thumbHeight = 100;  // points
        const int columns = 5;           // thumbnails per row
        const double margin = 20;        // points between thumbnails and page borders

        int rows = (int)Math.Ceiling(imageStreams.Count / (double)columns);
        double pageWidth = margin * 2 + columns * thumbWidth + (columns - 1) * margin;
        double pageHeight = margin * 2 + rows * thumbHeight + (rows - 1) * margin;

        // Step 3: Create a new PDF document and place thumbnails on a single page
        using (Document contactDoc = new Document())
        {
            Page page = contactDoc.Pages.Add();
            page.SetPageSize(pageWidth, pageHeight);

            for (int i = 0; i < imageStreams.Count; i++)
            {
                int col = i % columns;
                int row = i / columns;

                double llx = margin + col * (thumbWidth + margin);
                double lly = pageHeight - margin - (row + 1) * thumbHeight - row * margin;
                double urx = llx + thumbWidth;
                double ury = lly + thumbHeight;

                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                // Ensure the stream is positioned at the beginning before adding
                imageStreams[i].Position = 0;
                page.AddImage(imageStreams[i], rect);
            }

            // Step 4: Save the contact sheet PDF
            contactDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Contact sheet created: {outputPdfPath}");
    }
}