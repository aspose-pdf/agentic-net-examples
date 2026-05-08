using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string imagePath     = "logo.png";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the PDF document inside a using block (document-disposal-with-using rule)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Initialize PdfFileMend facade with the loaded document
            PdfFileMend mender = new PdfFileMend(pdfDoc);

            // Open the image once as a stream (will be reused for each page)
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
                for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
                {
                    Page page = pdfDoc.Pages[pageNum];

                    // Retrieve page dimensions (width and height in points)
                    double pageWidth  = page.Rect.Width;
                    double pageHeight = page.Rect.Height;

                    // Define margins as a percentage of page size (e.g., 5%)
                    double marginX = pageWidth  * 0.05;
                    double marginY = pageHeight * 0.05;

                    // Desired image size (e.g., 20% of page width, keep aspect ratio)
                    double imgWidth  = pageWidth  * 0.20;
                    double imgHeight = imgWidth; // square placeholder; adjust as needed

                    // Calculate lower‑left and upper‑right coordinates for bottom‑right placement
                    double lowerLeftX  = pageWidth  - marginX - imgWidth;
                    double lowerLeftY  = marginY;
                    double upperRightX = pageWidth  - marginX;
                    double upperRightY = marginY + imgHeight;

                    // Add the image to the current page at the calculated rectangle
                    // AddImage(Stream, int, float, float, float, float) overload
                    mender.AddImage(imgStream, pageNum,
                                    (float)lowerLeftX, (float)lowerLeftY,
                                    (float)upperRightX, (float)upperRightY);
                    
                    // Reset the stream position for the next iteration
                    imgStream.Position = 0;
                }
            }

            // Save the modified document (PdfFileMend inherits SaveableFacade)
            mender.Save(outputPdfPath);
            mender.Close(); // optional, releases resources held by the facade
        }

        Console.WriteLine($"Image placed on all pages and saved to '{outputPdfPath}'.");
    }
}