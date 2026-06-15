using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;   // required for TextFragment, TextState, Position

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block (ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            Page page = doc.Pages[1];

            // Create a TextFragment with the desired content
            TextFragment fragment = new TextFragment("Left aligned text added to the page.");

            // Align the text to the left margin
            fragment.TextState.HorizontalAlignment = HorizontalAlignment.Left;

            // Position the fragment (optional – adjust as needed)
            fragment.Position = new Position(50, 750); // X=50, Y=750 points

            // Add the fragment to the page's paragraph collection
            page.Paragraphs.Add(fragment);

            // Save the modified PDF (Document.Save writes PDF regardless of extension)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}