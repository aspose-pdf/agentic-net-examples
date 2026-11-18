using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class ReplaceRasterWithVector
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_vectorized.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Mapping of raster image identifiers (without extension) to vector SVG files.
        var vectorMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            // Example entries – adjust to match the actual image names in your PDF.
            { "image1", "vector1.svg" },
            { "photo",   "photo.svg" }
        };

        using (Document doc = new Document(inputPdf))
        {
            // Iterate through all pages (1‑based indexing).
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];
                var images = page.Resources.Images;

                // XImageCollection uses 1‑based indexing for Replace().
                for (int imgIdx = 1; imgIdx <= images.Count; imgIdx++)
                {
                    XImage img = images[imgIdx];

                    // Use the XImage.Name property to identify the image.
                    string imgName = img.Name ?? string.Empty;
                    if (string.IsNullOrEmpty(imgName))
                        continue; // Cannot identify; skip.

                    string baseName = Path.GetFileNameWithoutExtension(imgName);

                    if (vectorMap.TryGetValue(baseName, out string vectorPath) && File.Exists(vectorPath))
                    {
                        try
                        {
                            // Replace the raster image with the vector file stream.
                            using (FileStream vecStream = File.OpenRead(vectorPath))
                            {
                                // Replace expects a 1‑based index.
                                images.Replace(imgIdx, vecStream);
                            }

                            Console.WriteLine($"Replaced raster image '{imgName}' with vector '{Path.GetFileName(vectorPath)}' on page {pageIndex}.");
                        }
                        catch (Exception ex)
                        {
                            Console.Error.WriteLine($"Failed to replace image '{imgName}': {ex.Message}");
                        }
                    }
                }
            }

            // Save the modified PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Vectorized PDF saved to '{outputPdf}'.");
    }
}
