using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "booklet_resized.pdf";

        // Pages to which the margin resize will be applied (1‑based indexing)
        int[] selectedPages = { 1, 2, 3, 4, 5, 6 };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Apply a 15% margin on each side of the selected pages
                foreach (int pageNumber in selectedPages)
                {
                    // Ensure the page number is within the document range
                    if (pageNumber < 1 || pageNumber > doc.Pages.Count)
                        continue; // skip invalid page numbers

                    // Retrieve original page dimensions (points)
                    double width = doc.Pages[pageNumber].PageInfo.Width;
                    double height = doc.Pages[pageNumber].PageInfo.Height;

                    // Calculate 15% margins
                    double marginX = width * 0.15;
                    double marginY = height * 0.15;

                    // Set the new margins for the page using MarginInfo (left, top, right, bottom)
                    doc.Pages[pageNumber].PageInfo.Margin = new MarginInfo(marginX, marginY, marginX, marginY);
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Booklet layout PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
