using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "landscape_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (using the required lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Calculate current width and height from the MediaBox
                double width  = page.MediaBox.URX - page.MediaBox.LLX;
                double height = page.MediaBox.URY - page.MediaBox.LLY;

                // Swap width and height to make the page landscape
                page.SetPageSize(height, width);
            }

            // Save the modified document (using the required lifecycle rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Pages converted to landscape and saved to '{outputPath}'.");
    }
}