using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // Required for FontRepository

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "paged_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a TextStamp with a two‑digit page number (leading zero for 1‑9)
                string pageNumber = i.ToString("D2"); // e.g., "01", "02", ..., "10"
                TextStamp stamp = new TextStamp(pageNumber)
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Bottom,
                    BottomMargin        = 10 // distance from the bottom edge
                };

                // Optional styling
                stamp.TextState.FontSize = 12;
                stamp.TextState.Font = FontRepository.FindFont("Helvetica");
                stamp.TextState.ForegroundColor = Color.Gray;

                // Add the stamp to the current page
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers added and saved to '{outputPath}'.");
    }
}
