using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "even_pages_numbered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate using 1‑based page indexing (Aspose.Pdf uses 1‑based indexes)
            for (int pageNumber = 1; pageNumber <= doc.Pages.Count; pageNumber++)
            {
                // Apply page numbers only to even pages
                if (pageNumber % 2 == 0)
                {
                    // Create a PageNumberStamp with default format ("#")
                    PageNumberStamp stamp = new PageNumberStamp();

                    // Optional: set appearance of the stamp
                    stamp.HorizontalAlignment = HorizontalAlignment.Center;
                    stamp.VerticalAlignment   = VerticalAlignment.Bottom;
                    stamp.BottomMargin        = 20; // distance from bottom edge
                    stamp.TextState.FontSize  = 12;
                    stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

                    // Add the stamp to the current page
                    doc.Pages[pageNumber].AddStamp(stamp);
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Even‑page numbering saved to '{outputPath}'.");
    }
}