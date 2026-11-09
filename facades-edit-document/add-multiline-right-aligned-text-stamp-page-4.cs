using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // for TextState

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TextStamp with multiline content (use newline characters)
            TextStamp stamp = new TextStamp("First line of text\nSecond line of text\nThird line");

            // Right‑align the text within the stamp
            stamp.TextAlignment = HorizontalAlignment.Right;

            // Position the stamp at the bottom of the page
            stamp.VerticalAlignment = VerticalAlignment.Bottom;

            // Optional margins to fine‑tune placement
            stamp.BottomMargin = 10; // distance from bottom edge
            stamp.RightMargin  = 10; // distance from right edge

            // Set a cross‑platform color for the text
            stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Page indexing in Aspose.Pdf is 1‑based; ensure page 4 exists
            if (doc.Pages.Count >= 4)
            {
                doc.Pages[4].AddStamp(stamp);
            }
            else
            {
                Console.Error.WriteLine("The document has fewer than 4 pages.");
            }

            // Save the modified PDF (PDF format, so no SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text stamp applied and saved to '{outputPath}'.");
    }
}