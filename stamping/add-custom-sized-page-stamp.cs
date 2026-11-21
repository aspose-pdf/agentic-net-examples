using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string stampImg  = "stamp.png";   // image to use as stamp
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(stampImg))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImg}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Create an ImageStamp and configure its size (custom width & height)
            ImageStamp stamp = new ImageStamp(stampImg)
            {
                // Desired dimensions in points (1 point = 1/72 inch)
                Width  = 150,   // custom width
                Height = 100,   // custom height

                // Position the stamp relative to the page's lower‑left corner
                // (TopMargin and LeftMargin are measured from the bottom‑left origin)
                TopMargin  = 200,   // distance from bottom edge
                LeftMargin = 50,    // distance from left edge

                // Ensure the stamp is placed on top of existing content
                Background = false
            };

            // Target a specific page (e.g., page 2). Aspose.Pdf uses 1‑based indexing.
            int targetPageNumber = 2;
            if (targetPageNumber <= pdfDoc.Pages.Count)
            {
                Page targetPage = pdfDoc.Pages[targetPageNumber];
                targetPage.AddStamp(stamp);   // Apply the stamp to the chosen page
            }
            else
            {
                Console.Error.WriteLine($"Page {targetPageNumber} does not exist in the document.");
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"Stamp applied and saved to '{outputPdf}'.");
    }
}