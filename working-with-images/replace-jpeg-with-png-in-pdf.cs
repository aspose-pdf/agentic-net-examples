using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF containing JPEG images
        const string inputPdfPath  = "input.pdf";
        // PNG image that will replace each JPEG (same dimensions are kept by the PDF engine)
        const string pngReplacePath = "replacement.png";
        // Output PDF with PNG images in the original positions
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(pngReplacePath))
        {
            Console.Error.WriteLine($"Replacement PNG not found: {pngReplacePath}");
            return;
        }

        // Open the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate through all pages (1‑based indexing)
            foreach (Page page in pdfDoc.Pages)
            {
                // XImageCollection holds the image resources for the page
                var images = page.Resources.Images;

                // Replace each image resource with the PNG data.
                // The collection is also 1‑based.
                for (int idx = 1; idx <= images.Count; idx++)
                {
                    // Open the PNG file as a stream for each replacement.
                    // The stream is closed automatically by the using block.
                    using (FileStream pngStream = File.OpenRead(pngReplacePath))
                    {
                        // Replace keeps the original resource index, so all
                        // references (positions, scaling, etc.) remain unchanged.
                        images.Replace(idx, pngStream);
                    }
                }
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with PNG images: {outputPdfPath}");
    }
}