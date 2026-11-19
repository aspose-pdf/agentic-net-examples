using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string watermark = "Rotated Text";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            Page lastPage = doc.Pages[doc.Pages.Count];

            // Define a small margin from the right and bottom edges
            double margin = 10.0;
            double x = lastPage.PageInfo.Width - margin; // right‑most X
            double y = margin;                           // bottom‑most Y

            // Create a TextFragment, set rotation and position, then add to the page
            TextFragment fragment = new TextFragment(watermark);
            fragment.TextState.Rotation = 45.0; // degrees clockwise
            fragment.Position = new Position(x, y);
            lastPage.Paragraphs.Add(fragment);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated text added and saved to '{outputPath}'.");
    }
}
