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
        const string headerText = "My Header";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a TextFragment that will act as the header
                TextFragment header = new TextFragment(headerText)
                {
                    // Position the header near the top of the page (y coordinate is measured from the bottom)
                    Position = new Position(0, page.PageInfo.Height - 20) // 20 points from the top edge
                };

                // Configure the appearance of the header text
                header.TextState.Font = FontRepository.FindFont("Helvetica");
                header.TextState.FontSize = 12;
                header.TextState.ForegroundColor = Color.Gray;

                // Add the header fragment to the page's paragraph collection
                page.Paragraphs.Add(header);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Header added and saved to '{outputPath}'.");
    }
}
