using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_grayscale_images.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // Process only even‑numbered pages
                if (i % 2 == 0)
                {
                    // Convert the whole page to grayscale.
                    // This effectively replaces all images on the page with their grayscale versions.
                    // (Aspose.Pdf does not provide a direct per‑image grayscale replace without using Facades.)
                    doc.Pages[i].MakeGrayscale();
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}