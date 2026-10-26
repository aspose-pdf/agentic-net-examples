using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath      = "input.pdf";
        const string outputPath     = "output.pdf";
        const string placeholderImg = "placeholder.png"; // path to the placeholder graphic

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!File.Exists(placeholderImg))
        {
            Console.Error.WriteLine($"Placeholder image not found: {placeholderImg}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate over pages using 1‑based indexing (Aspose.Pdf uses 1‑based page numbers)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                // Process only odd‑numbered pages
                if (pageNum % 2 == 0) continue;

                Page page = doc.Pages[pageNum];

                // Iterate over all images on the page
                foreach (XImage img in page.Resources.Images)
                {
                    // Preserve the original image dimensions
                    double originalWidth  = img.Width;
                    double originalHeight = img.Height;

                    // Create an ImageStamp that uses the placeholder graphic
                    ImageStamp placeholderStamp = new ImageStamp(placeholderImg)
                    {
                        // Set the stamp size to match the original image dimensions
                        Width   = originalWidth,
                        Height  = originalHeight,

                        // Position the stamp at the lower‑left corner of the page.
                        // Exact original coordinates are not directly accessible via the core API,
                        // so we place the stamp at (0,0). Adjust XIndent/YIndent if needed.
                        XIndent = 0,
                        YIndent = 0,

                        // Ensure the stamp is drawn over existing content
                        Background = false
                    };

                    // Add the stamp to the current page (it will appear on top of the original image)
                    page.AddStamp(placeholderStamp);
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}