using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // needed for FontRepository, TextFragment, Position

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_chapter_numbers.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over each page and add a "Chapter #" footer using a TextFragment
            foreach (Page page in doc.Pages)
            {
                // Build the text "Chapter <page number>"
                string footerText = $"Chapter {page.Number}";

                // Create a TextFragment for the footer
                TextFragment tf = new TextFragment(footerText)
                {
                    // Center the text horizontally on the page
                    HorizontalAlignment = HorizontalAlignment.Center,
                    // Position the text near the bottom (20 points from the bottom edge)
                    Position = new Position(page.PageInfo.Width / 2, 20)
                };

                // Configure appearance
                tf.TextState.FontSize = 12;
                tf.TextState.Font = FontRepository.FindFont("Helvetica");
                tf.TextState.ForegroundColor = Color.DarkGray;

                // Add the fragment to the page's paragraphs collection
                page.Paragraphs.Add(tf);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with chapter page numbers: {outputPath}");
    }
}
