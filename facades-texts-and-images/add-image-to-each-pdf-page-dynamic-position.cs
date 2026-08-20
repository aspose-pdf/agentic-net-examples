using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // source PDF
        const string outputPdf = "output.pdf";     // PDF with added images
        const string imagePath = "logo.png";       // image to place

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Initialize PdfFileMend facade on the loaded document
            PdfFileMend mend = new PdfFileMend(doc);

            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Retrieve page dimensions (in points; 1 point = 1/72 inch)
                double pageWidth  = page.Rect.Width;
                double pageHeight = page.Rect.Height;

                // Define desired image size as a fraction of the page size
                // Here we use 20 % of the page width for both width and height
                double imgWidth  = pageWidth  * 0.20;
                double imgHeight = pageHeight * 0.20;

                // Define a margin from the page borders (e.g., 10 points)
                const double margin = 10.0;

                // Calculate lower‑left and upper‑right coordinates so the image
                // appears in the bottom‑right corner of the page
                double lowerLeftX  = pageWidth  - imgWidth - margin;
                double lowerLeftY  = margin;
                double upperRightX = pageWidth  - margin;
                double upperRightY = margin + imgHeight;

                // Add the image to the current page.
                // The stream must be opened for each call because AddImage consumes it.
                using (FileStream imgStream = File.OpenRead(imagePath))
                {
                    mend.AddImage(
                        imgStream,          // image stream
                        pageNum,            // target page number
                        (float)lowerLeftX,  // lower‑left X
                        (float)lowerLeftY,  // lower‑left Y
                        (float)upperRightX, // upper‑right X
                        (float)upperRightY  // upper‑right Y
                    );
                }
            }

            // Save the modified PDF (lifecycle rule: use Save(string))
            mend.Save(outputPdf);
            mend.Close(); // optional but explicit
        }

        Console.WriteLine($"Image placed on each page and saved to '{outputPdf}'.");
    }
}