using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // source PDF
        const string imagePath = "logo.png";       // image to place
        const string outputPdf = "output.pdf";     // result PDF

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the PDF document (lifecycle rule: wrap in using)
        using (Document doc = new Document(inputPdf))
        {
            // Create the PdfFileMend facade bound to the loaded document
            using (PdfFileMend mender = new PdfFileMend(doc))
            {
                // Open the image stream once – it will be reused for all pages
                using (FileStream imgStream = File.OpenRead(imagePath))
                {
                    // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
                    for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                    {
                        Page page = doc.Pages[pageNum];

                        // Retrieve page dimensions from the MediaBox (in points)
                        double pageLlx = page.MediaBox.LLX;
                        double pageLly = page.MediaBox.LLY;
                        double pageUrx = page.MediaBox.URX;
                        double pageUry = page.MediaBox.URY;

                        double pageWidth  = pageUrx - pageLlx;
                        double pageHeight = pageUry - pageLly;

                        // Desired image size: 15 % of page width, keep aspect ratio
                        const double widthFactor  = 0.15;
                        double imageWidth  = pageWidth * widthFactor;
                        double imageHeight = imageWidth; // square placeholder; adjust as needed

                        // Margin from page edges (5 % of page width)
                        double margin = pageWidth * 0.05;

                        // Place image at bottom‑right corner
                        double lowerLeftX  = pageUrx - margin - imageWidth;
                        double lowerLeftY  = pageLly + margin;
                        double upperRightX = pageUrx - margin;
                        double upperRightY = lowerLeftY + imageHeight;

                        // Add the image to the current page
                        // AddImage(Stream, int, float, float, float, float)
                        mender.AddImage(
                            imgStream,
                            pageNum,
                            (float)lowerLeftX,
                            (float)lowerLeftY,
                            (float)upperRightX,
                            (float)upperRightY);
                        
                        // Reset the stream position for the next iteration
                        imgStream.Position = 0;
                    }
                }

                // Save the modified PDF (lifecycle rule: use Save)
                mender.Save(outputPdf);
            }
        }

        Console.WriteLine($"Image placed on all pages. Output saved to '{outputPdf}'.");
    }
}