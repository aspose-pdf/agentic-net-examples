using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "resized_images.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Iterate over all paragraph elements on the page
                for (int p = 1; p <= page.Paragraphs.Count; p++)
                {
                    // Check if the paragraph is an Image object
                    if (page.Paragraphs[p] is Image img)
                    {
                        // Scale the image to 50 % of its original size.
                        // ImageScale applies uniformly to width and height.
                        img.ImageScale = 0.5f;

                        // Alternatively, you could set explicit dimensions:
                        // img.FixWidth  = img.FixWidth  * 0.5;
                        // img.FixHeight = img.FixHeight * 0.5;
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with resized images to '{outputPath}'.");
    }
}