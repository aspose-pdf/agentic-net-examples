using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_footer.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            int totalPages = doc.Pages.Count;

            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= totalPages; i++)
            {
                Page page = doc.Pages[i];

                // Build the footer text "Page X of Y"
                string footerText = $"Page {i} of {totalPages}";

                // Create a TextFragment for the footer
                TextFragment tf = new TextFragment(footerText)
                {
                    // Center the text horizontally on the page
                    HorizontalAlignment = HorizontalAlignment.Center,
                    // Position the text near the bottom of the page (20 points from the bottom edge)
                    Position = new Position(page.PageInfo.Width / 2, 20)
                };

                // Define the visual style of the footer text
                tf.TextState.Font = FontRepository.FindFont("Helvetica");
                tf.TextState.FontSize = 10;
                tf.TextState.ForegroundColor = Color.Gray;

                // Add the footer to the page
                page.Paragraphs.Add(tf);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with footer: {outputPath}");
    }
}
