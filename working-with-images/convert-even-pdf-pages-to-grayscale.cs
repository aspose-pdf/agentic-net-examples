using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                // Process only even‑numbered pages
                if (pageNum % 2 == 0)
                {
                    // Convert the entire page to grayscale.
                    // This includes all images on the page while leaving odd pages untouched.
                    doc.Pages[pageNum].MakeGrayscale();
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}