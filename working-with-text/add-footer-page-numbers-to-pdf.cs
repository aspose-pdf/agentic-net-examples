using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

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

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            int totalPages = doc.Pages.Count;

            // Iterate using 1‑based page indexing (Aspose.Pdf uses 1‑based indexes)
            for (int i = 1; i <= totalPages; i++)
            {
                Page page = doc.Pages[i];

                // Create a TextFragment with the page‑number placeholder
                TextFragment footer = new TextFragment($"Page {i} of {totalPages}");

                // Position the footer near the bottom of the page (adjust as needed)
                footer.Position = new Position(50, 30); // 50 points from left, 30 points from bottom

                // Optional styling
                footer.TextState.FontSize = 10;
                footer.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;

                // Add the footer to the page's paragraph collection
                page.Paragraphs.Add(footer);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Footer with page numbers added and saved to '{outputPath}'.");
    }
}