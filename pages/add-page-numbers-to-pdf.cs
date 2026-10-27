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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf uses 1‑based page numbers)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // Create a TextStamp with the current page number
                TextStamp pageNumberStamp = new TextStamp(i.ToString())
                {
                    // Center horizontally on the page
                    HorizontalAlignment = HorizontalAlignment.Center,
                    // Position near the bottom (you can adjust VerticalAlignment as needed)
                    VerticalAlignment   = VerticalAlignment.Bottom,
                    // Optional: set margin from the bottom edge
                    BottomMargin        = 20,
                    // Optional: set font size and color for better visibility
                    TextState = { FontSize = 12, ForegroundColor = Color.Black }
                };

                // Add the stamp to the current page (must be called per page)
                doc.Pages[i].AddStamp(pageNumberStamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers added and saved to '{outputPath}'.");
    }
}