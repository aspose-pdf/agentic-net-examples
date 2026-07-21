using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string headerImg = "header.png";   // decorative header image
        const string outputPdf = "output_with_header.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(headerImg))
        {
            Console.Error.WriteLine($"Header image not found: {headerImg}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Desired height of the header image (in points)
            const double headerHeight = 50.0;

            // Add the header image to each page
            foreach (Page page in doc.Pages)
            {
                // Get page dimensions from MediaBox
                Aspose.Pdf.Rectangle mediaBox = page.MediaBox;
                double pageWidth = mediaBox.URX - mediaBox.LLX;
                double pageHeight = mediaBox.URY - mediaBox.LLY;

                // Create an ImageStamp for the header
                ImageStamp headerStamp = new ImageStamp(headerImg)
                {
                    Width = pageWidth,               // stretch to full page width
                    Height = headerHeight,           // fixed header height
                    // Position: bottom‑left corner of the stamp should be just below the top edge
                    BottomMargin = pageHeight - headerHeight,
                    LeftMargin = mediaBox.LLX
                    // Note: ImageStamp in current Aspose.PDF version does not expose an IsBackground property.
                    // The stamp will be placed on top of existing content. If a background placement is required,
                    // consider using a FormXObject or adjusting page content manually.
                };

                // Add the stamp to the page
                page.AddStamp(headerStamp);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with decorative header saved to '{outputPdf}'.");
    }
}
