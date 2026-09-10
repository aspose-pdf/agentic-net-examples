using System;
using System.IO;
using Aspose.Pdf;

class ReplaceJpegWithPng
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string pngReplacementPath = "replacement.png";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(pngReplacementPath))
        {
            Console.Error.WriteLine($"Replacement PNG not found: {pngReplacementPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];
                var images = page.Resources.Images;

                // XImageCollection is 1‑based as well
                for (int imgIndex = 1; imgIndex <= images.Count; imgIndex++)
                {
                    // Replace the image with the PNG file.
                    // The Replace method expects a stream; the format is inferred from the data.
                    using (FileStream pngStream = File.OpenRead(pngReplacementPath))
                    {
                        images.Replace(imgIndex, pngStream);
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"All images replaced and saved to '{outputPdfPath}'.");
    }
}