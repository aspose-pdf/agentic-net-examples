using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace
using Aspose.Pdf.Text;          // For any text-related types if needed (not used here)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "portrait_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (global rule)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Get current MediaBox rectangle
                Aspose.Pdf.Rectangle mediaBox = page.MediaBox;

                // Calculate current width and height
                double width  = mediaBox.URX - mediaBox.LLX;
                double height = mediaBox.URY - mediaBox.LLY;

                // If the page is landscape (width > height), swap dimensions to make it portrait
                if (width > height)
                {
                    // Set new page size with width <= height
                    page.SetPageSize(height, width);
                }
                // If already portrait, no action needed
            }

            // Save the modified document (PDF format, no SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"All pages converted to portrait orientation and saved to '{outputPath}'.");
    }
}