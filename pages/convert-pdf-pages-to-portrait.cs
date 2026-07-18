using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "portrait_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using rule for document disposal)
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based (rule: page-indexing-one-based)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Retrieve current MediaBox dimensions
                Aspose.Pdf.Rectangle mediaBox = page.MediaBox;
                double width  = mediaBox.URX - mediaBox.LLX;
                double height = mediaBox.URY - mediaBox.LLY;

                // If the page is landscape (width > height), swap dimensions
                if (width > height)
                {
                    // Set new page size with height as width and width as height
                    page.SetPageSize(height, width);
                }
                // If already portrait, no change needed
            }

            // Save the modified document (using rule for document saving)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Portrait PDF saved to '{outputPath}'.");
    }
}