using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_landscape.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle: using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Retrieve current MediaBox dimensions
                Aspose.Pdf.Rectangle mediaBox = page.MediaBox;
                double width  = mediaBox.URX - mediaBox.LLX;
                double height = mediaBox.URY - mediaBox.LLY;

                // Swap width and height to convert portrait to landscape
                // SetPageSize updates the MediaBox accordingly
                page.SetPageSize(height, width);
            }

            // Save the modified document (lifecycle: save within using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"All pages converted to landscape and saved as '{outputPath}'.");
    }
}