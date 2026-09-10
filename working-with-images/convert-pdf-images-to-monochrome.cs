using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_monochrome.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];
                var images = page.Resources.Images;

                // XImageCollection uses 1‑based indexes as well
                for (int imgIndex = 1; imgIndex <= images.Count; imgIndex++)
                {
                    // Retrieve the original image stream
                    using (MemoryStream originalStream = new MemoryStream())
                    {
                        // Save the image to a stream (original format)
                        images[imgIndex].Save(originalStream);
                        originalStream.Position = 0;

                        // Replace the image with a black‑and‑white (monochrome) version.
                        // The overload with 'isBlackAndWhite = true' forces CCITT compression
                        // suitable for B/W images. Quality is set to 100 (max JPEG quality).
                        images.Replace(imgIndex, originalStream, 100, true);
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Monochrome PDF saved to '{outputPath}'.");
    }
}