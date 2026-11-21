using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_numbered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create the page number text with the custom prefix
                string pageNumberText = $"Chapter {i}";

                // TextFragment holds the text to be added
                TextFragment fragment = new TextFragment(pageNumberText)
                {
                    // Position the text near the bottom center of the page
                    Position = new Position(page.PageInfo.Width / 2, 20),

                    // Center align the text horizontally
                    HorizontalAlignment = HorizontalAlignment.Center
                };

                // Apply styling by modifying the existing TextState instance (read‑only property)
                fragment.TextState.FontSize = 12;
                fragment.TextState.Font = FontRepository.FindFont("Arial");
                fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

                // Add the fragment to the page's paragraph collection
                page.Paragraphs.Add(fragment);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers added and saved to '{outputPath}'.");
    }
}
