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
        const string headerText = "SECTION HEADING";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a TextFragment with bold uppercase text
                TextFragment header = new TextFragment(headerText.ToUpperInvariant());

                // Configure text appearance
                header.TextState.FontSize = 14;
                header.TextState.FontStyle = FontStyles.Bold;
                header.TextState.ForegroundColor = Aspose.Pdf.Color.Black; // cross‑platform color

                // Position the header near the top of the page
                // 50 points from the left edge, 50 points below the top edge
                double yPos = page.PageInfo.Height - 50;
                header.Position = new Position(50, yPos);

                // Add the header to the page
                page.Paragraphs.Add(header);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Header added and saved to '{outputPath}'.");
    }
}