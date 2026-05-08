using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_footer.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Prepare the footer text (generation date)
            string footerText = DateTime.Now.ToString("yyyy-MM-dd");

            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a text fragment with the footer text
                TextFragment tf = new TextFragment(footerText);

                // Position the text near the bottom margin (e.g., 30 points from the bottom)
                // X coordinate is centered horizontally
                double pageWidth = page.PageInfo.Width;
                tf.Position = new Position(pageWidth / 2, 30);

                // Center align the text
                tf.TextState.HorizontalAlignment = HorizontalAlignment.Center;

                // Set visual style (optional)
                tf.TextState.FontSize = 10;
                tf.TextState.Font = FontRepository.FindFont("Helvetica");
                tf.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;

                // Append the text fragment to the page using TextBuilder
                TextBuilder builder = new TextBuilder(page);
                builder.AppendText(tf);
            }

            // Save the modified document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with footer at '{outputPath}'.");
    }
}