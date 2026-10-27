using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPath  = "input.pdf";
        const string outputPath = "booklet_output.pdf";

        // Define which pages should receive the margin resize (1‑based indexing)
        // Example: pages 2, 4, 6
        List<int> selectedPages = new List<int> { 2, 4, 6 };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block (lifecycle rule)
            using (Document doc = new Document(inputPath))
            {
                // Apply a 15 % margin on each side of the selected pages
                foreach (int pageNumber in selectedPages)
                {
                    // Ensure the page number is within the document range
                    if (pageNumber < 1 || pageNumber > doc.Pages.Count)
                    {
                        Console.WriteLine($"Skipping invalid page number: {pageNumber}");
                        continue;
                    }

                    // Retrieve the original page dimensions (points)
                    Page page = doc.Pages[pageNumber];
                    double pageWidth  = page.PageInfo.Width;
                    double pageHeight = page.PageInfo.Height;

                    // Calculate 15 % of width/height for each margin side
                    double marginLeft   = 0.15 * pageWidth;
                    double marginRight  = 0.15 * pageWidth;
                    double marginTop    = 0.15 * pageHeight;
                    double marginBottom = 0.15 * pageHeight;

                    // Set the new margins for the current page using MarginInfo
                    page.PageInfo.Margin = new MarginInfo(marginLeft, marginRight, marginTop, marginBottom);
                }

                // Save the modified document (save rule)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Booklet PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
