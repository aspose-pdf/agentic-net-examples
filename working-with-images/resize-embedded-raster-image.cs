using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Text;                // For text-related types if needed

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // source PDF containing the raster image
        const string outputPath = "output_resized.pdf"; // PDF after resizing the image
        const double newWidth  = 200;   // desired width in points (1 point = 1/72 inch)
        const double newHeight = 150;   // desired height in points

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Scan the page's paragraph collection for Image objects
                for (int i = 1; i <= page.Paragraphs.Count; i++)
                {
                    // The Paragraphs collection is also 1‑based
                    var paragraph = page.Paragraphs[i];

                    // Check if the paragraph is an Image (raster image)
                    if (paragraph is Aspose.Pdf.Image img)
                    {
                        // Resize the image by setting FixWidth and FixHeight.
                        // These properties override the original dimensions when the PDF is saved.
                        img.FixWidth  = newWidth;
                        img.FixHeight = newHeight;

                        // Optionally, you can also adjust the image's position by modifying its
                        // Rectangle (left, bottom, right, top) if needed.
                        // Example: keep the lower‑left corner unchanged
                        // double left   = img.Rectangle.LLX;
                        // double bottom = img.Rectangle.LLY;
                        // img.Rectangle = new Aspose.Pdf.Rectangle(left, bottom, left + newWidth, bottom + newHeight);
                    }
                }
            }

            // Save the modified document. The Save method without SaveOptions always writes PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}