using System;
using System.IO;
using Aspose.Pdf;

class ReplaceJpegWithPng
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";      // source PDF
        const string outputPdfPath = "output.pdf";     // result PDF
        const string pngImagePath  = "replacement.png"; // PNG to substitute for each JPEG

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(pngImagePath))
        {
            Console.Error.WriteLine($"PNG image not found: {pngImagePath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIdx = 1; pageIdx <= pdfDoc.Pages.Count; pageIdx++)
            {
                Page page = pdfDoc.Pages[pageIdx];
                // XImageCollection holds the image resources for the page
                XImageCollection images = page.Resources.Images;

                // Replace each image resource with the PNG image.
                // The Replace method expects a 1‑based index.
                for (int imgIdx = 1; imgIdx <= images.Count; imgIdx++)
                {
                    // Load the PNG data into a stream for each replacement.
                    // The stream is closed after the Replace call.
                    using (FileStream pngStream = File.OpenRead(pngImagePath))
                    {
                        images.Replace(imgIdx, pngStream);
                    }
                }
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"All images replaced and saved to '{outputPdfPath}'.");
    }
}