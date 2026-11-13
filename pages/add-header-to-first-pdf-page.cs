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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count < 1)
            {
                Console.Error.WriteLine("Document contains no pages.");
                return;
            }

            // Configure margins for the first page
            MarginInfo margins = new MarginInfo
            {
                Top = 50,    // points from the top edge
                Bottom = 50,
                Left = 50,
                Right = 50
            };
            Page firstPage = doc.Pages[1];
            firstPage.PageInfo.Margin = margins;

            // Create a text fragment that will serve as the header
            TextFragment header = new TextFragment("Document Header");
            header.TextState.FontSize = 14;
            header.TextState.Font = FontRepository.FindFont("Arial");
            header.TextState.FontStyle = FontStyles.Bold;
            header.TextState.ForegroundColor = Aspose.Pdf.Color.DarkBlue;

            // Position the header inside the top margin.
            // PDF origin is bottom‑left, so Y = page height - top margin (+ small offset)
            double yPos = firstPage.PageInfo.Height - margins.Top + 5;
            double xPos = margins.Left;
            header.Position = new Position(xPos, yPos);

            // Add the header to the page
            firstPage.Paragraphs.Add(header);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Header added and saved to '{outputPath}'.");
    }
}