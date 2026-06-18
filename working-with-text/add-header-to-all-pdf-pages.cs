using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // FontRepository, TextState, Color

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_header.pdf";
        const string headerText = "My Document Header";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Loop through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a TextFragment that will act as the header
                TextFragment header = new TextFragment(headerText)
                {
                    // Center the text horizontally
                    HorizontalAlignment = HorizontalAlignment.Center,
                    // Position the text near the top of the page (Y coordinate is measured from the bottom)
                    Position = new Position(0, page.PageInfo.Height - 20)
                };

                // Configure the visual appearance of the header text
                // TextState is read‑only; modify its members instead of assigning a new instance
                header.TextState.Font = FontRepository.FindFont("Helvetica");
                header.TextState.FontSize = 12;
                header.TextState.ForegroundColor = Color.Gray;

                // Add the header fragment to the page's paragraph collection
                page.Paragraphs.Add(header);
            }

            // Save the modified PDF (output format is PDF, no SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Header added to all pages. Saved as '{outputPath}'.");
    }
}
