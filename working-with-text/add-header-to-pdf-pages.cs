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
        const string headerText = "Confidential Document";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based; iterate from 1 to Count inclusive
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a text fragment that will serve as the header
                TextFragment header = new TextFragment(headerText);
                header.TextState.FontSize = 12;
                header.TextState.FontStyle = FontStyles.Bold;
                header.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;

                // Position the header near the top of the page
                // Y coordinate is measured from the bottom, so subtract from page height
                header.Position = new Position(50, page.PageInfo.Height - 50);

                // Add the header to the page's paragraph collection
                page.Paragraphs.Add(header);
            }

            // Save the modified PDF (PDF format, so no SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Header added to all pages. Saved as '{outputPath}'.");
    }
}