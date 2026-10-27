using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_letter_pages.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document.
        Document doc = new Document(inputPath);

        // US Letter size in points (1 point = 1/72 inch).
        const double letterWidth = 8.5 * 72; // 612 points
        const double letterHeight = 11 * 72; // 792 points

        // Resize pages 3 through 6 (inclusive) to Letter dimensions.
        for (int pageNumber = 3; pageNumber <= 6; pageNumber++)
        {
            // Ensure the page exists.
            if (pageNumber <= doc.Pages.Count)
            {
                Page page = doc.Pages[pageNumber];
                page.SetPageSize(letterWidth, letterHeight);
            }
        }

        // Save the modified PDF.
        doc.Save(outputPath);

        Console.WriteLine($"Pages 3‑6 resized to Letter and saved as '{outputPath}'.");
    }
}
