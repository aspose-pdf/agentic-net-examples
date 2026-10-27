using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imagePath = "stamp.png";

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

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPdf))
        {
            // Retrieve the first page (1‑based indexing).
            Page page = doc.Pages[1];

            // Get page dimensions (points). Width and Height are in points (1/72 inch).
            double pageWidth  = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;

            // Define desired image size (e.g., 100x100 points).
            const double imageWidth  = 100;
            const double imageHeight = 100;

            // Define a margin from the page edges.
            const double margin = 20;

            // Calculate bottom‑right placement coordinates dynamically.
            // X coordinate of lower‑left corner:
            double llx = pageWidth - imageWidth - margin;
            // Y coordinate of lower‑left corner:
            double lly = margin;

            // Create an ImageStamp and position it using the calculated coordinates.
            ImageStamp stamp = new ImageStamp(imagePath)
            {
                // Set the size of the stamp.
                Width  = imageWidth,
                Height = imageHeight,
                // Position the lower‑left corner.
                XIndent = llx,
                YIndent = lly,
                // Ensure the image is not scaled beyond the defined size.
                // (Optional) Set the background to transparent.
                Background = false
            };

            // Add the stamp to the target page.
            page.AddStamp(stamp);

            // Save the modified PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image placed and PDF saved to '{outputPdf}'.");
    }
}
